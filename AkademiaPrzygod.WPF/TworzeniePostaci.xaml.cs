using AkademiaPrzygod.Core.Enums;
using AkademiaPrzygod.Core.Models;
using AkademiaPrzygod.Core.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AkademiaPrzygod.WPF
{
    /// <summary>
    /// Logika interakcji dla klasy TworzeniePostaci.xaml
    /// </summary>
    public partial class TworzeniePostaci : Window
    {
        public TworzeniePostaci()
        {
            InitializeComponent();
        }

        private void BtnStworz_Click(object sender, RoutedEventArgs e)
        {
            string imie = txtbImie.Text.Trim();
            if (string.IsNullOrEmpty(imie))
            {
                MessageBox.Show("Wprowadź imię");
                return;
            }

            KlasaPostaci klasa;
            if (rbMag.IsChecked == true)
            {
                klasa = KlasaPostaci.Mag;
            }else if(rbWojownik.IsChecked == true)
            {
                klasa = KlasaPostaci.Wojownik;
            }
            else
            {
                klasa=KlasaPostaci.Lotrzyk;
            }

            Statystyki.Bohater = new Bohater(imie, klasa);

            GraWindow oknoGry = new GraWindow();
            oknoGry.Show();
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
