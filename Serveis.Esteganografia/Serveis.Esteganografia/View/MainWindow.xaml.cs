using Serveis.Esteganografia.View;
using System;
using System.Collections.Generic;
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

namespace Serveis.Esteganografia
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static NavigationPage _navigationPage;

        public MainWindow()
        {
            App.TestEsteganografiar();
            InitializeComponent();
            _navigationPage = new NavigationPage(this);
        }

        // OnRenderSizeChange for update FontSize btns
        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {

            double newSizeWidth = sizeInfo.NewSize.Width;

            if(newSizeWidth < 600)
            {
                btnOpcio1.FontSize = 25;
                btnOpcio2.FontSize = 25;
                btnOpcio3.FontSize = 25;
            }
            else if(newSizeWidth >= 600 && sizeInfo.NewSize.Width <= 1200)
            {
                btnOpcio1.FontSize = 30;
                btnOpcio2.FontSize = 30;
                btnOpcio3.FontSize = 30;
            }
            else if(newSizeWidth > 1200)
            {
                btnOpcio1.FontSize = 40;
                btnOpcio2.FontSize = 40;
                btnOpcio3.FontSize = 40;
            }

            base.OnRenderSizeChanged(sizeInfo);
        }

        private void btnEstegText_Click(object sender, RoutedEventArgs e)
        {

            _navigationPage.Push(new EsteganografiaPage());

        }

        private void btnDestegText_Click(object sender, RoutedEventArgs e)
        {

            _navigationPage.Push(new DesesteganografiaPage());

        }

        private void btnEstegArxiu_Click(object sender, RoutedEventArgs e)
        {

            _navigationPage.Push(new EsteganografiaArxiuPage());

        }
    }
}
