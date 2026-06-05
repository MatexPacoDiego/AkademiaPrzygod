using AkademiaPrzygod.Core.Enums;
using AkademiaPrzygod.Core.Models;
using AkademiaPrzygod.Core.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace AkademiaPrzygod.WPF
{
    /// <summary>
    /// Logika interakcji dla klasy GraWindow.xaml
    /// </summary>
    public partial class GraWindow : Window
    {
        private List<Lokacja> _lokacje;
 
        public GraWindow()
        {
            InitializeComponent();
            ZainicjujLokacje();
            statsControl.Odswiez(Statystyki.Bohater);
        }

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
            foreach(Lokacja lokacja in _lokacje)
            {
                ListaLokacji.Items.Add($"{liczbaPorz++}. {lokacja.Nazwa} - {lokacja.Opis}" );
              
            }
        }


        private void BtnEksploruj_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnSklep_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnEkwipunek_Click(object sender, RoutedEventArgs e)
        {
            EkwipunekWindow oknoEkwipunek = new EkwipunekWindow();
            oknoEkwipunek.Show();
            this.Close();
        }

        private void BtnWroc_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainOkno = new MainWindow();
            mainOkno.Show();
            this.Close();
        }
    }
}
