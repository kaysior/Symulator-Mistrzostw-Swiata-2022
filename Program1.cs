using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Sym_MS
{
    internal class Program
    {
        //public static bool CzyPozostac = true;

        /*
        struct Druzyna
        {
            public const string REPREZENTACJA = "Reprezentacja";

            public string Reprezentacja;
        }

        struct Statystyki
        {
            public const string POSIADANIE_PILKI = "Posiadanie_pilki";
            public const string BRAMKI_ZDOBYTE = "Bramki_zdobyte";
            public const string BRAMKI_STRACONE = "Bramki_stracone";
            public const string STRZALY = "Strzaly";
            public const string STRZALY_CELNE = "Strzaly_celne";
            public const string RZUTY_ROZNE = "Rzuty_rozne";

            public string Posiadanie_pilki;
            public string Bramki_zdobyte;
            public string Bramki_stracone;
            public string Strzaly;
            public string Strzaly_celne;
            public string Rzuty_rozne;

            public void WyswietlDane()
            {
                Console.WriteLine($"Posiadanie pilki:{Posiadanie_pilki} Bramki zdobyte:{Bramki_zdobyte} Bramki stracone:{Bramki_stracone} " +
                    $"Strzaly:{Strzaly} Strzaly celne: {Strzaly_celne} Rzuty rozne: {Rzuty_rozne}");
            }

            public void OdczytajDane(string[] elementy)
            {
                foreach (var element in elementy)
                {
                    
                    var Element = element;
                    string wartosc = Element;

                    if (Element == POSIADANIE_PILKI)
                    {
                        Posiadanie_pilki = wartosc;
                    }
                    else if (Element == BRAMKI_ZDOBYTE)
                    {
                        Bramki_zdobyte = wartosc;
                    }
                    else if (Element == BRAMKI_STRACONE)
                    {
                        Bramki_stracone = wartosc;
                    }
                    else if (Element == STRZALY)
                    {
                        Strzaly = wartosc;
                    }
                    else if (Element == STRZALY_CELNE)
                    {
                        Strzaly_celne = wartosc;
                    }
                    else if (Element == RZUTY_ROZNE)
                    {
                        Rzuty_rozne = wartosc;
                    }

                }
            }

            public string WygenerujDane()
            {
                return $"{POSIADANIE_PILKI}={Posiadanie_pilki};{BRAMKI_ZDOBYTE}={Bramki_zdobyte};{BRAMKI_STRACONE}={Bramki_stracone};{STRZALY}={Strzaly}" +
                    $"{STRZALY_CELNE}={Strzaly_celne};{RZUTY_ROZNE}={Rzuty_rozne};";
            }
        }
         */

        static char MenuGlowne()
        {
            //Zwykle menu glowne, wybieramy co chcemy zrobic
            Console.WriteLine("*** Menu Glowne ***");
            Console.WriteLine("1. Wyswietlanie statystyk reprezentacji \n2. Symulacja meczu wybranych druzyn \n3. Symulacja Mistrzostw Swiata 2022 " +
                "\n4. Symulacja Mistrzostw Swiata (losowe grupy) \n0. Opuszczanie programu");

            char opcja = Console.ReadKey(true).KeyChar;

            return opcja;
        }

        static char Wariant1()
        {
            Console.WriteLine("*** Wybierz reprezentacje ***");

            //Musimy wybrac reprezentacje ktorej statystyki chcemy wyswietlic
            //Console.WriteLine($"1. {Reprezentacje[0]} \n2.  \n3.  \n4.  \n5.  \n6. ");

            char opcja = Console.ReadKey(true).KeyChar;

            return opcja;
        }

        static char Wariant2()
        {
            //Wyswietlamy wszystkie druzyny
            //Wybieramy 2 druzyny ktorych symulacje meczu chcemy przeprowadzic
            //Wyswietlamy statystyki wybranych druzyn
            //Przeprowadzamy symulacje meczu, np tworzac osobna funkcje SymulujMecz() - na podstawie tego wzoru co widzielismy
            Console.WriteLine("*** Wybierz wariant ***");
            Console.WriteLine("1. \n2.  \n3.  \n4.  \n5.  \n6. ");

            char opcja = Console.ReadKey(true).KeyChar;

            return opcja;
        }

        static char Wariant3()
        {
            //Tutaj musi byc funkcja symulacji calych mistrzostw swiata
            //Musimy podzielic druzyny na takie grupy jakie byly normalnie na MŚ
            //Przeprowadzamy symulacje poszczegolnych grup, na podstawie funkcji SymulujMecz() ktora przeprowadzi symulacje w tle i wyswietli tylko wyniki w grupach
            //Losujemy drabinke turniejowa dla druzyn ktore wyszly z grup
            //Przeprowadzamy symulacje meczy i wylaniamy zwyciezce calych MŚ
            Console.WriteLine("*** Wybierz wariant ***");
            Console.WriteLine("1. \n2.  \n3.  \n4.  \n5.  \n6. ");

            char opcja = Console.ReadKey(true).KeyChar;

            return opcja;
        }

        static char Wariant4()
        {
            //Program przeprowadza losowanie grup ze wszystkich reprezentacji
            //Przeprowadzamy symulacje poszczegolnych grup, na podstawie funkcji SymulujMecz() ktora przeprowadzi symulacje w tle i wyswietli tylko wyniki w grupach
            //Losujemy drabinke turniejowa dla druzyn ktore wyszly z grup
            //Przeprowadzamy symulacje meczy i wylaniamy zwyciezce calych MŚ
            Console.WriteLine("*** Wybierz wariant ***");
            Console.WriteLine("1. \n2.  \n3.  \n4.  \n5.  \n6. ");

            char opcja = Console.ReadKey(true).KeyChar;

            return opcja;

        }
        static void Main(string[] args)
        {
            Console.WriteLine("Witamy w symulatorze!\n\n");

            //Podpiecie pliku
            /*
            List<Druzyna> listaDruzyn = new List<Druzyna>();

            if (File.Exists("bazadanych.csv"))
            {

                using (StreamReader plik = new StreamReader("bazadanych.csv"))
                {

                    while (plik.EndOfStream == false)
                    {
                        string[] elementy = plik.ReadLine().Split(';');
                    }
                }
            }

            using (StreamWriter plik = new StreamWriter("bazadanych.csv"))
            {
                foreach (var druzyna in listaDruzyn)
                {
                    plik.WriteLine(druzyna.WygenerujDane());
                }
            }
            */

            Console.CursorVisible = false;
            bool CzyKontynuowacMenu = true;
            while (CzyKontynuowacMenu)
            {
                switch (MenuGlowne())
                {
                    case '1':
                        Console.Clear();
                        Console.Write("Wybrano opcje: ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Wyswietlanie statystyk reprezentacji");
                        Console.ResetColor();

                        //Wariant1();

                        break;

                    case '2':
                        Console.Clear();
                        Console.Write("Wybrano opcje: ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Symulacja meczu wybranych druzyn");
                        Console.ResetColor();

                        //Wariant2();

                        break;

                    case '3':
                        Console.Clear();
                        Console.Write("Wybrano opcje: ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Symulacja Mistrzostw Swiata 2022");
                        Console.ResetColor();

                        //Wariant3();

                        break;

                    case '4':
                        Console.Clear();
                        Console.Write("Wybrano opcje: ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Symulacja Mistrzostw Swiata (losowe grupy)");
                        Console.ResetColor();

                        //Wariant4();

                        break;

                    case '0':
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Opuszczanie programu...");
                        Console.ResetColor();
                        CzyKontynuowacMenu = false;
                        break;
                }
            }
            Console.CursorVisible = true;
        }
    }
}
