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
using System.Windows.Shapes;

namespace DrivingCarWPF
{
    /// <summary>
    /// Logique d'interaction pour dialogChoixVoiture.xaml
    /// </summary>
    public partial class dialogChoixVoiture : Window
    {
        public int choixImageVoiture = 0;
        public dialogChoixVoiture()
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;

            ImageBrush fondCanvas = new ImageBrush();
            fondCanvas.ImageSource = new BitmapImage(new Uri("img\\fondVoitureLuncher.jpg", UriKind.Relative));
            canvasChoixVoiture.Background = fondCanvas;

            ImageBrush imageDecor1 = new ImageBrush();
            imageDecor1.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img\\voiture1.png"));
            butVoiture1.Background = imageDecor1;

            ImageBrush imageDecor2 = new ImageBrush();
            imageDecor2.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img\\voiture2.png"));
            butVoiture2.Background = imageDecor2;

            ImageBrush imageDecor3 = new ImageBrush();
            imageDecor3.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img\\voiture3p.png"));
            butVoiture3.Background = imageDecor3;

            ImageBrush imageDecor4 = new ImageBrush();
            imageDecor4.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img\\voiture4.png"));
            butVoiture4.Background = imageDecor4;

        }

        private void butQuitter_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void butVoiture1_Click(object sender, RoutedEventArgs e)
        {
            this.choixImageVoiture = 1;
            this.DialogResult = true;
        }

        private void butVoiture2_Click(object sender, RoutedEventArgs e)
        {
            this.choixImageVoiture = 2;
            this.DialogResult = true;
        }

        private void butVoiture3_Click(object sender, RoutedEventArgs e)
        {
            this.choixImageVoiture = 3;
            this.DialogResult = true;
        }

        private void butVoiture4_Click(object sender, RoutedEventArgs e)
        {
            this.choixImageVoiture = 4;
            this.DialogResult = true;
        }

        

    }
}
