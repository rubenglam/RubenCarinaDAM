using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Serveis.Penjat
{
    class Client
    {
        public static void Començar()
        {

            byte[] data = new byte[1024];
            string input, stringData;
            IPEndPoint ipep = new IPEndPoint(
            IPAddress.Parse("127.0.0.1"), 9050);
            Socket server = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);
            }
            catch (SocketException e)
            {
                Console.WriteLine("Unable to connect to server.");
                Console.WriteLine(e.ToString());
                return;
            }

            bool partidaFinalitzada = false;
            char lletra;
            while (!partidaFinalitzada) {
                byte[] misatge = Utils.ReceiveData(server);
                partidaFinalitzada = Encoding.ASCII.GetString(misatge).Contains("lletra");

                if (!partidaFinalitzada) {

                    lletra = generarLletra();

                    data = Encoding.ASCII.GetBytes(lletra.ToString());
                    Utils.SendData(server, data);
                }

            }

            


        }
        private static char generarLletra()
        {
            Random r = new Random();
            int lletraAscii = r.Next(97, 123);
            char lletra = (char)lletraAscii;
            return lletra;
        }

    }
}
