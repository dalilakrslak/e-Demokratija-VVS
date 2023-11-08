using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace e_Demokratija
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Izbori izbori = new Izbori();
            izbori.KreirajIzbore();
            CSVMaker csvMaker = new CSVMaker();
            var glasaci = csvMaker.CitajGlasaceIzCSV(Path.Combine(Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.Parent.FullName, "e-Demokratija"), "glasaci.csv"));
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
                    case 0:
                        Console.WriteLine("Hvala Vam!");
                        Environment.Exit(0);
                        break;
                    case 1:
                        Console.WriteLine("\n---------------------------------------");
                        Console.WriteLine("|         Registracija glasaca        |");
                        Console.WriteLine("---------------------------------------\n");

                        Glasac pom = new Glasac();

                        Console.Write("Unesite ime: ");
                        string ime = Console.ReadLine();
                        //pom.DaLiJeImeIspravno(ime);

                        Console.Write("Unesite prezime: ");
                        string prezime = Console.ReadLine();
                        //pom.DaLiJePrezimeIspravno(prezime);

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
                        //pom.DaLiJeDatumaRodjenjaIspravan(datumRodjenja);

                        Glasac glasac = new Glasac(ime, prezime, datumRodjenja);
                        csvMaker.DodajGlasaca(glasac);

                        Console.WriteLine($"\nPoštovani/a {glasac.Ime} {glasac.Prezime}, uspješno ste registrovani. \nJEDINSTVENI IDENTIFIKACIONI KOD: {glasac.Kod}\n");

                        Console.WriteLine("Pritisnite bilo koju tipku za povratak na glavni izbornik.");
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.WriteLine("\n---------------------------------------");
                        Console.WriteLine("|               Glasanje              |");
                        Console.WriteLine("---------------------------------------\n");
                        bool registrovanGlasac = false;
                        Glasac trenutniGlasac = null;
                        while (!registrovanGlasac)
                        {
                            Console.Write("\nUnesite vaš JEDINSTVENI IDENTIFIKACIONI KOD: ");
                            string uneseniKod = Console.ReadLine();
                            foreach (Glasac glasac1 in glasaci)
                            {
                                if (uneseniKod.Equals(glasac1.Kod))
                                {
                                    registrovanGlasac = true;
                                    trenutniGlasac = glasac1;
                                    break;
                                }
                            }
                        }
                        if (registrovanGlasac)
                        {
                            Console.WriteLine($"\nDobrodošli {trenutniGlasac.Ime} {trenutniGlasac.Prezime}!");
                            Console.WriteLine("U nastavku izaberite jednu od opcija za glasanje:");
                            Console.WriteLine(" a - Glasanje za gradonačelnika");
                            Console.WriteLine(" b - Glasanje za načelnika");
                            Console.WriteLine(" c - Glasanje za vijećnika/vijećnike");
                            Console.WriteLine(" d - Glasanje za stranku\n");

                            Console.Write("Vaš odabir: ");
                            string unos = Convert.ToString(Console.ReadLine());
                            if (unos == "a")
                            {
                                if (trenutniGlasac.DaLiJeGlasaoZaGradonacelnika == false)
                                {
                                    Console.WriteLine("\nKandidati za gradonacelnika su: \n");
                                    foreach (Kandidat gradonacelnik in izbori.Kandidati)
                                    {
                                        if (gradonacelnik.Pozicija.ToString().Equals("gradonacelnik"))
                                        {
                                            if (gradonacelnik.Stranka != null)
                                                Console.WriteLine(gradonacelnik.RedniBroj + " - " + gradonacelnik.Ime + " " + gradonacelnik.Prezime + " (" + gradonacelnik.Stranka.Naziv + ")");
                                            else
                                                Console.WriteLine(gradonacelnik.RedniBroj + " - " + gradonacelnik.Ime + " " + gradonacelnik.Prezime + " (nezavisni kandidat)");
                                        }
                                    }
                                    Console.Write("\nVaš glas je za gradonačelnika pod rednim brojem: ");
                                    int redniBrojGradonacelnika = Int32.Parse(Console.ReadLine());
                                    foreach (Kandidat k in izbori.Kandidati)
                                    {
                                        if (k.RedniBroj == redniBrojGradonacelnika)
                                        {
                                            trenutniGlasac.DaLiJeGlasaoZaGradonacelnika = true;
                                            izbori.Glasovi.Add(new Glas(trenutniGlasac, k, DateTime.Now));
                                            break;
                                        }
                                    }
                                }
                                else
                                    Console.WriteLine("Vec ste glasali za gradonacelnika!");
                            }
                            else if (unos == "b")
                            {
                                if (trenutniGlasac.DaLiJeGlasaoZaNacelnika == false)
                                {
                                    Console.WriteLine("\nKandidati za načelnika su: \n");
                                    foreach (Kandidat nacelnik in izbori.Kandidati)
                                    {
                                        if (nacelnik.Pozicija.ToString().Equals("nacelnik"))
                                        {
                                            if (nacelnik.Stranka != null)
                                                Console.WriteLine(nacelnik.RedniBroj + " - " + nacelnik.Ime + " " + nacelnik.Prezime + " (" + nacelnik.Stranka.Naziv + ")");
                                            else
                                                Console.WriteLine(nacelnik.RedniBroj + " - " + nacelnik.Ime + " " + nacelnik.Prezime + " (nezavisni kandidat)");
                                        }
                                    }
                                    Console.Write("\nVaš glas je za načelnika pod rednim brojem: ");
                                    int redniBrojNacelnika = Int32.Parse(Console.ReadLine());
                                    foreach (Kandidat k in izbori.Kandidati)
                                    {
                                        if (k.RedniBroj == redniBrojNacelnika)
                                        {
                                            trenutniGlasac.DaLiJeGlasaoZaNacelnika = true;
                                            izbori.Glasovi.Add(new Glas(trenutniGlasac, k, DateTime.Now));
                                            break;
                                        }
                                    }
                                }
                                else
                                    Console.WriteLine("Vec ste glasali za nacelnika!");
                            }
                            else if (unos == "c")
                            {
                                if (trenutniGlasac.DaLiJeGlasaoZaVijecnika == false)
                                {
                                    Console.WriteLine("\nKandidati za vijecnika su: \n");
                                    foreach (Kandidat vijecnik in izbori.Kandidati)
                                    {
                                        if (vijecnik.Pozicija.ToString().Equals("vijecnik"))
                                        {
                                            if (vijecnik.Stranka != null)
                                                Console.WriteLine(vijecnik.RedniBroj + " - " + vijecnik.Ime + " " + vijecnik.Prezime + " (" + vijecnik.Stranka.Naziv + ")");
                                            else
                                                Console.WriteLine(vijecnik.RedniBroj + " - " + vijecnik.Ime + " " + vijecnik.Prezime + " (nezavisni kandidat)");
                                        }
                                    }
                                    Console.Write("\nVaš glas je za vijecnika pod rednim brojem: ");
                                    int redniBrojVijecnika = Int32.Parse(Console.ReadLine());
                                    foreach (Kandidat k in izbori.Kandidati)
                                    {
                                        if (k.RedniBroj == redniBrojVijecnika)
                                        {
                                            trenutniGlasac.DaLiJeGlasaoZaVijecnika = true;
                                            izbori.Glasovi.Add(new Glas(trenutniGlasac, k, DateTime.Now));
                                            break;
                                        }
                                    }
                                }
                                else
                                    Console.WriteLine("Vec ste glasali za gradonacelnika!");
                            }
                            else if (unos == "d")
                            {
                                if (trenutniGlasac.DaLiJeGlasaoZaVijecnika == false)
                                {
                                    Console.Write("Unesite naziv stranke: ");
                                    string nazivStranke = Console.ReadLine();
                                    bool provjera = false;
                                    while (!provjera)
                                    {
                                        Console.WriteLine("Unesite naziv stranke: ");
                                        foreach (Stranka s in izbori.Stranke)
                                        {
                                            if (s.Naziv.Equals(nazivStranke))
                                            {
                                                provjera = true;
                                                s.BrojGlasova++;
                                                break;
                                            }
                                        }
                                        if (!provjera)
                                            Console.WriteLine("Unijeli ste nepostojecu stranku. Pokusajte ponovo!");
                                    }
                                    if (provjera)
                                    {
                                        foreach (Kandidat k in izbori.Kandidati)
                                        {
                                            if (k.Stranka.Naziv.Equals(nazivStranke))
                                                k.BrojGlasova++;
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Ne moze se glasati za stranku ukoliko ste vec glasali za vijecnike!");
                                }
                            }
                        }
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
                    default:
                        Console.WriteLine("Nevažeći unos. Pritisnite bilo koju tipku za povratak na glavni izbornik.");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
