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
            lblParaula.Content = "Benvigut al penjat! Escriu fes click a \"Nou Joc\" per començar";
        }

        private void StartNewGame()
        {
            byte[] data = new byte[1024];
            partidaFinalitzada = false;
            string missatge = Servidor.ServidorContract.PATH_NEW_GAME;
            try
            {
                data = Encoding.ASCII.GetBytes(missatge);
                ConnectionManager.SendData(Client.GetClient(), data);
                byte[] reciveData;

                do
                {
                    reciveData = ConnectionManager.ReceiveData(Client.GetClient());
                }
                while (Encoding.ASCII.GetString(reciveData) == "101");

                lblParaula.Content = SimulateFontStretch(Encoding.UTF8.GetString(reciveData));
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
            if (tbxLletra.Text == null || tbxLletra.Text == "")
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
                    lletra = tbxLletra.Text;
                    data = Encoding.ASCII.GetBytes(Servidor.ServidorContract.PATH_SEND_LETTER + lletra);
                    ConnectionManager.SendData(Client.GetClient(), data);

                    byte[] missatge;
                    string[] resposta;

                    do
                    {
                        missatge = ConnectionManager.ReceiveData(Client.GetClient());
                    }
                    while (Encoding.ASCII.GetString(missatge) == "101");
                    resposta = Encoding.ASCII.GetString(missatge).Split('?');

                    string estat = resposta[0].Split('#')[1];
                    string intentsRestants = resposta[1].Split('#')[1];
                    string paraulaEnCurs = resposta[2].Split('#')[1];

                    partidaFinalitzada = (estat == "1") ? true : false;
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
                    lblParaula.Content = SimulateFontStretch(paraulaEnCurs);
                }
                else
                {
                    MessageBox.Show("Partida finalitzada", "Error");
                }
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            ConnectionManager.Stop();
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
