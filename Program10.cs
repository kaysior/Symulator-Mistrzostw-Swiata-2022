using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;


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

            public void WyswietlReprezentacje()
            {
                Console.WriteLine($"{Reprezentacja}");
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
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    statystyka.WyswietlDane();
                    Console.ResetColor();
                }
                    
            }

            Console.Write("\nSrednie statystyki z ostatnich");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" 10 ");
            Console.ResetColor();
            Console.WriteLine("meczy\n\n");
        }


        static char MenuWariant1()
        {
            Console.Write("*** Wybierz opcje ***");
            Console.WriteLine("\n 1. Wyswietl statystyki kolejnej reprezentacji \n 0. Powrot do menu programu\n");

            char opcja = Console.ReadKey(true).KeyChar;
            return opcja;
        }


        static void Wariant2()
        {
            //Wyswietlanie reprezentacji z numeracja
            int numerDruzyny = 0;

            foreach (var statystyka in statystykiDruzyn)
            {
                Console.WriteLine($"{numerDruzyny + 1}. {statystyka.Reprezentacja}");
                numerDruzyny++;
            }


            //Wybieramy 2 druzyny ktorych symulacje meczu chcemy przeprowadzic
            int numerDruzyny1 = Toolbox.WprowadzInt("\nWprowadz numer pierwszej druzyny: ", 1, statystykiDruzyn.Count);
            int numerDruzyny2 = Toolbox.WprowadzInt("\nWprowadz numer drugiej druzyny: ", 1, statystykiDruzyn.Count);


            //Wyswietlamy statystyki wybranych druzyn
            Console.Write($"\n\nStatystyki drużyny ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{statystykiDruzyn[numerDruzyny1 - 1].Reprezentacja}");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            statystykiDruzyn[numerDruzyny1 - 1].WyswietlDane();
            Console.ResetColor();

            Console.Write($"\n\nStatystyki drużyny ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{statystykiDruzyn[numerDruzyny2 - 1].Reprezentacja}");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            statystykiDruzyn[numerDruzyny2 - 1].WyswietlDane();
            Console.ResetColor();


            //Przeprowadzamy symulacje meczu na podstawie funkcji SymulujMecz()
            Console.WriteLine();
            SymulujMecz(numerDruzyny1 - 1, numerDruzyny2 - 1);
            Console.WriteLine();
        }

        static void SymulujMecz(int druzynaA, int druzynaB)
        {
            //wzor na wspolczynnik
            //double wspolczynnik = (srednia ilosc goli * srednia ilosc strzalow celnych) / srednia ilosc bramek straconych

            //obliczamy wspolczynnik druzyny 1
            double wspolczynnik1 = (statystykiDruzyn[druzynaA].Bramki_zdobyte * statystykiDruzyn[druzynaA].Strzaly_celne) / statystykiDruzyn[druzynaA].Bramki_stracone;

            //obliczamy wspolczynnik druzyny 2
            double wspolczynnik2 = (statystykiDruzyn[druzynaB].Bramki_zdobyte * statystykiDruzyn[druzynaB].Strzaly_celne) / statystykiDruzyn[druzynaB].Bramki_stracone;


            string druzyna1 = statystykiDruzyn[druzynaA].Reprezentacja;
            string druzyna2 = statystykiDruzyn[druzynaB].Reprezentacja;

            //porownujemy wspolczynnik druzyny 1 do wspolczynnika druzyny 2 i wylaniamy zwyciezce
            if (wspolczynnik1 > wspolczynnik2)
            {

                Console.WriteLine($"{druzyna1} vs {druzyna2}");
                Console.Write($"Wygrana druzyny ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{druzyna1}");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine($"{druzyna1} vs {druzyna2}");
                Console.Write($"Wygrana druzyny {druzyna2}");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{druzyna2}");
                Console.ResetColor();
            }
        }


        static char MenuWariant2()
        {
            Console.Write("*** Wybierz opcje ***");
            Console.WriteLine("\n1. Przeprowadz symulacje kolejnego meczu \n0. Powrot do menu programu\n");

            char opcja = Console.ReadKey(true).KeyChar;
            return opcja;
        }


        static void SymulujGrupe(int druzyna1, int druzyna2, int druzyna3, int druzyna4)
        {
            //obliczamy wspolczynnik druzyny 1
            double wspolczynnik1 = (statystykiDruzyn[druzyna1].Bramki_zdobyte * statystykiDruzyn[druzyna1].Strzaly_celne) / statystykiDruzyn[druzyna1].Bramki_stracone;

            //obliczamy wspolczynnik druzyny 2
            double wspolczynnik2 = (statystykiDruzyn[druzyna2].Bramki_zdobyte * statystykiDruzyn[druzyna2].Strzaly_celne) / statystykiDruzyn[druzyna2].Bramki_stracone;

            //obliczamy wspolczynnik druzyny 3
            double wspolczynnik3 = (statystykiDruzyn[druzyna3].Bramki_zdobyte * statystykiDruzyn[druzyna3].Strzaly_celne) / statystykiDruzyn[druzyna3].Bramki_stracone;

            //obliczamy wspolczynnik druzyny 4
            double wspolczynnik4 = (statystykiDruzyn[druzyna4].Bramki_zdobyte * statystykiDruzyn[druzyna4].Strzaly_celne) / statystykiDruzyn[druzyna4].Bramki_stracone;


            string dr1 = statystykiDruzyn[druzyna1].Reprezentacja;
            string dr2 = statystykiDruzyn[druzyna2].Reprezentacja;
            string dr3 = statystykiDruzyn[druzyna3].Reprezentacja;
            string dr4 = statystykiDruzyn[druzyna4].Reprezentacja;

            int punkty1 = 0;
            int punkty2 = 0;
            int punkty3 = 0;
            int punkty4 = 0;

            //porownujemy wspolczynniki druzyn w grupie i wyswietlamy punkty
            if (wspolczynnik1 > wspolczynnik2)
            {

                Console.WriteLine($"{dr1} vs {dr2}");
                Console.Write($"Wygrana druzyny ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{dr1}\n");
                Console.ResetColor();
                punkty1 += 3;
            }
            else
            {

                Console.WriteLine($"{dr1} vs {dr2}");
                Console.Write($"Wygrana druzyny ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{dr2}\n");
                Console.ResetColor();
                punkty2 += 3;
            }

            if (wspolczynnik1 > wspolczynnik3)
            {

                Console.WriteLine($"{dr1} vs {dr3}");
                Console.Write($"Wygrana druzyny ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{dr1}\n");
                Console.ResetColor();
                punkty1 += 3;
            }
            else
            {

                Console.WriteLine($"{dr1} vs {dr3}");
                Console.Write($"Wygrana druzyny ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{dr3}\n");
                Console.ResetColor();
                punkty3 += 3;
            }

            if (wspolczynnik1 > wspolczynnik4)
            {

                Console.WriteLine($"{dr1} vs {dr4}");
                Console.Write($"Wygrana druzyny ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{dr1}\n");
                Console.ResetColor();
                punkty1 += 3;
            }
            else
            {

                Console.WriteLine($"{dr1} vs {dr4}");
                Console.Write($"Wygrana druzyny ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{dr4}\n");
                Console.ResetColor();
                punkty4 += 3;
            }

            if (wspolczynnik3 > wspolczynnik4)
            {

                Console.WriteLine($"{dr3} vs {dr4}");
                Console.Write($"Wygrana druzyny ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{dr3}\n");
                Console.ResetColor();
                punkty3 += 3;
            }
            else
            {

                Console.WriteLine($"{dr3} vs {dr4}");
                Console.Write($"Wygrana druzyny ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{dr4}\n");
                Console.ResetColor();
                punkty4 += 3;
            }

            if (wspolczynnik2 > wspolczynnik4)
            {

                Console.WriteLine($"{dr2} vs {dr4}");
                Console.Write($"Wygrana druzyny ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{dr2}\n");
                Console.ResetColor();
                punkty2 += 3;
            }
            else
            {

                Console.WriteLine($"{dr2} vs {dr4}");
                Console.Write($"Wygrana druzyny ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{dr4}\n");
                Console.ResetColor();
                punkty4 += 3;
            }

            if (wspolczynnik2 > wspolczynnik3)
            {

                Console.WriteLine($"{dr2} vs {dr3}");
                Console.Write($"Wygrana druzyny ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{dr2}\n");
                Console.ResetColor();
                punkty2 += 3;
            }
            else
            {

                Console.WriteLine($"{dr2} vs {dr3}");
                Console.Write($"Wygrana druzyny ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{dr3}\n");
                Console.ResetColor();
                punkty3 += 3;
            }

            //tabela niesortowana
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nTabela punktow");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{dr1} {punkty1}");
            Console.WriteLine($"{dr2} {punkty2}");
            Console.WriteLine($"{dr3} {punkty3}");
            Console.WriteLine($"{dr4} {punkty4}");
            Console.ResetColor();

            //FAZA PUCHAROWA


            Console.WriteLine();

            //1,2
            if (punkty1 > punkty2 && punkty1 > punkty3 && punkty1 > punkty4 && punkty2 > punkty3 && punkty2 > punkty4)
            {
                Console.Write($"Awans druzyny ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"{dr1}");
                Console.ResetColor();
                Console.Write($"Awans druzyny ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"{dr2}");
                Console.ResetColor();
            }


            //2,1
            if (punkty2 > punkty1 && punkty2 > punkty3 && punkty2 > punkty4 && punkty1 > punkty3 && punkty1 > punkty4)
            {
                Console.Write($"Awans druzyny ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"{dr2}");
                Console.ResetColor();
                Console.Write($"Awans druzyny ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"{dr1}");
                Console.ResetColor();

            }


            //1,3
            if (punkty1 > punkty2 && punkty1 > punkty3 && punkty1 > punkty4 && punkty3 > punkty2 && punkty3 > punkty4)
            {
                Console.Write($"Awans druzyny ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"{dr1}");
                Console.ResetColor();
                Console.Write($"Awans druzyny ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"{dr3}");
                Console.ResetColor();
            }

            //3,1
            if (punkty3 > punkty1 && punkty3 > punkty2 && punkty3 > punkty4 && punkty1 > punkty2 && punkty1 > punkty4)
            {
                Console.Write($"Awans druzyny ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"{dr3}");
                Console.ResetColor();
                Console.Write($"Awans druzyny ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"{dr1}");
                Console.ResetColor();
            }


            //1,4
            if (punkty1 > punkty2 && punkty1 > punkty3 && punkty1 > punkty4 && punkty4 > punkty2 && punkty4 > punkty3)
            {
                Console.Write($"Awans druzyny ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"{dr1}");
                Console.ResetColor();
                Console.Write($"Awans druzyny ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"{dr4}");
                Console.ResetColor();
            }


            //4,1
            if (punkty4 > punkty1 && punkty4 > punkty2 && punkty4 > punkty3 && punkty1 > punkty2 && punkty1 > punkty3)
            {
                Console.Write($"Awans druzyny ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"{dr4}");
                Console.ResetColor();
                Console.Write($"Awans druzyny ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"{dr1}");
                Console.ResetColor();
            }


            //2,3
            if (punkty2 > punkty1 && punkty2 > punkty3 && punkty2 > punkty4 && punkty3 > punkty1 && punkty3 > punkty4)
            {
                Console.Write($"Awans druzyny ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"{dr2}");
                Console.ResetColor();
                Console.Write($"Awans druzyny ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"{dr3}");
                Console.ResetColor();
            }


            //3,2
            if (punkty3 > punkty1 && punkty3 > punkty2 && punkty3 > punkty4 && punkty2 > punkty1 && punkty2 > punkty4)
            {
                Console.Write($"Awans druzyny ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"{dr3}");
                Console.ResetColor();
                Console.Write($"Awans druzyny ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"{dr2}");
                Console.ResetColor();
            }


            //2,4
            if (punkty2 > punkty1 && punkty2 > punkty3 && punkty2 > punkty4 && punkty4 > punkty1 && punkty4 > punkty3)
            {
                Console.Write($"Awans druzyny ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"{dr2}");
                Console.ResetColor();
                Console.Write($"Awans druzyny ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"{dr4}");
                Console.ResetColor();
            }


            //4,2
            if (punkty4 > punkty1 && punkty4 > punkty2 && punkty4 > punkty3 && punkty2 > punkty1 && punkty2 > punkty3)
            {
                Console.Write($"Awans druzyny ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"{dr4}");
                Console.ResetColor();
                Console.Write($"Awans druzyny ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"{dr2}");
                Console.ResetColor();
            }


            //3,4
            if (punkty3 > punkty1 && punkty3 > punkty2 && punkty3 > punkty4 && punkty4 > punkty1 && punkty4 > punkty2)
            {
                Console.Write($"Awans druzyny ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"{dr3}");
                Console.ResetColor();
                Console.Write($"Awans druzyny ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"{dr4}");
                Console.ResetColor();
            }


            //4,3
            if (punkty4 > punkty1 && punkty4 > punkty2 && punkty4 > punkty3 && punkty3 > punkty1 && punkty3 > punkty2)
            {
                Console.Write($"Awans druzyny ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"{dr4}");
                Console.ResetColor();
                Console.Write($"Awans druzyny ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"{dr3}");
                Console.ResetColor();
            }

        }
   

        static void MeczeGrupowe()
        {
            //Wewnetrzne przypisane numerow druzyn
            int druzynaA1 = 1;
            int druzynaA2 = 2;
            int druzynaA3 = 3;
            int druzynaA4 = 4;
            int druzynaB1 = 5;
            int druzynaB2 = 6;
            int druzynaB3 = 7;
            int druzynaB4 = 8;
            int druzynaC1 = 9;
            int druzynaC2 = 10;
            int druzynaC3 = 11;
            int druzynaC4 = 12;
            int druzynaD1 = 13;
            int druzynaD2 = 14;
            int druzynaD3 = 15;
            int druzynaD4 = 16;
            int druzynaE1 = 17;
            int druzynaE2 = 18;
            int druzynaE3 = 19;
            int druzynaE4 = 20;
            int druzynaF1 = 21;
            int druzynaF2 = 22;
            int druzynaF3 = 23;
            int druzynaF4 = 24;
            int druzynaG1 = 25;
            int druzynaG2 = 26;
            int druzynaG3 = 27;
            int druzynaG4 = 28;
            int druzynaH1 = 29;
            int druzynaH2 = 30;
            int druzynaH3 = 31;
            int druzynaH4 = 32;

            string symulacja1 = "\nTrwa symulacja grupy A...\n";
            Console.ForegroundColor = ConsoleColor.Yellow;
            Toolbox.Pisanie(symulacja1);
            Console.ResetColor();
            Console.CursorVisible = false;
            Thread.Sleep(5000);
            Console.CursorVisible = true;

            //Mecze grupy A
            Console.WriteLine("\nMecze grupy A\n");
            SymulujGrupe(druzynaA1 - 1, druzynaA2 - 1, druzynaA3 - 1, druzynaA4 - 1);

            string symulacja2 = "\nTrwa symulacja grupy B...\n";
            Console.ForegroundColor = ConsoleColor.Yellow;
            Toolbox.Pisanie(symulacja2);
            Console.ResetColor();
            Console.CursorVisible = false;
            Thread.Sleep(5000);
            Console.CursorVisible = true;

            //Mecze grupy B
            Console.WriteLine("\nMecze grupy B\n");
            SymulujGrupe(druzynaB1 - 1, druzynaB2 - 1, druzynaB3 - 1, druzynaB4 - 1);

            string symulacja3 = "\nTrwa symulacja grupy C...\n";
            Console.ForegroundColor = ConsoleColor.Yellow;
            Toolbox.Pisanie(symulacja3);
            Console.ResetColor();
            Console.CursorVisible = false;
            Thread.Sleep(5000);
            Console.CursorVisible = true;

            //Mecze grupy C
            Console.WriteLine("\nMecze grupy C\n");
            SymulujGrupe(druzynaC1 - 1, druzynaC2 - 1, druzynaC3 - 1, druzynaC4 - 1);

            string symulacja4 = "\nTrwa symulacja grupy D...\n";
            Console.ForegroundColor = ConsoleColor.Yellow;
            Toolbox.Pisanie(symulacja4);
            Console.ResetColor();
            Console.CursorVisible = false;
            Thread.Sleep(5000);
            Console.CursorVisible = true;

            //Mecze grupy D
            Console.WriteLine("\nMecze grupy D\n");
            SymulujGrupe(druzynaD1 - 1, druzynaD2 - 1, druzynaD3 - 1, druzynaD4 - 1);

            string symulacja5 = "\nTrwa symulacja grupy E...\n";
            Console.ForegroundColor = ConsoleColor.Yellow;
            Toolbox.Pisanie(symulacja5);
            Console.ResetColor();
            Console.CursorVisible = false;
            Thread.Sleep(5000);
            Console.CursorVisible = true;

            //Mecze grupy E
            Console.WriteLine("\nMecze grupy E\n");
            SymulujGrupe(druzynaE1 - 1, druzynaE2 - 1, druzynaE3 - 1, druzynaE4 - 1);

            string symulacja6 = "\nTrwa symulacja grupy F...\n";
            Console.ForegroundColor = ConsoleColor.Yellow;
            Toolbox.Pisanie(symulacja6);
            Console.ResetColor();
            Console.CursorVisible = false;
            Thread.Sleep(5000);
            Console.CursorVisible = true;

            //Mecze grupy F
            Console.WriteLine("\nMecze grupy F\n");
            SymulujGrupe(druzynaF1 - 1, druzynaF2 - 1, druzynaF3 - 1, druzynaF4 - 1);

            string symulacja7 = "\nTrwa symulacja grupy G...\n";
            Console.ForegroundColor = ConsoleColor.Yellow;
            Toolbox.Pisanie(symulacja7);
            Console.ResetColor();
            Console.CursorVisible = false;
            Thread.Sleep(5000);
            Console.CursorVisible = true;

            //Mecze grupy G
            Console.WriteLine("\nMecze grupy G\n");
            SymulujGrupe(druzynaG1 - 1, druzynaG2 - 1, druzynaG3 - 1, druzynaG4 - 1);

            string symulacja8 = "\nTrwa symulacja grupy H...\n";
            Console.ForegroundColor = ConsoleColor.Yellow;
            Toolbox.Pisanie(symulacja8);
            Console.ResetColor();
            Console.CursorVisible = false;
            Thread.Sleep(5000);
            Console.CursorVisible = true;

            //Mecze grupy H
            Console.WriteLine("\nMecze grupy H\n");
            SymulujGrupe(druzynaH1 - 1, druzynaH2 - 1, druzynaH3 - 1, druzynaH4 - 1);
        }

        static void Wariant3()
        {
            //po wielu godzinach prob sprobowalismy najprostszym sposobem 32 ify i dzialalo
            //potem dalismy rade ale zostawiam na pamiatke
            /*
            int numerDruzyny = 0;

            foreach (var statystyka in statystykiDruzyn)
            {
                numerDruzyny++;

                if (numerDruzyny == 1)
                {
                    Console.WriteLine("Grupa A");
                    statystyka.WyswietlReprezentacje();
                } else if (numerDruzyny == 2)
                {
                    statystyka.WyswietlReprezentacje();
                }
                else if (numerDruzyny == 3)
                {
                    statystyka.WyswietlReprezentacje();
                }
                else if (numerDruzyny == 4)
                {
                    statystyka.WyswietlReprezentacje();
                }
                else if (numerDruzyny == 5)
                {
                    Console.WriteLine("\nGrupa B");
                    statystyka.WyswietlReprezentacje();
                }
                else if (numerDruzyny == 6)
                {
                    statystyka.WyswietlReprezentacje();
                }
                else if (numerDruzyny == 7)
                {
                    statystyka.WyswietlReprezentacje();
                }
                else if (numerDruzyny == 8)
                {
                    statystyka.WyswietlReprezentacje();
                }
                else if (numerDruzyny == 9)
                {
                    Console.WriteLine("\nGrupa C");
                    statystyka.WyswietlReprezentacje();
                }
                else if (numerDruzyny == 10)
                {
                    statystyka.WyswietlReprezentacje();
                }
                else if (numerDruzyny == 11)
                {
                    statystyka.WyswietlReprezentacje();
                }
                else if (numerDruzyny == 12)
                {
                    statystyka.WyswietlReprezentacje();
                }
                else if (numerDruzyny == 13)
                {
                    Console.WriteLine("\nGrupa D");
                    statystyka.WyswietlReprezentacje();
                }
                else if (numerDruzyny == 14)
                {
                    statystyka.WyswietlReprezentacje();
                }
                else if (numerDruzyny == 15)
                {
                    statystyka.WyswietlReprezentacje();
                }
                else if (numerDruzyny == 16)
                {
                    statystyka.WyswietlReprezentacje();
                }
                else if (numerDruzyny == 17)
                {
                    Console.WriteLine("\nGrupa E");
                    statystyka.WyswietlReprezentacje();
                }
                else if (numerDruzyny == 18)
                {
                    statystyka.WyswietlReprezentacje();
                }
                else if (numerDruzyny == 19)
                {
                    statystyka.WyswietlReprezentacje();
                }
                else if (numerDruzyny == 20)
                {
                    statystyka.WyswietlReprezentacje();
                }
                else if (numerDruzyny == 21)
                {
                    Console.WriteLine("\nGrupa F");
                    statystyka.WyswietlReprezentacje();
                }
                else if (numerDruzyny == 22)
                {
                    statystyka.WyswietlReprezentacje();
                }
                else if (numerDruzyny == 23)
                {
                    statystyka.WyswietlReprezentacje();
                }
                else if (numerDruzyny == 24)
                {
                    statystyka.WyswietlReprezentacje();
                }
                else if (numerDruzyny == 25)
                {
                    Console.WriteLine("\nGrupa G");
                    statystyka.WyswietlReprezentacje();
                }
                else if (numerDruzyny == 26)
                {
                    statystyka.WyswietlReprezentacje();
                }
                else if (numerDruzyny == 27)
                {
                    statystyka.WyswietlReprezentacje();
                }
                else if (numerDruzyny == 28)
                {
                    statystyka.WyswietlReprezentacje();
                }
                else if (numerDruzyny == 29)
                {
                    Console.WriteLine("\nGrupa H");
                    statystyka.WyswietlReprezentacje();
                }
                else if (numerDruzyny == 30)
                {
                    statystyka.WyswietlReprezentacje();
                }
                else if (numerDruzyny == 31)
                {
                    statystyka.WyswietlReprezentacje();
                }
                else if (numerDruzyny == 32)
                {
                    statystyka.WyswietlReprezentacje();
                }
            }
            */

            //Wypisanie grup MS 2022
            var i = 0;
            foreach (var statystyka in statystykiDruzyn)
            {
                if (i % 4 == 0)
                {
                    Console.Write("\nGrupa: ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(Convert.ToChar(65 + (i / 4)));
                    Console.ResetColor();
                    statystyka.WyswietlReprezentacje();
                } else
                {
                    statystyka.WyswietlReprezentacje();
                }
                i++;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nGrupy Mistrzostw Swiata 2022");
            Console.ResetColor();
        }


        static char MenuWariant3()
        {
            Console.WriteLine();
            Console.Write("*** Wybierz opcje ***");
            Console.WriteLine("\n1. Przeprowadz symulacje Mistrzostw Swiata 2022 \n0. Powrot do menu programu");

            char opcja = Console.ReadKey(true).KeyChar;
            return opcja;
        }


        static void Main(string[] args)
        {
            OdczytajPlik();
            Console.ForegroundColor = ConsoleColor.Yellow;
            string powitanie = "Witamy w symulatorze Mistrzostw Swiata 2022!\n\n";
            Toolbox.Pisanie(powitanie);
            Console.ResetColor();

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
                                    Console.Clear();
                                    CzyPozostac = false;
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
                                    Console.Clear();
                                    CzyPozostac = false;
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
                                case '1':
                                    MeczeGrupowe();
                                    break;
                                case '0':
                                    Console.Write("Powrot do menu programu...\n");
                                    Thread.Sleep(3000);
                                    Console.Clear();
                                    CzyPozostac = false;
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

