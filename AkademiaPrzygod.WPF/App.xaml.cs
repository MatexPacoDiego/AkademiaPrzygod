using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Media;

namespace AkademiaPrzygod.WPF
{
    /// <summary>
    /// Główna klasa aplikacji WPF – zarządza startem i muzyką w tle.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Globalny odtwarzacz muzyki dostępny z każdego okna aplikacji.
        /// </summary>
        public static MediaPlayer Muzyka = new MediaPlayer();

        /// <summary>
        /// Uruchamia aplikację i startuje muzykę w tle z zapętleniem.
        /// </summary>
        /// <param name="e">Argumenty startowe aplikacji.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Muzyka.Open(new System.Uri("gra.mp3", System.UriKind.Relative));
            Muzyka.MediaEnded += (s, ev) =>
            {
                Muzyka.Position = System.TimeSpan.Zero;
                Muzyka.Play();
            };
            Muzyka.Play();
        }
    }
}