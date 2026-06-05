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
    /// Logika interakcji dla klasy StatystykiControl.xaml
    /// </summary>
    public partial class StatystykiControl : UserControl
    {
        public StatystykiControl()
        {
            InitializeComponent();
        }

        public void Odswiez(Bohater b)
        {
            tbImie.Text = b.Imie;
            if (b.Klasa.ToString() == "Lotrzyk")
            {
                tbKlasa.Text = "Łotrzyk";
            }
            else
            {
                tbKlasa.Text = b.Klasa.ToString();
            }
           
            tbPoziom.Text = $"Poziom: {b.Poziom}";
            tbAtak.Text = $"Atak: {b.Atak}";
            tbObrona.Text = $"Obrona: {b.Obrona}";
            tbZloto.Text = $"Zloto: {b.Zloto}";
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
