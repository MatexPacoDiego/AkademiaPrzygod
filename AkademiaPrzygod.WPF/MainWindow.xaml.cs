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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AkademiaPrzygod.WPF
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            tbWersja.Text = "v" + Konfiguracja.WersjaGry;
            CzyJestBohater();
        }

        private void BtnStatystyki_Click(object sender, RoutedEventArgs e)
        {
            StatystykiGlobalne oknoStatystyki=new StatystykiGlobalne();
            oknoStatystyki.Show();
            this.Close();
        }

        private void CzyJestBohater()
        {
            if (Statystyki.Bohater != null)
            {
                btnNowaGra.Visibility=Visibility.Collapsed;
                btnWczytaj.Visibility=Visibility.Visible;
            }
        }

        private void BtnNowaGra_Click(object sender, RoutedEventArgs e)
        {
            TworzeniePostaci oknoPostac = new TworzeniePostaci();
            oknoPostac.Show();
            this.Close();
        }

        private void BtnWyjscie_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnWczytajGre(object sender, RoutedEventArgs e)
        {
            GraWindow oknoGry=new GraWindow();
            this.Close();
            oknoGry.Show();
        }
    }
}
