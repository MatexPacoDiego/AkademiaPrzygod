using AkademiaPrzygod.Core.Enums;
using AkademiaPrzygod.Core.Models;
using AkademiaPrzygod.Core.Static;
using System.Windows;

namespace AkademiaPrzygod.WPF
{
    /// <summary>
    /// Okno tworzenia postaci – gracz podaje imię i wybiera klasę bohatera.
    /// </summary>
    public partial class TworzeniePostaci : Window
    {
        /// <summary>
        /// Inicjuje okno tworzenia postaci.
        /// </summary>
        public TworzeniePostaci()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Tworzy bohatera na podstawie wpisanego imienia i wybranej klasy,
        /// następnie otwiera okno gry.
        /// </summary>
        private void BtnStworz_Click(object sender, RoutedEventArgs e)
        {
            string imie = txtbImie.Text.Trim();
            if (string.IsNullOrEmpty(imie))
            {
                new KomunikatWindow("Wprowadź imię").ShowDialog();
                return;
            }

            KlasaPostaci klasa;
            if (rbMag.IsChecked == true)
            {
                klasa = KlasaPostaci.Mag;
                imie = $"🧙 {imie}";
            }
            else if (rbWojownik.IsChecked == true)
            {
                klasa = KlasaPostaci.Wojownik;
                imie = $"⚔️ {imie}";
            }
            else
            {
                klasa = KlasaPostaci.Lotrzyk;
                imie = $"🗡️ {imie}";
            }

            Statystyki.Bohater = new Bohater(imie, klasa);

            GraWindow oknoGry = new GraWindow();
            oknoGry.Show();
            this.Close();
        }

        /// <summary>
        /// Wraca do menu głównego.
        /// </summary>
        private void BtnWroc_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainOkno = new MainWindow();
            mainOkno.Show();
            this.Close();
        }
    }
}