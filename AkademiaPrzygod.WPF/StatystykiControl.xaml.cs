using AkademiaPrzygod.Core.Models;
using AkademiaPrzygod.Core.Static;
using System.Windows.Controls;

namespace AkademiaPrzygod.WPF
{
    /// <summary>
    /// Reużywalny panel statystyk bohatera – wyświetlany w każdym oknie gry.
    /// </summary>
    public partial class StatystykiControl : UserControl
    {
        /// <summary>
        /// Inicjuje kontrolkę statystyk.
        /// </summary>
        public StatystykiControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Odświeża wszystkie pola kontrolki na podstawie aktualnych danych bohatera.
        /// </summary>
        /// <param name="b">Bohater którego statystyki mają być wyświetlone.</param>
        public void Odswiez(Bohater b)
        {
            tbImie.Text = b.Imie;

            // specjalna obsługa polskiego znaku w nazwie klasy
            if (b.Klasa.ToString() == "Lotrzyk")
                tbKlasa.Text = "Łotrzyk";
            else
                tbKlasa.Text = b.Klasa.ToString();

            tbPoziom.Text = $"Poziom: {b.Poziom}";
            tbAtak.Text = $"Atak: {b.Atak}";
            tbObrona.Text = $"Obrona: {b.Obrona}";
            tbZloto.Text = $"Złoto: {b.Zloto}";
            tbHP.Text = $"{b.Zdrowie}/{b.MaxZdrowie}";
            tbXP.Text = $"{b.Doswiadczenie}/{Konfiguracja.XPDoAwansu}";

            pbHP.Maximum = b.MaxZdrowie;
            pbHP.Value = b.Zdrowie;

            pbXP.Maximum = Konfiguracja.XPDoAwansu;
            pbXP.Value = b.Doswiadczenie;

            tbEkwipunek.Text = $"Ekwipunek: {b.PobierzEkwipunek().Count}/{Konfiguracja.MaksymalnyEkwipunek}";
        }
    }
}