using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication1;
using TransmisionDatos;

namespace TransmisionDatos
{
    public class TcpRequestBuilder : GenericBuilder
    {
        private byte[] _tcpRequest;
        private byte[] _tcpHeader = new byte[7];
        private static int _requestIdentifier = 1;

        private TcpRequestBuilder() { }

        private static TcpRequestBuilder _instance;

        public static TcpRequestBuilder GetInstance()
        {
            if (_instance == null)
            {
                _instance = new TcpRequestBuilder();
            }

            return _instance;
        }

        public override byte[] BuildReadRegisterRequest(int deviceId, int startAddress, int registerQuantity)
        {
            // Obtenemos el PDU
            pdu = BuildReadRegisterPdu(startAddress, registerQuantity);

            // Obtenemos el header
            _tcpHeader = BuildTcpHeader(pdu.Length, deviceId);

            // Construimos la request
            _tcpRequest = RequestUtils.Combine(_tcpHeader, pdu);

            return _tcpRequest;
        }

        public override byte[] BuildWriteRegisterRequest(int deviceId, int registerAddress, int registerValue)
        {
            // Obtenemos el PDU
            pdu = BuildWriteRegisterPdu(registerAddress, registerValue);

            // Obtenemos el header
            _tcpHeader = BuildTcpHeader(pdu.Length, deviceId);

            // Construimos la request
            _tcpRequest = RequestUtils.Combine(_tcpHeader, pdu);

            return _tcpRequest;
        }

        public override byte[] BuildWriteMultipleRegistersRequest(int deviceId, int startingAddress, int registerQuantity, int[] registersValues)
        {
            // Obtenemos el PDU
            pdu = BuildWriteMultipleRegistersPdu(startingAddress, registerQuantity, registersValues);

            // Obtenemos el header
            _tcpHeader = BuildTcpHeader(pdu.Length, deviceId);

            // Construimos la request
            _tcpRequest = RequestUtils.Combine(_tcpHeader, pdu);

            return _tcpRequest;
        }

        //Metodo q construye el header de 7 bytes de la request
        private byte[] BuildTcpHeader(int followingBytesLength, int deviceId)
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

            //Number of following bytes
            _tcpHeader[4] = 0;
            _tcpHeader[5] = Convert.ToByte(followingBytesLength + 1);

            //Id del dispositivo
            _tcpHeader[6] = Convert.ToByte(deviceId);

            return _tcpHeader;
        }

     }
}
