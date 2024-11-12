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
    /// Logique d'interaction pour pageChoixDecor.xaml
    /// </summary>
    public partial class pageChoixDecor : Window
    {
        public int choixImageDecor = 0;
        public pageChoixDecor()
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;

            ImageBrush imageDecor1 = new ImageBrush();
            imageDecor1.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img\\road_0.png"));
            butDecor1.Background = imageDecor1;


            ImageBrush imageDecor2 = new ImageBrush();
            imageDecor2.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img\\road_0neige.png"));
            butDecor2.Background = imageDecor2;

            ImageBrush fondCanvas = new ImageBrush();
            fondCanvas.ImageSource = new BitmapImage(new Uri("img\\fondVoitureLuncher.jpg", UriKind.Relative));
            canvasChoixDecor.Background = fondCanvas;
        }

        
        private void butDecor1_Click(object sender, RoutedEventArgs e)
        {
            this.choixImageDecor = 1;
            this.DialogResult = true;
        }

        private void butDecor2_Click(object sender, RoutedEventArgs e)
        {
            this.choixImageDecor = 2;
            this.DialogResult = true;
        }

        private void butRetour_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
