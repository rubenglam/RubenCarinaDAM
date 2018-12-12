using Microsoft.Win32;
using Serveis.Esteganografia.Renders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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
    public partial class EsteganografiaPage : Window
    {

        private Serveis.Esteganografia.ClassesEsteganografiques.EsteganografiarText _esteganografiar = new Esteganografia.ClassesEsteganografiques.EsteganografiarText();
        private ImageSource _defaultSource;
        private Bitmap _bitmapOriginal;
        private BitmapImage _bitMapEstegano;

        public EsteganografiaPage()
        {


            InitializeComponent();
            _defaultSource = new BitmapImage(new Uri(@"\Resources\drawable\image_background.png", UriKind.Relative));
            pictureBox1.Source = _defaultSource;

            btnBackControl.Content = new System.Windows.Controls.Image()
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
                    _bitmapOriginal = new Bitmap(pathImage);
                    pictureBox1.Source = Utils.ConvertTo.BitmapToImageSource(_bitmapOriginal);

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
            pictureBox2.Width = (sizeInfo.NewSize.Width / 2);
            pictureBox1.Height = (sizeInfo.NewSize.Height / 2);
            pictureBox2.Height = (sizeInfo.NewSize.Height / 2);

            txtBox.Width = txtBoxLayout.RenderSize.Width;

            base.OnRenderSizeChanged(sizeInfo);
        }

        private void EsteganografiarButton_Click(object sender, RoutedEventArgs e)
        {

            if (pictureBox1.Source == _defaultSource) MessageBox.Show("Imatge incorrecta", "No hi ha cap imatge seleccionada");
            else if (txtBox.Text == "") MessageBox.Show("Falta un text", "Introdueix un text per codificar");
            else if (EmptyBitsTextBox() == 1)
            {
                
                Bitmap bitmapEstegano = _esteganografiar.Esteganografiar(_bitmapOriginal, txtBox.Text);
                _bitMapEstegano = Utils.ConvertTo.BitmapToImageSource(bitmapEstegano);
                pictureBox2.Source = _bitMapEstegano;

            }
            else if (EmptyBitsTextBox() == 2)
            {

                short[] bits = new short[8];
                for(int i = 0; i < 8; i++)
                {

                    bits[i] = (short)Convert.ToInt32((stkBitsLayout.Children[i] as TextBox).Text);

                }
                _esteganografiar.BitsMissatge = bits;
                Bitmap bitmapEstegano = _esteganografiar.Esteganografiar(_bitmapOriginal, txtBox.Text);
                _bitMapEstegano = Utils.ConvertTo.BitmapToImageSource(bitmapEstegano);
                pictureBox2.Source = _bitMapEstegano;
               
            }
            else MessageBox.Show("Camps incorrectes", "Omple tots els camps en el bits");

        }

        private int EmptyBitsTextBox()
        {

            int resultat = 0;
            int elementsPlens = 0;

            for(int i = 0; i < stkBitsLayout.Children.Count; i++)
            {

                elementsPlens += (stkBitsLayout.Children[i] as TextBox).Text != "" ? 1 : 0;

            }

            if (elementsPlens == 0) resultat = 1;
            else if (elementsPlens == 8) resultat = 2;

            return resultat;

        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {

            if (_bitMapEstegano != null)
            {

                Utils.ConvertTo.BitmapImage2Bitmap(_bitMapEstegano).Save("bitmap.bmp");
                MessageBox.Show("Guardat correctament");

            }
            else MessageBox.Show("No hi ha cap imatge per guardar");

        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);

        }
    }
}
