using AkademiaPrzygod.Core.Enums;
using AkademiaPrzygod.Core.Models;
using AkademiaPrzygod.Core.Static;
using System.Collections.Generic;
using System.Windows;

namespace AkademiaPrzygod.WPF
{
    /// <summary>
    /// Okno głównej pętli gry – eksploracja, sklep, ekwipunek.
    /// </summary>
    public partial class GraWindow : Window
    {
        private List<Lokacja> _lokacje;

        /// <summary>
        /// Inicjuje okno gry, tworzy lokacje i odświeża statystyki bohatera.
        /// </summary>
        public GraWindow()
        {
            InitializeComponent();
            ZainicjujLokacje();
            statsControl.Odswiez(Statystyki.Bohater);
        }

        /// <summary>
        /// Tworzy i wypełnia listę lokacji dostępnych w grze.
        /// </summary>
        public void ZainicjujLokacje()
        {
            _lokacje = new List<Lokacja>();

            Lokacja biblioteka = new Lokacja("📚 Biblioteka Zakazana", "Ciemne korytarze pełne ksiąg");
            biblioteka.DodajWroga(new Wrog("👺 Stary Goblin", "Goblin", 30, 8, 3, 20, 10, TrudnoscWroga.Latwy));
            biblioteka.DodajWroga(new Wrog("🐀 Szczur Archiwalny", "Szczur", 20, 6, 2, 15, 5, TrudnoscWroga.Latwy));
            biblioteka.DodajPrzedmiot(new Przedmiot("🧪 Stara Mikstura", TypPrzedmiotu.Mikstura, 0, "Leczy 30 HP"));
            _lokacje.Add(biblioteka);

            Lokacja sala = new Lokacja("⚗️ Sala Prób", "Ognista arena egzaminów");
            sala.DodajWroga(new Wrog("🔥 Ognisty Elemental", "Elemental", 60, 18, 8, 45, 30, TrudnoscWroga.Sredni));
            sala.DodajWroga(new Wrog("🧌 Troll Egzaminacyjny", "Troll", 80, 22, 10, 55, 40, TrudnoscWroga.Sredni));
            _lokacje.Add(sala);

            Lokacja laboratorium = new Lokacja("🧫 Laboratorium", "Bulgoczące mikstury i dziwne zapachy");
            laboratorium.DodajWroga(new Wrog("🦍 Golem Alchemiczny", "Golem", 70, 20, 12, 50, 35, TrudnoscWroga.Sredni));
            laboratorium.DodajPrzedmiot(new Przedmiot("🍵 Mikstura Mocy", TypPrzedmiotu.Mikstura, 0, "Leczy 30 HP"));
            _lokacje.Add(laboratorium);

            Lokacja wieza = new Lokacja("🗼 Wieża Mistrza", "Siedziba najpotężniejszego maga");
            wieza.DodajWroga(new Wrog("👻 Duch Oblany Student", "Duch", 120, 30, 15, 100, 80, TrudnoscWroga.Trudny));
            _lokacje.Add(wieza);

            int liczbaPorz = 1;
            foreach (Lokacja lokacja in _lokacje)
            {
                ListaLokacji.Items.Add($"{liczbaPorz++}. {lokacja.Nazwa} - {lokacja.Opis}");
            }
        }

        /// <summary>
        /// Obsługuje eksplorację wybranej lokacji – losuje spotkanie z wrogiem lub przedmiotem.
        /// </summary>
        private void BtnEksploruj_Click(object sender, RoutedEventArgs e)
        {
            if (ListaLokacji.SelectedItem != null)
            {
                Lokacja lokacja = _lokacje[ListaLokacji.SelectedIndex];
                object spotkanie = lokacja.LosujSpotkanie();

                if (spotkanie is Wrog wrog)
                {
                    var okno = new WalkaWindow(wrog);
                    okno.Show();
                    this.Close();
                    statsControl.Odswiez(Statystyki.Bohater);
                }
                else if (spotkanie is Przedmiot przedmiot)
                {
                    if (Statystyki.Bohater.DodajPrzedmiot(przedmiot))
                    {
                        statsControl.Odswiez(Statystyki.Bohater);
                        Statystyki.ZebranePrzedmioty++;
                        new KomunikatWindow($"Znalazłeś: {przedmiot.Nazwa}!\nDodano do ekwipunku.").ShowDialog();
                    }
                    else
                        new KomunikatWindow("Ekwipunek pełny").ShowDialog();
                }
                else
                {
                    new KomunikatWindow("Nic tu nie ma...").ShowDialog();
                }
            }
            else
            {
                new KomunikatWindow("Wybierz najpierw lokację").ShowDialog();
            }
        }

        /// <summary>
        /// Otwiera okno sklepu i zamyka okno gry.
        /// </summary>
        private void BtnSklep_Click(object sender, RoutedEventArgs e)
        {
            SklepWindow sklepOkno = new SklepWindow();
            sklepOkno.Show();
            this.Close();
        }

        /// <summary>
        /// Otwiera okno ekwipunku i zamyka okno gry.
        /// </summary>
        private void BtnEkwipunek_Click(object sender, RoutedEventArgs e)
        {
            EkwipunekWindow oknoEkwipunek = new EkwipunekWindow("Main");
            oknoEkwipunek.Show();
            this.Close();
        }

        /// <summary>
        /// Wraca do menu głównego i zamyka okno gry.
        /// </summary>
        private void BtnWroc_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainOkno = new MainWindow();
            mainOkno.Show();
            this.Close();
        }
    }
}