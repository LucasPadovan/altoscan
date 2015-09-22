using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TransmisionDatos
{
    public class PortManagerHelper
    {

        public PortManagerHelper()
        {
        }
        public void readPortManagerBuffer(PortManager portManager, int counter, string[] hexaOutputString, string[] decimalOutputString, string[] binaryOutputString, string[] statusOutputString)
        {
            while (true)
            {
                string[] portManagerResponse = portManager.ReadPort();
                if (portManagerResponse[0] != "" && portManagerResponse[1] != "" && portManagerResponse[2] != "" && portManagerResponse[3] != "")
                {
                    hexaOutputString[counter] = portManagerResponse[0];
                    decimalOutputString[counter] = portManagerResponse[1];
                    binaryOutputString[counter] = portManagerResponse[2];
                    statusOutputString[counter] = portManagerResponse[3];


                    if (counter == 0 || (counter > 0 && hexaOutputString[counter - 1] != hexaOutputString[counter]))
                        break;
                }
                Thread.Sleep(200);
            }
        }

        public void generateFunction3Requests(List<byte[]> requests, int DispositiveId, int FirstParam, int SecondParam, int variablesLimit)
        {
            //Calculamos cantidad request y el tamaño de la ultima request
            int variablesLeft    = SecondParam % variablesLimit;
            int numberOfRequests = 1;
            int extraRequests    = (SecondParam - 1) / variablesLimit;
            numberOfRequests    += extraRequests;

            for (int i = 0; i < numberOfRequests; i++)
            {
                //Armamos las request necesarias
                int startingAddress = FirstParam + variablesLimit * i;
                //Si es la ultima request, le seteamos como cantidad de registros el resto de la division 
                if (i == extraRequests)
                {
                    requests.Add(RequestBuilder.BuildReadRegisterRequest(DispositiveId, startingAddress, variablesLeft));
                }
                //Si no es la ultima request, armamos con el limite maximo como cantidad de registros
                else
                {
                    requests.Add(RequestBuilder.BuildReadRegisterRequest(DispositiveId, startingAddress, variablesLimit));
                }
            };
        }

        public void generateFunction6Requests(List<byte[]> requests, int DispositiveId, int FirstParam, string[] ThirdParam, int variablesLimit)
        {
            int value;

            //Tercer parametro es un array de valores, la funcion 6 solo escribe un valor, por eso se toma el primero del array
            if (int.TryParse(ThirdParam[0], out value)) { }
            else                                        { value = 0; }

            //Creamos la request de funcion 6, direccion inicial y valor a escribir y la agregamos
            requests.Add(RequestBuilder.BuildWriteRegisterRequest(DispositiveId, FirstParam, value));
        }

        public void generateFunction16Requests(List<byte[]> requests, int DispositiveId, int FirstParam, int SecondParam, string[] ThirdParam, int variablesLimit)
        {
            int[] values;
            int counter = 0;

            //Si la cantidad de variables es 0 o menos, la request no se arma bien
            if (SecondParam <= 0)
            {
                values = new int[0]; //catch exception on asking for cero elements or less
            }

            //Si hay mas de 0 variables
            else
            {
                //Creamos un array de cantidad igual a la cantidad de variables a escribir
                values = new int[SecondParam];

                //Por cada uno de los valores de las variables
                foreach (var paramString in ThirdParam)
                {
                    //Si el contador es menor a la cantidad de variables, ponemos el valor en "values" que van a la request
                    if (counter < SecondParam && int.TryParse(paramString, out values[counter]))
                    {
                        //Pasamos a la siguiente variable
                        counter++;
                    }
                }
            }

            //Pone la request en el listado con la direccion inicial, cantidad de registros y los valores de las variables
            requests.Add(RequestBuilder.BuildWriteMultipleRegistersRequest(DispositiveId, FirstParam, SecondParam, values));
        }

        public string generateRequestsString(List<byte[]> requests)
        {
            string requestsString = "";
            foreach (var request in requests)
            {
                if (requestsString != "") { requestsString += ", "; }
                requestsString += BitConverter.ToString(request);
            }
            return requestsString;
        }
    }
}
