using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Serveis.Penjat
{
    public class Utils
    {

        public static int SendData(Socket s, byte[] data)
        {

            byte[] aEnviar = new byte[data.Length + 1];
            aEnviar[0] = (byte)data.Length;

            Array.Copy(data, 0, aEnviar, 1, data.Length);

            int totalEnviat = 0;
            int sizeData = aEnviar.Length;
            int dataLeft = sizeData;
            int sent;
            while (totalEnviat < sizeData)
            {
                //bytes enviats, cal comprovar que no n'hi hagi encara per enviar.
                sent = s.Send(aEnviar, totalEnviat, dataLeft, SocketFlags.None);
                totalEnviat += sent;
                dataLeft -= sent;
            }
            return totalEnviat;
        }

        public static byte[] ReceiveData(Socket s)
        {
            byte[] bufferLongitud = new byte[1];
            int bRebuts = s.Receive(bufferLongitud, SocketFlags.None);

            if (bRebuts != 1)
                throw new Exception("No he pogut llegir la longitud del missatge");

            int total = 0;
            int size = bufferLongitud[0];
            int dataleft = size;
            byte[] iData = new byte[size];
            int iRecv;
            while (total < size)
            {
                iRecv = s.Receive(iData, total, dataleft, 0);
                if (iRecv == 0)
                {
                    iData = Encoding.ASCII.GetBytes("exit ");
                    throw new Exception("Error");
                }
                total += iRecv;
                dataleft -= iRecv;
            }
            return iData;
        }
    }
}
