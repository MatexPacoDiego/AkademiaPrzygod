# ⚔️ Akademia Przygód

Tekstowa gra RPG napisana w C# jako projekt szkolny z programowania obiektowego.
Dostępna w dwóch wersjach – konsolowej i graficznej (WPF).

---

## 📋 Opis projektu

Gracz wciela się w ucznia Akademii Magii, który musi przejść przez cztery lokacje,
pokonać wrogów, zdobywać doświadczenie i awansować na kolejne poziomy.
Po drodze można zbierać przedmioty, korzystać ze sklepu i zarządzać ekwipunkiem.

---

## 🎮 Funkcje gry

- **Trzy klasy postaci** – Wojownik, Mag, Łotrzyk (każda z innymi statystykami)
- **Turowy system walki** – Atak, Użyj przedmiot, Ucieknij
- **Cztery lokacje** – Biblioteka Zakazana, Sala Prób, Laboratorium, Wieża Mistrza
- **System ekwipunku** – maksymalnie 10 przedmiotów
- **Sklep** – zakup mikstur, broni i zbroi za złoto
- **System poziomów** – awans co 100 XP (maksymalnie poziom 10)
- **Statystyki globalne** – liczba walk, zabitych wrogów, zebranych przedmiotów
- **Animacje walki** – w wersji WPF postacie poruszają się na Canvas
- **Muzyka w tle** – osobne utwory dla menu, walki i wygranej/przegranej

---

## 🧱 Zastosowane elementy OOP

| Element | Gdzie zastosowano |
|---|---|
| Klasy i obiekty | Bohater, Wrog, Przedmiot, Walka, Lokacja, Sklep |
| Właściwości (get/set) | Wszystkie pola klas udostępniane przez właściwości |
| Pola prywatne | `_zdrowie`, `_ekwipunek`, `_doswiadczenie` itp. |
| Modyfikator `private set` | Zdrowie, Poziom, Atak, Obrona bohatera |
| Pole `readonly` | `_nazwaGatunku` w klasie Wrog |
| Klasy statyczne | `Konfiguracja`, `Statystyki` |
| Enum | `TypPrzedmiotu`, `KlasaPostaci`, `TrudnoscWroga`, `AkcjaGracza` |
| Override ToString() | Bohater, Wrog, Przedmiot |
| Komentarze XML | Każda klasa i metoda publiczna |
| Konstruktory z parametrami | Wszystkie klasy domenowe |

---

## 🗺️ Lokacje i wrogowie

| Lokacja | Wrogowie | Trudność |
|---|---|---|
| 📚 Biblioteka Zakazana | Stary Goblin, Szczur Archiwalny | Łatwy |
| ⚗️ Sala Prób | Ognisty Elemental, Troll Egzaminacyjny | Średni |
| 🧫 Laboratorium | Golem Alchemiczny | Średni |
| 🗼 Wieża Mistrza | Duch Oblany Student | Trudny |

---

## 🚀 Jak uruchomić

### Wersja konsolowa
1. Otwórz `AkademiaPrzygod.sln` w Visual Studio
2. Ustaw `AkademiaPrzygod.Console` jako projekt startowy
3. Naciśnij `F5`

### Wersja WPF
1. Otwórz `AkademiaPrzygod.sln` w Visual Studio
2. Ustaw `AkademiaPrzygod.WPF` jako projekt startowy
3. Upewnij się że pliki muzyczne (`gra.mp3`, `walka.mp3`, `win.mp3`, `defeat.mp3`)
   znajdują się w folderze `bin/Debug`
4. Naciśnij `F5`

---

## ⚙️ Wymagania

- Visual Studio 2019 lub nowszy
- .NET Framework 4.7.2
- Windows (wymagane przez WPF)

---

## 📁 Pliki muzyczne

Gra wymaga czterech plików MP3 w folderze `bin/Debug` projektu WPF:

| Plik | Kiedy gra |
|---|---|
| `gra.mp3` | Menu główne i eksploracja |
| `walka.mp3` | Podczas walki |
| `win.mp3` | Po wygranej walce |
| `defeat.mp3` | Po przegranej lub ucieczce |

---

## 👨‍💻 Autor

Mateusz Urbański 3TEP
