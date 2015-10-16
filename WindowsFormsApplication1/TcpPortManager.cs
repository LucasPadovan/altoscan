using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using AltoScan;

namespace TransmisionDatos
{
    public class TcpPortManager
    {
        private TcpClient client;
        private Form1 Form;
        private int tcpPort;

        public TcpPortManager(int port, Form1 form)
        {
            tcpPort = port;
            Form = form;
            client = new TcpClient();
            connectClient();

            //IAsyncResult ar = client.BeginConnect("127.0.0.1", tcpPort, null, null);
            //System.Threading.WaitHandle wh = ar.AsyncWaitHandle;
            //try
            //{
            //    if (!ar.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(5), false))
            //    {
            //        client.Close();
            //        throw new TimeoutException();
            //    }

            //    client.EndConnect(ar);
            //}
            //finally
            //{
            //    wh.Close();
            //}

        }
        private void connectClient()
        {
            try
            {
                if (!client.Client.Connected)
                    client.Client.Connect(IPAddress.Parse("127.0.0.1"), tcpPort);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private void disconnectClient()
        {
            client.Client.Close();
        }

        public void Write(byte[] request)
        {
            connectClient();

            client.Client.Send(request);
        }

        //Metodo para leer la respuesta
        public void ReadPort(string header, int timeout)
        {
            NetworkStream networkStream = client.GetStream();

            // Si existe respuesta
            if (networkStream.CanRead)
            {
                //Obtenemos el stream
                byte[] response = new byte[5000];

                client.Client.ReceiveTimeout = timeout;
                client.Client.Receive(response);

                //Obtenemos los bytes a leer despues del header
                byte bytesToRead = response[5];

                //Obtenemos los bytes a leer en total
                int iBytesToRead = 6 + Convert.ToInt32(bytesToRead);

                //Obtenemos la response final cortando el stream la cantidad de bytes obtenida previamente
                //Forma mega rustica de hacerlo pero es lo q hay, #graciasSISHARP
                byte[] finalResponse = response.Take(iBytesToRead).ToArray();

                //TODO: Hacer lo q pinte con la respuesta
                //No hay q chequear CRC por q no tiene, solamente chequear codigos de error
                //y pasar la respuesta a hexa, binario, y decimal

                int count = 0;
                string decString = header;
                string binString = header;
                string hexaString = header + BitConverter.ToString(finalResponse); ;
                string statusString = header;

                foreach (var mByte in finalResponse)
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

                statusString += "Mensaje recibido satisfactoriamente.";

                ////TODO ADD EVENT
                //string statusCode = Convert.ToString(binString[9]);
                //if (statusCode == "1")
                //    statusString += "Error en la trama.";

                //  disconnectClient();

                var responses = new string[4];

                responses[0] = hexaString;
                responses[1] = decString;
                responses[2] = binString;
                responses[3] = statusString;

                Form.OnPortDataReceived(responses);
            }
        }

    }
}
