using AkademiaPrzygod.Core.Enums;
using AkademiaPrzygod.Core.Models;
using AkademiaPrzygod.Core.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkademiaPrzygod.Core.Models
{
    /// <summary>
    /// Reprezentuje bohatera gry.
    /// </summary>
    public class Bohater
    {
        private int _zdrowie;
        private int _maxZdrowie;
        private int _doswiadczenie;
        private List<Przedmiot> _ekwipunek;
        private string _imie;
        private KlasaPostaci _klasa;
        private int _atak;
        private int _obrona;
        private int _poziom;
        private int _zloto;

        public string Imie
        {
            get { return _imie; }
            private set { _imie = value; }
        }

        public KlasaPostaci Klasa
        {
            get { return _klasa; }
            private set { _klasa = value; }
        }

        public int Zdrowie
        {
            get { return _zdrowie; }
            private set { _zdrowie = Math.Max(0, Math.Min(value, MaxZdrowie)); }
        }

        public int MaxZdrowie
        {
            get { return _maxZdrowie; }
            private set { _maxZdrowie = value > 0 ? value : 1; }
        }

        public int Atak
        {
            get { return _atak; }
            private set { _atak = value; }
        }

        public int Obrona
        {
            get { return _obrona; }
            private set { _obrona = value; }
        }

        public int Poziom
        {
            get { return _poziom; }
            private set { _poziom = value; }
        }

        public int Doswiadczenie
        {
            get { return _doswiadczenie; }
            private set { _doswiadczenie = value; }
        }

        public int Zloto
        {
            get { return _zloto; }
            set { _zloto = value; }
        }

        /// <summary>
        /// Konstruktor tworzący bohatera na podstawie imienia i klasy.
        /// </summary>
        public Bohater(string imie, KlasaPostaci klasa)
        {
            Imie = imie;
            Klasa = klasa;
            _ekwipunek = new List<Przedmiot>();
            Poziom = 1;
            Zloto = 100;

            UstawStatystyki(Klasa);
        }

        /// <summary>
        /// Ustawia statystyki startowe zależnie od klasy.
        /// </summary>
        private void UstawStatystyki(KlasaPostaci klasa)
        {
            switch (klasa)
            {
                case KlasaPostaci.Wojownik:
                    MaxZdrowie = 120;
                    Atak = 20;
                    Obrona = 15;
                    break;
                case KlasaPostaci.Mag:
                    MaxZdrowie = 80;
                    Atak = 30;
                    Obrona = 8;
                    break;
                case KlasaPostaci.Lotrzyk:
                    MaxZdrowie = 100;
                    Atak = 25;
                    Obrona = 10;
                    break;
            }
            Zdrowie = MaxZdrowie;
        }

        /// <summary>
        /// Wykonuje atak na wroga, zwraca zadane obrażenia.
        /// </summary>
        public int AtakujWroga(Wrog cel)
        {
            int obrazenia = Math.Max(1, Atak - cel.Obrona);  
            cel.OtrzymajObrazenia(obrazenia);
            return obrazenia;
        }

        /// <summary>
        /// Redukuje zdrowie bohatera o podaną wartość.
        /// </summary>
        public void OtrzymajObrazenia(int obrazenia)
        {
            Zdrowie -= obrazenia;
        }

        /// <summary>
        /// Dodaje XP i sprawdza czy bohater awansuje.
        /// </summary>
        public void DodajDoswiadczenie(int xp)
        {
            Doswiadczenie += xp;
            if (Doswiadczenie >= Konfiguracja.XPDoAwansu)
            {
                Awans();
            }
        }

        /// <summary>
        /// Awansuje bohatera na kolejny poziom.
        /// </summary>
        private void Awans()
        {
            if (Poziom >= Konfiguracja.MaksymalnyPoziom) return;
            Poziom++;
            _doswiadczenie = 0;
            MaxZdrowie += 10;
            Zdrowie = MaxZdrowie;
            Atak += 3;
            Obrona += 2;
            Console.WriteLine($"Awans! Osiągnąłeś poziom {Poziom}!");
        }

        /// <summary>
        /// Dodaje przedmiot do ekwipunku.
        /// </summary>
        public bool DodajPrzedmiot(Przedmiot p)
        {
            if (_ekwipunek.Count < Konfiguracja.MaksymalnyEkwipunek)
            {
                _ekwipunek.Add(p);
                return true;
            }
            return false;
            

        }

        /// <summary>
        /// Używa przedmiotu z ekwipunku o podanym indeksie.
        /// </summary>
        public void UzyjPrzedmiot(int index)
        {
            if (index < 0 || index >= _ekwipunek.Count) return;
            Przedmiot p = _ekwipunek[index];
            p.Uzyj(this);
            _ekwipunek.RemoveAt(index);
        }

        /// <summary>
        /// Sprawdza czy bohater żyje.
        /// </summary>
        public bool CzyZyje()
        {
            if (Zdrowie > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Próba ucieczki z walki.
        /// </summary>
        public bool Uciekaj()
        {
            Random rnd = new Random();
            return rnd.Next(100) < Konfiguracja.SzansaUcieczki;
        }

        /// <summary>
        /// Wyświetla statystyki bohatera.
        /// </summary>
        public void PokazStatystyki()
        {
            Console.WriteLine($"=== {Imie} ({Klasa}) ===");
            Console.WriteLine($"Poziom: {Poziom}");
            Console.WriteLine($"HP: {Zdrowie}/{MaxZdrowie}");
            Console.WriteLine($"Atak: {Atak} | Obrona: {Obrona}");
            Console.WriteLine($"XP: {Doswiadczenie}/{Konfiguracja.XPDoAwansu}");
            Console.WriteLine($"Złoto: {Zloto}");
            Console.WriteLine($"Ekwipunek: {_ekwipunek.Count}/{Konfiguracja.MaksymalnyEkwipunek}");
        }

        /// <summary>
        /// Zwraca listę przedmiotów w ekwipunku (tylko do odczytu).
        /// </summary>
        public List<Przedmiot> PobierzEkwipunek()
        {
            return _ekwipunek;
        }

        /// <summary>
        /// Odejmuje złoto od bohatera.
        /// </summary>
        /// <param name="wartosc">Wartosc złota do odjęcia.</param>
        public void OdejmijZloto(int wartosc)
        {
            Zloto -= wartosc;
        }

        /// <summary>
        /// Dodaje złoto do bohatera.
        /// </summary>
        /// <param name="wartosc">Wartosc do dodania.</param>
        public void DodajZloto(int wartosc)
        {
            Zloto += wartosc;
        }

        /// <summary>
        /// Leczy bohatera o podaną wartość.
        /// </summary>
        /// <param name="ile">Ilość HP do przywrócenia.</param>
        public void Ulecz(int ile)
        {
            Zdrowie += ile;
            Console.WriteLine($"{Imie} uleczony! HP: {Zdrowie}/{MaxZdrowie}");
        }

        /// <summary>
        /// Zwiększa atak bohatera.
        /// </summary>
        /// <param name="ile">Wartość o jaką wzrośnie atak.</param>
        public void ZwiekszAtak(int ile)
        {
            Atak += ile;
        }

        /// <summary>
        /// Zwiększa obronę bohatera.
        /// </summary>
        /// <param name="ile">Wartość o jaką wzrośnie obrona.</param>
        public void ZwiekszObrone(int ile)
        {
            Obrona += ile;
        }

        public override string ToString()
        {
            return $"{Imie} | {Klasa} | Lvl {Poziom} | HP {Zdrowie}/{MaxZdrowie}";
        }
    }
}