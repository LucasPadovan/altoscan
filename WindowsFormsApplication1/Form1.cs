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

        internal delegate void SerialDataReceivedEventHandlerDelegate(
                 object sender, SerialDataReceivedEventArgs e);

        internal delegate void SerialPinChangedEventHandlerDelegate(
                 object sender, SerialPinChangedEventArgs e);

        delegate void SetTextCallback(string text);

        string InputData = String.Empty;

        Thread ConfigScan;
        Thread WriterThread;

        Boolean terminate = false;

        byte[] request = new byte[8];
        List<byte[]> requests;

        PortManager portManager;

        string[] hexaOutputString;
        string[] decimalOutputString;
        string[] binaryOutputString;

        int variablesLimit = 125;

        int timeout;
        int numberOfRetries;

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

        /**
         * Inicializa todos los campos del formulario
         */
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
            functionToImplement.Items.Add(03);
            functionToImplement.Items.Add(06);
            functionToImplement.Items.Add(16);
            functionToImplement.Items.ToString();
            //get the first item print it in the text 
            functionToImplement.Text = functionToImplement.Items[0].ToString();

        }

        //Listener que verifica que haya datos en el buffer
        private void port_DataReceived_1(object sender, SerialDataReceivedEventArgs e)
        {
            InputData = ComPort.ReadExisting();
            if (InputData != String.Empty)
            {
                this.BeginInvoke(new SetTextCallback(AppendTextOnWindow), new object[] { InputData });
            }
        }

        //Muestra datos en el formulario (respuesta en decimal, hexa y binario)
        private void AppendTextOnWindow(string text)
        {
            decimalOutput.Text += text;
            hexaOutput.Text    += text;
            binOutput.Text     += text;
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
                //aca deberia dividir los requests en varios.
                int DispositiveId;
                int FirstParam;
                int SecondParam;

                string   stringy          = ReadTextArea();
                string[] ThirdParam       = stringy.Split(",".ToArray());
                string   FunctionSelected = ReadFunctionSelected();

                //deberia usar una expresion regular para sacar todo lo que sea un "-"
                bool dispositiveHasValidText = dispositiveID.Text    == "" || dispositiveID.Text    == "-";
                bool firstParamHasValidText  = inputFirstParam.Text  == "" || inputFirstParam.Text  == "-";
                bool secondParamHasValidText = inputSecondParam.Text == "" || inputSecondParam.Text == "-";

                //con esto evitamos que se traten de parsear valores que no se puedan hacer int. Podria usar un TryParse o algo asi tambien.
                if (dispositiveHasValidText) { DispositiveId = 0; }
                else                         { DispositiveId = int.Parse(dispositiveID.Text); }

                if (firstParamHasValidText)  { FirstParam = 0; }
                else                         { FirstParam = int.Parse(inputFirstParam.Text); }

                if (secondParamHasValidText) { SecondParam = 0; }
                else                         { SecondParam = int.Parse(inputSecondParam.Text); }

                switch (FunctionSelected) {
                    case "3":
                        //Habilita los parametros que la funcion 3 necesita
                        EnableInputFirstParam(true); //Dir inicial
                        EnableInputSecondParam(true); //Cantidad de variables
                        EnableInputThirdParam(false); //Valor de variables

                        //Calculamos cantidad request y el tamaño de la ultima request
                        int variablesLeft    = SecondParam % variablesLimit;
                        int numberOfRequests = 1;
                        int extraRequests    = ( SecondParam - 1 ) / variablesLimit;
                        numberOfRequests    += extraRequests;

                        //Lista para agregar request en caso de que sea necesario mas de una request
                        requests = new List<byte[]>();

                        for (int i = 0; i < numberOfRequests; i++)
                        {
                            //Armamos las request necesarias
                            int startingAddress = FirstParam + variablesLimit * i;
                            //Si es la ultima request, le seteamos como cantidad de registros el resto de la division 
                            if (i == extraRequests) {
                                requests.Add(RequestBuilder.BuildReadRegisterRequest(DispositiveId, startingAddress, variablesLeft));
                            }
                            //Si no es la ultima request, armamos con el limite maximo como cantidad de registros
                            else
                            {
                                requests.Add(RequestBuilder.BuildReadRegisterRequest(DispositiveId, startingAddress, variablesLimit));
                            }
                        };

                        //request = RequestBuilder.BuildReadRegisterRequest(DispositiveId, FirstParam, SecondParam);
                        break;

                    case "6":
                        int value;

                        //Habilito los parametros para la funcion 6
                        EnableInputFirstParam(true); //Dir inicial
                        EnableInputSecondParam(false); //Cantidad de variables
                        EnableInputThirdParam(true); //Valor de variables

                        //Tercer parametro es un array de valores, la funcion 6 solo escribe un valor, por eso se toma el primero del array
                        if (int.TryParse(ThirdParam[0], out value)) { }
                        else { value = 0; }

                        //Creamos listado de requests que va a tener solo una request
                        requests = new List<byte[]>();

                        //Creamos la request de funcion 6, direccion inicial y valor a escribir
                        request = RequestBuilder.BuildWriteRegisterRequest(DispositiveId, FirstParam, value);

                        //Agregamos la request al listado
                        requests.Add(request);
                        break;

                    case "16":
                        int[] values;
                        int   counter = 0;

                        //Habilitamos todos los parametros para funcion 16, pos inicial, cantidad de vars, valores de las vars-
                        EnableInputFirstParam(true); //Dir inicial
                        EnableInputSecondParam(true); //Cantidad de variables
                        EnableInputThirdParam(true); //Valor de variables
                        
                        //Si la cantidad de variables es 0 o menos, la request no se arma bien
                        if (SecondParam <= 0)
                        {
                            values = new int[0]; //catch exception on asking for cero elements or less
                        }

                        //Si hay mas de 0 variables
                        else
                        {
                            //Creamos un array de cantidad igual a la cantidad de variables a escribir
                            values = new int[SecondParam];
                            
                            //Por cada uno de los valores de las variables
                            foreach (var paramString in ThirdParam)
                            {
                                //Si el contador es menor a la cantidad de variables, ponemos el valor en "values" que van a la request
                                if (counter < SecondParam && int.TryParse(paramString, out values[counter]))
                                {
                                    //Pasamos a la siguiente variable
                                    counter++;
                                }
                            }   
                        }

                        //Arma listado de requests
                        requests = new List<byte[]>();

                        //Pone la request en el listado con la direccion inicial, cantidad de registros y los valores de las variables
                        request = RequestBuilder.BuildWriteMultipleRegistersRequest(DispositiveId, FirstParam, SecondParam, values);
                        requests.Add(request);
                        break;
                }

                //Arma la string con las request que se van a mostrar en el form, separadas por comas
                string TextToWrite = "";
                foreach (var request in requests)
                {
                    if (TextToWrite != "") { TextToWrite += ", "; }
                    TextToWrite += BitConverter.ToString(request);
                }
                
                //Escribe en el form la/s request/s armada/s
                WriteConfig(TextToWrite);

                Thread.Sleep(200);
            }
        }
        private void WriteConfig(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(WriteConfig), new object[] { value });
                return;
            }
            toSendTextBox.Text = value;
        }
        private string ReadFunctionSelected()
        {
            string returnValue = "3";
            this.Invoke((MethodInvoker)delegate()
            {
                returnValue = functionToImplement.Text;
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
            hexaOutput.Text    += hexaValue;
            binOutput.Text     += binaryValue;
        }


        //Toma las request armadas guardadas en un param de esta clase, y ejecuta metodos del port
        //    Cosas a implementar:
        //    Si no se puede comunicar que largue un error y termine
        private void startQuery_Click(object sender, EventArgs e)
        {
            //String portName, int baudRate, Parity parity, int dataBits, StopBits stopBits
            string portName = cboPorts.Text;
            string parityName = cboParity.Text;
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

            //Por cada intento
            for (int currentRetry = 0; currentRetry < numberOfRetries; currentRetry++ )
            {
                //Calculamos inicio del intento
                DateTime initialTime = DateTime.Now;
                var differenceInMilliseconds = (DateTime.Now - initialTime).TotalMilliseconds;
                
                //Mientras no se pase del timeout del intento
                while (differenceInMilliseconds < timeout)
                {
                    //Actualizamos diferencia entre el inicio y este momento de ejecucion
                    differenceInMilliseconds = (DateTime.Now - initialTime).TotalMilliseconds;
                    try
                    {
                        //Abrimos el puerto
                        portManager.OpenPort();
                        //Inicializamos las variables del formulario como arrays de string de tamaño = cant de requests
                        hexaOutputString    = new string[requests.Count];
                        decimalOutputString = new string[requests.Count];
                        binaryOutputString  = new string[requests.Count];

                        int counter = 0;
                        foreach (var request in requests)
                        {
                            //si el id de dispositivo o algo esta mal, al querer escribir se detona
                            portManager.Write(request, 0, request.Length);
                            //Aca leemos el buffer todo el tiempo, hasta que algo vuelve
                            readPortBuffer(portManager, counter);
                            //Cuando tiene las variables completas, escribe el formulario
                            WriteOutput(hexaOutputString[counter], decimalOutputString[counter], binaryOutputString[counter]);
                            counter++;
                        }
                        currentRetry = numberOfRetries;
                        break;
                    }
                    catch { }
                    currentRetry++;
                }
            }

        }

        private void stopQuery_Click(object sender, EventArgs e)
        {
        }

        private void cleanOutputButton_Click(object sender, EventArgs e)
        {
            decimalOutput.Text = "";
            hexaOutput.Text    = "";
            binOutput.Text     = "";
        }

        private void TerminateThreads(object sender, EventArgs e)
        {
            terminate = true;
            if (WriterThread != null && WriterThread.ThreadState == ThreadState.Running)
            {
                WriterThread.Abort();
            }
            ConfigScan.Abort();
        }

        private void readPortBuffer(PortManager portManager, int counter)
        {
            while (true)
            {
                string[] portManagerResponse = portManager.ReadPort();
                if (portManagerResponse[0] != "" && portManagerResponse[1] != "" && portManagerResponse[2] != "")
                {
                    string header                = "\nResponse " + Convert.ToString(counter) + " -- ";
                    hexaOutputString[counter]    = header + portManagerResponse[0];
                    decimalOutputString[counter] = header + portManagerResponse[1];
                    binaryOutputString[counter]  = header + portManagerResponse[2];
                    break;
                }
                Thread.Sleep(200);
            }
        }

    }
}
