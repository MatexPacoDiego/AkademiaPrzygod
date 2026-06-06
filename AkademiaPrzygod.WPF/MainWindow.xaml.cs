using AkademiaPrzygod.Core.Static;
using System.Windows;

namespace AkademiaPrzygod.WPF
{
    /// <summary>
    /// Ekran powitalny – menu główne aplikacji.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Inicjuje okno główne, ustawia wersję gry i sprawdza czy istnieje bohater.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            tbWersja.Text = "v" + Konfiguracja.WersjaGry;
            CzyJestBohater();
        }

        /// <summary>
        /// Otwiera okno statystyk globalnych.
        /// </summary>
        private void BtnStatystyki_Click(object sender, RoutedEventArgs e)
        {
            StatystykiGlobalne oknoStatystyki = new StatystykiGlobalne();
            oknoStatystyki.Show();
            this.Close();
        }

        /// <summary>
        /// Sprawdza czy bohater już istnieje i odpowiednio pokazuje przyciski.
        /// </summary>
        private void CzyJestBohater()
        {
            if (Statystyki.Bohater != null)
            {
                btnNowaGra.Visibility = Visibility.Collapsed;
                btnWczytaj.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Otwiera okno tworzenia postaci.
        /// </summary>
        private void BtnNowaGra_Click(object sender, RoutedEventArgs e)
        {
            TworzeniePostaci oknoPostac = new TworzeniePostaci();
            oknoPostac.Show();
            this.Close();
        }

        /// <summary>
        /// Zamyka aplikację.
        /// </summary>
        private void BtnWyjscie_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Wczytuje istniejącą grę i otwiera okno gry.
        /// </summary>
        private void BtnWczytajGre(object sender, RoutedEventArgs e)
        {
            GraWindow oknoGry = new GraWindow();
            oknoGry.Show();
            this.Close();
        }
    }
}