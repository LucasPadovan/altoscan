using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication1;
using TransmisionDatos;

namespace TransmisionDatos
{
    public class TCPRequestBuilder : GenericBuilder
    {
        private byte[] _tcpRequest;
        private byte[] _tcpHeader;
        private static int _requestIdentifier = 1;

        public override byte[] BuildReadRegisterRequest(int deviceId, int startAddress, int registerQuantity)
        {
            throw new NotImplementedException();
        }

        public override byte[] BuildWriteRegisterRequest(int deviceId, int registerAddress, int registerValue)
        {
            throw new NotImplementedException();
        }

        public override byte[] BuildWriteMultipleRegistersRequest(int deviceId, int startingAddress, int registerQuantity, int[] registersValues)
        {
            throw new NotImplementedException();
        }

        private byte[] buildTcpHeader(int followingBytesLength, int deviceId)
        {
            //Convertimos el identificador de la request a hexa
            byte[] requestId = RequestUtils.ConvertToBytes(_requestIdentifier);
            _tcpHeader[0] = requestId[0];
            _tcpHeader[1] = requestId[1];

            //Aumentamos el identificador de la request
            _requestIdentifier++;

            //Protocol Identifier
            _tcpHeader[2] = 0;
            _tcpHeader[3] = 0;

            //Number of following bytes = 6
            _tcpHeader[5] = 0;
            _tcpHeader[6] = Convert.ToByte(followingBytesLength + 1);


        }
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
