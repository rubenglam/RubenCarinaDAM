using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Media;
using System.Windows.Media;

namespace Serveis.Esteganografia.Renders
{
    public class glamTextBox : TextBox
    {

        private string _placeHolder;

        public glamTextBox()
        {

            Text = "Default placeholder";
            Foreground = Brushes.DarkGray;
            GotFocus += GlamTextBox_GotFocus;
            LostFocus += GlamTextBox_LostFocus;

        }

        /// <summary>
        /// UnFocus textBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GlamTextBox_LostFocus(object sender, RoutedEventArgs e)
        {

            var textBox = sender as TextBox;

            if (textBox.Text == "")
            {

                Text = _placeHolder;
                Foreground = Brushes.Black;

            }

        }

        /// <summary>
        /// OnFocus textBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GlamTextBox_GotFocus(object sender, RoutedEventArgs e)
        {

            var textBox = sender as TextBox;

            if (textBox.Text == _placeHolder)
            {

                Text = "";
                Foreground = Brushes.DarkGray;

            }

        }

        /// <summary>
        /// Permet modificar el text del placeholder
        /// </summary>
        public string Placeholder
        {

            get { return _placeHolder; }
            set { _placeHolder = value; }

        }

    }
}
