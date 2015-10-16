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


namespace AltoScan
{
    public partial class Form1 : Form
    {

        SerialPort ComPort = new SerialPort();

        delegate void SetTextCallback(string text);

        byte[] request = new byte[8];
        List<byte[]> requests;

        PortManager portManager;
        TcpPortManager tcpPortManager;

        string[] hexaOutputString;
        string[] decimalOutputString;
        string[] binaryOutputString;
        string[] statusOutputString;

        int variablesLimit = 125;

        int timeout;
        int numberOfRetries;

        PortManagerHelper portManagerHelper = new PortManagerHelper();

        public delegate void DataReceived(string[] parameter);

        public event DataReceived PortDataReceived;

        public void OnPortDataReceived(string[] parameter)
        {
            var handler = PortDataReceived;
            if (handler != null)
                handler(parameter);

            this.Invoke((MethodInvoker)delegate()
            {
                this.hexaOutput.Text    += parameter[0] + "\n";
                this.decimalOutput.Text += parameter[1] + "\n";
                this.binOutput.Text     += parameter[2] + "\n";
                this.errorTextBox.Text  += parameter[3];
                //guardar aca el statusOutputString pasandole con un parameter[4] el indice donde tiene que guardar cada cosa.
            });
        }

        public Form1()
        {
            InitializeComponent();
            InitializeFields();
            
            //Observador que recibe datos del puerto
            ComPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(port_DataReceived_1);
            
            //Cuando cambie un valor en la interfaz genero los requests para no tener un bucle todo el tiempo
            BindEventsForInputs();

            //Deshabilita los campos que no corresponden a la conexión elegida.
            DisableFieldsForConnection();
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

            //Conection type
            connectionType.Items.Add("TCP/IP");
            connectionType.Items.Add("Serial Port");
            //get the first item print it in the text 
            connectionType.Text = connectionType.Items[0].ToString();
        }

        //Leer configuracion del form y armo los requests
        private void BuildRequests() {
            int DispositiveId;
            int FirstParam;
            int SecondParam;

            string   textAreaString   = ReadTextArea();
            string[] ThirdParam       = textAreaString.Split(",".ToArray());
            string   FunctionSelected = ReadFunctionSelected();

            numberOfRetries = int.Parse(numberOfRetriesInput.Text);
            timeout         = int.Parse(timeoutInput.Text);

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
                    portManagerHelper.generateFunction3Requests(requests, DispositiveId, FirstParam, SecondParam, variablesLimit, connectionType.Text);

                    break;

                case "6":
                    //Habilito los parametros para la funcion 6
                    EnableInputFirstParam(true); //Dir inicial
                    EnableInputSecondParam(false); //Cantidad de variables
                    EnableInputThirdParam(true); //Valor de variables

                    //Llamamos a la funcion del helper que se encarga de generar todas las requests necesarias a partir de la informaicon obtenida del formulario.
                    portManagerHelper.generateFunction6Requests(requests, DispositiveId, FirstParam, ThirdParam, variablesLimit, connectionType.Text);
                       
                    break;

                case "16":
                    //Habilitamos todos los parametros para funcion 16, pos inicial, cantidad de vars, valores de las vars-
                    EnableInputFirstParam(true); //Dir inicial
                    EnableInputSecondParam(true); //Cantidad de variables
                    EnableInputThirdParam(true); //Valor de variables

                    //Llamamos a la funcion del helper que se encarga de generar todas las requests necesarias a partir de la informaicon obtenida del formulario.
                    portManagerHelper.generateFunction16Requests(requests, DispositiveId, FirstParam, SecondParam, ThirdParam, variablesLimit, connectionType.Text);

                    break;
            }

            //Arma la string con las request que se van a mostrar en el form, separadas por comas
            string TextToWrite = portManagerHelper.generateRequestsString(requests);
            //Escribe en el form la/s request/s armada/s
            WriteToSendTextBox(TextToWrite);
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

        //Deshabilita los campos para tcp/ip o serial port
        public void DisableFieldsForConnection()
        {
            string selectedConnection = connectionType.Text;
            switch (selectedConnection)
            {
                case "TCP/IP":
                    tcpListeningPort.Enabled = true;
                    cboBaudRate.Enabled      = false;
                    cboDataBits.Enabled      = false;
                    cboParity.Enabled        = false;
                    cboPorts.Enabled         = false;
                    cboStopBits.Enabled      = false;
                    break;
                case "Serial Port":
                    tcpListeningPort.Enabled = false;
                    cboBaudRate.Enabled      = true;
                    cboDataBits.Enabled      = true;
                    cboParity.Enabled        = true;
                    cboPorts.Enabled         = true;
                    cboStopBits.Enabled      = true;
                    break;
            }
        }


        //Toma las request armadas guardadas en un param de esta clase, y ejecuta metodos del port
        //    Cosas a implementar:
        //    Si no se puede comunicar que largue un error y termine
        private void startQuery_Click(object sender, EventArgs e)
        {
            //Armo las requests con la información presente en el formulario antes de abrir ninguna conexión.
            BuildRequests();

            if (connectionType.Text == "TCP/IP")
                initializeTcpPortManager();
            else
                initializeSerialPortManager();
            
            //Envia los requests generados a la salida inicalizada.
            sendRequests();
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

        private void initializeSerialPortManager()
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

            portManager = new PortManager(portName, baudRate, parity, dataBits, stopBits,this);
        }

        private void initializeTcpPortManager()
        {
            int tcpPort     = parseToInt(tcpListeningPort.Text);
            tcpPortManager  = new TcpPortManager(tcpPort, this);
        }

        private void sendRequests()
        {
            int counter = 0;
            //Por cada intento
            for (int currentRetry = 1; currentRetry <= numberOfRetries; currentRetry++)
            {
                //Inicializamos las variables del formulario como arrays de string de tamaño = cant de requests
                hexaOutputString    = new string[requests.Count];
                decimalOutputString = new string[requests.Count];
                binaryOutputString  = new string[requests.Count];
                statusOutputString  = new string[requests.Count];

                try
                {

                    if (connectionType.Text != "TCP/IP")
                    {
                        //Abrimos el puerto
                        //portManager.ClosePort(); //cuando le damos a un dispositivo inexistente y volvemos a uno existente sigue
                        portManager.OpenPort(timeout);
                    }

                    foreach (var request in requests)
                    {
                        //Tenemos que evitar hacer un request innecesario, si el primero falló, no seguimos para evitar quedar leyendo una respuesta que nunca llegará.
                        if (counter == 0 || (statusOutputString[counter - 1] != null && statusOutputString[counter - 1] != "Error en la trama."))
                        {
                            //Cabecera para marcar las respuestas
                            string header = "Response " + Convert.ToString(counter) + " -- ";
                            
                            if (connectionType.Text == "TCP/IP")
                            {
                                //si el id de dispositivo o algo esta mal, al querer escribir se detona
                                tcpPortManager.Write(request);

                                //Aca leemos el buffer todo el tiempo, hasta que algo vuelve
                                tcpPortManager.ReadPort(header, timeout);
                                statusOutputString[counter] = errorTextBox.Text;
                            }
                            else
                            {
                                portManager.Write(request, 0, request.Length, timeout, header); //si el id de dispositivo o algo esta mal, al querer escribir se detona
                            }

                            counter++;
                        }
                    }

                    currentRetry = numberOfRetries;
                    break;
                }
                catch (Exception e)
                {
                    if (connectionType.Text != "TCP/IP")
                        portManager.ClosePort();

                    statusOutputString[counter] = "Intento " + Convert.ToString(currentRetry) + " - " + e.Message;
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
            string value  = "";
            int counter   = 0;
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

        //Binders
        private void BindEventsForInputs()
        {
            this.inputDispositiveId.TextChanged      += inputDispositiveId_TextChanged;
            this.inputFunction.SelectedValueChanged  += inputFunction_SelectedValueChanged;
            this.inputFirstParam.TextChanged         += inputFirstParam_TextChanged;
            this.inputSecondParam.TextChanged        += inputSecondParam_TextChanged;
            this.inputThirdParam.TextChanged         += inputThirdParam_TextChanged;
            this.connectionType.SelectedValueChanged += connectionType_SelectedValueChanged;
        }

        void inputThirdParam_TextChanged(object sender, EventArgs e)
        {
            BuildRequests();
        }

        void inputSecondParam_TextChanged(object sender, EventArgs e)
        {
            BuildRequests();
        }

        void inputFirstParam_TextChanged(object sender, EventArgs e)
        {
            BuildRequests();
        }

        void inputFunction_SelectedValueChanged(object sender, EventArgs e)
        {
            BuildRequests();
        }

        void inputDispositiveId_TextChanged(object sender, EventArgs e)
        {
            BuildRequests();
        }

        void connectionType_SelectedValueChanged(object sender, EventArgs e)
        {
            BuildRequests();
            DisableFieldsForConnection();
        }

    }
}
