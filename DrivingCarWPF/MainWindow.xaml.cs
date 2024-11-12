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
using System.Windows.Media.Animation;
using System.Windows.Threading;
using System.Media;
using System.Numerics;


namespace DrivingCarWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool gauche, droit, reculer, avancer, pause = false;      
        private int vitesseJoueur = 10;                                     //Plus le chiffre est petit plus la vitesse de déplacement est faible
        private DispatcherTimer minuterie = new DispatcherTimer();
        private int vitesse = 3;                                            //Plus le chiffre est petit plus la vitesse sera élever
        private bool rotation = false;
        private SoundPlayer sonVoiture;
        private SoundPlayer sonPneu;
        private int imageDecor = 0;
        private bool premiereIteration = true;
        private Storyboard storyboard = new Storyboard();
        private int imageVoiture = 0;
        private int score = 0;
        private int meilleurScore = 0;


        public MainWindow()
        {
#if DEBUG
            Console.WriteLine("Debug");
#endif
            InitializeComponent();
            WindowStyle = WindowStyle.None;

            Luncher dialogLuncher = new Luncher();
            dialogLuncher.ShowDialog();

            pageChoixDecor dialogPageChoix = new pageChoixDecor();
            dialogPageChoix.ShowDialog();

            this.imageDecor = dialogPageChoix.choixImageDecor;
            
            if (this.imageDecor == 1)
            {
                ImageBrush imgRoute1 = new ImageBrush();
                imgRoute1.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img\\road_0.png"));
                route1.Fill = imgRoute1;
                route2.Fill = imgRoute1;
            }
            else
            {
                ImageBrush imgRoute1 = new ImageBrush();
                imgRoute1.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img\\road_0neige.png"));
                route1.Fill = imgRoute1;
                route2.Fill = imgRoute1;
            }

            dialogChoixVoiture pageChoixVoiture = new dialogChoixVoiture();
            pageChoixVoiture.ShowDialog();

            this.imageVoiture = pageChoixVoiture.choixImageVoiture;

            ImageBrush imgVoiture = new ImageBrush();
            string imagePath = AppDomain.CurrentDomain.BaseDirectory + "img\\voiture";

            switch (this.imageVoiture)
            {
                case 1:
                    imgVoiture.ImageSource = new BitmapImage(new Uri(imagePath + "1.png"));
                    break;
                case 2:
                    imgVoiture.ImageSource = new BitmapImage(new Uri(imagePath + "2.png"));
                    break;
                case 3:
                    imgVoiture.ImageSource = new BitmapImage(new Uri(imagePath + "3.png"));
                    break;
                case 4:
                    imgVoiture.ImageSource = new BitmapImage(new Uri(imagePath + "4.png"));
                    break;
                
                default:
                    
                    break;
            }

            voiture.Fill = imgVoiture;



            ImageBrush imghuile = new ImageBrush();
            imghuile.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img\\huile1.png"));
            huileMoteur.Fill = imghuile;
            

            minuterie.Tick += GameEngine;
            minuterie.Interval = TimeSpan.FromMilliseconds(16);             // rafraissement toutes les 16 milliseconds
            minuterie.Start();                                              // lancement du timer

            sonVoiture = new SoundPlayer("sons\\sonVoitureAccélération.wav");
            sonVoiture.PlayLooping();

            sonPneu = new SoundPlayer("sons\\Crissement-de-pneus.wav");
        }

        
        private void deplacementRouteInfinie()
        {
            TimeSpan duree = TimeSpan.FromSeconds(vitesse);                 // Durée de l'animation

            
            DoubleAnimation animation1 = new DoubleAnimation                // Création d'une animation pour route1
            {
                From = -1,
                To = 684,                                                   
                Duration = duree,
                RepeatBehavior = RepeatBehavior.Forever
            };

                                                                            // Création d'une animation pour route2
            DoubleAnimation animation2 = new DoubleAnimation
            {
                From = -684,                                                
                To = 1,
                Duration = duree,
                RepeatBehavior = RepeatBehavior.Forever
            };

                                                                            // Appliquer les animations à Canvas.Top des rectangles
            Storyboard.SetTarget(animation1, route1);
            Storyboard.SetTargetProperty(animation1, new PropertyPath(Canvas.TopProperty));

            Storyboard.SetTarget(animation2, route2);
            Storyboard.SetTargetProperty(animation2, new PropertyPath(Canvas.TopProperty));   
            
            storyboard.Children.Add(animation1);                            // Créer et lancer le storyboard
            storyboard.Children.Add(animation2);
            storyboard.Begin();
        }


        private void voiture_KeyDown(object sender, KeyEventArgs e)
        {
                                                         // On gère les booléens gauche,droite, avancer et reculer en fonction de l’appui de la touche
            if (e.Key == Key.Left)
            {
                gauche = true;
                sonPneu.Play();
                
            }
            if (e.Key == Key.Right)
            {
                droit = true;
                sonPneu.Play();
                
            }
            if (e.Key == Key.Down)
            {
                reculer = true;
            }
            if (e.Key == Key.Up)
            {
                avancer = true;
            }
            if (e.Key == Key.P)
            {
                if (minuterie.IsEnabled)
                {
                    pause = true;
                    minuterie.Stop();
                    storyboard.Pause();
                    canvasPause.Visibility = Visibility.Visible;
                    sonVoiture.Stop();
                }
                else
                {
                    pause = false;
                    minuterie.Start();
                    storyboard.Resume();
                    canvasPause.Visibility = Visibility.Collapsed;
                    sonVoiture.Play();
                }

            }

        }
#region touche relâcher
        private void voiture_KeyUp(object sender, KeyEventArgs e)
        {
                                                 // On gère les booléens gauche, droite, avance, reculer en fonction du relâchement de la touche
            if (e.Key == Key.Left)
            {
                gauche = false;
                sonPneu.Stop();
                sonVoiture.Play();
            }
            if (e.Key == Key.Right)
            {
                droit = false;
                sonPneu.Stop();
                sonVoiture.Play();
            }
            if (e.Key == Key.Down)
            {
                reculer = false;
            }
            if (e.Key == Key.Up)
            {
                avancer = false;
            }
            if (e.Key == Key.P)
            {
                pause = false;
            }
        }
#endregion

        private void butRejouer_Click(object sender, RoutedEventArgs e)
        {
            // Reset game state
            Canvas.SetLeft(voiture, 205); 
            Canvas.SetTop(voiture, 491);  
            score = 0;                                   // Réinitialise le score
            scoreTextBlock.Text = "Score: " + score;     // Mise à jour du score

            
            storyboard.Resume();                         // Arrêt le storyboard 
            canvasPerdu.Visibility = Visibility.Collapsed;  // Affiche le canvaPause
            minuterie.Start();                              // Démarre la minuterie

            this.KeyDown += voiture_KeyDown;            
            this.KeyUp += voiture_KeyUp;

            
            if (score > meilleurScore)
            {               
                meilleurScore = score;
                meilleurScoreTextBlock.Text = "Meilleur Score: " + meilleurScore;
            }
        }

        private void butQuitter_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();                 // Eteint l'application
        }


        private void GameEngine(object sender, EventArgs e)
        {
            
            // création d’un rectangle joueur et obstacle pour la détection de collision
            Rect obstacle = new Rect(Canvas.GetLeft(huileMoteur), Canvas.GetTop(huileMoteur), huileMoteur.Width, huileMoteur.Height);

            Rect joueur = new Rect(Canvas.GetLeft(voiture), Canvas.GetTop(voiture), voiture.Width, voiture.Height);
            if (premiereIteration && !pause)
            {
                deplacementRouteInfinie();
                premiereIteration = false;

            }

            if (joueur.IntersectsWith(obstacle))
            {
                minuterie.Stop();
                storyboard.Pause();
                canvasPerdu.Visibility = Visibility.Visible;
                sonVoiture.Stop();
                sonPneu.Stop();
            }

            Canvas.SetTop(huileMoteur, Canvas.GetTop(huileMoteur) + vitesse+2.5);

                                                    // Vérifie si l'huile est en dehors du canvas
            if (Canvas.GetTop(huileMoteur) > monCanvas.ActualHeight)
            {
                                                    // Génère une nouvelle position
                Random rand = new Random();
                double randomHorizontalPosition = rand.Next(0, (int)(monCanvas.ActualWidth - huileMoteur.Width + 1));
                Canvas.SetLeft(huileMoteur, randomHorizontalPosition);
                Canvas.SetTop(huileMoteur, -huileMoteur.Height);
            }



            // déplacement à gauche et droite de vitesseJoueur avec vérification des limites de fenêtre gauche et droite
            if (gauche && Canvas.GetLeft(voiture) > 0)
            {
                commenceRotationAnimation(-15);
                Canvas.SetLeft(voiture, Canvas.GetLeft(voiture) - vitesseJoueur);
                
            }
            else if (droit && Canvas.GetLeft(voiture) + voiture.Width < Application.Current.MainWindow.Width)
            {
                Console.WriteLine("je suis dans le if droite");

                commenceRotationAnimation(15);                                     // Commencer l'animation de rotation vers la droite
                Canvas.SetLeft(voiture, Canvas.GetLeft(voiture) + vitesseJoueur);
                
            }
            else
            {
                if (rotation)                                   // Arrêter l'animation de rotation si la touche n'est pas maintenue
                {
                    Console.WriteLine("StopRotation");
                    ArrêtAnimationRatation();
                }
                                                    
            }

            if (avancer && Canvas.GetTop(voiture) > 0)
            {
                Canvas.SetTop(voiture, Canvas.GetTop(voiture) - vitesseJoueur);
            }
            else if (reculer && Canvas.GetTop(voiture) + voiture.Height < Application.Current.MainWindow.Height)
            {
                Canvas.SetTop(voiture, Canvas.GetTop(voiture) + vitesseJoueur);
            }

            score += 1;             // Augmente le score tout les 16 millisecondes


            scoreTextBlock.Text = "Score: " + score;

            if (score > meilleurScore)
            {
                meilleurScore = score;
                meilleurScoreTextBlock.Text = "Meilleur Score: " + meilleurScore;
            }

        }

        
        private void commenceRotationAnimation(double angle)
        {
            if (!rotation)
            {
                                                                            // Créer une rotation pour la voiture
                RotateTransform tourne = new RotateTransform();
                tourne.CenterX = voiture.Width / 2;
                tourne.CenterY = voiture.Height / 2;

                DoubleAnimation ratationAnimation = new DoubleAnimation
                {
                    From = tourne.Angle,
                    To = angle,
                    Duration = TimeSpan.FromSeconds(0.2),                   // Durée de chaque rotation
                    RepeatBehavior = RepeatBehavior.Forever                 // Répéter l'animation indéfiniment
                };

                                                                            
                voiture.RenderTransform = tourne;                           // Appliquer l'animation à la voiture
                tourne.BeginAnimation(RotateTransform.AngleProperty, ratationAnimation);
                rotation = true;
            }
        }

        private void ArrêtAnimationRatation()
        {
            if (rotation)
            {                                                               // Arrêter l'animation de rotation
                voiture.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, null);
                rotation = false;
            }
        }

    }
}