using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Serveis.Penjat.Utils
{
    public static class ConnectionManager
    {
        static Thread servidorTask;
        static Servidor servidor;

        public static void Start()
        {
            if(servidor == null)
            {
                servidor = new Servidor();
                servidorTask = new Thread(servidor.Start);
                servidorTask.Start();
            }
        }

        public static void Stop()
        {
            servidorTask.Abort();
        }

        public static int SendData(Socket socket, byte[] data)
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
                sent = socket.Send(aEnviar, totalEnviat, dataLeft, SocketFlags.None);
                totalEnviat += sent;
                dataLeft -= sent;
            }
            return totalEnviat;
        }

        public static byte[] ReceiveData(Socket socket)
        {
            byte[] bufferLongitud = new byte[1];
            int bRebuts = socket.Receive(bufferLongitud, SocketFlags.None);

            if (bRebuts != 1)
                throw new Exception("No he pogut llegir la longitud del missatge");

            int total = 0;
            int size = bufferLongitud[0];
            int dataleft = size;
            byte[] data = new byte[size];
            int recv;
            while (total < size)
            {
                recv = socket.Receive(data, total, dataleft, 0);
                if (recv == 0)
                {
                    data = Encoding.ASCII.GetBytes("exit ");
                    throw new Exception("Error");
                }
                total += recv;
                dataleft -= recv;
            }
            return data;
        }
    }
}
