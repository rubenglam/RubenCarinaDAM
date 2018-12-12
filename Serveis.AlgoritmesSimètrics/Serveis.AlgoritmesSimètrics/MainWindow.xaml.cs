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
using Serveis___Algoritmes_simètrics.NavigationBar;
using Serveis___Algoritmes_simètrics.Enums;

namespace Serveis___Algoritmes_simètrics
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {

            InitializeComponent();

            topBar.InvalidateSize();

            topBar.MouseDown += TopBar_MouseDown;

            leftBorder.Cursor = Cursors.SizeWE;
            topBorder.Cursor = Cursors.SizeNS;
            rightBorder.Cursor = Cursors.SizeWE;
            botBorder.Cursor = Cursors.SizeNS;

            InitializeItems();

        }

        private void InitializeItems()
        {

            boxTextPla.Text = "Introdueix un text normal";
            boxTextPla.Foreground = Brushes.DarkSlateGray;
            boxTextPla.GotFocus += BoxTextPla_GotFocus;
            boxTextPla.LostFocus += BoxTextPla_LostFocus;

            boxClauX.Text = "Introdueix la clau";
            boxClauX.Foreground = Brushes.DarkSlateGray;
            boxClauX.GotFocus += BoxClau_GotFocus;
            boxClauX.LostFocus += BoxClau_LostFocus;

            boxTextXifrat.Text = "";
            boxTextXifrat.Foreground = Brushes.DarkSlateGray;
            boxTextXifrat.GotFocus += BoxTextD_GotFocus;
            boxTextXifrat.LostFocus += BoxTextD_LostFocus;

            boxClauD.Text = "Introdueix la clau";
            boxClauD.Foreground = Brushes.DarkSlateGray;
            boxClauD.GotFocus += BoxClauD_GotFocus;
            boxClauD.LostFocus += BoxClauD_LostFocus;

        }

        // Events Entry Box
        private void BoxClau_LostFocus(object sender, RoutedEventArgs e)
        {

            var item = sender as TextBox;

            if (item.Text == "")
            {

                item.Text = "Introdueix la clau";
                item.Foreground = Brushes.DarkSlateGray;

            }

        }
        private void BoxClau_GotFocus(object sender, RoutedEventArgs e)
        {

            var item = sender as TextBox;

            if (item.Text == "Introdueix la clau")
            {

                item.Text = "";
                item.Foreground = Brushes.Black;

            }

        }
        private void BoxTextPla_LostFocus(object sender, RoutedEventArgs e)
        {

            var item = sender as TextBox;

            if (item.Text == "")
            {

                item.Text = "Introdueix un text normal";
                item.Foreground = Brushes.DarkSlateGray;

            }

        }
        private void BoxTextPla_GotFocus(object sender, RoutedEventArgs e)
        {

            var item = sender as TextBox;

            if (item.Text == "Introdueix un text normal")
            {

                item.Text = "";
                item.Foreground = Brushes.Black;

            }
          
        }
        // Events Entry Box
        private void BoxClauD_LostFocus(object sender, RoutedEventArgs e)
        {

            var item = sender as TextBox;

            if (item.Text == "")
            {

                item.Text = "Introdueix la clau";
                item.Foreground = Brushes.DarkSlateGray;

            }

        }
        private void BoxClauD_GotFocus(object sender, RoutedEventArgs e)
        {

            var item = sender as TextBox;

            if (item.Text == "Introdueix la clau")
            {

                item.Text = "";
                item.Foreground = Brushes.Black;

            }

        }
        private void BoxTextD_LostFocus(object sender, RoutedEventArgs e)
        {

            var item = sender as TextBox;

            if (item.Text == "")
            {

                item.Text = "Introdueix un text normal";
                item.Foreground = Brushes.DarkSlateGray;

            }

        }
        private void BoxTextD_GotFocus(object sender, RoutedEventArgs e)
        {

            var item = sender as TextBox;

            if (item.Text == "Introdueix un text normal")
            {

                item.Text = "";
                item.Foreground = Brushes.Black;

            }

        }


        private void TopBar_MouseDown(object sender, MouseButtonEventArgs e)
        {

            if (e.ChangedButton == MouseButton.Left && e.ClickCount >= 2)
            {

                if(WindowState == WindowState.Normal) this.WindowState = WindowState.Maximized;
                else this.WindowState = WindowState.Normal;

            }

        }

        /// <summary>
        /// Event OnSizeWindow changed
        /// </summary>
        /// <param name="sizeInfo"></param>
        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {

            topBar.InvalidateSize();
            TabControl.Width = sizeInfo.NewSize.Width;
            resultatLabelX.Width = sizeInfo.NewSize.Width - 40;
            resultatLabelD.Width = sizeInfo.NewSize.Width - 40;

            base.OnRenderSizeChanged(sizeInfo);
        }

        /// <summary>
        /// Acció de xifrar per el button "Xifrar"
        /// </summary>
        /// <param name="sender">Button</param>
        /// <param name="e">EventArguments</param>
        private void btnXifrar_Click(object sender, RoutedEventArgs e)
        {

            string text = boxTextPla.Text;
            string clau = boxClauX.Text;
            string resultat = "";

            switch (comboBoxX.SelectedItem)
            {

                case TipusAlgoritmes.Polibi:
                    Algoritmes.Polibi polibi = new Algoritmes.Polibi(clau);
                    resultat = polibi.Xifrar(text);
                    break;

                case TipusAlgoritmes.JuliCèsar:
                    Algoritmes.JC juliCesar = new Algoritmes.JC(Convert.ToInt32(clau), "abcdefghijklmnopqrstuvwxyz");
                    resultat = juliCesar.Xifrar(text);
                    break;

                case TipusAlgoritmes.Inventat:
                    Algoritmes.Inventat inventat = new Algoritmes.Inventat(clau);
                    resultat = inventat.Xifrar(text);
                    break;

                case TipusAlgoritmes.Transposició:
                    Algoritmes.Transposicio transposicio = new Algoritmes.Transposicio(clau);
                    resultat = transposicio.Xifrar(text);
                    break;

            }

            labelResultatX.Text = resultat;

        }

        /// <summary>
        /// Inicialització del comboBox de xifrar amb els items de la enumeració "TipusAlgoritmes"
        /// </summary>
        /// <param name="sender">ComboBox</param>
        /// <param name="e">EventArguments</param>
        private void comboBox_Initialized(object sender, EventArgs e)
        {

            var comboBox = sender as ComboBox;

            comboBox.Items.Add(TipusAlgoritmes.Polibi);
            comboBox.Items.Add(TipusAlgoritmes.JuliCèsar);
            comboBox.Items.Add(TipusAlgoritmes.Inventat);
            comboBox.Items.Add(TipusAlgoritmes.Transposició);

        }

        private void btnDesxifrar_Click(object sender, RoutedEventArgs e)
        {

            string text = boxTextXifrat.Text;
            string clau = boxClauD.Text;
            string resultat = "";

            switch (comboBoxD.SelectedItem)
            {

                case TipusAlgoritmes.Polibi:
                    Algoritmes.Polibi polibi = new Algoritmes.Polibi(clau);
                    resultat = polibi.Desxifrar(text);
                    break;

                case TipusAlgoritmes.JuliCèsar:
                    Algoritmes.JC juliCesar = new Algoritmes.JC(Convert.ToInt32(clau), "abcdefghijklmnopqrstuvwxyz");
                    resultat = juliCesar.Desxifrar(text);
                    break;

                case TipusAlgoritmes.Inventat:
                    Algoritmes.Inventat inventat = new Algoritmes.Inventat(clau);
                    resultat = inventat.Desxifrar(text);
                    break;

                case TipusAlgoritmes.Transposició:
                    Algoritmes.Transposicio transposicio = new Algoritmes.Transposicio(clau);
                    resultat = transposicio.Desxifrar(text);
                    break;

            }

            labelResultatD.Text = resultat;


        }
    }

}
