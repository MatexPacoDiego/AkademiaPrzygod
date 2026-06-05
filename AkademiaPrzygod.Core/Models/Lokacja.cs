using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkademiaPrzygod.Core.Models
{
    /// <summary>
    /// Reprezentuje lokację w grze – zawiera wrogów i przedmioty do znalezienia.
    /// </summary>
    public class Lokacja
    {
        private string _nazwa;
        private string _opis;
        private List<Wrog> _listaWrogow;
        private List<Przedmiot> _listaPrzedmiotow;

        public string Nazwa
        {
            get { return _nazwa; }
            private set { _nazwa = value; }
        }

        public string Opis
        {
            get { return _opis; }
            private set { _opis = value; }
        }

        /// <summary>
        /// Tworzy nową lokację z podaną nazwą i opisem.
        /// </summary>
        /// <param name="nazwa">Nazwa lokacji.</param>
        /// <param name="opis">Opis lokacji.</param>
        public Lokacja(string nazwa, string opis)
        {
            Nazwa = nazwa;
            Opis = opis;
            _listaWrogow = new List<Wrog>();
            _listaPrzedmiotow = new List<Przedmiot>();
        }

        /// <summary>
        /// Dodaje wroga do lokacji.
        /// </summary>
        /// <param name="w">Wróg do dodania.</param>
        public void DodajWroga(Wrog w)
        {
            _listaWrogow.Add(w);
        }

        /// <summary>
        /// Dodaje przedmiot do lokacji.
        /// </summary>
        /// <param name="p">Przedmiot do dodania.</param>
        public void DodajPrzedmiot(Przedmiot p)
        {
            _listaPrzedmiotow.Add(p);
        }

        /// <summary>
        /// Losuje spotkanie z wrogiem lub przedmiotem.
        /// </summary>
        /// <returns>Obiekt typu Wrog lub Przedmiot, null jeśli lokacja pusta.</returns>
        public object LosujSpotkanie()
        {
            Random rnd = new Random();
            if (rnd.Next(2) == 0 && _listaWrogow.Count > 0)
            {
                return _listaWrogow[rnd.Next(_listaWrogow.Count)].Klonuj(); 
            }
            if (_listaPrzedmiotow.Count > 0)
            {
                return _listaPrzedmiotow[rnd.Next(_listaPrzedmiotow.Count)];  
            }
            return null;
        }



        /// <summary>
        /// Zwraca czytelny opis lokacji.
        /// </summary>
        public override string ToString()
        {
            return $"{Nazwa} - {Opis} (Wrogowie: {_listaWrogow.Count}, Przedmioty: {_listaPrzedmiotow.Count})";
        }
    }
}
