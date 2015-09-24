using System;
using System.Linq;

namespace TransmisionDatos
{
    public class RequestBuilder
    {
        private static byte[] request;

        private const int READ_REGISTER = 3;
        private const int WRITE_REGISTER = 6;
        private const int WRITE_MULTIPLE_REGISTERS = 16;

        //Funcion 3
        public static byte[] BuildReadRegisterRequest(int deviceId, int startAddress, int registerQuantity)
        {
            //Array de string de 8 bytes
            request = new byte[8];
            //Dispositivo 
            request[0] = Convert.ToByte(deviceId);
            //Funcion de lectura
            request[1] = Convert.ToByte(READ_REGISTER);

            //Transformamos direccion de inicio de lectura a 2 bytes
            if (startAddress < 256)
            {
                request[2] = 0;
                request[3] = Convert.ToByte(startAddress);
            }
            else
            {
                string hexValue = startAddress.ToString("X");
                byte[] hexLength = StringToByteArray(hexValue);
                request[2] = hexLength[0];
                request[3] = hexLength[1];
            }

            //Transformamos la cantidad de registros a leer a 2 bytes
            if (registerQuantity < 256)
            {
                request[4] = 0;
                request[5] = Convert.ToByte(registerQuantity);
            }
            else
            {
                string hexValue = registerQuantity.ToString("X");
                byte[] hexLength = StringToByteArray(hexValue);
                request[4] = hexLength[0];
                request[5] = hexLength[1];
            }

            byte[] crc = GetCRC(request);
            request[6] = crc[0];
            request[7] = crc[1];

            //Console.WriteLine("Request: " + BitConverter.ToString(request));

            return request;
        }

        //Funcion 6
        public static byte[] BuildWriteRegisterRequest(int deviceId, int registerAddress, int registerValue)
        {
            //Array de string de largo 8
            request = new byte[8];
            //ID dispositivo
            request[0] = Convert.ToByte(deviceId);
            //Nro de funcion
            request[1] = Convert.ToByte(WRITE_REGISTER);

            //Transformamos direccion de registro de inicio en 2 bytes
            if (registerAddress < 256)
            {
                request[2] = 0;
                request[3] = Convert.ToByte(registerAddress);
            }
            else
            {
                string hexValue = registerValue.ToString("X");
                byte[] hexLength = StringToByteArray(hexValue); 
                request[2] = hexLength[0];
                request[3] = hexLength[1];
            }

            //Transformamos el valor que vamos a escribir en 2 bytes
            if (registerValue < 256)
            {
                request[4] = 0;
                request[5] = Convert.ToByte(registerValue);
            }
            else
            {
                string hexValue = registerValue.ToString("X");
                byte[] hexLength = StringToByteArray(hexValue);
                request[4] = hexLength[0];
                request[5] = hexLength[1];
            }

            byte[] crc = GetCRC(request);
            request[6] = crc[0];
            request[7] = crc[1];

            //Console.WriteLine("Request: " + BitConverter.ToString(request));

            return request;
        }

        //Funcion 16
        public static byte[] BuildWriteMultipleRegistersRequest(int deviceId, int startingAddress, int registerQuantity,
            int[] registersValues)
        {
            //Largo de la request, 9 mas el nro de registros * 2
            int requestLength = 9 + (registerQuantity*2);

            //Creamos array de bytes segun el largo que necesitemos
            request = new byte[requestLength];
            request[0] = Convert.ToByte(deviceId);
            request[1] = Convert.ToByte(WRITE_MULTIPLE_REGISTERS);
            if (startingAddress < 256)
            {
                request[2] = 0;
                request[3] = Convert.ToByte(startingAddress);
            }
            else
            {
                string hexValue = startingAddress.ToString("X");
                byte[] hexLength = StringToByteArray(hexValue);
                request[2] = hexLength[0];
                request[3] = hexLength[1];
            }

            if (registerQuantity < 256)
            {
                request[4] = 0;
                request[5] = Convert.ToByte(registerQuantity);
            }
            else
            {
                string hexValue = registerQuantity.ToString("X");
                byte[] hexLength = StringToByteArray(hexValue);
                request[4] = hexLength[0];
                request[5] = hexLength[1];
            }

            if(registerQuantity<=255)
                //Cantidad de bytes que corresponden a los registros a escribir
                request[6] = Convert.ToByte(registerQuantity*2);
            else
            {
                // TODO
            }
            //Posicion donde vamos a empezar a escribir valores a la request
            int valuesStartingByte = 7; 
            //Desde 0 hasta la cantidad de registros
            for (int i = 0; i < registerQuantity; i++)
            {
                int value = registersValues[i];

                //Transforma el entero en 2 bytes
                if (value < 256)
                {
                    request[valuesStartingByte] = 0;
                    request[valuesStartingByte+1] = Convert.ToByte(value);
                }
                else
                {
                    string hexValue = value.ToString("X");
                    byte[] hexLength = StringToByteArray(hexValue);
                    request[valuesStartingByte] = hexLength[0];
                    request[valuesStartingByte+1] = hexLength[1];
                }

                //Movemos a la derecha 2 bytes para el siguiente registro
                valuesStartingByte = valuesStartingByte + 2;
            }

            byte[] crc = GetCRC(request);
            //Ubicamos CRC en la penultima y ultima posicion de la request
            request[requestLength-2] = crc[0];
            request[requestLength-1] = crc[1];

            //Console.WriteLine("Request: " + BitConverter.ToString(request));

            return request;
        }

        public static byte[] GetCRC(byte[] message)
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

        protected static byte[] StringToByteArray(string hex)
        {
            String paddedHex = hex.PadLeft(4, '0');
            String highString = paddedHex.Substring(0, 2);
            String lowString = paddedHex.Substring(2, 2);
            Byte highByte = Convert.ToByte(int.Parse(highString, System.Globalization.NumberStyles.HexNumber));
            Byte lowByte = Convert.ToByte(int.Parse(lowString, System.Globalization.NumberStyles.HexNumber));
            Byte[] response = new byte[2];
            response[0] = highByte;
            response[1] = lowByte;
            return response;
        }
    }
}
