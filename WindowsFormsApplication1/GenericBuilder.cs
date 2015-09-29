using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication1;
using TransmisionDatos;

namespace TransmisionDatos
{
    public abstract class GenericBuilder
    {
        //PDU se compone de FunctionCode y los Datos

        protected byte[] pdu;

        protected const int READ_REGISTER = 3;
        protected const int WRITE_REGISTER = 6;
        protected const int WRITE_MULTIPLE_REGISTERS = 16;

        //Funcion 3
        protected byte[] BuildReadRegisterPdu(int startAddress, int registerQuantity)
        {
            //Array de string de 5 bytes
            pdu = new byte[5];

            //Funcion de lectura
            pdu[0] = Convert.ToByte(READ_REGISTER);

            //Transformamos la cantidad de registros a leer a 2 bytes
            byte[] byStartAddress = RequestUtils.ConvertToBytes(startAddress);

            pdu[1] = byStartAddress[0];
            pdu[2] = byStartAddress[1];

            byte[] byRegisterQuantity = RequestUtils.ConvertToBytes(registerQuantity);

            pdu[3] = byRegisterQuantity[0];
            pdu[4] = byRegisterQuantity[1];

            return pdu;
        }

        //Funcion 6
        protected byte[] BuildWriteRegisterPdu(int registerAddress, int registerValue)
        {
            //Array de string de largo 5
            pdu = new byte[5];

            //Nro de funcion
            pdu[0] = Convert.ToByte(WRITE_REGISTER);

            //Transformamos la direccion de registro a leer a 2 bytes
            byte[] byRegisterAddress = RequestUtils.ConvertToBytes(registerAddress);

            pdu[1] = byRegisterAddress[0];
            pdu[2] = byRegisterAddress[1];

            //Transformamos el valor que vamos a escribir en 2 bytes
            byte[] byRegisterValue = RequestUtils.ConvertToBytes(registerValue);

            pdu[3] = byRegisterValue[0];
            pdu[4] = byRegisterValue[1];

            return pdu;
        }

        //Funcion 16
        protected byte[] BuildWriteMultipleRegistersPdu(int startingAddress, int registerQuantity, int[] registersValues)
        {
            //Largo de la request, 6 mas el nro de registros * 2
            int requestLength = 6 + (registerQuantity * 2);

            //Creamos array de bytes segun el largo que necesitemos
            pdu = new byte[requestLength];

            pdu[0] = Convert.ToByte(WRITE_MULTIPLE_REGISTERS);

            //Calculamos starting address
            byte[] byStartingAddress = RequestUtils.ConvertToBytes(startingAddress);
            pdu[1] = byStartingAddress[0];
            pdu[2] = byStartingAddress[1];

            //Calculamos cantidad de registros
            byte[] byRegisterQuanntity = RequestUtils.ConvertToBytes(registerQuantity);
            pdu[3] = byRegisterQuanntity[0];
            pdu[4] = byRegisterQuanntity[1];

            if (registerQuantity < 256)
                //Cantidad de bytes que corresponden a los registros a escribir, se limita a 255
                pdu[5] = Convert.ToByte(registerQuantity * 2);
            else
            {
                pdu[5] = Convert.ToByte(255);
            }


            //Posicion donde vamos a empezar a escribir valores a la request
            int valuesStartingByte = 6;
            //Desde 0 hasta la cantidad de registros
            for (int i = 0; i < registerQuantity; i++)
            {
                int value = registersValues[i];

                //Calculamos cantidad de registros
                byte[] byValue = RequestUtils.ConvertToBytes(value);
                pdu[valuesStartingByte] = byValue[0];
                pdu[valuesStartingByte+1] = byValue[1];

                //Movemos a la derecha 2 bytes para el siguiente registro
                valuesStartingByte = valuesStartingByte + 2;
            }

            return pdu;
        }

        public abstract byte[] BuildReadRegisterRequest(int deviceId, int startAddress, int registerQuantity);

        public abstract byte[] BuildWriteRegisterRequest(int deviceId, int registerAddress, int registerValue);

        public abstract byte[] BuildWriteMultipleRegistersRequest(int deviceId, int startingAddress, int registerQuantity,
            int[] registersValues);
    }
}
