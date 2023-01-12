using System;
using System.Text.RegularExpressions;

namespace Sym_MS
{
    internal class Program
    {
        public static bool CzyPozostac = true;

        //struct do podlaczenia bazy

        //podlaczenie bazy

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
            Console.WriteLine("1. KATAR \n2.  \n3.  \n4.  \n5.  \n6. ");

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
jioafnioegfiogaiogbwea
