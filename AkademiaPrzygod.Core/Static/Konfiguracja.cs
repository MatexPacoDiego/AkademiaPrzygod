using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkademiaPrzygod.Core.Static
{
    /// <summary>
    /// Globalna konfiguracja gry. Klasa statyczna – nie można jej instancjonować.
    /// </summary>
    public static class Konfiguracja
    {
        /// <summary>Oficjalna nazwa gry.</summary>
        public static readonly string NazwaGry = "Akademia Przygód";

        /// <summary>Wersja aplikacji.</summary>
        public static string WersjaGry = "1.1";

        /// <summary>Maksymalny poziom bohatera.</summary>
        public static int MaksymalnyPoziom = 10;

        /// <summary>XP potrzebne do awansu na kolejny poziom.</summary>
        public static int XPDoAwansu = 100;

        /// <summary>Maksymalna liczba przedmiotów w ekwipunku.</summary>
        public static int MaksymalnyEkwipunek = 10;

        /// <summary>Szansa na ucieczkę z walki (0-100).</summary>
        public static int SzansaUcieczki = 40;
    }
}
