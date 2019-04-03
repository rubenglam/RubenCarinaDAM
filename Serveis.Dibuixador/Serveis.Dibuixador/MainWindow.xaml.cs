using Serveis.Dibuixador.Services;
using Serveis.Dibuixador.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
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

namespace Serveis.Dibuixador
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ConnectionManager.Start(this);
            Client.Start();
            ConnectionManager.SendData(Client.GetClient(), Encoding.ASCII.GetBytes("Connexió correcta"));
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (ConnectionManager.IsRunning)
            {
                string stop = Servidor.STOP;
                byte[] data = Encoding.ASCII.GetBytes(stop);
                ConnectionManager.SendData(Client.GetClient(), data);
            }
            Client.Stop();
            ConnectionManager.Stop();
            base.OnClosing(e);
        }

        private void BtnEnviarCoordenades_Click(object sender, RoutedEventArgs e)
        {
            string textEnviar = entryCoordenades.Text;
            ConnectionManager.SendData(Client.GetClient(), Encoding.ASCII.GetBytes(textEnviar));
            string serverAnswer = Encoding.ASCII.GetString(ConnectionManager.ReceiveData(Client.GetClient()));
            if(serverAnswer == Servidor.BAD_REQUEST)
            {
                MessageBox.Show("Les dades tenen un format incorrecte: xxxyyyXXXYYY.\nExemple: 001002003004 -> P1(1,2) & P2(3,4)", "Error");
            }
        }
    }
}
