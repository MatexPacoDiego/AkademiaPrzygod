using AkademiaPrzygod.Core.Static;
using System.Windows;

namespace AkademiaPrzygod.WPF
{
    /// <summary>
    /// Okno statystyk globalnych – wyświetla podsumowanie całej sesji gry.
    /// </summary>
    public partial class StatystykiGlobalne : Window
    {
        /// <summary>
        /// Inicjuje okno i wyświetla aktualne statystyki globalne sesji.
        /// </summary>
        public StatystykiGlobalne()
        {
            InitializeComponent();
            tbStatWalki.Text = Statystyki.LiczbaWalk.ToString();
            tbStatWrogowie.Text = Statystyki.LiczbaZabitegoWrogow.ToString();
            tbStatPrzedmioty.Text = Statystyki.ZebranePrzedmioty.ToString();
        }

        /// <summary>
        /// Wraca do menu głównego i zamyka okno statystyk.
        /// </summary>
        private void BtnWroc_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainOkno = new MainWindow();
            mainOkno.Show();
            this.Close();
        }
    }
}