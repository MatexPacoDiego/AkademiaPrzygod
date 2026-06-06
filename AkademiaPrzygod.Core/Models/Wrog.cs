using AkademiaPrzygod.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkademiaPrzygod.Core.Models
{
    /// <summary>
    /// Reprezentuje przeciwnika w walce.
    /// </summary>
    public class Wrog
    {
        private string _nazwa;
        private readonly string _nazwaGatunku;
        private int _zdrowie;
        private int _maxZdrowie;
        private int _atak;
        private int _obrona;
        private int _nagrodeXP;
        private int _nagrodeZloto;
        private TrudnoscWroga _trudnosc;

        public string Nazwa
        {
            get { return _nazwa; }
            private set { _nazwa = value; }
        }

        public string NazwaGatunku
        {
            get { return _nazwaGatunku; }
        }

        public int Zdrowie
        {
            get { return _zdrowie; }
            private set { _zdrowie = value; }
        }

        public int MaxZdrowie
        {
            get { return _maxZdrowie; }
            private set { _maxZdrowie = value; }
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

        public int NagrodeXP
        {
            get { return _nagrodeXP; }
            private set { _nagrodeXP = value; }
        }

        public int NagrodeZloto
        {
            get { return _nagrodeZloto; }
            private set { _nagrodeZloto = value; }
        }

        public TrudnoscWroga Trudnosc
        {
            get { return _trudnosc; }
            private set { _trudnosc = value; }
        }

        /// <summary>
        /// Tworzy nowego wroga z podanymi statystykami.
        /// </summary>
        /// <param name="nazwa">Imię wroga.</param>
        /// <param name="nazwaGatunku">Gatunek wroga.</param>
        /// <param name="zdrowie">Punkty zdrowia.</param>
        /// <param name="atak">Siła ataku.</param>
        /// <param name="obrona">Wartość obrony.</param>
        /// <param name="xp">Nagroda XP.</param>
        /// <param name="zloto">Nagroda złoto.</param>
        /// <param name="trudnosc">Poziom trudności.</param>
        public Wrog(string nazwa, string nazwaGatunku, int zdrowie, int atak, int obrona, int xp, int zloto, TrudnoscWroga trudnosc)
        {
            Nazwa = nazwa;
            _nazwaGatunku = nazwaGatunku;
            MaxZdrowie = zdrowie;
            Zdrowie = zdrowie;
            Atak = atak;
            Obrona = obrona;
            NagrodeXP = xp;
            NagrodeZloto = zloto;
            Trudnosc = trudnosc;
        }

        /// <summary>
        /// Atakuje bohatera i zwraca zadane obrażenia.
        /// </summary>
        /// <param name="cel">Bohater który otrzyma obrażenia.</param>
        /// <returns>Liczba zadanych obrażeń.</returns>
        public int AtakujBohatera(Bohater cel)
        {
            int obrazenia = Math.Max(1, Atak - cel.Obrona);  // tu liczymy
            cel.OtrzymajObrazenia(obrazenia);
            return obrazenia;
        }

        /// <summary>
        /// Redukuje zdrowie wroga o podaną wartość.
        /// </summary>
        /// <param name="obrazenia">Liczba obrażeń do zadania.</param>
        public void OtrzymajObrazenia(int obrazenia)
        {
            Zdrowie -= obrazenia;
            if (Zdrowie < 0) Zdrowie = 0;
        }

        /// <summary>
        /// Sprawdza czy wróg żyje.
        /// </summary>
        /// <returns>True jeśli zdrowie większe od zera.</returns>
        public bool CzyZyje()
        {
            return Zdrowie > 0;
        }

        /// <summary>
        /// Tworzy kopię wroga z pełnym zdrowiem.
        /// </summary>
        public Wrog Klonuj()
        {
            return new Wrog(Nazwa, NazwaGatunku, MaxZdrowie, Atak, Obrona, NagrodeXP, NagrodeZloto, Trudnosc);
        }

        /// <summary>
        /// Wyświetla statystyki wroga w konsoli.
        /// </summary>
        public void PokazStatystyki()
        {
            Console.WriteLine($"=== {Nazwa} ({NazwaGatunku}) ===");
            Console.WriteLine($"HP: {Zdrowie}/{MaxZdrowie}");
            Console.WriteLine($"Atak: {Atak} | Obrona: {Obrona}");
            Console.WriteLine($"Nagroda: {NagrodeXP} XP, {NagrodeZloto} złota");
            Console.WriteLine($"Trudność: {Trudnosc}");
        }

        /// <summary>
        /// Zwraca czytelny opis wroga.
        /// </summary>
        public override string ToString()
        {
            return $"[{Trudnosc}] {Nazwa} | HP {Zdrowie}/{MaxZdrowie} | Atak {Atak}";
        }
    }
}