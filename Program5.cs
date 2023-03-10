using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml.Linq;
using static Sym_MS2022.Program;

namespace Sym_MS2022
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
                "\n0. Opuszczanie programu");

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

            Console.WriteLine("\nSrednie statystyki z ostatnich 10 meczy\n\n");
        }

        static char MenuWariant1()
        {
            Console.Write("*** Wybierz opcje ***");
            Console.WriteLine("\n 1. Wyswietl statystyki kolejnej reprezentacji \n 0. Powrot do menu programu\n");

            char opcja = Console.ReadKey(true).KeyChar;
            return opcja;
        }

        static void SymulujMecz(int druzynaA, int druzynaB)
        {
 
            //wzor na wspolczynnik
            //double wspolczynnik = (srednia ilosc goli * srednia ilosc strzalow celnych) / srednia ilosc bramek straconych


            //obliczamy wspolczynnik druzyny 1
            double wspolczynnik1 = (statystykiDruzyn[druzynaA].Bramki_zdobyte * statystykiDruzyn[druzynaA].Strzaly_celne) / statystykiDruzyn[druzynaA].Bramki_stracone;

            //obliczamy wspolczynnik druzyny 2
            double wspolczynnik2 = (statystykiDruzyn[druzynaB].Bramki_zdobyte * statystykiDruzyn[druzynaB].Strzaly_celne) / statystykiDruzyn[druzynaB].Bramki_stracone;


            //porownujemy wspolczynnik druzyny 1 do wspolczynnika druzyny 2 i wylaniamy zwyciezce
            if (wspolczynnik1 > wspolczynnik2)
            {
                Console.WriteLine($"Wygrana druzyny {statystykiDruzyn[druzynaA].Reprezentacja}");
            } else
            {
                Console.WriteLine($"Wygrana druzyny {statystykiDruzyn[druzynaB].Reprezentacja}");
            }
        }

        static void Wariant2()
        {
            //Wyswietlanie reprezentacji z numeracja
            int numerDruzyny = 0;

            foreach (var statystyka in statystykiDruzyn)
            {
                Console.WriteLine($"{numerDruzyny + 1} {statystyka.Reprezentacja}");
                numerDruzyny++;
            }


            //Wybieramy 2 druzyny ktorych symulacje meczu chcemy przeprowadzic
            int numerDruzyny1 = Toolbox.WprowadzInt("\nWprowadz numer pierwszej druzyny: ",1, statystykiDruzyn.Count);
            int numerDruzyny2 = Toolbox.WprowadzInt("\nWprowadz numer drugiej druzyny: ", 1, statystykiDruzyn.Count);


            //Wyswietlamy statystyki wybranych druzyn
            Console.WriteLine($"Statystyki drużyny {statystykiDruzyn[numerDruzyny1 - 1].Reprezentacja}");
            statystykiDruzyn[numerDruzyny1 - 1].WyswietlDane();

            Console.WriteLine($"Statystyki drużyny {statystykiDruzyn[numerDruzyny2 - 1].Reprezentacja}");
            statystykiDruzyn[numerDruzyny2 - 1].WyswietlDane();


            //Przeprowadzamy symulacje meczu na podstawie funkcji SymulujMecz()
            SymulujMecz(numerDruzyny1 - 1, numerDruzyny2 - 1);
        }

        static char MenuWariant2()
        {
            Console.Write("*** Wybierz opcje ***");
            Console.WriteLine("\n 1. Przeprowadz symulacje kolejnego meczu \n 0. Powrot do menu programu\n");

            char opcja = Console.ReadKey(true).KeyChar;
            return opcja;
        }

        static void Wariant3()
        {
            //Tutaj musi byc funkcja symulacji calych mistrzostw swiata
            //Musimy podzielic druzyny na takie grupy jakie byly normalnie na MŚ
            //Przeprowadzamy symulacje poszczegolnych grup, na podstawie funkcji SymulujMecz() ktora przeprowadzi symulacje w tle i wyswietli tylko wyniki w grupach
            //Losujemy drabinke turniejowa dla druzyn ktore wyszly z grup
            //Przeprowadzamy symulacje meczy i wylaniamy zwyciezce calych MŚ

            Console.WriteLine("Tu bedzie symulacja MS 2022");

        }

        static char MenuWariant3()
        {
            Console.WriteLine();
            Console.Write("*** Wybierz opcje ***");
            Console.WriteLine("\n0. Powrot do menu programu");

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
                        CzyPozostac = true;
                        Console.Clear();
                        Console.Write("Wybrano opcje: ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Wyswietlanie statystyk reprezentacji");
                        Console.ResetColor();
                        Console.CursorVisible = true;

                        //Wyswietlanie reprezentacji
                        Console.WriteLine();
                        foreach (var statystyka in statystykiDruzyn)
                        {
                            Console.WriteLine($"{statystyka.Reprezentacja}");
                        }

                        Wariant1();
                        while (CzyPozostac)
                        {
                            Console.CursorVisible = false;
                            switch (MenuWariant1())
                            {
                                case '1':
                                    Console.CursorVisible = true;
                                    Console.Write("Wybrano opcje: ");
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Wyswietlanie statystyk reprezentacji");
                                    Console.ResetColor();
                                    Wariant1();
                                    break;
                                case '0':
                                    Console.Write("Powrot do menu programu...\n");
                                    Thread.Sleep(3000);
                                    CzyPozostac = false;
                                    Console.Clear();
                                    break;
                            }
                        }
                        break;

                    case '2':
                        CzyPozostac = true;
                        Console.Clear();
                        Console.Write("Wybrano opcje: ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Symulacja meczu wybranych druzyn");
                        Console.ResetColor();
                        Console.CursorVisible = true;
                        Wariant2();

                        while (CzyPozostac)
                        {
                            Console.CursorVisible = false;
                            switch (MenuWariant2())
                            {
                                case '1':
                                    Console.CursorVisible = true;
                                    Console.Write("Wybrano opcje: ");
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Symulacja meczu wybranych druzyn");
                                    Console.ResetColor();
                                    Wariant2();
                                    break;
                                case '0':
                                    Console.Write("Powrot do menu programu...\n");
                                    Thread.Sleep(3000);
                                    CzyPozostac = false;
                                    Console.Clear();
                                    break;
                            }
                        }
                        break;

                    case '3':
                        CzyPozostac = true;
                        Console.Clear();
                        Console.Write("Wybrano opcje: ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Symulacja Mistrzostw Swiata 2022");
                        Console.ResetColor();
                        Console.CursorVisible = true;
                        Wariant3();
                        while (CzyPozostac)
                        {
                            Console.CursorVisible = false;
                            switch (MenuWariant3())
                            {
                                case '0':
                                    Console.Write("Powrot do menu programu...\n");
                                    Thread.Sleep(3000);
                                    CzyPozostac = false;
                                    Console.Clear();
                                    break;
                            }
                        }
                        break;


                    case '0':
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

