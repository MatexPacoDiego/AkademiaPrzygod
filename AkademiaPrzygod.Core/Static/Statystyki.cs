using AkademiaPrzygod.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkademiaPrzygod.Core.Static
{
    /// <summary>
    /// Globalne statystyki sesji gry.
    /// </summary>
    public static class Statystyki
    {
        public static Bohater Bohater;
        public static int LiczbaWalk=0;
        public static int LiczbaZabitegoWrogow=0;
        public static int ZebranePrzedmioty=0;

        /// <summary>
        /// Wyświetla podsumowanie statystyk sesji.
        /// </summary>
        public static void PokazPodsumowanie()
        {
            Console.WriteLine($"Stoczone walki: {LiczbaWalk}");
            Console.WriteLine($"Liczba zabitych wrogów: {LiczbaZabitegoWrogow}");
            Console.WriteLine($"Zebrane przedmioty: {ZebranePrzedmioty}");
        }
    }
}
