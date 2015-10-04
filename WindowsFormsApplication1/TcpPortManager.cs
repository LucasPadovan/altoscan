using System.Net;
using System.Net.Sockets;

namespace TransmisionDatos
{
    public class TcpPortManager
    {
        private TcpClient client;

        public TcpPortManager(int a)
        {
            client = new TcpClient();
            client.Client.Connect(IPAddress.Parse("127.0.0.1"), 502);
        }

        public void WritePort(byte[] request)
        {
            //Chequeamos q este conectado y si no lo conectamos
            if (!client.Client.Connected)
            {
                client.Client.Connect(IPAddress.Parse("127.0.0.1"), 502);
            }

            client.Client.Send(request);
        }

    }
}
