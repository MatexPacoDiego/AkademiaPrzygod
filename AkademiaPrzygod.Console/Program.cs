using AkademiaPrzygod.Core.Enums;
using AkademiaPrzygod.Core.Models;
using AkademiaPrzygod.Core.Static;
using System;
using System.Collections.Generic;

namespace AkademiaPrzygod.ConsoleApp
{
    class Program
    {
        static Bohater _bohater;
        static List<Lokacja> _lokacje;
        static Sklep _sklep;

        /// <summary>
        /// Punkt wejścia aplikacji. Inicjuje lokacje i uruchamia menu główne.
        /// </summary>
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            ZainicjujLokacje();
            MenuGlowne();
        }

        /// <summary>
        /// Wyświetla menu główne i obsługuje wybór gracza.
        /// </summary>
        static void MenuGlowne()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("╔════════════════════════════╗");
                Console.WriteLine($"║    AKADEMIA PRZYGÓD v{Konfiguracja.WersjaGry}   ║");
                Console.WriteLine("╚════════════════════════════╝");
                if (_bohater != null)
                {
                    Console.WriteLine("1. Wczytaj grę");
                }
                else
                {
                    Console.WriteLine("1. Nowa gra");
                }
                
                
                Console.WriteLine("2. Statystyki globalne");
                Console.WriteLine("0. Wyjście");
                Console.Write("\nWybierz opcję: ");

                string wybor = Console.ReadLine();
                switch (wybor)
                {
                    case "1":
                        if (_bohater != null)
                        {
                            MenuGry();
                        }
                        else
                        {
                            NowaGra();
                        }
                        
                        break;
                    case "2":
                        Console.WriteLine();
                        Statystyki.PokazPodsumowanie();
                        Console.WriteLine("\nWciśnij dowolny przycisk aby kontynuować");
                        Console.ReadKey();
                        break;
                    case "0":
                        Console.WriteLine("👋 Żegnaj");
                        return;
                    default:
                        Console.WriteLine("Zła opcja!");
                        Console.WriteLine("\nWciśnij dowolny przycisk aby kontynuować");
                        Console.ReadKey();
                        break;
                }
            }
        }

        /// <summary>
        /// Obsługuje tworzenie nowej gry – pobiera imię i klasę bohatera.
        /// </summary>
        static void NowaGra()
        {
            Console.Clear();

            string imie = "";
            while (string.IsNullOrWhiteSpace(imie))
            {
                Console.Write("Podaj imię bohatera: ");
                imie = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(imie))
                    Console.WriteLine("Imię nie może być puste!");
            }

            Console.WriteLine("\nWybierz klasę:");
            Console.WriteLine("1. Wojownik (HP: 120, Atak: 20, Obrona: 15)");
            Console.WriteLine("2. Mag      (HP:  80, Atak: 30, Obrona:  8)");
            Console.WriteLine("3. Łotrzyk  (HP: 100, Atak: 25, Obrona: 10)");

            KlasaPostaci klasa = KlasaPostaci.Wojownik;
            bool poprawnaKlasa = false;
            while (!poprawnaKlasa)
            {
                Console.Write("Wybór (1-3): ");
                switch (Console.ReadLine())
                {
                    case "1":
                        klasa = KlasaPostaci.Wojownik;
                        poprawnaKlasa = true;
                        break;
                    case "2":
                        klasa = KlasaPostaci.Mag;
                        poprawnaKlasa = true;
                        break;
                    case "3":
                        klasa = KlasaPostaci.Lotrzyk;
                        poprawnaKlasa = true;
                        break;
                    default:
                        Console.WriteLine("Wpisz 1, 2 lub 3!");
                        break;
                }
            }

            _bohater = new Bohater(imie, klasa);
            _sklep = new Sklep("Sklep Akademii");

            Console.WriteLine($"\nWitaj {imie}! Przygoda czeka!");
            Console.WriteLine("\nWciśnij dowolny przycisk aby kontynuować");
            Console.ReadKey();
            MenuGry();
        }

        /// <summary>
        /// Główna pętla gry – wyświetla menu i obsługuje akcje gracza.
        /// </summary>
        static void MenuGry()
        {
            while (_bohater.CzyZyje())
            {
                Console.Clear();
                Console.WriteLine(_bohater.ToString());
                Console.WriteLine("\n1. Eksploruj lokację");
                Console.WriteLine("2. Sklep");
                Console.WriteLine("3. Ekwipunek");
                Console.WriteLine("4. Statystyki bohatera");
                Console.WriteLine("0. Wróć do menu głównego");
                Console.Write("\nWybierz opcję: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        Eksploruj();
                        break;
                    case "2":
                        OtworzSklep();
                        break;
                    case "3":
                        PokazEkwipunek();
                        break;
                    case "4":
                        _bohater.PokazStatystyki();
                        Console.WriteLine("\nWciśnij dowolny przycisk aby kontynuować");
                        Console.ReadKey();
                        break;
                    case "0":
                        return;
                }
            }
            Console.WriteLine("Twój bohater poległ... Koniec gry!");
            Console.WriteLine("\nWciśnij dowolny przycisk aby kontynuować");
            Console.ReadKey();
        }

        /// <summary>
        /// Obsługuje eksplorację lokacji – losuje spotkanie z wrogiem lub przedmiotem.
        /// </summary>
        static void Eksploruj()
        {
            Console.Clear();
            Console.WriteLine("Wybierz lokację:");
            for (int i = 0; i < _lokacje.Count; i++)
                Console.WriteLine($"{i + 1}. {_lokacje[i].Nazwa} – {_lokacje[i].Opis}");

            Console.Write("Wybór: ");
            if (!int.TryParse(Console.ReadLine(), out int wybor) || wybor < 1 || wybor > _lokacje.Count)
            {
                Console.WriteLine("Zła opcja!");
                Console.ReadKey();
                return;
            }

            Lokacja lokacja = _lokacje[wybor - 1];
            object spotkanie = lokacja.LosujSpotkanie();

            if (spotkanie is Wrog wrog)
            {
                Console.WriteLine($"\nNapotkałeś: {wrog.Nazwa}!");
                Console.WriteLine("\nWciśnij dowolny przycisk aby kontynuować");
                Console.ReadKey();
                ProwadzWalke(wrog);
            }
            else if (spotkanie is Przedmiot przedmiot)
            {
                Console.WriteLine($"\nZnalazłeś: {przedmiot.Nazwa}!");
                if (_bohater.DodajPrzedmiot(przedmiot))
                {
                    Console.WriteLine("Dodano do ekwipunku!");
                    Statystyki.ZebranePrzedmioty++;
                }
                else Console.WriteLine("Ekwipunek pełny!");
                Console.WriteLine("\nWciśnij dowolny przycisk aby kontynuować");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("\nNic tu nie ma...");
                Console.WriteLine("\nWciśnij dowolny przycisk aby kontynuować");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Prowadzi turową walkę między bohaterem a wrogiem.
        /// </summary>
        /// <param name="wrog">Wróg z którym bohater walczy.</param>
        static void ProwadzWalke(Wrog wrog)
        {
            Walka walka = new Walka(_bohater, wrog);
            Statystyki.LiczbaWalk++;

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"⚔  WALKA: {_bohater.Imie} vs {wrog.Nazwa}");
                Console.WriteLine($"\nTwoje HP: {_bohater.Zdrowie}/{_bohater.MaxZdrowie}");
                Console.WriteLine($"HP wroga: {wrog.Zdrowie}/{wrog.MaxZdrowie}");
                Console.WriteLine("\n1. Atak");
                Console.WriteLine("2. Użyj przedmiot");
                Console.WriteLine("3. Ucieknij");
                Console.Write("Wybór: ");

                AkcjaGracza akcja;
                switch (Console.ReadLine())
                {
                    case "2": akcja = AkcjaGracza.UzyjPrzedmiot; break;
                    case "3": akcja = AkcjaGracza.Ucieknij; break;
                    default: akcja = AkcjaGracza.Atak; break;
                }

                if (akcja == AkcjaGracza.UzyjPrzedmiot)
                {
                    var ekwipunek = _bohater.PobierzEkwipunek();
                    if (ekwipunek.Count == 0)
                    {
                        Console.WriteLine("\nNie masz żadnych przedmiotów!");
                        Console.ReadKey();
                        continue;
                    }

                    Console.WriteLine("\n=== Ekwipunek ===");
                    for (int i = 0; i < ekwipunek.Count; i++)
                        Console.WriteLine($"{i + 1}. {ekwipunek[i]}");

                    Console.Write("\nWybierz numer przedmiotu (0 aby anulować): ");
                    if (int.TryParse(Console.ReadLine(), out int indexP) && indexP > 0)
                    {
                        _bohater.UzyjPrzedmiot(indexP - 1);
                        Console.WriteLine($"Twoje HP po użyciu: {_bohater.Zdrowie}/{_bohater.MaxZdrowie}");
                    }
                    else
                    {
                        Console.WriteLine("Anulowano!");
                        Console.ReadKey();
                        continue;
                    }

                    int obrazeniaWr = wrog.AtakujBohatera(_bohater);
                    Console.WriteLine($"\nWróg atakuje! Zadaje {obrazeniaWr} obrażeń!");
                    Console.WriteLine($"Twoje HP: {_bohater.Zdrowie}/{_bohater.MaxZdrowie}");
                    if (!_bohater.CzyZyje())
                    {
                        Console.WriteLine("\nPoległeś...");
                        Console.ReadKey();
                        return;
                    }
                    Console.WriteLine("\nWciśnij dowolny przycisk aby kontynuować");
                    Console.ReadKey();
                    continue;
                }

                WynikTury wynik = walka.WykonajTure(akcja);
                Console.WriteLine($"\n{wynik.Opis}");

                if (!wynik.WalkaTrwa)
                {
                    if (wynik.BohaterWygral)
                    {
                        Console.WriteLine($"\nWygrałeś! +{wynik.ZdobyteXP} XP, +{wynik.ZdobyteZloto} złota!");
                        _bohater.DodajDoswiadczenie(wynik.ZdobyteXP);
                        _bohater.DodajZloto(wynik.ZdobyteZloto);
                        Statystyki.LiczbaZabitegoWrogow++;
                    }
                    else
                    {
                        Console.WriteLine("\nPrzegrałeś...");
                    }
                    Console.WriteLine("\nWciśnij dowolny przycisk aby kontynuować");
                    Console.ReadKey();
                    return;
                }
                Console.WriteLine("\nWciśnij dowolny przycisk aby kontynuować");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Obsługuje interfejs sklepu – wyświetla ofertę i umożliwia zakup.
        /// </summary>
        static void OtworzSklep()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Złoto: {_bohater.Zloto}");
                Console.WriteLine($"=== {_sklep.Nazwa} ===");
                foreach (string towar in _sklep.PokazOferte())
                {
                    Console.WriteLine(towar);
                }
                Console.WriteLine("\nWybierz numer przedmiotu lub 0 aby wyjść:");
                Console.Write("Wybór: ");

                if (!int.TryParse(Console.ReadLine(), out int wybor)) continue;
                if (wybor == 0) return;

                if (_sklep.Kup(_bohater, wybor - 1) && _bohater.PobierzEkwipunek().Count+1<=Konfiguracja.MaksymalnyEkwipunek)
                    Console.WriteLine("Zakup udany!");
                else
                    Console.WriteLine("Za mało złota / zły numer! / za mało miejsca w ekwipunku");
                Console.WriteLine("\nWciśnij dowolny przycisk aby kontynuować");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Wyświetla ekwipunek bohatera i umożliwia użycie przedmiotu.
        /// </summary>
        static void PokazEkwipunek()
        {
            Console.Clear();
            var ekwipunek = _bohater.PobierzEkwipunek();
            if (ekwipunek.Count == 0)
            {
                Console.WriteLine("Ekwipunek jest pusty!");
                Console.WriteLine("\nWciśnij dowolny przycisk aby kontynuować");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("=== Ekwipunek ===");
            for (int i = 0; i < ekwipunek.Count; i++)
                Console.WriteLine($"{i + 1}. {ekwipunek[i]}");

            Console.Write("\nWybierz numer aby użyć lub 0 aby wyjść: ");
            if (int.TryParse(Console.ReadLine(), out int wybor) && wybor > 0)
                _bohater.UzyjPrzedmiot(wybor - 1);

            Console.WriteLine("\nWciśnij dowolny przycisk aby kontynuować");
            Console.ReadKey();
        }

        /// <summary>
        /// Tworzy i wypełnia listę lokacji dostępnych w grze.
        /// </summary>
        static void ZainicjujLokacje()
        {
            _lokacje = new List<Lokacja>();

            Lokacja biblioteka = new Lokacja("📚 Biblioteka Zakazana", "Ciemne korytarze pełne ksiąg");
            biblioteka.DodajWroga(new Wrog("👺 Stary Goblin", "Goblin", 30, 8, 3, 20, 10, TrudnoscWroga.Latwy));
            biblioteka.DodajWroga(new Wrog("🐀 Szczur Archiwalny", "Szczur", 20, 6, 2, 15, 5, TrudnoscWroga.Latwy));
            biblioteka.DodajPrzedmiot(new Przedmiot("🧪 Stara Mikstura", TypPrzedmiotu.Mikstura, 0, "Leczy 30 HP"));
            _lokacje.Add(biblioteka);

            Lokacja sala = new Lokacja("⚗️ Sala Prób", "Ognista arena egzaminów");
            sala.DodajWroga(new Wrog("🔥 Ognisty Elemental", "Elemental", 60, 18, 8, 45, 30, TrudnoscWroga.Sredni));
            sala.DodajWroga(new Wrog("🧌 Troll Egzaminacyjny", "Troll", 80, 22, 10, 55, 40, TrudnoscWroga.Sredni));
            _lokacje.Add(sala);

            Lokacja laboratorium = new Lokacja("🧫 Laboratorium", "Bulgoczące mikstury i dziwne zapachy");
            laboratorium.DodajWroga(new Wrog("🦍 Golem Alchemiczny", "Golem", 70, 20, 12, 50, 35, TrudnoscWroga.Sredni));
            laboratorium.DodajPrzedmiot(new Przedmiot("🍵 Mikstura Mocy", TypPrzedmiotu.Mikstura, 0, "Leczy 30 HP"));
            _lokacje.Add(laboratorium);

            Lokacja wieza = new Lokacja("🗼 Wieża Mistrza", "Siedziba najpotężniejszego maga");
            wieza.DodajWroga(new Wrog("👻 Duch Oblany Student", "Duch", 120, 30, 15, 100, 80, TrudnoscWroga.Trudny));
            _lokacje.Add(wieza);
        }
    }
}