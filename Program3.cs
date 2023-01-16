using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace symms_wersja2
{
    internal class Program
    {
        public const string NazwaPliku = "baza_danych.csv";
        public static List<Statystyki> statystykiDruzyn = new List<Statystyki>();
        public static bool CzyPozostac = true;

        public struct Statystyki
        {
            public string Reprezentacja;
            public double Posiadanie_pilki;
            public double Bramki_zdobyte;
            public double Bramki_stracone;
            public double Strzaly;
            public double Strzaly_celne;
            public double Rzuty_rozne;

            public void WyswietlDane()
            {
                Console.WriteLine($"\nPosiadanie pilki: {Posiadanie_pilki} \nBramki zdobyte: {Bramki_zdobyte} \nBramki stracone: {Bramki_stracone} " +
                    $"\nStrzaly: {Strzaly} \nStrzaly celne: {Strzaly_celne} \nRzuty rozne: {Rzuty_rozne}");
            }

            public void OdczytajDane(string[] elementy)
            {
                Reprezentacja = elementy[0];
                Posiadanie_pilki = double.Parse(elementy[1]);
                Bramki_zdobyte = double.Parse(elementy[2]);
                Bramki_stracone = double.Parse(elementy[3]);
                Strzaly = double.Parse(elementy[4]);
                Strzaly_celne = double.Parse(elementy[5]);
                Rzuty_rozne = double.Parse(elementy[6]);
            }

            public string WygenerujDane()
            {
                return $"{Posiadanie_pilki};{Bramki_zdobyte};{Bramki_stracone};{Strzaly};{Strzaly_celne};{Rzuty_rozne}";
            }
        }

        static char MenuGlowne()
        {
            //Zwykle menu glowne, wybieramy co chcemy zrobic
            Console.WriteLine("*** Menu Glowne ***");
            Console.WriteLine("1. Wyswietlanie statystyk reprezentacji \n2. Symulacja meczu wybranych druzyn \n3. Symulacja Mistrzostw Swiata 2022 " +
                "\n4. Symulacja Mistrzostw Swiata (losowe grupy) \n0. Opuszczanie programu");

            char opcja = Console.ReadKey(true).KeyChar;

            return opcja;
        }

        static public void OdczytajPlik()
        {
            if (File.Exists(NazwaPliku))
            {

                using (StreamReader plik = new StreamReader(NazwaPliku))
                {

                    int licznikLinijek = 0;

                    while (plik.EndOfStream == false)
                    {
                        string[] elementy = plik.ReadLine().Split(';');
                        if (licznikLinijek > 0)
                        {
                            Statystyki statystyka = new Statystyki();
                            statystyka.OdczytajDane(elementy);
                            statystykiDruzyn.Add(statystyka);
                        }
                        licznikLinijek++;
                    }
                }
            }
        }

        static void Wariant1()
        {
            //Musimy wpisac nazwe reprezentacji ktorej statystyki chcemy wyswietlic

            Console.Write("\nWpisz nazwe reprezentacji: ");
            string nazwaDruzyny = Console.ReadLine().ToUpper();

            foreach (var statystyka in statystykiDruzyn)
            {
                if (statystyka.Reprezentacja.ToUpper() == nazwaDruzyny)
                    statystyka.WyswietlDane();
            }

            Console.WriteLine("\nŚrednie statystyki z ostatnich 10 meczy\n\n");
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
            OdczytajPlik();
            Console.WriteLine("Witamy w symulatorze!\n\n");
            

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
                        Console.CursorVisible = true;
                        Wariant1();
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

