using AkademiaPrzygod.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkademiaPrzygod.Core.Models
{
    /// <summary>
    /// Reprezentuje sklep w grze gdzie bohater może kupować przedmioty.
    /// </summary>
    public class Sklep
    {
        private List<Przedmiot> _towar;
        private string _nazwa;

        public string Nazwa
        {
            get { return _nazwa; }
            private set { _nazwa = value; }
        }

        /// <summary>
        /// Tworzy nowy sklep z domyślnymi przedmiotami.
        /// </summary>
        /// <param name="nazwa">Nazwa sklepu.</param>
        public Sklep(string nazwa)
        {
            Nazwa = nazwa;
            _towar = new List<Przedmiot>();
            DodajTowar(new Przedmiot("🧃 Gumijagody", TypPrzedmiotu.Mikstura, 50, "⛑️ Leczy 30 HP"));
            DodajTowar(new Przedmiot("🧙 Różdżka Ognia", TypPrzedmiotu.Broń, 100, "⚔️ Zwiększa atak o 5"));
            DodajTowar(new Przedmiot("🛡️ Tarcza Zaklęć", TypPrzedmiotu.Zbroja, 80, "🤚 Zwiększa obronę o 5"));
        }

        /// <summary>
        /// Dodaje przedmiot do oferty sklepu.
        /// </summary>
        /// <param name="p">Przedmiot do dodania.</param>
        public void DodajTowar(Przedmiot p)
        {
            _towar.Add(p);
        }

        /// <summary>
        /// Wyświetla ofertę sklepu.
        /// </summary>
        public List<string> PokazOferte()
        {
            int liczbaPorzad = 1;
            List<string> listaTowarow = new List<string>();
            foreach(Przedmiot p in _towar)
            {
                listaTowarow.Add($"{liczbaPorzad++}. {p.Nazwa} - {p.Wartosc} złota - {p.Opis}");
            }
            return listaTowarow;
        }

        /// <summary>
        /// Kupuje przedmiot z listy sklepu.
        /// </summary>
        /// <param name="b">Bohater który kupuje.</param>
        /// <param name="index">Indeks przedmiotu (od 0).</param>
        /// <returns>True jeśli zakup się udał.</returns>
        public bool Kup(Bohater b, int index)
        {
            if (index < 0 || index >= _towar.Count)
                return false;

            Przedmiot towar = _towar[index];
            if (b.Zloto >= towar.Wartosc)
            {
                b.OdejmijZloto(towar.Wartosc);  // nowa metoda w Bohater!
                b.DodajPrzedmiot(towar);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Zwraca czytelny opis sklepu.
        /// </summary>
        public override string ToString()
        {
            return $"Sklep: {Nazwa} ({_towar.Count} towary)";
        }
    }
}