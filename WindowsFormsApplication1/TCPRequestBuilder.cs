using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransmisionDatos;

namespace TransmisionDatos
{
    public class TCPRequestBuilder : RequestBuilder
    {
        private static byte[] request;

        public static byte[] BuildTCPReadRegisterRequest(int identifier, int deviceId, int startAddress,
            int registerQuantity)
        {
            request = new byte[12];

            //Identificacion de la transacción
            if (identifier < 256)
            {
                request[0] = 0;
                request[1] = Convert.ToByte(identifier);
            }
            else
            {
                string hexValue = identifier.ToString("X");
                byte[] hexIdentifier = StringToByteArray(hexValue);
                request[0] = hexIdentifier[0];
                request[1] = hexIdentifier[1];
            }

            //Protocol Identifier: 2 bytes = 0
            request[3] = 0;
            request[4] = 0;

            //Number of following bytes = 6
            request[5] = 0;
            request[6] = 6;

            //Obtenemos el array de bytes de una request COM comun y le sacamos los bytes de CRC
            byte[] comRequest = BuildReadRegisterRequest(deviceId, startAddress, registerQuantity);
            comRequest = comRequest.Where((source, index) => index != 8 && index != 7).ToArray();

            //Seguimos armando la request
            request[7] = comRequest[0];
            request[8] = comRequest[1];
            request[9] = comRequest[2];
            request[10] = comRequest[3];
            request[11] = comRequest[4];
            request[12] = comRequest[5];

            return request;

        }

    }
}
