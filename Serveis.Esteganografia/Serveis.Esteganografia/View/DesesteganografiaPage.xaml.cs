using Microsoft.Win32;
using Serveis.Esteganografia.Renders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Serveis.Esteganografia.View
{
    /// <summary>
    /// Lógica de interacción para EsteganografiaPage.xaml
    /// </summary>
    public partial class DesesteganografiaPage : Window
    {

        private Serveis.Esteganografia.ClassesEsteganografiques.EsteganografiarText _desesteganografiar = new Esteganografia.ClassesEsteganografiques.EsteganografiarText();
        private ImageSource _defaultSource;
        private BitmapImage _bitMap;

        public DesesteganografiaPage()
        {

            InitializeComponent();
            _defaultSource = pictureBox1.Source;

            btnBackControl.Content = new Image()
            {

                Source = new BitmapImage(new Uri(@"\Resources\drawable\ic_arrow_back_black_48dp.png", UriKind.Relative)),

            };

            btnBackControl.Click += BtnBackControl_Click;
            btnCarregar.Click += BtnCarregar_Click;

            topBar.Width = (this.Content as DockPanel).Width;

        }

        private void BtnCarregar_Click(object sender, RoutedEventArgs e)
        {

            var fileContent = string.Empty;
            var filePath = string.Empty;
            string pathImage = null;

            try
            {

                OpenFileDialog openFileDialog = new OpenFileDialog();

                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Bitmap(*.bmp)|*.bmp";
                openFileDialog.FilterIndex = 0;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == true)
                {

                    pathImage = openFileDialog.FileName;

                    _bitMap = new BitmapImage(new Uri(pathImage));
                    pictureBox1.Source = _bitMap;

                }

            }
            catch (Exception exception) {
            
	            MessageBox.Show("Error", "Error no tractat");
            
			}         

        }

        protected override void OnClosing(CancelEventArgs e)
        {

            if(!btnBackClicked) MainWindow._navigationPage.PopAll();

            base.OnClosing(e);
        }

        private bool btnBackClicked = false;
        private void BtnBackControl_Click(object sender, RoutedEventArgs e)
        {

            btnBackClicked = true;
            MainWindow._navigationPage.Pop(this); 

        }
        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {

            topBar.Width = (this.Content as DockPanel).Width;
            pictureBox1.Width = (sizeInfo.NewSize.Width / 2);
            pictureBox1.Height = (sizeInfo.NewSize.Height / 2);

            base.OnRenderSizeChanged(sizeInfo);
        }

        private void DesesteganografiaButton_Click(object sender, RoutedEventArgs e)
        {

            if (pictureBox1.Source == _defaultSource) MessageBox.Show("Imatge incorrecta", "No hi ha cap imatge seleccionada");
            else {

                if (txtBits1.Text != "" && txtBits2.Text != "" && txtBits3.Text != "" && txtBits4.Text != "" && txtBits5.Text != "" && txtBits6.Text != "" && txtBits7.Text != "" && txtBits8.Text != "") _desesteganografiar.BitsMissatge = new short[8] { ((short)Convert.ToInt32(txtBits1.Text)), ((short)Convert.ToInt32(txtBits2.Text)), ((short)Convert.ToInt32(txtBits3.Text)), ((short)Convert.ToInt32(txtBits4.Text)), ((short)Convert.ToInt32(txtBits5.Text)), ((short)Convert.ToInt32(txtBits6.Text)), ((short)Convert.ToInt32(txtBits7.Text)), ((short)Convert.ToInt32(txtBits8.Text)) };
                lblResultat.Content = _desesteganografiar.Desesteganografiar(Utils.ConvertTo.BitmapImage2Bitmap(_bitMap));

            }

        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);

        }

    }
}
