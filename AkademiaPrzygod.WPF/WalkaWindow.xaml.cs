using AkademiaPrzygod.Core.Enums;
using AkademiaPrzygod.Core.Models;
using AkademiaPrzygod.Core.Static;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace AkademiaPrzygod.WPF
{
    /// <summary>
    /// Okno turowej walki między bohaterem a wrogiem z animacją na Canvas.
    /// </summary>
    public partial class WalkaWindow : Window
    {
        private Bohater _bohater;
        private Wrog _wrog;
        private Walka _walka;
        private double _bohaterStartX = 60;
        private double _wrogStartX;

        /// <summary>
        /// Inicjuje okno walki, tworzy obiekt walki i uruchamia muzykę bojową.
        /// </summary>
        /// <param name="wrog">Wróg z którym bohater będzie walczył.</param>
        public WalkaWindow(Wrog wrog)
        {
            InitializeComponent();
            _bohater = Statystyki.Bohater;
            _wrog = wrog;
            _walka = new Walka(_bohater, wrog);
            Statystyki.LiczbaWalk++;
            App.Muzyka.Open(new Uri("walka.mp3", UriKind.Relative));
            App.Muzyka.Play();
            Loaded += WalkaWindow_Loaded;
        }

        /// <summary>
        /// Po załadowaniu okna ustawia pozycje postaci i wyświetla dane walki.
        /// </summary>
        private void WalkaWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _wrogStartX = walkaCanvas.ActualWidth - 160;
            Canvas.SetLeft(wrogPanel, _wrogStartX);

            tbNazwaWroga.Text = _wrog.Nazwa;
            tbGatunekWroga.Text = _wrog.NazwaGatunku;
            tbBohaterNazwa.Text = _bohater.Imie;
            tbWrogNazwa.Text = _wrog.Nazwa;
            tbWrogEmotka.Text = PobierzEmotkeWroga(_wrog.NazwaGatunku);
            tbBohaterEmotka.Text = PobierzEmotkeBohater(_bohater.Klasa);

            OdswiezUI();
            DodajLog($"🥊 Walka rozpoczęta! {_bohater.Imie} vs {_wrog.Nazwa}!");
        }

        /// <summary>
        /// Zwraca emotikonę odpowiadającą gatunkowi wroga.
        /// </summary>
        /// <param name="gatunek">Nazwa gatunku wroga.</param>
        /// <returns>Emotikona jako string.</returns>
        private string PobierzEmotkeWroga(string gatunek)
        {
            switch (gatunek)
            {
                case "Goblin": return "👺";
                case "Szczur": return "🐀";
                case "Elemental": return "🔥";
                case "Troll": return "👹";
                case "Golem": return "🗿";
                case "Duch": return "👻";
                default: return "👾";
            }
        }

        /// <summary>
        /// Zwraca emotikonę odpowiadającą klasie bohatera.
        /// </summary>
        /// <param name="klasa">Klasa postaci bohatera.</param>
        /// <returns>Emotikona jako string.</returns>
        private string PobierzEmotkeBohater(KlasaPostaci klasa)
        {
            switch (klasa)
            {
                case KlasaPostaci.Mag: return "🧙";
                case KlasaPostaci.Lotrzyk: return "🗡️";
                default: return "⚔️";
            }
        }

        /// <summary>
        /// Odświeża statystyki bohatera i pasek HP wroga w UI.
        /// </summary>
        private void OdswiezUI()
        {
            statsControl.Odswiez(_bohater);
            pbHPWroga.Maximum = _wrog.MaxZdrowie;
            pbHPWroga.Value = _wrog.Zdrowie;
            tbHPWroga.Text = $"{_wrog.Zdrowie}/{_wrog.MaxZdrowie}";
        }

        /// <summary>
        /// Dodaje linię tekstu do logu walki i przewija na dół.
        /// </summary>
        /// <param name="tekst">Tekst do dodania w logu.</param>
        private void DodajLog(string tekst)
        {
            tbLog.Text += tekst + "\n\n";
            logScroll.ScrollToBottom();
        }

        /// <summary>
        /// Animuje ruch bohatera w stronę wroga i z powrotem.
        /// </summary>
        /// <param name="poAnimacji">Akcja wykonywana po zakończeniu animacji.</param>
        private void AnimujAtakBohater(Action poAnimacji)
        {
            double celX = _wrogStartX - 100;
            var animacja = new DoubleAnimation
            {
                From = _bohaterStartX,
                To = celX,
                Duration = TimeSpan.FromMilliseconds(300),
                AutoReverse = true
            };
            animacja.Completed += (s, e) => poAnimacji();
            bohaterPanel.BeginAnimation(Canvas.LeftProperty, animacja);
        }

        /// <summary>
        /// Animuje ruch wroga w stronę bohatera i z powrotem.
        /// </summary>
        /// <param name="poAnimacji">Akcja wykonywana po zakończeniu animacji.</param>
        private void AnimujAtakWrog(Action poAnimacji)
        {
            double celX = _bohaterStartX + 100;
            var animacja = new DoubleAnimation
            {
                From = _wrogStartX,
                To = celX,
                Duration = TimeSpan.FromMilliseconds(300),
                AutoReverse = true
            };
            animacja.Completed += (s, e) => poAnimacji();
            wrogPanel.BeginAnimation(Canvas.LeftProperty, animacja);
        }

        /// <summary>
        /// Obsługuje kliknięcie przycisku Atak – animuje atak bohatera i wroga.
        /// </summary>
        private void BtnAtak_Click(object sender, RoutedEventArgs e)
        {
            UstawPrzyciski(false);
            AnimujAtakBohater(() =>
            {
                WynikTury wynik = _walka.WykonajTure(AkcjaGracza.Atak);
                DodajLog("➡️ " + wynik.Opis);
                OdswiezUI();

                if (!wynik.WalkaTrwa)
                {
                    ZakonczWalke(wynik);
                    return;
                }

                AnimujAtakWrog(() =>
                {
                    OdswiezUI();
                    UstawPrzyciski(true);
                });
            });
        }

        /// <summary>
        /// Obsługuje użycie przedmiotu podczas walki – otwiera ekwipunek,
        /// po zamknięciu wróg atakuje bohatera.
        /// </summary>
        private void BtnPrzedmiot_Click(object sender, RoutedEventArgs e)
        {
            var ekwipunek = _bohater.PobierzEkwipunek();
            if (ekwipunek.Count == 0)
            {
                new KomunikatWindow("Nie masz żadnych przedmiotów!").ShowDialog();
                return;
            }

            EkwipunekWindow okno = new EkwipunekWindow("Walka");
            okno.ShowDialog();

            UstawPrzyciski(false);
            AnimujAtakWrog(() =>
            {
                int obrazenia = _wrog.AtakujBohatera(_bohater);
                DodajLog("➡️ " + $"Wróg atakuje! Zadaje {obrazenia} obrażeń!");
                OdswiezUI();

                if (!_bohater.CzyZyje())
                {
                    ZakonczWalke(new WynikTury { WalkaTrwa = false, BohaterWygral = false });
                    return;
                }
                UstawPrzyciski(true);
            });
        }

        /// <summary>
        /// Obsługuje próbę ucieczki z walki.
        /// </summary>
        private void BtnUcieknij_Click(object sender, RoutedEventArgs e)
        {
            WynikTury wynik = _walka.WykonajTure(AkcjaGracza.Ucieknij);
            DodajLog("➡️ " + wynik.Opis);

            if (!wynik.WalkaTrwa)
            {
                App.Muzyka.Open(new Uri("defeat.mp3", UriKind.Relative));
                App.Muzyka.Play();
                new KomunikatWindow("Uciekłeś z pola walki!").ShowDialog();
                buttonsGrid.Visibility = Visibility.Collapsed;
                btnOk.Visibility = Visibility.Visible;
            }
            else
            {
                OdswiezUI();
            }
        }

        /// <summary>
        /// Kończy walkę – przyznaje nagrody lub wyświetla informację o porażce.
        /// </summary>
        /// <param name="wynik">Wynik ostatniej tury walki.</param>
        private void ZakonczWalke(WynikTury wynik)
        {
            UstawPrzyciski(false);
            if (wynik.BohaterWygral)
            {
                _bohater.DodajDoswiadczenie(wynik.ZdobyteXP);
                _bohater.DodajZloto(wynik.ZdobyteZloto);
                Statystyki.LiczbaZabitegoWrogow++;
                App.Muzyka.Open(new Uri("win.mp3", UriKind.Relative));
                App.Muzyka.Play();
                new KomunikatWindow("Wygrałeś walkę!").ShowDialog();
                DodajLog($"🏆 Wygrana! +{wynik.ZdobyteXP} XP, +{wynik.ZdobyteZloto} złota!");
                pbHPWroga.Value = 0;
                OdswiezUI();
                buttonsGrid.Visibility = Visibility.Collapsed;
                btnOk.Visibility = Visibility.Visible;
            }
            else
            {
                DodajLog($"❌ Poległeś... - pokonuje cię {_wrog.Nazwa}");
                App.Muzyka.Open(new Uri("defeat.mp3", UriKind.Relative));
                App.Muzyka.Play();
                new KomunikatWindow("🥺 Poległeś!").ShowDialog();
                buttonsGrid.Visibility = Visibility.Collapsed;
                btnOk.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Włącza lub wyłącza przyciski akcji walki.
        /// </summary>
        /// <param name="aktywne">True jeśli przyciski mają być aktywne.</param>
        private void UstawPrzyciski(bool aktywne)
        {
            btnAtak.IsEnabled = aktywne;
            btnPrzedmiot.IsEnabled = aktywne;
            btnUcieknij.IsEnabled = aktywne;
        }

        /// <summary>
        /// Wraca do okna gry po zakończeniu walki i włącza muzykę główną.
        /// </summary>
        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            GraWindow oknoGry = new GraWindow();
            oknoGry.Show();
            App.Muzyka.Open(new Uri("gra.mp3", UriKind.Relative));
            App.Muzyka.Play();
            this.Close();
        }
    }
}