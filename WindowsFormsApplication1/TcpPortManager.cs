using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TransmisionDatos
{
    public class TcpPortManager
    {
        private TcpClient client;

        public TcpPortManager()
        {
            client = new TcpClient();
            client.Client.Connect(IPAddress.Parse("127.0.0.1"), 502);
        }

        public void Write(byte[] request)
        {
            //Chequeamos q este conectado y si no lo conectamos
            if (!client.Client.Connected)
            {
                client.Client.Connect(IPAddress.Parse("127.0.0.1"), 502);
            }
            
            client.Client.Send(request);
        }

        //Metodo para leer la respuesta
        public void ReadPort()
        {
            NetworkStream networkStream = client.GetStream();

            // Si existe respuesta
            if (networkStream.CanRead)
            {
                //Obtenemos el stream
                byte[] response = new byte[50];
                client.Client.Receive(response);

                //Obtenemos los bytes a leer despues del header
                byte bytesToRead = response[5];

                //Obtenemos los bytes a leer en total
                int iBytesToRead =  6 + Convert.ToInt32(bytesToRead);

                //Obtenemos la response final cortando el stream la cantidad de bytes obtenida previamente
                //Forma mega rustica de hacerlo pero es lo q hay, #graciasSISHARP
                byte[] finalResponse = response.Take(iBytesToRead).ToArray();

                //TODO: Hacer lo q pinte con la respuesta
                //No hay q chequear CRC por q no tiene, solamente chequear codigos de error
                //y pasar la respuesta a hexa, binario, y decimal
            }
        }

    }
}
