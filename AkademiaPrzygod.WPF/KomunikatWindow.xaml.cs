using System.Windows;

namespace AkademiaPrzygod.WPF
{
    /// <summary>
    /// Proste okno dialogowe wyświetlające komunikat dla gracza.
    /// </summary>
    public partial class KomunikatWindow : Window
    {
        /// <summary>
        /// Inicjuje okno komunikatu i wyświetla podany tekst.
        /// </summary>
        /// <param name="text">Treść komunikatu do wyświetlenia.</param>
        public KomunikatWindow(string text)
        {
            InitializeComponent();
            tbWynik.Text = text;
        }

        /// <summary>
        /// Zamyka okno komunikatu po kliknięciu przycisku OK.
        /// </summary>
        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}