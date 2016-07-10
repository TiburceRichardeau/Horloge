using System.Windows;
using System.Windows.Input;

namespace Heure
{
    /// <summary>
    /// Logique d'interaction pour WindowInfo.xaml
    /// </summary>
    public partial class WindowInfo : Window
    {
        public WindowInfo()
        {
            string info = "Application developpée par Tiburce Richardeau\n\nIcon made by Freepik from flaticon.com is licensed under CC BY 3.0\n\nTheme MaterialDesignInXamlToolkit by ButchersBoy under Ms-PL License\nhttps://github.com/ButchersBoy/MaterialDesignInXamlToolkit";
            InitializeComponent();
            labelInfo.Content = info;
            labelInfo.Height = info.Length;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.MaxHeight = 250;
            this.MaxWidth = 500;
            this.MinHeight = 250;
            this.MinWidth = 500;
        }

        // Permet de déplacer la fenetre avec la souris
        // Utile étant donnée que l'application n'a plus de bord
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        // action du bouton Ok
        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Lien vers flaticon, utiliser dans le constructeur pour la fenetre info
        private void lienFlaticon_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.flaticon.com/");
        }

        //Lien vers gitHub Theme, utiliser dans le constructeur pour la fenetre info
        private void buttonlienTheme_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/ButchersBoy/MaterialDesignInXamlToolkit");
        }
    }
}
