using System;

namespace TransmisionDatos
{
    public class RequestBuilder
    {
        private static byte[] request;

        private const int READ_REGISTER = 3;
        private const int WRITE_REGISTER = 6;
        private const int WRITE_MULTIPLE_REGISTERS = 16;

        public static byte[] BuildReadRegisterRequest(int deviceId, int startAddress, int registerQuantity)
        {
            request = new byte[8];
            request[0] = Convert.ToByte(deviceId);
            request[1] = Convert.ToByte(READ_REGISTER);
            if (startAddress < 255)
            {
                request[2] = 0;
                request[3] = Convert.ToByte(startAddress);
            }
            else
            {
                string hexValue = startAddress.ToString("X");
                byte[] hexAddress = System.Text.Encoding.UTF8.GetBytes(hexValue);
                request[2] = hexAddress[0];
                request[3] = hexAddress[1];
            }

            if (registerQuantity < 255)
            {
                request[4] = 0;
                request[5] = Convert.ToByte(registerQuantity);
            }
            else
            {
                string hexValue = registerQuantity.ToString("X");
                byte[] hexLength = System.Text.Encoding.UTF8.GetBytes(hexValue);
                request[4] = hexLength[0];
                request[5] = hexLength[1];
            }

            byte[] crc = GetCRC(request);
            request[6] = crc[0];
            request[7] = crc[1];

            Console.WriteLine("Request: " + BitConverter.ToString(request));

            return request;
        }

        public static byte[] BuildWriteRegisterRequest(int deviceId, int registerAddress, int registerValue)
        {
            request = new byte[8];
            request[0] = Convert.ToByte(deviceId);
            request[1] = Convert.ToByte(WRITE_REGISTER);
            if (registerAddress < 255)
            {
                request[2] = 0;
                request[3] = Convert.ToByte(registerAddress);
            }
            else
            {
                string hexValue = registerAddress.ToString("X");
                byte[] hexAddress = System.Text.Encoding.UTF8.GetBytes(hexValue);
                request[2] = hexAddress[0];
                request[3] = hexAddress[1];
            }

            if (registerValue < 255)
            {
                request[4] = 0;
                request[5] = Convert.ToByte(registerValue);
            }
            else
            {
                string hexValue = registerValue.ToString("X");
                byte[] hexLength = System.Text.Encoding.UTF8.GetBytes(hexValue);
                request[4] = hexLength[0];
                request[5] = hexLength[1];
            }

            byte[] crc = GetCRC(request);
            request[6] = crc[0];
            request[7] = crc[1];

            Console.WriteLine("Request: " + BitConverter.ToString(request));

            return request;
        }

        public static byte[] BuildWriteMultipleRegistersRequest(int deviceId, int startingAddress, int registerQuantity,
            int[] registersValues)
        {
            int requestLength = 9 + (registerQuantity*2);

            request = new byte[requestLength];
            request[0] = Convert.ToByte(deviceId);
            request[1] = Convert.ToByte(WRITE_MULTIPLE_REGISTERS);
            if (startingAddress < 255)
            {
                request[2] = 0;
                request[3] = Convert.ToByte(startingAddress);
            }
            else
            {
                string hexValue = startingAddress.ToString("X");
                byte[] hexAddress = System.Text.Encoding.UTF8.GetBytes(hexValue);
                request[2] = hexAddress[0];
                request[3] = hexAddress[1];
            }

            if (registerQuantity < 255)
            {
                request[4] = 0;
                request[5] = Convert.ToByte(registerQuantity);
            }
            else
            {
                string hexValue = registerQuantity.ToString("X");
                byte[] hexLength = System.Text.Encoding.UTF8.GetBytes(hexValue);
                request[4] = hexLength[0];
                request[5] = hexLength[1];
            }

            request[6] = Convert.ToByte(registerQuantity*2);

            int valuesStartingByte = 7; 
            for (int i = 0; i < registerQuantity; i++)
            {
                int value = registersValues[i];

                if (value < 255)
                {
                    request[valuesStartingByte] = 0;
                    request[valuesStartingByte+1] = Convert.ToByte(value);
                }
                else
                {
                    string hexValue = value.ToString("X");
                    byte[] hexAddress = System.Text.Encoding.UTF8.GetBytes(hexValue);
                    request[valuesStartingByte] = hexAddress[0];
                    request[valuesStartingByte+1] = hexAddress[1];
                }

                valuesStartingByte = valuesStartingByte + 2;
            }

            byte[] crc = GetCRC(request);
            request[requestLength-2] = crc[0];
            request[requestLength-1] = crc[1];

            Console.WriteLine("Request: " + BitConverter.ToString(request));

            return request;
        }

        private static byte[] GetCRC(byte[] message)
        {
            //Function expects a modbus message of any length as well as a 2 byte CRC array in which to 
            //return the CRC values:

            ushort CRCFull = 0xFFFF;
            byte CRCHigh = 0xFF, CRCLow = 0xFF;
            char CRCLSB;

            for (int i = 0; i < (message.Length) - 2; i++)
            {
                CRCFull = (ushort)(CRCFull ^ message[i]);

                for (int j = 0; j < 8; j++)
                {
                    CRCLSB = (char)(CRCFull & 0x0001);
                    CRCFull = (ushort)((CRCFull >> 1) & 0x7FFF);

                    if (CRCLSB == 1)
                        CRCFull = (ushort)(CRCFull ^ 0xA001);
                }
            }
            byte[] CRC = new byte[2];
            CRC[1] = CRCHigh = (byte)((CRCFull >> 8) & 0xFF);
            CRC[0] = CRCLow = (byte)(CRCFull & 0xFF);
            return CRC;
        }

    }
}
