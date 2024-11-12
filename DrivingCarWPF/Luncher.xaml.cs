using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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
    /// Logique d'interaction pour Luncher.xaml
    /// </summary>
    public partial class Luncher : Window
    {
        private SoundPlayer soundPlayer;
        public Luncher()
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;

            ImageBrush fondCanvas = new ImageBrush();
            fondCanvas.ImageSource = new BitmapImage(new Uri("img\\fondVoitureLuncher.jpg", UriKind.Relative));
            canvasAccueil.Background = fondCanvas;

            ImageBrush imgParametre = new ImageBrush();
            imgParametre.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img\\parametres.png"));
            butParametre.Background = imgParametre;
            soundPlayer = new SoundPlayer("sons\\sonsPageAccueil.wav");
            soundPlayer.PlayLooping();

        }

        private void buttonJouer_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            
        }
        private void butParametre_Click(object sender, RoutedEventArgs e)
        {
            DialogCommandeJeux choixParametre = new DialogCommandeJeux();
            choixParametre.Show();
        }

        private void buttonAnnuler_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
