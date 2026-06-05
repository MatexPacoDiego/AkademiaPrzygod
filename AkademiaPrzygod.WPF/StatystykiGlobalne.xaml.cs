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
    /// Logika interakcji dla klasy StatystykiGlobalne.xaml
    /// </summary>
    public partial class StatystykiGlobalne : Window
    {
        public StatystykiGlobalne()
        {
            InitializeComponent();
            tbStatWalki.Text = Statystyki.LiczbaWalk.ToString();
            tbStatWrogowie.Text = Statystyki.LiczbaZabitegoWrogow.ToString();
            tbStatPrzedmioty.Text = Statystyki.ZebranePrzedmioty.ToString();
        }

        private void BtnWroc_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainOkno = new MainWindow();
            mainOkno.Show();
            this.Close();
        }
    }
}
