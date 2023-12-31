﻿using System;
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
                            Kandidat kand = new Kandidat();
                            if (unos == "a")
                            {
                                if (trenutniGlasac.DaLiJeGlasaoZaGradonacelnika == false)
                                {
                                    Console.WriteLine("\nKandidati za gradonacelnika su: \n");
                                    kand.IspisiKandidateZaGradonacelnika(kandidati);
                                    Console.Write("\nVaš glas je za gradonačelnika pod rednim brojem: ");
                                    int redniBrojGradonacelnika = Int32.Parse(Console.ReadLine());
                                    foreach (Kandidat k in kandidati)
                                    {
                                        if (k.RedniBroj == redniBrojGradonacelnika)
                                        {
                                            Glas glas = new Glas(trenutniGlasac, k);
                                            csvMaker.DodajGlas(glas);
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
                                    kand.IspisiKandidateZaNacelnika(kandidati);
                                    Console.Write("\nVaš glas je za načelnika pod rednim brojem: ");
                                    int redniBrojNacelnika = Int32.Parse(Console.ReadLine());
                                    foreach (Kandidat k in kandidati)
                                    {
                                        if (k.RedniBroj == redniBrojNacelnika)
                                        {
                                            Glas glas = new Glas(trenutniGlasac, k);
                                            csvMaker.DodajGlas(glas);
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
                                    kand.IspisiKandidateZaVijecnike(kandidati);
                                    Console.Write("\nVaš glas je za vijecnika pod rednim brojem: ");
                                    int redniBrojVijecnika = Int32.Parse(Console.ReadLine());
                                    foreach (Kandidat k in kandidati)
                                    {
                                        if (k.RedniBroj == redniBrojVijecnika)
                                        {
                                            Glas glas = new Glas(trenutniGlasac, k);
                                            csvMaker.DodajGlas(glas);
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
                        if (supervizor.DaLiJePasswordIspravan(Console.ReadLine()))
                        {
                            Console.WriteLine("\nVi ste supervizor! Molimo izaberite jednu od sljedećih opcija:");
                            Console.WriteLine("1 - Dodavanje kandidata");
                            Console.WriteLine("2 - Brisanje kandidata");
                            Console.WriteLine("3 - Dodavanje stranke");
                            Console.WriteLine("4 - Izmjena stranke");
                            Console.WriteLine("5 - Brisanje stranke\n");
                            Console.Write("Unesite odgovarajući broj za izbor: ");
                            unosSupervizora = Int32.Parse(Console.ReadLine());
                            if (unosSupervizora == 1)
                            {
                                Kandidat kandidat = new Kandidat();
                                Console.Write("\nUnesite ime: ");
                                string imeKandidata = Console.ReadLine();
                                kandidat.DaLiJeImeIspravno(imeKandidata);

                                Console.Write("Unesite prezime: ");
                                string prezimeKandidata = Console.ReadLine();
                                kandidat.DaLiJePrezimeIspravno(prezimeKandidata);

                                Console.Write("Unesite dan rodjenja: ");
                                string danKandidataString = Console.ReadLine();

                                Console.Write("Unesite mjesec rodjenja: ");
                                string mjesecKandidataString = Console.ReadLine();

                                Console.Write("Unesite godinu rodjenja: ");
                                string godinaKandidataString = Console.ReadLine();

                                int danK = Int32.Parse(danKandidataString);
                                int mjesecK = Int32.Parse(mjesecKandidataString);
                                int godinaK = Int32.Parse(godinaKandidataString);

                                DateTime datumRodjenjaKandidata = new DateTime(godinaK, mjesecK, danK);
                                kandidat.DaLiJeDatumaRodjenjaIspravan(datumRodjenjaKandidata);

                                Console.Write("Unesite opis kandidata: ");
                                string opis = Console.ReadLine();
                                kandidat.DaLiJeOpisIspravan(opis);

                                Console.Write("Unesite naziv stranke kandidata: ");
                                string nazivStranke = "";

                                while (true)
                                {
                                    nazivStranke = Console.ReadLine();
                                    if (supervizor.DaLiStrankaPostoji(stranke, nazivStranke))
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Stranka ne postoji, unesite ponovo naziv stranke: ");
                                    }
                                }

                                Console.WriteLine("Pozicija kandidata (gradonacelnik, nacelnik ili vijecnik): ");
                                string pozicija = "";

                                while (true)
                                {
                                    pozicija = Console.ReadLine();
                                    if (kandidat.DaLiJePozicijaIspravna(pozicija))
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Neispravan unos, molimo vas unesite jednu od pozicija (gradonacelnik, nacelnik ili vijecnik): ");
                                    }
                                }
                                Pozicija poz = Pozicija.vijecnik;
                                if (pozicija == "vijecnik")
                                    poz = Pozicija.vijecnik;
                                if (pozicija == "nacelnik")
                                    poz = Pozicija.nacelnik;
                                if (pozicija == "gradonacelnik")
                                    poz = Pozicija.gradonacelnik;

                                Stranka s = null;
                                foreach (Stranka str in stranke)
                                {
                                    if (str.Naziv.Equals(nazivStranke))
                                    {
                                        s = str;
                                        break;
                                    }
                                }
                                kandidat = new Kandidat(imeKandidata, prezimeKandidata, datumRodjenjaKandidata, poz, opis, s);

                                csvMaker.DodajKandidata(kandidat);
                                Console.WriteLine($"\nUspješno ste dodali kandidata {kandidat.Ime} {kandidat.Prezime}!");
                            }
                            else if (unosSupervizora == 2)
                            {
                                Console.Write("\nUnesite redni broj kandidata kojeg želite izbrisati: ");
                                string unos = Console.ReadLine();
                                if (supervizor.DaLiKandidatPostoji(kandidati, Int32.Parse(unos)))
                                {
                                    for (int i = 0; i < kandidati.Count; i++)
                                    {
                                        if (kandidati[i].RedniBroj.Equals(unos))
                                        {
                                            kandidati.RemoveAt(i);
                                            break;
                                        }
                                    }
                                    csvMaker.AzurirajKandidateIzCSV(kandidati);
                                    Console.WriteLine("\nKandidat uspješno izbrisan!");
                                }
                                else
                                {
                                    Console.WriteLine("\nKandidat ne postoji!");
                                }
                            }
                            else if (unosSupervizora == 3)
                            {
                                Console.Write("\nUnesite naziv stranke: ");
                                string naziv = Console.ReadLine();
                                if (supervizor.DaLiStrankaPostoji(stranke, naziv))
                                {
                                    Console.WriteLine("Stranka već postoji!");
                                }
                                else
                                {
                                    Console.Write("Unesite opis stranke: ");
                                    string opis = Console.ReadLine();
                                    Stranka stranka = new Stranka(naziv, opis);
                                    csvMaker.DodajStranku(stranka);
                                    Console.WriteLine($"\nUspješno ste dodali stranku {stranka.Naziv}!");
                                }
                            }
                            else if (unosSupervizora == 4)
                            {
                                Console.Write("\nUnesite naziv stranke koju želite izmijeniti: ");
                                string naziv = Console.ReadLine();
                                if (supervizor.DaLiStrankaPostoji(stranke, naziv))
                                {
                                    Console.Write("\nUnesite novi naziv stranke: ");
                                    string noviNaziv = Console.ReadLine();
                                    Console.Write("Unesite novi opis stranke: ");
                                    string noviOpis = Console.ReadLine();
                                    //supervizor.IzmijeniStranku(csvMaker, stranke, naziv, noviNaziv, noviOpis);
                                    foreach (Stranka s in stranke)
                                    {
                                        if (s.Naziv.Equals(naziv))
                                        {
                                            s.Naziv = noviNaziv;
                                            s.Opis = noviOpis;
                                            break;
                                        }
                                    }
                                    csvMaker.AzurirajStrankeIzCSV(stranke);
                                    Console.WriteLine("\nIzmjene su uspješne!");
                                }
                                else
                                {
                                    Console.WriteLine("\nStranka ne postoji!");
                                }
                            }
                            else if (unosSupervizora == 5)
                            {
                                Console.Write("\nUnesite naziv stranke koju želite izbrisati: ");
                                string naziv = Console.ReadLine();
                                if (supervizor.DaLiStrankaPostoji(stranke, naziv))
                                {
                                    for (int i = 0; i < stranke.Count; i++)
                                    {
                                        if (stranke[i].Naziv.Equals(naziv))
                                        {
                                            stranke.RemoveAt(i);
                                            break;
                                        }
                                    }
                                    csvMaker.AzurirajStrankeIzCSV(stranke);
                                    Console.WriteLine("\nStranka uspješno izbrisana!");
                                }
                                else
                                {
                                    Console.WriteLine("\nStranka ne postoji!");
                                }
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
                        Glas g = new Glas();
                        g.IspisStanja(glasaci, stranke, kandidati, glasovi);
                        g.IspisStanjaGradonacelnika(glasaci, kandidati);
                        g.IspisStanjaNacelnika(glasaci, kandidati);
                        g.IspisStanjaVijecnika(glasaci, kandidati);
                        g.IspisStanjaStranke(stranke);
                        Console.WriteLine("\nPritisnite bilo koju tipku za povratak na glavni izbornik.");
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
