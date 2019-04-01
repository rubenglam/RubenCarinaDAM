using Serveis.Dibuixador.Model;
using Serveis.Dibuixador.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Serveis.Dibuixador.Services
{
    public class Servidor
    {

        Canvas _canvas;

        public Servidor(Canvas canvas)
        {
            _canvas = canvas;
        }

        bool isRunning;
        Serveis.Dibuixador.Model.Linia linia;

        public void Start()
        {
            isRunning = true;
            byte[] data = new byte[1024];
            
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any,
            9050);
            Socket newsock = new
            Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            newsock.Bind(ipep);
            newsock.Listen(10);
            Console.WriteLine("Waiting for a client...");
            //Accepto la connexió amb el client, tot queda dins un nou socket que utilitzarem sempre
            //per connectar-nos amb aquest client.
            Socket client = newsock.Accept();
            //Informació del client.
            IPEndPoint clientep = (IPEndPoint)client.RemoteEndPoint;
            Console.WriteLine("Connected with {0} at port {1}",
                clientep.Address, clientep.Port);

            while (isRunning)
            {

                byte[] missatge = ConnectionManager.ReceiveData(client);
                // Format 001002003004 -> Punt1: {1,2} , Punt2: {3,4} 
                string badFormatLinia = Encoding.ASCII.GetString(missatge);

                if (GoodFormat(badFormatLinia))
                {
                    linia = GetPointFromRequest(badFormatLinia);
                }
                Action drawLineAction = new Action(DrawLine);
                drawLineAction.Invoke();
                
            }
            Console.WriteLine("Disconnected from {0}",
            clientep.Address);
            client.Close();
            newsock.Close();
        }

        void DrawLine()
        {

        }

        bool GoodFormat(string request)
        {
            bool format = true;
            if (request.Length == 12)
            {
                int posicioActual = 0;
                while (posicioActual < request.Length && format)
                {
                    char mCaracterActual = Convert.ToChar(request[posicioActual]);
                    format = mCaracterActual >= 48 && mCaracterActual <= 57;
                    posicioActual++;
                }
                return format;
            }
            else return false;
        }

        Linia GetPointFromRequest(string request)
        {
            Linia linia = new Linia();
            linia.PuntOrigen = new System.Windows.Point(Convert.ToDouble(request[0] + request[1] + request[2]), Convert.ToDouble(request[4] + request[5] + request[6]));
            linia.PuntFinal = new System.Windows.Point(Convert.ToDouble(request[7] + request[8] + request[9]), Convert.ToDouble(request[10] + request[11] + request[12]));
            return linia;
        }

        public void Stop()
        {
            isRunning = false;
        }

        public bool IsRunning => isRunning;

    }
}
