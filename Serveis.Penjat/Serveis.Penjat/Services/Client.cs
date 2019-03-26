using Serveis.Penjat.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Serveis.Penjat.Services
{
    public static class Client
    {
        static Socket socket;

        public static void Start()
        {
            byte[] data = new byte[1024];
            string input, stringData;
            IPEndPoint ipep = new IPEndPoint(
            IPAddress.Parse("127.0.0.1"), 9050);
            socket = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);
            try
            {
                socket.Connect(ipep);
            }
            catch (SocketException e)
            {
                Console.WriteLine("Unable to connect to server.");
                Debug.WriteLine(e.ToString());
            }
        }

        public static void Stop()
        {
            if (socket.Connected)
            {
                try
                {
                    socket.Disconnect(true);
                }
                catch (SocketException e)
                {
                    Console.WriteLine("Unable to disconnect server");
                    Debug.WriteLine(e.ToString());
                }
            }
            else
            {
                Console.WriteLine("Not connected");
            }
        }

        public static Socket GetClient()
        {
            return socket;
        }

        static char GenerarLletra()
        {
            Random r = new Random();
            int lletraAscii = r.Next(97, 123);
            char lletra = (char)lletraAscii;
            return lletra;
        }
    }
}
