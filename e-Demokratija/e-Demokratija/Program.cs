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
            CSVMaker csvMaker = new CSVMaker();
            var glasaci = csvMaker.CitajGlasaceIzCSV(Path.Combine(Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.Parent.FullName, "e-Demokratija"), "glasaci.csv"));
            var stranke = csvMaker.CitajStrankeIzCSV(Path.Combine(Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.Parent.FullName, "e-Demokratija"), "stranke.csv"));
            var kandidati = csvMaker.CitajKandidateIzCSV(Path.Combine(Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.Parent.FullName, "e-Demokratija"), "kandidati.csv"));
            var glasovi = csvMaker.CitajGlasoveIzCSV(Path.Combine(Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.Parent.FullName, "e-Demokratija"), "glasovi.csv"));
            Supervizor supervizor = new Supervizor();
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

                        Glasac glasac = new Glasac();

                        Console.Write("Unesite ime: ");
                        string ime = Console.ReadLine();
                        glasac.DaLiJeImeIspravno(ime);

                        Console.Write("Unesite prezime: ");
                        string prezime = Console.ReadLine();
                        glasac.DaLiJePrezimeIspravno(prezime);

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
                        glasac.DaLiJeDatumaRodjenjaIspravan(datumRodjenja);

                        glasac = new Glasac(ime, prezime, datumRodjenja);
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

                            Console.Write("Unesite odgovarajuće slovo za izbor: ");
                            string unos = Convert.ToString(Console.ReadLine());
                            if (unos == "a")
                            {
                                if (trenutniGlasac.DaLiJeGlasaoZaGradonacelnika == false)
                                {
                                    Console.WriteLine("\nKandidati za gradonacelnika su: \n");
                                    foreach (Kandidat gradonacelnik in kandidati)
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
                                    foreach (Kandidat k in kandidati)
                                    {
                                        if (k.RedniBroj == redniBrojGradonacelnika)
                                        {
                                            trenutniGlasac.DaLiJeGlasaoZaGradonacelnika = true;
                                            Glas glas = new Glas(trenutniGlasac, k);
                                            csvMaker.DodajGlas(glas);
                                            k.BrojGlasova++;
                                            csvMaker.AzurirajKandidateIzCSV(kandidati);
                                            csvMaker.AzurirajGlasaceIzCSV(glasaci);
                                            break;
                                        }
                                    }
                                }
                                else
                                    Console.WriteLine("\nVec ste glasali za gradonacelnika!");
                            }
                            else if (unos == "b")
                            {
                                if (trenutniGlasac.DaLiJeGlasaoZaNacelnika == false)
                                {
                                    Console.WriteLine("\nKandidati za načelnika su: \n");
                                    foreach (Kandidat nacelnik in kandidati)
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
                                    foreach (Kandidat k in kandidati)
                                    {
                                        if (k.RedniBroj == redniBrojNacelnika)
                                        {
                                            trenutniGlasac.DaLiJeGlasaoZaNacelnika = true;
                                            Glas glas = new Glas(trenutniGlasac, k);
                                            csvMaker.DodajGlas(glas);
                                            k.BrojGlasova++;
                                            csvMaker.AzurirajKandidateIzCSV(kandidati);
                                            csvMaker.AzurirajGlasaceIzCSV(glasaci);
                                            break;
                                        }
                                    }
                                }
                                else
                                    Console.WriteLine("\nVec ste glasali za nacelnika!");
                            }
                            else if (unos == "c")
                            {
                                if (trenutniGlasac.DaLiJeGlasaoZaVijecnika == false)
                                {
                                    Console.WriteLine("\nKandidati za vijecnika su: \n");
                                    foreach (Kandidat vijecnik in kandidati)
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
                                    foreach (Kandidat k in kandidati)
                                    {
                                        if (k.RedniBroj == redniBrojVijecnika)
                                        {
                                            trenutniGlasac.DaLiJeGlasaoZaVijecnika = true;
                                            Glas glas = new Glas(trenutniGlasac, k);
                                            csvMaker.DodajGlas(glas);
                                            k.BrojGlasova++;
                                            csvMaker.AzurirajKandidateIzCSV(kandidati);
                                            csvMaker.AzurirajGlasaceIzCSV(glasaci);
                                            break;
                                        }
                                    }
                                }
                                else
                                    Console.WriteLine("\nVec ste glasali za vijecnika!");
                            }
                            else if (unos == "d")
                            {
                                if (trenutniGlasac.DaLiJeGlasaoZaVijecnika == false)
                                {
                                    Console.Write("\nUnesite naziv stranke: ");
                                    string nazivStranke = Console.ReadLine();
                                    bool provjera = false;
                                    while (!provjera)
                                    {
                                        foreach (Stranka s in stranke)
                                        {
                                            if (s.Naziv.Equals(nazivStranke))
                                            {
                                                provjera = true;
                                                s.BrojGlasova++;
                                                csvMaker.AzurirajStrankeIzCSV(stranke);
                                                break;
                                            }
                                        }
                                        if (!provjera)
                                            Console.WriteLine("\nUnijeli ste nepostojecu stranku. Pokusajte ponovo!");
                                    }
                                    if (provjera)
                                    {
                                        foreach (Kandidat k in kandidati)
                                        {
                                            if (k.Stranka.Naziv.Equals(nazivStranke))
                                            {
                                                k.BrojGlasova++;
                                                csvMaker.AzurirajKandidateIzCSV(kandidati);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("\nNe moze se glasati za stranku ukoliko ste vec glasali za vijecnike!");
                                }
                            }
                        }
                        Console.WriteLine("\nPritisnite bilo koju tipku za povratak na glavni izbornik.");
                        Console.ReadKey();
                        break;
                    case 3:
                        int unosSupervizora = -1;
                        Console.Write("\nDobrodošli! Molimo vas da potvrdite svoj identitet unosom vaše lozinke: ");
                        if (Console.ReadLine() == "admin")
                        {
                            Console.WriteLine("\nVi ste supervizor! Molimo izaberite jednu od sljedećih opcija:");
                            Console.WriteLine("1 - Dodavanje kandidata");
                            Console.WriteLine("2 - Brisanje kandidata");
                            Console.WriteLine("3 - Dodavanje stranke");
                            Console.WriteLine("4 - Izmjena stranke");
                            Console.WriteLine("5 - Brisanje stranke\n");
                            Console.Write("Unesite odgovarajući broj za izbor: ");
                            unosSupervizora = Int32.Parse(Console.ReadLine());
                            if(unosSupervizora == 1)
                            {
                                supervizor.DodajKandidata(csvMaker, stranke, kandidati);
                            }
                            else if (unosSupervizora == 2)
                            {
                                supervizor.IzbrisiKandidata(csvMaker, kandidati);
                            }
                            else if (unosSupervizora == 3)
                            {
                                supervizor.DodajStranku(csvMaker, stranke);
                            }
                            else if (unosSupervizora == 4)
                            {
                                supervizor.IzmijeniStranku(csvMaker, stranke); 
                            }
                            else if (unosSupervizora == 5)
                            {
                                supervizor.IzbrisiStranku(csvMaker, stranke);
                            }
                        }
                        else
                        {
                            Console.WriteLine("\nPogresna sifra! Pokusajte ponovo!");
                        }
                        Console.WriteLine("\nPritisnite bilo koju tipku za povratak na glavni izbornik.");
                        Console.ReadKey();
                        break;
                    case 4:
                        Console.Write("\nUkupno je registrovano " + glasaci.Count + " glasaca.\n");
                        int brojacZaGradonacelnika = 0, brojacZaNacelnika = 0, brojacZaVijecnike = 0, brojacZaStranke = 0;
                        foreach (Glasac g in glasaci)
                        {
                            if (g.DaLiJeGlasaoZaGradonacelnika)
                                brojacZaGradonacelnika++;
                            if (g.DaLiJeGlasaoZaNacelnika)
                                brojacZaNacelnika++;
                            if (g.DaLiJeGlasaoZaVijecnika)
                                brojacZaVijecnike++;
                        }

                        foreach (Stranka s in stranke)
                        {
                            brojacZaStranke += s.BrojGlasova;
                        }
                        Console.WriteLine("Od ukupno " + glasaci.Count + " glasalo je " + brojacZaGradonacelnika + " za gradonacelnika, " + brojacZaNacelnika + " za nacelnika, " + brojacZaVijecnike + " za vijecnike i " + brojacZaStranke + " za stranke generalno.\n");
                        kandidati.Sort((kandidat1, kandidat2) => kandidat1.BrojGlasova.CompareTo(kandidat2.BrojGlasova));
                        //ispis glasanja -> nakon dodanih kandidata i glasova
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
