using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TransmisionDatos
{
    public class PortManager
    {

        private SerialPort port;
        private String portName;
        private int baudRate;
        private Parity parity;
        private int dataBits;
        private StopBits stopBits;
        private delegate void SetTextDeleg(string text);

        private string hexaString    = "";
        private string decimalString = "";
        private string binaryString  = "";

        private bool reponseReceived = false;

        public PortManager(String portName, int baudRate, Parity parity, int dataBits, StopBits stopBits)
        {
            if (this.port == null)
            {
                this.portName = portName;
                this.baudRate = baudRate;
                this.parity = parity;
                this.dataBits = dataBits;
                this.stopBits = stopBits;

                this.port = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
                this.port.DtrEnable = true;
                this.port.RtsEnable = true;
                this.port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);

            }
            else
            {
                this.port.BaudRate = baudRate;
                this.port.Parity = parity;
                this.port.DataBits = dataBits;
                this.port.StopBits = stopBits;
            }
        }

        public void OpenPort()
        {
            if (!port.IsOpen)
            {
                port.Open();
            }
        }
        public void ClosePort()
        {
            port.Close();
        }

        public void Write(byte[] request, int offset, int count)
        {
            if (!port.IsOpen)
            { port.Open(); }
            //port.ReadTimeout = 200;
            //port.WriteTimeout = 200;
            //request, 0, cantidad de bytes de la request
            port.Write(request, offset, count);
        }

        public string[] ReadPort()
        {
            var response = new string[3];

            response[0]  = hexaString;
            response[1]  = decimalString;
            response[2]  = binaryString;

            return response;
        }

        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // Show all the incoming data in the port's buffer

            reponseReceived  = false;
            int bytes        = port.BytesToRead;
            byte[] buffer    = new byte[bytes];
            int count        = buffer.Length;
            int count2       = buffer.Length;
            string decString = "";
            string binString = "";

            port.Read(buffer, 0, bytes);
            port.Close();
            foreach (var mByte in buffer)
            {
                if (--count > 0) { decString += Convert.ToInt16(mByte).ToString() + "-"; }
                else             { decString += Convert.ToInt16(mByte).ToString();       }
            }

            foreach (byte mByte in buffer)
            {
                if (--count2 > 0) { binString += Convert.ToString(mByte, 2).PadLeft(8, '0') + "-"; }
                else              { binString += Convert.ToString(mByte, 2).PadLeft(8, '0');       }
            }

            hexaString      = BitConverter.ToString(buffer);
            decimalString   = decString;
            binaryString    = binString;
            reponseReceived = true;

            Console.WriteLine("Bytes to read: "       + bytes        );
            Console.WriteLine("Response in hexa: "    + hexaString   );
            Console.WriteLine("Response in decimal: " + decimalString);
            Console.WriteLine("Response in binary: "  + binaryString );
        }
    }
}
