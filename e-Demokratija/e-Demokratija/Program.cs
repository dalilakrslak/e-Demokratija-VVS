using System;
using System.Collections.Generic;
using System.IO;

namespace e_Demokratija
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Izbori izbori = new Izbori();

            while (true)
            {
                Console.Clear();

                Console.WriteLine("---------------------------------------");
                Console.WriteLine("|        Online lokalni izbori        |");
                Console.WriteLine("---------------------------------------\n");

                Console.WriteLine("Molimo Vas da odaberete jedan od sljedećih izbora:");
                Console.WriteLine(" 1. Registracija glasača");
                Console.WriteLine(" 2. Glasanje");
                Console.WriteLine(" 3. Supervizor");
                Console.WriteLine(" 4. Prikaz stanja glasanja");
                Console.WriteLine(" 0. Izlaz\n");

                Console.Write("Unesite odgovarajući broj za izbor: ");
                int izbor = Int32.Parse(Console.ReadLine());

                switch (izbor)
                {
                    case 1:
                        Console.WriteLine("---------------------------------------");
                        Console.WriteLine("|         Registracija glasaca        |");
                        Console.WriteLine("---------------------------------------\n");

                        Console.Write("Unesite ime: ");
                        string ime = Console.ReadLine();

                        Console.Write("Unesite prezime: ");
                        string prezime = Console.ReadLine();

                        Console.Write("Unesite dan rodjenja: ");
                        string danString = Console.ReadLine();

                        Console.Write("Unesite mjesec rodjenja: ");
                        string mjesecString = Console.ReadLine();

                        Console.Write("Unesite godinu rodjenja: ");
                        string godinaString = Console.ReadLine();

                        int dan = Int32.Parse(danString);
                        int mjesec = Int32.Parse(mjesecString);
                        int godina = Int32.Parse(godinaString);

                        DateTime datumRodjenja = new DateTime(godina, mjesec, dan);

                        Glasac glasac = new Glasac(ime, prezime, datumRodjenja);

                        izbori.Glasaci.Add(glasac);

                        Console.WriteLine($"\nPoštovani/a {glasac.Ime} {glasac.Prezime}, uspješno ste registrovani. \nJEDINSTVENI IDENTIFIKACIONI KOD: {glasac.Kod}\n");


                        Console.WriteLine("Pritisnite bilo koju tipku za povratak na glavni izbornik.");
                        Console.ReadKey();
                        break;
                    case 2:
                
                        Console.WriteLine("Pritisnite bilo koju tipku za povratak na glavni izbornik.");
                        Console.ReadKey();
                        break;
                    case 3:
                        // Implementacija supervizora
                        Console.WriteLine("Pritisnite bilo koju tipku za povratak na glavni izbornik.");
                        Console.ReadKey();
                        break;
                    case 4:
                        // Implementacija prikaza stanja glasanja
                        Console.WriteLine("Pritisnite bilo koju tipku za povratak na glavni izbornik.");
                        Console.ReadKey();
                        break;
                    case 0:
                        Console.WriteLine("Hvala Vam!");
                        break;
                    default:
                        Console.WriteLine("Nevažeći unos. Pritisnite bilo koju tipku za povratak na glavni izbornik.");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
