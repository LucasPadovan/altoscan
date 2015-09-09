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

        int variablesLimit = 120;

        int timeout;
        int numberOfRetries;

        public Form1()
        {
            InitializeComponent();
            InitializeFields();
            StartConfigScanThread();
            ComPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(port_DataReceived_1);
            this.FormClosed += TerminateThreads;
        }

        private void InitializeFields()
        {
            string[] ArrayComPortsNames = null;

            //Com Ports
            ArrayComPortsNames = SerialPort.GetPortNames();
            foreach (var portName in ArrayComPortsNames)
            {
                cboPorts.Items.Add(portName);
            }
            cboPorts.Text = ArrayComPortsNames[1];

            //Baud Rate
            ArrayList baudRates = new ArrayList() {300, 600, 1200, 2400, 9600, 14400, 19200, 38400, 57600, 115200};
            foreach (var baudRate in baudRates)
            {
                cboBaudRate.Items.Add(baudRate);

            };
            cboBaudRate.Items.ToString();
            //get the first item print it in the text 
            cboBaudRate.Text = cboBaudRate.Items[4].ToString();
            
            //Data Bits
            cboDataBits.Items.Add(7);
            cboDataBits.Items.Add(8);
            //get the first item print it in the text 
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

        private void port_DataReceived_1(object sender, SerialDataReceivedEventArgs e)
        {
            InputData = ComPort.ReadExisting();
            if (InputData != String.Empty)
            {
                this.BeginInvoke(new SetTextCallback(AppendTextOnWindow), new object[] { InputData });
            }
        }

        private void AppendTextOnWindow(string text)
        {
            decimalOutput.Text += text;
            hexaOutput.Text    += text;
            binOutput.Text     += text;
        }
        private void WriteTextOnWindow(string text)
        {
            decimalOutput.Text = text;
            hexaOutput.Text    = text;
            binOutput.Text     = text;
        }
        private void StartConfigScanThread()
        {
            ConfigScan = new Thread(ReadConfig);
            ConfigScan.Start();
        }
        private void ReadConfig() {
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
                        EnableInputFirstParam(true);
                        EnableInputSecondParam(true);
                        EnableInputThirdParam(false);

                        int variablesLeft    = SecondParam % variablesLimit;
                        int numberOfRequests = 1;
                        int extraRequests    = ( SecondParam - 1 ) / variablesLimit;
                        numberOfRequests    += extraRequests;
                        // deberia hacer un array con esta cantidad de hilos, en el que les pase el index que estoy laburando ahora.
                        // deberia inicializar los array de salida con esta cantidad de elementos
                        // los hilos deberian guardar sus valores de salida en el lugar del array segun el index que les paso.

                        requests = new List<byte[]>();
                        for (int i = 0; i < numberOfRequests; i++)
                        {
                            int startingAddress = FirstParam + variablesLimit * i;
                            if (i == extraRequests) {
                                requests.Add(RequestBuilder.BuildReadRegisterRequest(DispositiveId, startingAddress, variablesLeft));
                            }
                            else
                            {
                                requests.Add(RequestBuilder.BuildReadRegisterRequest(DispositiveId, startingAddress, variablesLimit));
                            }
                        };

                        request = RequestBuilder.BuildReadRegisterRequest(DispositiveId, FirstParam, SecondParam);
                        break;
                    case "6":
                        int value;

                        EnableInputFirstParam(true);
                        EnableInputSecondParam(false);
                        EnableInputThirdParam(true);

                        if (int.TryParse(ThirdParam[0], out value)) { }
                        else { value = 0; }
                        requests = new List<byte[]>();
                        request = RequestBuilder.BuildWriteRegisterRequest(DispositiveId, FirstParam, value);
                        requests.Add(request);
                        break;
                    case "16":
                        int[] values;
                        int   counter = 0;

                        EnableInputFirstParam(true);
                        EnableInputSecondParam(true);
                        EnableInputThirdParam(true);
                        
                        if (SecondParam <= 0)
                        {
                            values = new int[0]; //catch exception on asking for cero elements or less
                        }
                        else
                        {
                            values = new int[SecondParam]; //catch exception on asking for cero elements or less
                            
                            foreach (var paramString in ThirdParam)
                            {
                                if (counter < SecondParam && int.TryParse(paramString, out values[counter]))
                                {
                                    //si el numero es mayor a lo que puede contener 4 digitos hexadecimales, poner el maximo posible.
                                    counter++;
                                }
                            }   
                        }
                        requests = new List<byte[]>();
                        request = RequestBuilder.BuildWriteMultipleRegistersRequest(DispositiveId, FirstParam, SecondParam, values);
                        requests.Add(request);
                        break;
                }

                string TextToWrite = "";
                foreach (var request in requests)
                {
                    if (TextToWrite != "") { TextToWrite += ", "; }
                    TextToWrite += BitConverter.ToString(request);
                }
                
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


        // Start of button function binding
        // Cosas a implementar:
        //  Si no se puede comunicar que largue un error y termine


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

            //usar manejo de excepciones para cuando pida con ciertos parámetros y falle la escritura/lectura
            portManager.OpenPort();
            hexaOutputString    = new string[requests.Count];
            decimalOutputString = new string[requests.Count];
            binaryOutputString  = new string[requests.Count];
            
            int counter = 0;
            foreach(var request in requests)
            {
                portManager.Write(request, 0, request.Length);
                readPortBuffer(portManager, counter);
                portManager.CleanPortBuffer();
                WriteOutput(hexaOutputString[counter], decimalOutputString[counter], binaryOutputString[counter]);
                counter ++;
            }
            portManager.ClosePort();
        }

        private void stopQuery_Click(object sender, EventArgs e)
        {
           // writeTextOnWindow("");
        }

        private void cleanOutputButton_Click(object sender, EventArgs e)
        {
            WriteTextOnWindow("");
        }

        private void TerminateThreads(object sender, EventArgs e)
        {
            terminate = true;
            if (WriterThread != null && WriterThread.ThreadState == ThreadState.Running)
            {
                WriterThread.Abort();
            }
            if (ConfigScan != null && ConfigScan.ThreadState == ThreadState.Running)
            {
                ConfigScan.Abort();
            }
        }

        private void readPortBufferAndWriteOnWindow(PortManager portManager)
        {
            while (true)
            {
                string[] portManagerResponse = portManager.ReadPort();
                if (portManagerResponse[0] != "" && portManagerResponse[1] != "" && portManagerResponse[2] != "")
                {
                    WriteOutput(portManagerResponse[0], portManagerResponse[1], portManagerResponse[2]);
                    break;
                }
                Thread.Sleep(200);
            }
        }

        private void readPortBuffer(PortManager portManager, int counter)
        {
            int actualNumberOfRetries = -1;
            while (actualNumberOfRetries <= numberOfRetries)
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
                Thread.Sleep(timeout);
                actualNumberOfRetries ++;
            }
        }

    }
}
