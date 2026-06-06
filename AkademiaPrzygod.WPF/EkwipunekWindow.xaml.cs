using AkademiaPrzygod.Core.Models;
using AkademiaPrzygod.Core.Static;
using System.Windows;
using System.Windows.Controls;

namespace AkademiaPrzygod.WPF
{
    /// <summary>
    /// Okno ekwipunku – umożliwia przeglądanie i używanie przedmiotów bohatera.
    /// </summary>
    public partial class EkwipunekWindow : Window
    {
        /// <summary>
        /// Inicjuje okno ekwipunku i ustawia widoczność przycisków
        /// zależnie od kontekstu (walka lub menu gry).
        /// </summary>
        /// <param name="okno">Kontekst otwarcia: "Main" lub "Walka".</param>
        public EkwipunekWindow(string okno)
        {
            InitializeComponent();
            statsControl.Odswiez(Statystyki.Bohater);
            PobierzEkwipunek(Statystyki.Bohater);
            if (okno == "Main")
            {
                btnWroc.Visibility = Visibility.Visible;
                btnWrocWalka.Visibility = Visibility.Collapsed;
            }
            else
            {
                btnWroc.Visibility = Visibility.Collapsed;
                btnWrocWalka.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Obsługuje użycie wybranego przedmiotu z listy ekwipunku.
        /// </summary>
        private void BtnUzyj_Click(object sender, RoutedEventArgs e)
        {
            if (listaEkwipunek.SelectedItem == null)
            {
                new KomunikatWindow("Najpierw wybierz przedmiot").ShowDialog();
                return;
            }

            string item = listaEkwipunek.SelectedItem.ToString();
            if (item == "Brak przedmiotów w ekwipunku")
            {
                new KomunikatWindow("Nie masz przedmiotów w ekwipunku").ShowDialog();
                return;
            }

            if (Statystyki.Bohater.PobierzEkwipunek().Count == 0)
                return;

            Statystyki.Bohater.UzyjPrzedmiot(listaEkwipunek.SelectedIndex);
            PobierzEkwipunek(Statystyki.Bohater);
            statsControl.Odswiez(Statystyki.Bohater);
        }

        /// <summary>
        /// Wczytuje i wyświetla ekwipunek bohatera na liście.
        /// </summary>
        /// <param name="bohater">Bohater którego ekwipunek jest wyświetlany.</param>
        private void PobierzEkwipunek(Bohater bohater)
        {
            listaEkwipunek.Items.Clear();
            var ekwipunek = bohater.PobierzEkwipunek();

            if (ekwipunek.Count == 0)
            {
                listaEkwipunek.Items.Add("Brak przedmiotów w ekwipunku");
                return;
            }

            int liczbaPorzad = 1;
            foreach (var item in ekwipunek)
            {
                listaEkwipunek.Items.Add($"{liczbaPorzad++}. {item}");
            }
        }

        /// <summary>
        /// Wraca do okna gry z menu głównego.
        /// </summary>
        private void BtnWroc_Click(object sender, RoutedEventArgs e)
        {
            GraWindow oknoGra = new GraWindow();
            oknoGra.Show();
            this.Close();
        }

        /// <summary>
        /// Zamyka okno ekwipunku i wraca do walki.
        /// </summary>
        private void BtnWrocWalka_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}