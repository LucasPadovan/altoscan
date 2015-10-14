using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransmisionDatos;

namespace AltoScan
{
    public class SerialPortRequestBuilder : GenericBuilder

    {
        private byte[] _serialPortRequest;
        private byte[] _crc;
        private byte[] _byDeviceId;
        private byte[] _crcFiller = new byte[2];

        private static SerialPortRequestBuilder _instance;

        private SerialPortRequestBuilder()
        {
        }

        public static SerialPortRequestBuilder GetInstance()
        {
            if (_instance == null)
            {
                _instance = new SerialPortRequestBuilder();
            }
            
            return _instance;
        }

    public override byte[] BuildReadRegisterRequest(int deviceId, int startAddress, int registerQuantity)
    {
        //Obtengo el deviceId
        _byDeviceId = RequestUtils.BuildDeviceId(deviceId);
        
        //Obtengo el PDU
        pdu = base.BuildReadRegisterPdu(startAddress, registerQuantity);
        
        //Calculo CRC
        _crc = RequestUtils.GetCrc(RequestUtils.Combine(_byDeviceId, pdu, _crcFiller));

        //Combino los 3 arrays en 1
        _serialPortRequest = RequestUtils.Combine(_byDeviceId, pdu, _crc);
        
        return _serialPortRequest;
    }

    public override byte[] BuildWriteRegisterRequest(int deviceId, int registerAddress, int registerValue)
    {
        //Obtengo el deviceId
        _byDeviceId = RequestUtils.BuildDeviceId(deviceId);
        
        //Obtengo el PDU
        pdu = base.BuildWriteRegisterPdu(registerAddress, registerAddress);

        //Calculo CRC
        _crc = RequestUtils.GetCrc(RequestUtils.Combine(_byDeviceId, pdu, _crcFiller));

        //Combino los 2 arrays en 1
        _serialPortRequest = RequestUtils.Combine(pdu, _crc);

        return _serialPortRequest;

    }

    public override byte[] BuildWriteMultipleRegistersRequest(int deviceId, int startingAddress, int registerQuantity, int[] registersValues)
    {
        //Obtengo el deviceId
        _byDeviceId = RequestUtils.BuildDeviceId(deviceId);
        
        //Obtengo el PDU
        pdu = base.BuildWriteMultipleRegistersPdu(startingAddress, registerQuantity, registersValues);

        //Calculo CRC
        _crc = RequestUtils.GetCrc(RequestUtils.Combine(_byDeviceId, pdu, _crcFiller));

        //Combino los 2 arrays en 1
        _serialPortRequest = RequestUtils.Combine(pdu, _crc);

        return _serialPortRequest;
        }

    }
}
