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
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Serveis.Dibuixador.Services
{
    public class Servidor
    {

        public const string BAD_REQUEST = "BAD_REQUEST";
        public const string DATA_SENT = "DATA_SENT";
        public const string STOP = "STOP";

        MainWindow _context;

        public Servidor(MainWindow context)
        {
            _context = context;
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

                if (badFormatLinia != STOP)
                {

                    if (GoodFormat(badFormatLinia))
                    {
                        linia = GetPointFromRequest(badFormatLinia);
                        Action drawLineAction = new Action(DrawLine);
                        _context.Dispatcher.BeginInvoke(DispatcherPriority.Background, drawLineAction);
                        ConnectionManager.SendData(client, Encoding.ASCII.GetBytes(DATA_SENT));
                    }
                    else
                    {
                        ConnectionManager.SendData(client, Encoding.ASCII.GetBytes(BAD_REQUEST));
                    }
                }
                else { isRunning = false; }
            }
            Console.WriteLine("Disconnected from {0}",
            clientep.Address);
            client.Close();
            newsock.Close();
        }

        void DrawLine()
        {
            Line line = new Line();
            line.StrokeThickness = 1;
            line.Stroke = Brushes.Black;
            line.X1 = linia.PuntOrigen.X;
            line.X2 = linia.PuntFinal.X;
            line.Y1 = linia.PuntOrigen.Y;
            line.Y2 = linia.PuntFinal.Y;
            _context.canvas.Children.Add(line);
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
            linia.PuntOrigen = new System.Windows.Point(Convert.ToDouble(request.Substring(0,3)), Convert.ToDouble(request.Substring(3,3)));
            linia.PuntFinal = new System.Windows.Point(Convert.ToDouble(request.Substring(6,3)), Convert.ToDouble(request.Substring(9,3)));
            return linia;
        }

        public void Stop()
        {
            isRunning = false;
        }

        public bool IsRunning => isRunning;

    }
}
