using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;


namespace Heure
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Les paramètres sauvegardés
        bool sec = Properties.Settings.Default.sec;
        bool date = Properties.Settings.Default.date;
        double SaveWidth = Properties.Settings.Default.Width;
        double SaveHeight = Properties.Settings.Default.Height;
        double positionTop = Properties.Settings.Default.PositionTop;
        double positionLeft = Properties.Settings.Default.PositionLeft;
        DispatcherTimer dispatcherTimer;

        /// <summary>
        /// Constructeur par défaut de la fenetre principale 
        /// Fenetre qui permet d'afficher l'heure
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            RestoreSettings();
            RectAgrandir.Width = this.Width;
            AfficherHeure();
        }

        private void AfficherHeure()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            //dispatcherTimer.Interval = new TimeSpan(0, 0, 1); // Met a jour teoute les secondes 
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 500); // Met a jour teoutes les 0.5 secondes
            dispatcherTimer.Start();
        }

        private void RestoreSettings()
        {
            this.Width = SaveWidth;
            this.Height = SaveHeight;
            this.Top = positionTop;
            this.Left = positionLeft;
            //this.MinHeight = 154;
            //this.MinWidth = 517;
            Log l = new Log(Log.Type.Infos, "Demarrage de la fenetre");
            l = new Log(Log.Type.Infos, "Secondes affichées : " + sec);
            l = new Log(Log.Type.Infos, "date affichée : " + date);
            l = new Log(Log.Type.Infos, "Width : " + this.Width);
            l = new Log(Log.Type.Infos, "Height : " + this.Height);
            l = new Log(Log.Type.Infos, "Top : " + this.Top);
            l = new Log(Log.Type.Infos, "Left : " + this.Left);
        }

        /// <summary>
        /// Permet de récuperer le temps actuel et le mettre dans le label labelTime
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
                string Heure = DateTime.Now.Hour.ToString();
                string Minute = DateTime.Now.Minute.ToString();
                string Seconde = DateTime.Now.Second.ToString();

                if (Heure.Length == 1)
                {
                    Heure = "0" + Heure[0];
                }

                if (Minute.Length == 1)
                {
                    Minute = "0" + Minute[0];
                }

                if (Seconde.Length == 1)
                {
                    Seconde = "0" + Seconde[0];
                }
                string Time=string.Empty;
                if (sec)
                {
                    Time = Heure + " : " + Minute + " : " + Seconde;
                }else if (!sec)
                {
                    Time = Heure + " : " + Minute;
                }

                if (date)
                {
                    int day = DateTime.Now.Day;
                    int month = DateTime.Now.Month;
                    string dayS = DateTime.Now.Day.ToString();
                    string monthS= DateTime.Now.Month.ToString();

                if (day < 10)
                    {
                        dayS = "0" + day.ToString();
                    }

                    if (month < 10)
                    {
                        monthS = "0" + month.ToString();
                    }

                    Time = Time + "\n" + dayS + "/" + monthS + "/" + DateTime.Now.Year;
                }
                
                TextBlockTime.Text = Time;
        }

        /// <summary>
        ///  Permet de déplacer la fenetre avec la souris
        /// Utile étant donnée que l'application n'a plus de bord
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        /// <summary>
        /// Action du clic sur bouton de fermeture
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Sauvegarde l'emplacement de la fentre a la fermeture de l'application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.date = date;
            Properties.Settings.Default.sec = sec;
            Properties.Settings.Default.Width = SaveWidth;
            Properties.Settings.Default.Height = SaveHeight;
            Properties.Settings.Default.PositionTop = positionTop;
            Properties.Settings.Default.PositionLeft = positionLeft;
            Properties.Settings.Default.Save();
            Log l = new Log(Log.Type.Infos, "Fermeture de le fenetre : ");
            l = new Log(Log.Type.Infos, "Secondes affichées : " + sec);
            l = new Log(Log.Type.Infos, "date affichée : " + date);
            l = new Log(Log.Type.Infos, "Width : " + this.Width);
            l = new Log(Log.Type.Infos, "Height : " + this.Height);
            l = new Log(Log.Type.Infos, "Top : " + this.Top);
            l = new Log(Log.Type.Infos, "Left : " + this.Left);
            dispatcherTimer.Stop();
        }

        /// <summary>
        /// Méthode qui gère la réduction de la fenetre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonReduire_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
            Log l = new Log(Log.Type.Infos, "Fenetre minimisée");
        }

        /// <summary>
        /// Methode qui permet d'agrandir ou de diminuer la taille de la fenetre avec le bouton buttonAgrandir
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAgrandir_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState != WindowState.Maximized) // Si la fentre n'est pas agrandie
            {
                // On change la valeur de la taille max de l'appli, ces grandes valeurs permettre de prendre en charge de grande résolutions comme le 4k (3840*2160)
                this.WindowState = WindowState.Maximized; // Puis on agrandit la fentre
                Log l = new Log(Log.Type.Infos, "Fenetre agrandie");
            }
            else // Sinon la fentre doit être diminuée 
            {
                //this.MinHeight = 154;
                //this.MinWidth = 517;
                this.WindowState = WindowState.Normal; // On la remet a sa taille normale
                Log l = new Log(Log.Type.Infos, "Fenetre diminuée");
            }
        }

        /// <summary>
        /// Permet d'ouvrir la fenetre d'infos au clic sur le bouton
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonInfo_Click(object sender, RoutedEventArgs e)
        {
            // Permet d'afficher une fenetre avec les infos sur l'applications
            WindowInfo win = new WindowInfo();
            win.ShowDialog();
            Log l = new Log(Log.Type.Infos, "Affichage de la fenetre des infos");
        }

        /// <summary>
        /// Permet de gerer l'affichage ou non des secondes dans l'affichage de l'heure
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSec_Click(object sender, RoutedEventArgs e)
        {
            sec = !sec;
            dispatcherTimer_Tick(sender, e);
            Log l = new Log(Log.Type.Infos, "affichage des secondes : " + sec);
        }

        /// <summary>
        /// Permet de gerer l'affichage de la fenetre quand elle change de taille
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.Width < 360)
            {
                buttonSec.Visibility = Visibility.Hidden;
                buttonInfo.Visibility = Visibility.Hidden;
                buttonAgrandir.Visibility = Visibility.Hidden;
                buttonReduire.Visibility = Visibility.Hidden;
                buttonDate.Visibility = Visibility.Hidden;
            }
            else
            {
                buttonSec.Visibility = Visibility.Visible;
                buttonInfo.Visibility = Visibility.Visible;
                buttonAgrandir.Visibility = Visibility.Visible;
                buttonReduire.Visibility = Visibility.Visible;
                buttonDate.Visibility = Visibility.Visible;
            }

            SaveHeight = this.Height;
            SaveWidth = this.Width;
            Log l = new Log(Log.Type.Infos, "Nouvelle taille de la fenetre :");
            l = new Log(Log.Type.Infos, "Height : " + this.Height);
            l = new Log(Log.Type.Infos, "Width : " + this.Width);
            RectAgrandir.Width = this.Width;
        }

        /// <summary>
        /// Permet de gerer le bouton qui permet d'afficher ou de masquer la date
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDate_Click(object sender, RoutedEventArgs e)
        {
            date = !date;
            Log l = new Log(Log.Type.Infos, "Affichage de la date : " + date);
            dispatcherTimer_Tick(sender, e);
        }

        /// <summary>
        /// Permet de sauvegarder l'emplacement de la fenetre quand elle est déplacée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_LocationChanged(object sender, EventArgs e)
        {
            positionTop = this.Top;
            positionLeft = this.Left;
            Log l = new Log(Log.Type.Infos, "Déplacement de la fenetre :");
            l = new Log(Log.Type.Infos, "Top : " + this.Top);
            l = new Log(Log.Type.Infos, "Left : " + this.Left);
        }

        /// <summary>
        /// Permet de gerer la taille de la fenetre quand on double clic sur la partie haute
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageAgrandir_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (this.WindowState != WindowState.Maximized) // Si la fentre n'est pas agrandie
                {
                    // On change la valeur de la taille max de l'appli, ces grandes valeurs permettre de prendre en charge de grande résolutions comme le 4k (3840*2160)
                    this.WindowState = WindowState.Maximized; // Puis on agrandit la fentre
                    Log l = new Log(Log.Type.Infos, "Fenetre agrandie");
                }
                else // Sinon la fentre doit être diminuée 
                {
                    //this.MinHeight = 154;
                    //this.MinWidth = 517;
                    this.WindowState = WindowState.Normal; // On la remet a sa taille normale
                    Log l = new Log(Log.Type.Infos, "Fenetre diminuée");
                }
            }
        }
    }
}
