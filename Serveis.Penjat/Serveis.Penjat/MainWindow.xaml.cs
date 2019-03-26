using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Serveis.Penjat
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static bool partidaFinalitzada;
        public static Socket server;
        public MainWindow()
        {

            Thread server = new Thread(Servidor.Començar);
            Client.Començar();
            InitializeComponent();
            partidaFinalitzada = false;
            
            

        }

        public class Client
        {
            public static void Començar()
            {

                byte[] data = new byte[1024];
                string input, stringData;
                IPEndPoint ipep = new IPEndPoint(
                IPAddress.Parse("127.0.0.1"), 9050);
                server = new Socket(AddressFamily.InterNetwork,
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
                


            }
            private static char generarLletra()
            {
                Random r = new Random();
                int lletraAscii = r.Next(97, 123);
                char lletra = (char)lletraAscii;
                return lletra;
            }

        }

        private void BtnEnviar_Click(object sender, RoutedEventArgs e)
        {
            byte[] data = new byte[1024];
            string lletra;
            if (!partidaFinalitzada)
            {
                lletra = tbxLletra.Text;
                data = Encoding.ASCII.GetBytes(lletra);
                Utils.SendData(server, data);

                
                byte[] misatge = Utils.ReceiveData(server);
                string resposta = Encoding.ASCII.GetString(misatge);
                // Rebrem 0 si la partida esta en curs, 1 si esta finalitzada
                partidaFinalitzada = resposta[0] == '1';

                if (partidaFinalitzada)
                {
                    string final = resposta.Substring(1, 2);
                    if (final == "01") lblEstat.Content = "Felicitats! Has trobat la paraula!";
                    else lblEstat.Content = "Oh! La proxima vegada hi haura mes sort!";

                }
                else
                {
                    string intents = resposta.Substring(1, 2);
                    lblEstat.Content = intents;
                    resposta = resposta.Substring(3);
                    lblParaula.Content = resposta;
                }
                

            }
        }
    }
}
