using Serveis.Penjat.Services;
using Serveis.Penjat.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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

        public bool partidaFinalitzada = false;
        
        public MainWindow()
        {
            InitializeComponent();
            lblParaula.Text = "Benvigut al penjat! Escriu fes click a \"Nou Joc\" per començar";
        }

        private void StartNewGame()
        {
            byte[] data = new byte[1024];
            partidaFinalitzada = false;
            string missatge = Servidor.ServidorContract.PATH_NEW_GAME;
            try
            {
                if(ConnectionManager.GameIsRunning)
                {
                    string restart = Servidor.ServidorContract.PATH_EXIT;
                    data = Encoding.ASCII.GetBytes(restart);
                    ConnectionManager.SendData(Client.GetClient(), data);
                }
                data = Encoding.ASCII.GetBytes(missatge);
                ConnectionManager.SendData(Client.GetClient(), data);
                byte[] reciveData;

                reciveData = ConnectionManager.ReceiveData(Client.GetClient());

                partidaFinalitzada = false;
                lblParaula.Text = SimulateFontStretch(Encoding.UTF8.GetString(reciveData));
                tbxLletra.Text = null;
                SetCurrentPenjatImage(6);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
                MessageBox.Show(exception.Message, "ERROR");
            }
        }

        private void BtnEnviar_Click(object sender, RoutedEventArgs e)
        {
            byte[] data = new byte[1024];
            string lletra;
            if (!ConnectionManager.GameIsRunning)
            {
                MessageBox.Show("Cal iniciar connexió", "Error");
            }
            else if(partidaFinalitzada)
            {
                MessageBox.Show("Partida finalitzada", "Error");
            }
            else if (tbxLletra.Text == null || tbxLletra.Text == "")
            {
                MessageBox.Show("No has introduït cap valor", "Error");
            }
            else if(tbxLletra.Text.Length > 1)
            {
                MessageBox.Show("Introdueix només 1 lletra", "Error");
            }
            else
            {
                if (!partidaFinalitzada)
                {
                    lletra = tbxLletra.Text.ToLower();
                    data = Encoding.ASCII.GetBytes(Servidor.ServidorContract.PATH_SEND_LETTER + lletra);
                    ConnectionManager.SendData(Client.GetClient(), data);

                    byte[] missatge;
                    string[] resposta;

                    missatge = ConnectionManager.ReceiveData(Client.GetClient());
                    resposta = Encoding.ASCII.GetString(missatge).Split('?');

                    int estat = Convert.ToInt32(resposta[0].Split('#')[1]);
                    int intentsRestants = Convert.ToInt32(resposta[1].Split('#')[1]);
                    string paraulaEnCurs = resposta[2].Split('#')[1];

                    partidaFinalitzada = (estat == 1) ? true : false;

                    SetCurrentPenjatImage(intentsRestants);

                    lblParaula.Text = SimulateFontStretch(paraulaEnCurs);
                    tbxLletra.Text = null;

                    if (estat == 1) MessageBox.Show("Has guanyat", "Felicitats");
                    if (estat == 0 && intentsRestants == 0) MessageBox.Show("Has perdut", "Mala sort");

                    /*
                    if (partidaFinalitzada)
                    {
                        string final = resposta.Substring(1, 2);
                        if (final == "01") MessageBox.Show("Felicitats! Has trobat la paraula!", "Felicitats");
                        else MessageBox.Show("Oh! La proxima vegada hi haura mes sort!", ":(");
                    }
                    else
                    {
                        //string intents = resposta.Substring(1, 2);
                        //resposta = resposta.Substring(3);
                        lblParaula.Content = resposta;
                    }
                    */
                    
                }
            }
        }

        private void SetCurrentPenjatImage(int intentsRestants)
        {
            switch(intentsRestants)
            {
                case 0:
                    imgPenjat.Source = new BitmapImage(new Uri(@"/Resources/Drawable/penjat_7.png", UriKind.Relative));
                    break;
                case 1:
                    imgPenjat.Source = new BitmapImage(new Uri(@"/Resources/Drawable/penjat_6.png", UriKind.Relative));
                    break;
                case 2:
                    imgPenjat.Source = new BitmapImage(new Uri(@"/Resources/Drawable/penjat_5.png", UriKind.Relative));
                    break;
                case 3:
                    imgPenjat.Source = new BitmapImage(new Uri(@"/Resources/Drawable/penjat_4.png", UriKind.Relative));
                    break;
                case 4:
                    imgPenjat.Source = new BitmapImage(new Uri(@"/Resources/Drawable/penjat_3.png", UriKind.Relative));
                    break;
                case 5:
                    imgPenjat.Source = new BitmapImage(new Uri(@"/Resources/Drawable/penjat_2.png", UriKind.Relative));
                    break;
                case 6:
                    imgPenjat.Source = new BitmapImage(new Uri(@"/Resources/Drawable/penjat_1.png", UriKind.Relative));
                    break;
                case 7:
                    imgPenjat.Source = null;
                    break;
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (ConnectionManager.GameIsRunning)
            {
                string stop = Servidor.ServidorContract.PATH_EXIT;
                byte[] data = Encoding.ASCII.GetBytes(stop);
                ConnectionManager.SendData(Client.GetClient(), data);
            }
            ConnectionManager.Stop();
            Client.Stop();
            base.OnClosing(e);
        }

        string SimulateFontStretch(string text)
        {
            string finalText = "";
            for(int i = 0; i < text.Length - 1; i++)
            {
                finalText += text[i] + " ";
            }
            finalText += text[text.Length - 1];
            return finalText;
        }

        private void BtnNouJoc_Click(object sender, RoutedEventArgs e)
        {
            StartNewGame();
        }
    }
}
