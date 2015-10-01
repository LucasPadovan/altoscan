using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Windows.Forms;
using System.Collections;
using System.Threading;
using TransmisionDatos;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {

        SerialPort ComPort = new SerialPort();

        delegate void SetTextCallback(string text);

        Thread ConfigScan;

        Boolean terminate = false;

        byte[] request = new byte[8];
        List<byte[]> requests;

        PortManager portManager;

        string[] hexaOutputString;
        string[] decimalOutputString;
        string[] binaryOutputString;
        string[] statusOutputString;

        int variablesLimit = 125;

        int timeout;
        int numberOfRetries;

        PortManagerHelper portManagerHelper = new PortManagerHelper();

        public Form1()
        {
            InitializeComponent();
            InitializeFields();
            //Hilo que arma la request a medida q cambian los inputs
            StartConfigScanThread();
            //Observador que recibe datos del puerto
            ComPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(port_DataReceived_1);
            this.FormClosed += TerminateThreads;
        }

        //Inicializa todos los campos del formulario
        private void InitializeFields()
        {
            string[] ArrayComPortsNames = null;

            //Agrega todos los Com Ports al form
            ArrayComPortsNames = SerialPort.GetPortNames();
            foreach (var portName in ArrayComPortsNames)
            {
                cboPorts.Items.Add(portName);
            }
            cboPorts.Text = ArrayComPortsNames[1];

            //Agrega Baud Rate al form
            ArrayList baudRates = new ArrayList() {300, 600, 1200, 2400, 9600, 14400, 19200, 38400, 57600, 115200};
            foreach (var baudRate in baudRates)
            {
                cboBaudRate.Items.Add(baudRate);

            };
            cboBaudRate.Items.ToString();

            //get the first item and print it in the text 
            cboBaudRate.Text = cboBaudRate.Items[4].ToString();
            
            //Data Bits
            cboDataBits.Items.Add(7);
            cboDataBits.Items.Add(8);
            //get the first item and print it in the text 
            cboDataBits.Text = cboDataBits.Items[1].ToString();

            //Stop Bits
            cboStopBits.Items.Add("None");
            cboStopBits.Items.Add("One");
            cboStopBits.Items.Add("OnePointFive");
            cboStopBits.Items.Add("Two");
            //get the first item print in the text
            cboStopBits.Text = cboStopBits.Items[1].ToString();

            //Parity 
            cboParity.Items.Add("None");
            cboParity.Items.Add("Even");
            cboParity.Items.Add("Mark");
            cboParity.Items.Add("Odd");
            cboParity.Items.Add("Space");
            //get the first item print in the text
            cboParity.Text = cboParity.Items[1].ToString();

            //Handshake
            cboHandShaking.Items.Add("None");
            cboHandShaking.Items.Add("XOnXOff");
            cboHandShaking.Items.Add("RequestToSend");
            cboHandShaking.Items.Add("RequestToSendXOnXOff");
            //get the first item print it in the text 
            cboHandShaking.Text = cboHandShaking.Items[0].ToString();

            //Function to implement
            inputFunction.Items.Add(03);
            inputFunction.Items.Add(06);
            inputFunction.Items.Add(16);
            inputFunction.Items.ToString();
            //get the first item print it in the text 
            inputFunction.Text = inputFunction.Items[0].ToString();

        }

        //Inicia el hilo de escaneo de configuración desde form
        private void StartConfigScanThread()
        {
            ConfigScan = new Thread(ReadConfig);
            ConfigScan.Start();
        }

        //Leer configuracion del form
        private void ReadConfig() {
            //Mientras el hilo de configuracion este vivo 
            while (!terminate && ConfigScan.IsAlive)
            {
                int DispositiveId;
                int FirstParam;
                int SecondParam;

                string   textAreaString   = ReadTextArea();
                string[] ThirdParam       = textAreaString.Split(",".ToArray());
                string   FunctionSelected = ReadFunctionSelected();

                //Usamos una funcion para convertir a int los valores del formulario evitando valores no deseados.
                DispositiveId = parseToInt(inputDispositiveId.Text);
                FirstParam    = parseToInt(inputFirstParam.Text);
                SecondParam   = parseToInt(inputSecondParam.Text);
                
                //Lista para agregar request en caso de que sea necesario mas de una request
                requests = new List<byte[]>();

                switch (FunctionSelected) {
                    case "3":
                        //Habilita los parametros que la funcion 3 necesita
                        EnableInputFirstParam(true); //Dir inicial
                        EnableInputSecondParam(true); //Cantidad de variables
                        EnableInputThirdParam(false); //Valor de variables

                        //Llamamos a la funcion del helper que se encarga de generar todas las requests necesarias a partir de la informaicon obtenida del formulario.
                        portManagerHelper.generateFunction3Requests(requests, DispositiveId, FirstParam, SecondParam, variablesLimit);

                        break;

                    case "6":
                        //Habilito los parametros para la funcion 6
                        EnableInputFirstParam(true); //Dir inicial
                        EnableInputSecondParam(false); //Cantidad de variables
                        EnableInputThirdParam(true); //Valor de variables

                        //Llamamos a la funcion del helper que se encarga de generar todas las requests necesarias a partir de la informaicon obtenida del formulario.
                        portManagerHelper.generateFunction6Requests(requests, DispositiveId, FirstParam, ThirdParam, variablesLimit);
                       
                        break;

                    case "16":
                        //Habilitamos todos los parametros para funcion 16, pos inicial, cantidad de vars, valores de las vars-
                        EnableInputFirstParam(true); //Dir inicial
                        EnableInputSecondParam(true); //Cantidad de variables
                        EnableInputThirdParam(true); //Valor de variables

                        //Llamamos a la funcion del helper que se encarga de generar todas las requests necesarias a partir de la informaicon obtenida del formulario.
                        portManagerHelper.generateFunction16Requests(requests, DispositiveId, FirstParam, SecondParam, ThirdParam, variablesLimit);

                        break;
                }

                //Arma la string con las request que se van a mostrar en el form, separadas por comas
                string TextToWrite = portManagerHelper.generateRequestsString(requests);
                //Escribe en el form la/s request/s armada/s
                WriteToSendTextBox(TextToWrite);

                Thread.Sleep(200);
            }
        }

        //Listener que verifica que haya datos en el buffer
        private void port_DataReceived_1(object sender, SerialDataReceivedEventArgs e)
        {
            string InputData = ComPort.ReadExisting();
            if (InputData != String.Empty)
            {
                this.BeginInvoke(new SetTextCallback(WriteToSendTextBox), new object[] { InputData });
            }
        }


        //Toma las request armadas guardadas en un param de esta clase, y ejecuta metodos del port
        //    Cosas a implementar:
        //    Si no se puede comunicar que largue un error y termine
        private void startQuery_Click(object sender, EventArgs e)
        {
            //Inicializo el port manager con la información que tengo en el formulario
            initializePortManager();

            //Como los requests se estan generando todo el tiempo, al hacer click se los envia al puerto y se espera la respuesta.
            sendRequestsToPort();
        }

        private void stopQuery_Click(object sender, EventArgs e)
        {
        }

        private void cleanOutputButton_Click(object sender, EventArgs e)
        {
            decimalOutput.Text = "";
            hexaOutput.Text    = "";
            binOutput.Text     = "";
            errorTextBox.Text  = "";
        }

        private void TerminateThreads(object sender, EventArgs e)
        {
            terminate = true;
            ConfigScan.Abort();
        }

        private void readPortBuffer(PortManager portManager, int counter)
        {
            try { 
            portManagerHelper.readPortManagerBuffer(portManager, counter, hexaOutputString, decimalOutputString, binaryOutputString, statusOutputString);
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
        }

        private void initializePortManager()
        {
            //String portName, int baudRate, Parity parity, int dataBits, StopBits stopBits
            string portName     = cboPorts.Text;
            string parityName   = cboParity.Text;
            string stopBitsName = cboStopBits.Text;

            int baudRate = int.Parse(cboBaudRate.Text);
            int dataBits = int.Parse(cboDataBits.Text);

            numberOfRetries = int.Parse(numberOfRetriesInput.Text);
            timeout = int.Parse(timeoutInput.Text);

            Parity parity = Parity.None;
            switch (parityName)
            {
                case "None":
                    parity = Parity.None;
                    break;
                case "Even":
                    parity = Parity.Even;
                    break;
                case "Mark":
                    parity = Parity.Mark;
                    break;
                case "Odd":
                    parity = Parity.Odd;
                    break;
                case "Space":
                    parity = Parity.Space;
                    break;
            }

            StopBits stopBits = StopBits.None;
            switch (stopBitsName)
            {
                case "None":
                    stopBits = StopBits.None;
                    break;
                case "One":
                    stopBits = StopBits.One;
                    break;
                case "Two":
                    stopBits = StopBits.Two;
                    break;
                case "OnePointFive":
                    stopBits = StopBits.OnePointFive;
                    break;
            }

            portManager = new PortManager(portName, baudRate, parity, dataBits, stopBits);
        }

        private void sendRequestsToPort()
        {
            //Por cada intento
            for (int currentRetry = 1; currentRetry <= numberOfRetries; currentRetry++)
            {
                try
                {
                    //Abrimos el puerto
                    portManager.OpenPort(timeout);
                    //Inicializamos las variables del formulario como arrays de string de tamaño = cant de requests
                    hexaOutputString    = new string[requests.Count];
                    decimalOutputString = new string[requests.Count];
                    binaryOutputString  = new string[requests.Count];
                    statusOutputString  = new string[requests.Count];

                    int counter = 0;
                    foreach (var request in requests)
                    {
                        //Tenemos que evitar hacer un request innecesario, si el primero falló, no seguimos para evitar quedar leyendo una respuesta que nunca llegará.
                        if (counter == 0 || (statusOutputString[counter - 1] != null && statusOutputString[counter - 1] != "Error en la trama."))
                        {
                            //si el id de dispositivo o algo esta mal, al querer escribir se detona
                            portManager.Write(request, 0, request.Length, timeout);
                            //Aca leemos el buffer todo el tiempo, hasta que algo vuelve
                            readPortBuffer(portManager, counter);
                            //Cuando tiene las variables completas, escribe el formulario
                            string header = "\nResponse " + Convert.ToString(counter) + " -- ";
                            WriteOutput(
                                header + hexaOutputString[counter],
                                header + decimalOutputString[counter],
                                header + binaryOutputString[counter]
                            );
                            counter++;
                        }
                    }
                    WriteStatusTextBox();
                    currentRetry = numberOfRetries;
                    break;
                }
                catch (Exception e)
                {
                    statusOutputString = new string[1];
                    statusOutputString[0] = "Intento " + Convert.ToString(currentRetry) + " - Error de timeout";
                    WriteStatusTextBox();
                }
            }
        }

        // Methods to write the fields from another thread

        private void WriteToSendTextBox(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(WriteToSendTextBox), new object[] { value });
                return;
            }
            toSendTextBox.Text = value;
        }
        private void WriteStatusTextBox()
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action(WriteStatusTextBox));
                return;
            }
            string value = "";
            int counter = 0;
            string header = "Response ";
            foreach (string status in statusOutputString)
            {
                value += header + Convert.ToString(counter) + "--" + status + " ";
                counter++;
            }
            errorTextBox.Text = value;
        }
        private string ReadFunctionSelected()
        {
            string returnValue = "3";
            this.Invoke((MethodInvoker)delegate()
            {
                returnValue = inputFunction.Text;
            });
            return returnValue;
        }
        private string ReadTextArea()
        {
            string returnValue = "0";
            this.Invoke((MethodInvoker)delegate()
            {
                returnValue = inputThirdParam.Text;
            });
            if (returnValue == "")
            {
                returnValue = "0";
            }
            return returnValue;
        }
        private void EnableInputFirstParam(bool value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<bool>(EnableInputFirstParam), new object[] { value });
                return;
            }
            inputFirstParam.Enabled = value;
        }
        private void EnableInputSecondParam(bool value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<bool>(EnableInputSecondParam), new object[] { value });
                return;
            }
            inputSecondParam.Enabled = value;
        }
        private void EnableInputThirdParam(bool value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<bool>(EnableInputThirdParam), new object[] { value });
                return;
            }
            inputThirdParam.Enabled = value;
        }
        public void WriteOutput(string hexaValue, string decimalValue, string binaryValue)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string, string, string>(WriteOutput), new object[] { hexaValue, decimalValue, binaryValue });
                return;
            }
            decimalOutput.Text += decimalValue;
            hexaOutput.Text += hexaValue;
            binOutput.Text += binaryValue;
        }

        //Parsers
        private int parseToInt(string text)
        {
            //deberia usar una expresion regular para sacar todo lo que sea un "-"
            bool textIsInvalid = text == "" || text == "-";

            int response = 0;
            
            //con esto evitamos que se traten de parsear valores que no se puedan hacer int. Podria usar un TryParse o algo asi tambien.
            if (textIsInvalid) response = 0;
            else               response = int.Parse(text);

            return response;
        }
    }
}
