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
    /// Logika interakcji dla klasy EkwipunekWindow.xaml
    /// </summary>
    public partial class EkwipunekWindow : Window
    {
        public EkwipunekWindow()
        {
            InitializeComponent();
            statsControl.Odswiez(Statystyki.Bohater);
            PobierzEkwipunek(Statystyki.Bohater);
        }

        private void BtnWroc_Click(object sender, RoutedEventArgs e)
        { 
            GraWindow oknoGry=new GraWindow();
            this.Close();
            oknoGry.Show();
            
           
        }

        private void BtnUzyj_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PobierzEkwipunek(Bohater bohater)
        {
            var ekwipunek = bohater.PobierzEkwipunek();
            int liczbaPorzad = 1;
            if (ekwipunek.Count > 0)
            {
                listaEkwipunek.Items.Clear();
                foreach(var item in ekwipunek)
                {
                    listaEkwipunek.Items.Add($"{liczbaPorzad++}. {item.Nazwa} - {item.Opis} ({item.Wartosc} złota)");
                }
            }

        }
    }
}
