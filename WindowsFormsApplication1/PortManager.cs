using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AltoScan;

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

        private string hexaString = "";
        private string decimalString = "";
        private string binaryString = "";
        private string statusString = "";

        private Form1 Form;
        public PortManager(String portName, int baudRate, Parity parity, int dataBits, StopBits stopBits, Form1 form )
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

                Form = form;
            }
            else
            {
                this.port.BaudRate = baudRate;
                this.port.Parity = parity;
                this.port.DataBits = dataBits;
                this.port.StopBits = stopBits;
            }
        }

        public void OpenPort(int timeout)
        {
            if (!port.IsOpen)
            {
                port.Open();
            }
            port.WriteTimeout = timeout;
            port.ReadTimeout  = timeout;
        }
        public void ClosePort()
        {
            port.Close();
        }

        //Verifica que el puerto este abierto (lo abre si esta cerrado) 
        public void Write(byte[] request, int offset, int count, int timeout)
        {
            OpenPort(timeout);
          
            //request, 0, cantidad de bytes de la request
            try
            {
                port.Write(request, offset, count);
            }
            catch (TimeoutException e)
            {
                Console.WriteLine(e.Message);
                port.Close();
                throw new Exception(); //TODO: Reiniciar hilos necesarios. (Utilizar evento para llamar metodos del form)
            }

        }

        public string[] ReadPort()
        {
            var response = new string[4];

            response[0] = hexaString;
            response[1] = decimalString;
            response[2] = binaryString;
            response[3] = statusString;

            return response;
        }

        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // Show all the incoming data in the port's buffer

            statusString     = "No se han recibido datos";
            int bytes        = port.BytesToRead;
            byte[] buffer    = new byte[bytes];
            int count        = buffer.Length;
            string decString = "";
            string binString = "";

            port.Read(buffer, 0, bytes);
            port.DiscardOutBuffer();
            port.DiscardInBuffer();
            port.Close();

            if (checkCRC(buffer))
            {

                foreach (var mByte in buffer)
                {
                    if (--count > 0)
                    {
                        decString += Convert.ToInt16(mByte).ToString() + "-";
                        binString += Convert.ToString(mByte, 2).PadLeft(8, '0') + "-";
                    }
                    else
                    {
                        decString += Convert.ToInt16(mByte).ToString();
                        binString += Convert.ToString(mByte, 2).PadLeft(8, '0');
                    }
                }

                hexaString    = BitConverter.ToString(buffer);
                decimalString = decString;
                binaryString  = binString;
                statusString  = "Mensaje recibido satisfactoriamente.";

                //TODO ADD EVENT
                string statusCode = Convert.ToString(binaryString[9]);
                if (statusCode == "1")
                    statusString = "Error en la trama.";


                var response = new string[4];

                response[0] = hexaString;
                response[1] = decimalString;
                response[2] = binaryString;
                response[3] = statusString;

                Form.OnPortDataReceived(response);

                //TODO: hacer una clase aparte para esto.
                //switch (statusCode)
                //{
                //    case "83":
                //        statusString = "Error 83, se intento leer más variables de las que existen";
                //        break;
                //    case "86":
                //        statusString = "Error 86, se intento escribir en una posición inexistente";
                //        break;
                //    case "90":
                //        statusString = "Error 90, se intento escribir en una posición inexistente";
                //        break;
                //}

                //Console.WriteLine("Bytes to read: " + bytes);
                //Console.WriteLine("Response in hexa: " + hexaString);
                //Console.WriteLine("Response in decimal: " + decimalString);
                //Console.WriteLine("Response in binary: " + binaryString);
            }
            else
            {
                statusString = "CRC incorrecto";
            }
        }

        private bool checkCRC(byte[] response)
        {
            byte[] responseCRC = new byte[2];
            responseCRC[0] = response[response.Length - 2];
            responseCRC[1] = response[response.Length - 1];

            byte[] calculatedCRC = RequestUtils.GetCrc(response);

            if (responseCRC[0] == calculatedCRC[0] && responseCRC[1] == calculatedCRC[1])
            {
                return true;
            }
            return false;
        }
    }
}

