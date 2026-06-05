using AkademiaPrzygod.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkademiaPrzygod.Core.Models
{
    /// <summary>
    /// Reprezentuje przedmiot w ekwipunku bohatera.
    /// </summary>
    public class Przedmiot
    {
        private string _nazwa;
        private TypPrzedmiotu _typ;
        private int _wartosc;
        private string _opis;

        public string Nazwa
        {
            get { return _nazwa; }
            set { _nazwa = value; }
        }

        public TypPrzedmiotu Typ
        {
            get { return _typ; }
            set {  _typ = value; }
        }

        public int Wartosc
        {
            get { return _wartosc;}
            set { _wartosc = value; }
        }

        public string Opis
        {
            get { return _opis;}
            set { _opis = value; }
        }

       public Przedmiot(string nazwa, TypPrzedmiotu typ, int wartosc, string opis)
        {
            Nazwa = nazwa;
            Typ = typ;
            Wartosc = wartosc;
            Opis = opis;
        }


        /// <summary>
        /// Stosuje efekt przedmiotu na bohatera.
        /// </summary>
        public void Uzyj(Bohater b)
        {
            if (Typ == TypPrzedmiotu.Mikstura)
            {
                b.Ulecz(30);
                Console.WriteLine($"Użyto {Nazwa} – uleczono 30 HP!");
            }
            else if (Typ == TypPrzedmiotu.Bron)
            {
                b.ZwiekszAtak(5);
                Console.WriteLine($"Użyto {Nazwa} – atak wzrósł o 5!");
            }
            else if (Typ == TypPrzedmiotu.Zbroja)
            {
                b.ZwiekszObrone(5);
                Console.WriteLine($"Użyto {Nazwa} – obrona wzrosła o 5!");
            }
        }

       

        public override string ToString()
        {
            return $"[{Typ}] {Nazwa} - {Opis} ({Wartosc} złota)";
        }
    }
}
