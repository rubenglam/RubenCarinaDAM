using Microsoft.Win32;
using Serveis.Esteganografia.Renders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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
using System.Windows.Shapes;

namespace Serveis.Esteganografia.View
{
    /// <summary>
    /// Lógica de interacción para EsteganografiaPage.xaml
    /// </summary>
    public partial class EsteganografiaArxiuPage : Window
    {

        System.Drawing.Bitmap _bitmapImagePDF;
        Bitmap _bitmapOriginal;
        BitmapImage _bitmapEstegano;
        short[] _contentPDF;

        public EsteganografiaArxiuPage()
        {

            InitializeComponent();

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
                openFileDialog.Filter = "Bitmap(*.bmp)|*.bmp|Pdf|*.pdf|Tots els fitxers|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == true)
                {

                    string extension = openFileDialog.FileName.Split('.').Last();
                    pathImage = openFileDialog.FileName;

                    if (extension == "pdf")
                    {

                        byte[] bytesPDF = System.IO.File.ReadAllBytes(pathImage);
                        _contentPDF = new short[bytesPDF.Length];
                        for (int i = 0; i < bytesPDF.Length; i++) _contentPDF[i] = (short)Convert.ToInt32(bytesPDF[i]);
                        pictureBox1.Source = new BitmapImage(new Uri(@"\Resources\drawable\pdf_icon.png", UriKind.Relative));

                    }
                    else
                    {

                        pathImage = openFileDialog.FileName;
                        _bitmapOriginal = new System.Drawing.Bitmap(pathImage);
                        pictureBox2.Source = Utils.ConvertTo.BitmapToImageSource(_bitmapOriginal);

                    }

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
            pictureBox1.Width = (sizeInfo.NewSize.Width / 2) - 20;
            pictureBox2.Width = (sizeInfo.NewSize.Width / 2) - 20;
            pictureBox1.Height = (sizeInfo.NewSize.Height / 2) - 10;
            pictureBox2.Height = (sizeInfo.NewSize.Height / 2) - 10;

            base.OnRenderSizeChanged(sizeInfo);
        }

        private void Esteganografiar_Click(object sender, RoutedEventArgs e)
        {

            int pdfSize = _contentPDF.Length;
            int originalSize = _bitmapOriginal.Size.Height * _bitmapOriginal.Size.Width;

            if (pdfSize + 4 > originalSize) throw new Exception("L'arxiu es molt gran per guardar-lo a la imatge");
            else
            {

                ClassesEsteganografiques.EsteganografiarFitxer esteganografiarFitxer = new ClassesEsteganografiques.EsteganografiarFitxer();
                _bitmapEstegano = Utils.ConvertTo.BitmapToImageSource(esteganografiarFitxer.Esteganografiar(_bitmapOriginal, _contentPDF));


            }

        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {

            if (_bitmapEstegano != null)
            {

                Utils.ConvertTo.BitmapImage2Bitmap(_bitmapEstegano).Save("bitmappdf.bmp");
                MessageBox.Show("Guardat correctament");

            }
            else MessageBox.Show("No hi ha cap imatge per guardar");
        }

        private void Desesteganografiar_Click(object sender, RoutedEventArgs e)
        {
            if (_bitmapOriginal != null)
            {

                ClassesEsteganografiques.EsteganografiarFitxer esteganografiarFitxer = new ClassesEsteganografiques.EsteganografiarFitxer();
                int[] fitxer = esteganografiarFitxer.Desesteganografiar(_bitmapOriginal);
                byte[] fitxerBytes = new byte[fitxer.Length];

                for(int i = 0; i < fitxerBytes.Length; i++)
                {

                    fitxerBytes[i] = Convert.ToByte(fitxer[i]);

                }

                try
                {

                    SaveFileDialog saveFileDialog = new SaveFileDialog();

                    saveFileDialog.InitialDirectory = "c:\\";
                    saveFileDialog.Filter = "Pdf|*.pdf|Tots els fitxers|*.*";
                    saveFileDialog.FilterIndex = 0;
                    saveFileDialog.RestoreDirectory = true;

                    System.IO.FileStream file = System.IO.File.Create("pdf_from_bitmap.pdf");
                    for(int i = 0; i < fitxerBytes.Length; i++) file.WriteByte(fitxerBytes[i]);

                }
                catch (Exception exception)
                {

                    MessageBox.Show("Error", "Error no tractat");

                }

            }
            else MessageBox.Show("No hi ha cap imatge per desesteganografiar");
        }
    }
}
