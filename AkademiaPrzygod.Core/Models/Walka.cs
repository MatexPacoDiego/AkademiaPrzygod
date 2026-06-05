using AkademiaPrzygod.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkademiaPrzygod.Core.Models
{
    /// <summary>
    /// Zarządza przebiegiem walki między bohaterem a wrogiem.
    /// </summary>
    public class Walka
    {
        private Bohater _bohater;
        private Wrog _wrog;

        /// <summary>
        /// Tworzy nową walkę między bohaterem a wrogiem.
        /// </summary>
        /// <param name="bohater">Bohater gracza.</param>
        /// <param name="wrog">Przeciwnik.</param>
        public Walka(Bohater bohater, Wrog wrog)
        {
            _bohater = bohater;
            _wrog = wrog;
        }

        /// <summary>
        /// Wykonuje jedną turę walki na podstawie akcji gracza.
        /// </summary>
        /// <param name="akcja">Akcja wybrana przez gracza.</param>
        /// <returns>Wynik tury z opisem i stanem walki.</returns>
        public WynikTury WykonajTure(AkcjaGracza akcja)
        {
            WynikTury wynik = new WynikTury();
            wynik.WalkaTrwa = true;

            switch (akcja)
            {
                case AkcjaGracza.Atak:
                    int obrazeniaBo = _bohater.AtakujWroga(_wrog);
                    wynik.Opis = $"Bohater zadał {obrazeniaBo} obrażeń!";
                    if (!_wrog.CzyZyje())
                    {
                        wynik.WalkaTrwa = false;
                        wynik.BohaterWygral = true;
                        wynik.ZdobyteXP = _wrog.NagrodeXP;
                        wynik.ZdobyteZloto = _wrog.NagrodeZloto;
                        wynik.Opis += $"\n{_wrog.Nazwa} pokonany! +{_wrog.NagrodeXP} XP, +{_wrog.NagrodeZloto} złota!";
                    }
                    break;

                case AkcjaGracza.UzyjPrzedmiot:
                    var ekwipunek = _bohater.PobierzEkwipunek();
                    if (ekwipunek.Count == 0)
                    {
                        wynik.Opis = "Nie masz żadnych przedmiotów!";
                    }
                    else
                    {
                        wynik.Opis = "POKAZ_EKWIPUNEK";
                        wynik.WalkaTrwa = true;
                    }
                    break;

                case AkcjaGracza.Ucieknij:
                    if (_bohater.Uciekaj())
                    {
                        wynik.WalkaTrwa = false;
                        wynik.Opis = "Uciekłeś!";
                    }
                    else
                    {
                        wynik.Opis = "Nie udało się uciec!";
                    }

                    break;
            }

            if (wynik.WalkaTrwa)
            {
                int obrazeniaWr = _wrog.AtakujBohatera(_bohater);
                wynik.Opis += $"\nWróg zadał {obrazeniaWr} obrażeń! HP: {_bohater.Zdrowie}/{_bohater.MaxZdrowie}";
                if (!_bohater.CzyZyje())
                {
                    wynik.WalkaTrwa = false;
                    wynik.BohaterWygral = false;
                    wynik.Opis += "\nPoległeś w walce...";
                }
            }

            return wynik;
        }
    }
}
