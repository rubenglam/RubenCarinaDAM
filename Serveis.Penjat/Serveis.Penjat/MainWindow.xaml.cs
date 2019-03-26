using Serveis.Penjat.Services;
using Serveis.Penjat.Utils;
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
        
        public MainWindow()
        {
            InitializeComponent();
            partidaFinalitzada = false;
        }

        private void BtnEnviar_Click(object sender, RoutedEventArgs e)
        {
            byte[] data = new byte[1024];
            string lletra;
            if (!partidaFinalitzada)
            {
                lletra = tbxLletra.Text;
                data = Encoding.ASCII.GetBytes(lletra);
                ConnectionManager.SendData(Client.GetClient(), data);

                
                byte[] misatge = ConnectionManager.ReceiveData(Client.GetClient());
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
