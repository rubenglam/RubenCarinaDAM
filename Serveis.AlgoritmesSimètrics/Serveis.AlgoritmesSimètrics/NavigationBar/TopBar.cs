using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Serveis___Algoritmes_simètrics.NavigationBar
{
    public class TopBar : Grid
    {

        private double _ViewHeight;
        private double _ViewWidth;
        private string _pathIcon;

        public TopBar()
        {

            BrushConverter bc = new BrushConverter();
            Brush brush;

            _pathIcon = "Resources\\icon.png";

            DrawBounds();
            
            brush = (Brush)bc.ConvertFrom("#2196F3");
            brush.Freeze();
            this.Background = brush;
            AddNativeChildren();

            this.MouseDown += TopBar_MouseDown;

        }

        #region PRIVATE FUNCTIONS

        private void AddNativeChildren()
        {

            InitializeHamburgerButton();
            InitializeTitleApp();
            InitializeControlButtons();
            AddListeners();

        }

        private void AddListeners()
        {

              

        }

        /// <summary>
        /// Crear los botones de control derecho
        /// </summary>
        private void InitializeControlButtons()
        {

            Grid gridControl = new Grid();
            Button closeButton = new Button();
            Button resizeButton = new Button();
            Button minimizeButton = new Button();
            Image imageClose = new Image();
            Image resizeClose = new Image();
            Image minimizeClose = new Image();
            BitmapImage bImageClose = new BitmapImage(new Uri(@"Resources\closetv2_tra.png",
                UriKind.RelativeOrAbsolute));
            BitmapImage bImageResize = new BitmapImage(new Uri(@"Resources\icono_resizet.png",
               UriKind.RelativeOrAbsolute));
            BitmapImage bImageMinimize = new BitmapImage(new Uri(@"Resources\resumeButton_centrado.png",
               UriKind.RelativeOrAbsolute));

            gridControl.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            gridControl.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            gridControl.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

            gridControl.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });

            imageClose.Source = bImageClose;
            closeButton.Content = imageClose;
            closeButton.Style = Application.Current.FindResource("MyButton") as Style;
            closeButton.Background = Brushes.Red;
            closeButton.BorderBrush = Brushes.Transparent;
            closeButton.BorderThickness = new Thickness(0, 0, 0, 0);
            closeButton.Click += CloseButton_Click;
            closeButton.MouseEnter += CloseButton_MouseEnter;
            closeButton.MouseLeave += CloseButton_MouseLeave;

            resizeClose.Source = bImageResize;
            resizeButton.Content = resizeClose;
            resizeButton.Style = Application.Current.FindResource("MyButton") as Style;
            resizeButton.Background = Brushes.Transparent;
            resizeButton.BorderBrush = Brushes.Transparent;
            resizeButton.BorderThickness = new Thickness(0, 0, 0, 0);
            resizeButton.Click += ResizeButton_Click;
            resizeButton.MouseEnter += BlueBtn_MouseEnter;
            resizeButton.MouseLeave += BlueBtn_MouseLeave;

            minimizeClose.Source = bImageMinimize;
            minimizeButton.Content = minimizeClose;
            minimizeButton.Style = Application.Current.FindResource("MyButton") as Style;
            minimizeButton.Background = Brushes.Transparent;
            minimizeButton.BorderBrush = Brushes.Transparent;
            minimizeButton.BorderThickness = new Thickness(0, 0, 0, 0);
            minimizeButton.Click += MinimizeButton_Click;
            minimizeButton.MouseEnter += BlueBtn_MouseEnter;
            minimizeButton.MouseLeave += BlueBtn_MouseLeave;

            gridControl.Children.Add(closeButton);
            gridControl.Children.Add(resizeButton);
            gridControl.Children.Add(minimizeButton);

            Grid.SetColumn(gridControl, 2);
            Grid.SetRow(gridControl, 0);
            Grid.SetColumn(closeButton, 2);
            Grid.SetRow(closeButton, 0);
            Grid.SetColumn(resizeButton, 1);
            Grid.SetRow(resizeButton, 0);
            Grid.SetColumn(minimizeButton, 0);
            Grid.SetRow(minimizeButton, 0);

            this.Children.Add(gridControl);

        }

        /// <summary>
        /// Crear title box
        /// </summary>
        private void InitializeTitleApp()
        {

            Label titleLabel = new Label();

            titleLabel.Content = "Algoritmes Simètrics";
            titleLabel.FontSize = 14;
            titleLabel.Foreground = Brushes.White;
            titleLabel.FontWeight = FontWeights.DemiBold;
            titleLabel.FontFamily = new FontFamily("GoogleSansMedium");
            titleLabel.VerticalAlignment = VerticalAlignment.Center;

            Grid.SetColumn(titleLabel, 1);
            Grid.SetRow(titleLabel, 0);

            this.Children.Add(titleLabel);

        }

        /// <summary>
        /// Crear menu hamburger
        /// </summary>
        private BitmapImage _bImage;
        private void InitializeHamburgerButton()
        {

            Button hamburgerMenu = new Button();
            Image image = new Image();
            _bImage = new BitmapImage(new Uri(_pathIcon,
                UriKind.RelativeOrAbsolute));

            image.Source = _bImage;
            hamburgerMenu.Content = image;
            hamburgerMenu.Margin = new Thickness(3, 2, 2, 2);
            hamburgerMenu.Style = Application.Current.FindResource("MyButton") as Style;
            hamburgerMenu.Background = Brushes.Transparent;
            hamburgerMenu.BorderBrush = Brushes.Transparent;
            hamburgerMenu.BorderThickness = new Thickness(0, 0, 0, 0);
            hamburgerMenu.MouseEnter += BlueBtn_MouseEnter;
            hamburgerMenu.MouseLeave += BlueBtn_MouseLeave;
            hamburgerMenu.Click += HamburgerMenu_Click;

            Grid.SetColumn(hamburgerMenu, 0);
            Grid.SetRow(hamburgerMenu, 0);

            this.Children.Add(hamburgerMenu);

        }

        /// <summary>
        /// Layout topBar definitions
        /// </summary>
        private void DrawBounds()
        {

            this.ColumnDefinitions.Add(new ColumnDefinition() { Width = new System.Windows.GridLength(0.06, System.Windows.GridUnitType.Star) });
            this.ColumnDefinitions.Add(new ColumnDefinition() { Width = new System.Windows.GridLength(0.69, System.Windows.GridUnitType.Star) });
            this.ColumnDefinitions.Add(new ColumnDefinition() { Width = new System.Windows.GridLength(0.25, System.Windows.GridUnitType.Star) });

            this.RowDefinitions.Add(new RowDefinition() { Height = new System.Windows.GridLength(1, System.Windows.GridUnitType.Star) });

        }
        /// <summary>
        /// Actualitzar el width i el height del element
        /// </summary>
        private void UpdateBounds()
        {

            this.Width = _ViewWidth;
            this.Height = _ViewHeight;

            if(this.Width > 600)
            {

                this.ColumnDefinitions[0].Width = new GridLength(0.02, GridUnitType.Star);
                this.ColumnDefinitions[1].Width = new GridLength(0.78, GridUnitType.Star);
                this.ColumnDefinitions[2].Width = new GridLength(0.15, GridUnitType.Star);

            }
            else
            {

                this.ColumnDefinitions[0].Width = new System.Windows.GridLength(0.06, System.Windows.GridUnitType.Star);
                this.ColumnDefinitions[1].Width = new System.Windows.GridLength(0.69, System.Windows.GridUnitType.Star);
                this.ColumnDefinitions[2].Width = new System.Windows.GridLength(0.25, System.Windows.GridUnitType.Star);

            }

        }

        #endregion

        #region PUBLIC FUNCTIONS

        /// <summary>
        /// Obtenir el tamany de la pantalla i actualitzar el tamany d'quest element
        /// </summary>
        public void InvalidateSize()
        {

            _ViewHeight = (this.Parent as Grid).RowDefinitions[0].Height.Value;
            _ViewWidth = (((this.Parent as Grid).Parent as DockPanel).Parent as Window).Width;
            UpdateBounds();

        }

        #endregion

        /// <summary>
        /// Modificar el icono de la aplicación
        /// </summary>
        public String Icon
        {

            get { return _pathIcon; }
            set {

                _pathIcon = value;
                _bImage = new BitmapImage(new Uri(_pathIcon,
                UriKind.RelativeOrAbsolute));

            }

        }

        #region EVENTS

        /// <summary>
        /// Moure el topBar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TopBar_MouseDown(object sender, MouseButtonEventArgs e)
        {

            var window = (((this.Parent as Grid).Parent) as DockPanel).Parent as Window;

            if (e.ChangedButton == MouseButton.Left)
                window.DragMove();

        }

        private void HamburgerMenu_Click(object sender, RoutedEventArgs e)
        {

            var button = sender as Button;
            BrushConverter bc = new BrushConverter();
            Brush brush;

            brush = (Brush)bc.ConvertFrom("#6eb9f7");
            brush.Freeze();

            button.Background = brush;

        }
        private void BlueBtn_MouseLeave(object sender, MouseEventArgs e)
        {

            var button = sender as Button;
            BrushConverter bc = new BrushConverter();
            Brush brush;

            brush = (Brush)bc.ConvertFrom("#2196F3");
            brush.Freeze();

            button.Background = brush;

        }
        private void BlueBtn_MouseEnter(object sender, MouseEventArgs e)
        {

            var button = sender as Button;
            BrushConverter bc = new BrushConverter();
            Brush brush;

            brush = (Brush)bc.ConvertFrom("#3da2f5");
            brush.Freeze();

            button.Background = brush;

        }
        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {

            var view = ((this.Parent as Grid).Parent as DockPanel).Parent as Window;

            view.WindowState = WindowState.Minimized;

        }
        private void ResizeButton_Click(object sender, RoutedEventArgs e)
        {

            var view = ((this.Parent as Grid).Parent as DockPanel).Parent as Window;

            if (view.WindowState == WindowState.Maximized) view.WindowState = WindowState.Normal;
            else view.WindowState = WindowState.Maximized;

        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {

            var view = ((this.Parent as Grid).Parent as DockPanel).Parent as Window;
            var button = sender as Button;
            BrushConverter bc = new BrushConverter();
            Brush brush;

            brush = (Brush)bc.ConvertFrom("#bb2725");
            brush.Freeze();

            button.Background = brush;

            view.Close();

        }
        private void CloseButton_MouseEnter(object sender, MouseEventArgs e)
        {

            var button = sender as Button;
            BrushConverter bc = new BrushConverter();
            Brush brush;

            // brush = (Brush)bc.ConvertFrom("#3da2f5"); BLUE
            brush = (Brush)bc.ConvertFrom("#ff6f6e");
            brush.Freeze();

            button.Background = brush;

        }
        private void CloseButton_MouseLeave(object sender, MouseEventArgs e)
        {

            var button = sender as Button;

            button.Background = Brushes.Red; ;

        }

        #endregion

    }
}
