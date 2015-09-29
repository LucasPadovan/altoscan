using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public static class RequestUtils
    {
        //Metodo q transforma un string hexa a un array de bytes
        public static byte[] StringToByteArray(string hex)
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

        //Metodo para obtener el CRC de un array de bytes
        public static byte[] GetCrc(byte[] message)
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

        //Combina 2 arrays de bytes en Uno
        public static byte[] Combine(byte[] first, byte[] second)
        {
            byte[] ret = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);
            return ret;
        }

        //Combina 3 arrays en uno solo
        public static byte[] Combine(byte[] first, byte[] second, byte[] third)
        {
            byte[] ret = new byte[first.Length + second.Length + third.Length];
            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);
            Buffer.BlockCopy(third, 0, ret, first.Length + second.Length,
                             third.Length);
            return ret;
        }

        //Construye un array de 1 byte con el id del dispositivo
        public static byte[] BuildDeviceId(int deviceId)
        {
            byte[] byDeviceId = new byte[1];
            byDeviceId[0] = Convert.ToByte(deviceId);
            return byDeviceId;
        }

        //Transforma un entero en 2 bytes
        public static byte[] ConvertToBytes(int number)
        {
            byte[] result = new byte[2];
            
            if (number < 256)
            {
                result[0] = 0;
                result[1] = Convert.ToByte(number);
            }
            else
            {
                string hexValue = number.ToString("X");
                byte[] hexLength = StringToByteArray(hexValue);
                result[0] = hexLength[0];
                result[1] = hexLength[1];
            }

        }


    }
}
