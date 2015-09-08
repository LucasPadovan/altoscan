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

        PortManager portManager;

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
                int DispositiveId = int.Parse(dispositiveID.Text);
                int FirstParam    = int.Parse(inputFirstParam.Text);
                int SecondParam   = int.Parse(inputSecondParam.Text);

                string   stringy          = ReadTextArea();
                string[] ThirdParam       = stringy.Split(",".ToArray());
                string   FunctionSelected = ReadFunctionSelected();

                switch (FunctionSelected) {
                    case "3":
                        EnableInputFirstParam(true);
                        EnableInputSecondParam(true);
                        EnableInputThirdParam(false);
                        request = RequestBuilder.BuildReadRegisterRequest(DispositiveId, FirstParam, SecondParam);
                        break;
                    case "6":
                        int value;

                        EnableInputFirstParam(true);
                        EnableInputSecondParam(false);
                        EnableInputThirdParam(true);

                        if (int.TryParse(ThirdParam[0], out value)) { }
                        else { value = 0; }
                        request = RequestBuilder.BuildWriteRegisterRequest(DispositiveId, FirstParam, value);
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
                        request = RequestBuilder.BuildWriteMultipleRegistersRequest(DispositiveId, FirstParam, SecondParam, values);
                        break;
                }

                string TextToWrite = BitConverter.ToString(request);
                
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
            decimalOutput.Text = decimalValue;
            hexaOutput.Text    = hexaValue;
            binOutput.Text     = binaryValue;
        }


        // Start of button function binding
        // Cosas a implementar:
        //  un buffer interno (array de strings) para guardar todo lo que haya que escribir, uno por cada output, para las consultas sucesivas.
        //  dividir el metodo de escribir en los outputs del de leer y guardar en el buffer interno lo leido del port manager.
        //  consultas sucesivas: cuando se pidan mas de 120 valores, o se quieran escribir mas de esos, se dividira usando modulo y el resto: 130 mod 120 = 10, 130/120 = 1, se deberan hacer dos consultas, donde la primera sea de 120 valores desde el 0, y la segunda sea de 10 valores desde el 120. El buffer interno a crear sera un array de dos arrays de 3 strings cada uno, correspondiente a cada salida.
        //  timeout: si se da timeout de 2 segundos, que el sleep del thread de leer del buffer del portManager sea de 100ms y lo ejecute 20 veces al guardar el tiempo de inicializacion en una variable y compararlo con el itempo actual de ejecucion. Si no se obtiene nada pasado ese tiempo, aumentar uno a las repeticiones y comparar con lo obtenido del campo ingresado.


        private void startQuery_Click(object sender, EventArgs e)
        {
            //String portName, int baudRate, Parity parity, int dataBits, StopBits stopBits
            string portName = cboPorts.Text;
            string parityName = cboParity.Text;
            string stopBitsName = cboStopBits.Text;

            int baudRate = int.Parse(cboBaudRate.Text);
            int dataBits = int.Parse(cboDataBits.Text);

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

            PortManager portManager = new PortManager(portName, baudRate, parity, dataBits, stopBits);

            //usar manejo de excepciones para cuando pida con ciertos parámetros y falle la escritura/lectura
            portManager.OpenPort();
            portManager.Write(request, 0, request.Length);

            WriterThread = new Thread(() => readPortBufferAndWriteOnWindow(portManager));
            WriterThread.Start();
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

    }
}
