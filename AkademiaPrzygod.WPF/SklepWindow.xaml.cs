using AkademiaPrzygod.Core.Models;
using AkademiaPrzygod.Core.Static;
using System.Windows;

namespace AkademiaPrzygod.WPF
{
    /// <summary>
    /// Okno sklepu – umożliwia zakup przedmiotów za złoto bohatera.
    /// </summary>
    public partial class SklepWindow : Window
    {
        private Sklep _sklep;

        /// <summary>
        /// Inicjuje okno sklepu, tworzy sklep i wczytuje ofertę.
        /// </summary>
        public SklepWindow()
        {
            InitializeComponent();
            statsControl.Odswiez(Statystyki.Bohater);
            tbSklep.Text = "SKLEP AKADEMII";
            _sklep = new Sklep(tbSklep.Text.ToString());
            WczytajOferte();
        }

        /// <summary>
        /// Obsługuje zakup wybranego przedmiotu ze sklepu.
        /// </summary>
        private void BtnKup_Click(object sender, RoutedEventArgs e)
        {
            if (listaSklep.SelectedItem == null)
            {
                new KomunikatWindow("Najpierw wybierz przedmiot").ShowDialog();
                return;
            }

            string item = listaSklep.SelectedItem.ToString();
            if (item == "Pusto na półkach")
            {
                new KomunikatWindow("Nie ma produktów w sklepie").ShowDialog();
                return;
            }
            if (Statystyki.Bohater.PobierzEkwipunek().Count + 1 > 10)
            {
                new KomunikatWindow("Zakup nie udany. Ekwipunek jest pełny").ShowDialog();
                return;
            }

            if (_sklep.Kup(Statystyki.Bohater, listaSklep.SelectedIndex))
            {
                new KomunikatWindow("Zakup udany").ShowDialog();
            }
            else
            {
                new KomunikatWindow("Za mało złota").ShowDialog();
                return;
            }
            statsControl.Odswiez(Statystyki.Bohater);
        }

        /// <summary>
        /// Wraca do okna gry i zamyka sklep.
        /// </summary>
        private void BtnWroc_Click(object sender, RoutedEventArgs e)
        {
            GraWindow oknoGra = new GraWindow();
            oknoGra.Show();
            this.Close();
        }

        /// <summary>
        /// Wczytuje i wyświetla ofertę sklepu na liście.
        /// </summary>
        private void WczytajOferte()
        {
            if (_sklep.PokazOferte().Count > 0)
            {
                foreach (string towar in _sklep.PokazOferte())
                {
                    listaSklep.Items.Add(towar);
                }
            }
            else
            {
                listaSklep.Items.Add("Pusto na półkach");
            }
        }
    }
}