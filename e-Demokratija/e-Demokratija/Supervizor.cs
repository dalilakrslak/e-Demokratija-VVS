using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Demokratija
{
    public class Supervizor
    {
        public string password;
        public Supervizor()
        {
            password = "";
        }
        public string Password
        {
            get { return password; }
        }
        public void DodajKandidata(CSVMaker csvMaker, List<Stranka> stranke, List<Kandidat> kandidati)
        {
            Console.Write("\nUnesite ime: ");
            string imeKandidata = Console.ReadLine();

            Console.Write("Unesite prezime: ");
            string prezimeKandidata = Console.ReadLine();

            Console.Write("Unesite dan rodjenja: ");
            string danString = Console.ReadLine();

            Console.Write("Unesite mjesec rodjenja: ");
            string mjesecString = Console.ReadLine();

            Console.Write("Unesite godinu rodjenja: ");
            string godinaString = Console.ReadLine();

            int dan = Int32.Parse(danString);
            int mjesec = Int32.Parse(mjesecString);
            int godina = Int32.Parse(godinaString);

            DateTime datumRodjenjaKandidata = new DateTime(dan, mjesec, godina);

            Console.Write("Unesite opis kandidata: ");
            string opis = Console.ReadLine();

            Console.Write("Unesite naziv stranke kandidata: ");
            string nazivStranke = "";

            while (true)
            {
                nazivStranke = Console.ReadLine();
                bool temp = true;
                foreach (Stranka stranka in stranke)
                {
                    if (nazivStranke.Equals(stranka.Naziv))
                    {
                        temp = true;

                    }
                }
                if (temp)
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
                if (pozicija.Equals("gradonacelnik") && pozicija.Equals("nacelnik") && pozicija.Equals("vijecnik"))
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
            Kandidat kandidat = new Kandidat(imeKandidata, prezimeKandidata, datumRodjenjaKandidata, poz, opis, s);

            csvMaker.DodajKandidata(kandidat);
            Console.WriteLine($"\nUspješno ste dodali kandidata {kandidat.Ime} {kandidat.Prezime}!");
        }
        public void DodajStranku(CSVMaker csvMaker, List<Stranka> stranke)
        {
            string nazivStranke = "";
            bool pronasao = false;

            Console.Write("\nUnesite naziv stranke: ");
            string naziv = Console.ReadLine();

            foreach (Stranka s in stranke)
            {
                if (!s.Naziv.Equals(naziv))
                {
                    pronasao = true;
                    break;
                }
                if (pronasao)
                {
                    Console.WriteLine("Stranka već postoji!");
                }
                else
                {
                    nazivStranke = naziv;
                    Console.Write("Unesite opis stranke: ");
                    string opis = Console.ReadLine();

                    Stranka stranka = new Stranka(nazivStranke, opis);

                    csvMaker.DodajStranku(stranka);
                    Console.WriteLine($"\nUspješno ste dodali stranku {stranka.Naziv}!");
                }
            }

        }

        public void IzmijeniStranku(CSVMaker csvMaker, List<Stranka> stranke)
        {
            Console.Write("\nUnesite naziv stranke koju želite izmijeniti: ");
            string naziv = Console.ReadLine();

            bool pronasao = false;

            foreach (Stranka s in stranke)
            {
                if (s.Naziv.Equals(naziv))
                {
                    pronasao = true;
                    Console.Write("\nUnesite novi naziv stranke: ");
                    string noviNaziv = Console.ReadLine();
                    Console.Write("Unesite novi opis stranke: ");
                    string noviOpis = Console.ReadLine();
                    s.Naziv = noviNaziv;
                    s.Opis = noviOpis;
                    break;
                }
            }

            if (pronasao)
            {
                csvMaker.AzurirajStrankeIzCSV(stranke);
                Console.WriteLine("\nIzmjene su uspješne!");
            }
            else
            {
                Console.WriteLine("Stranka nije pronađena.");
            }
        }
        public void IzbrisiKandidata(CSVMaker csvMaker, List<Kandidat> kandidati)
        {
            Console.Write("\nUnesite redni broj kandidata kojeg želite izbrisati: ");
            string unos = Console.ReadLine();
            int redniBroj = Int32.Parse(unos);
            bool izbrisan = false;

            for (int i = 1; i <= kandidati.Count; i++)
            {
                if (kandidati[i].RedniBroj.Equals(redniBroj))
                {
                    kandidati.RemoveAt(i--);
                    izbrisan = true;
                    break;
                }
            }
            if (izbrisan)
            {
                csvMaker.AzurirajKandidateIzCSV(kandidati);
                Console.WriteLine("\nKandidat uspješno izbrisan!");
            }
            else
            {
                Console.WriteLine("\nKandidat ne postoji!");
            }
        }
        public void IzbrisiStranku(CSVMaker csvMaker, List<Stranka> stranke)
        {
            Console.Write("\nUnesite naziv stranke koju želite izbrisati: ");
            string naziv = Console.ReadLine();
            bool izbrisan = false;

            for (int i = 1; i < stranke.Count; i++)
            {
                if (stranke[i].Naziv.Equals(naziv))
                {
                    stranke.RemoveAt(i);
                    izbrisan = true;
                    break;
                }
            }
            if (izbrisan)
            {
                csvMaker.AzurirajStrankeIzCSV(stranke);
                Console.WriteLine("\nStranka uspješno izbrisana!");
            }
            else
            {
                Console.WriteLine("\nStranka ne postoji!");
            }
        }
    }
}