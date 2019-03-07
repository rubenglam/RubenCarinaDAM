using CercadorDeFitxers.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Threading;

namespace CercadorDeFitxers
{

    public partial class MainWindow : Window
    {

        FileSearchHandler fileSearchHandler;

        public MainWindow()
        {
            InitializeComponent();
            fileSearchHandler = new FileSearchHandler();
            this.DataContext = fileSearchHandler;
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            
            
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if (snackbar.IsActive) snackbar.IsActive = false;

            if (entryContingutFitxer.Text == "" || entryContingutFitxer == null ||
                entryDirectory.Text == "" || entryDirectory == null || entryNomFitxer.Text == ""
                || entryNomFitxer == null)
            {
                snackbar.IsActive = true;
                var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(2) };
                timer.Start();
                timer.Tick += (senderTimer, args) =>
                {
                    snackbar.IsActive = false;
                    timer.Stop();
                };
            }
            else
            {
                SearchParams searchParams = new SearchParams(entryDirectory.Text, entryNomFitxer.Text, entryContingutFitxer.Text);
                fileSearchHandler.Search(searchParams);
            }
        }
    }
}
