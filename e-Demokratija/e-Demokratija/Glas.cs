using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Demokratija
{
    public class Glas
    {
        private Glasac glasac;
        private Kandidat kandidat;
        public Glas() { }

        public Glas(Glasac glasac, Kandidat kandidat)
        {
            this.glasac = glasac;
            this.kandidat = kandidat;

            if (kandidat.Stranka != null)
            {
                kandidat.BrojGlasova = kandidat.BrojGlasova + 1;
                kandidat.Stranka.BrojGlasova = kandidat.Stranka.BrojGlasova + 1;
            }
            else
            {
                kandidat.BrojGlasova = kandidat.BrojGlasova + 1;
            }

            if (kandidat.Pozicija == Pozicija.gradonacelnik)
                glasac.DaLiJeGlasaoZaGradonacelnika = true;
            if (kandidat.Pozicija == Pozicija.nacelnik)
                glasac.DaLiJeGlasaoZaNacelnika = true;
            if (kandidat.Pozicija == Pozicija.vijecnik)
                glasac.DaLiJeGlasaoZaVijecnika = true;
        }
        public Glasac Glasac
        {
            get => glasac;
            set => glasac = value;
        }
        public Kandidat Kandidat
        {
            get => kandidat;
            set => kandidat = value;
        }
        private bool DaLiJeGlasanjePocelo(List<Glas> glasovi)
        {
            if (glasovi.Count == 0)
                return false;
            return true;
        }
        private bool UkupniIzborniPragKandidata(List<Glasac> glasaci)
        {
            int ukupanBrojMogucihGlasova = glasaci.Count();
            int ukupanBrojGlasova = 0;
            foreach (Glasac g in glasaci)
            {
                if (g.DaLiJeGlasaoZaGradonacelnika || g.DaLiJeGlasaoZaNacelnika || g.DaLiJeGlasaoZaVijecnika)
                    ukupanBrojGlasova++;
            }

            if (ukupanBrojGlasova >= ukupanBrojMogucihGlasova * 0.2)
                return true;
            return false;
        }
        static void SortiranjeKandidata(List<Kandidat> kandidati)
        {
            int n = kandidati.Count;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (kandidati[j].BrojGlasova < kandidati[j + 1].BrojGlasova)
                    {
                        Kandidat temp = kandidati[j];
                        kandidati[j] = kandidati[j + 1];
                        kandidati[j + 1] = temp;
                    }
                }
            }
        }
        public void IspisStanjaGradonacelnika(List<Glasac> glasaci, List<Kandidat> kandidati)
        {
            int brojacZaGradonacelnika = 0;
            foreach (Glasac g in glasaci)
            {
                if (g.DaLiJeGlasaoZaGradonacelnika)
                    brojacZaGradonacelnika++;
            }
            Console.WriteLine("\nGlasanje za gradonačelnika: ");
            Console.WriteLine(" - Ukupan broj glasova: " + brojacZaGradonacelnika);
            Console.WriteLine(" - Prva tri mjesta: ");
            SortiranjeKandidata(kandidati);
            int brojac = 0;
            foreach (var kandidat in kandidati)
            {
                if (kandidat.Pozicija == Pozicija.gradonacelnik)
                {
                    Console.WriteLine($"     {kandidat.Ime} {kandidat.Prezime} ({kandidat.Stranka.Naziv}) - {kandidat.BrojGlasova} glasova");
                    brojac++;
                    if (brojac == 3)
                        break;
                }
            }
        }
        public void IspisStanjaNacelnika(List<Glasac> glasaci, List<Kandidat> kandidati)
        {
            int brojacZaNacelnika = 0;
            foreach (Glasac g in glasaci)
            {
                if (g.DaLiJeGlasaoZaNacelnika)
                    brojacZaNacelnika++;
            }
            Console.WriteLine("\nGlasanje za nacelnika: ");
            Console.WriteLine(" - Ukupan broj glasova: " + brojacZaNacelnika);
            Console.WriteLine(" - Prva tri mjesta: ");
            SortiranjeKandidata(kandidati);
            int brojac = 0;
            foreach (var kandidat in kandidati)
            {
                if (kandidat.Pozicija == Pozicija.nacelnik)
                {
                    Console.WriteLine($"     {kandidat.Ime} {kandidat.Prezime} ({kandidat.Stranka.Naziv}) - {kandidat.BrojGlasova} glasova");
                    brojac++;
                    if (brojac == 3)
                        break;
                }
            }
        }
        public void IspisStanjaVijecnika(List<Glasac> glasaci, List<Kandidat> kandidati)
        {
            int brojacZaVijecnika = 0;
            foreach (Glasac g in glasaci)
            {
                if (g.DaLiJeGlasaoZaVijecnika)
                    brojacZaVijecnika++;
            }
            Console.WriteLine("\nGlasanje za vijecnika: ");
            Console.WriteLine(" - Ukupan broj glasova: " + brojacZaVijecnika);
            Console.WriteLine(" - Prvih pet mjesta: ");
            SortiranjeKandidata(kandidati);
            int brojac = 0;
            foreach (var kandidat in kandidati)
            {
                if (kandidat.Pozicija == Pozicija.vijecnik)
                {
                    Console.WriteLine($"     {kandidat.Ime} {kandidat.Prezime} ({kandidat.Stranka.Naziv}) - {kandidat.BrojGlasova} glasova");
                    brojac++;
                    if (brojac == 5)
                        break;
                }
            }
        }
        public void IspisStanjaStranke(List<Stranka> stranke)
        {
            int brojacZaStranke = 0;
            foreach (Stranka s in stranke)
            {
                brojacZaStranke += s.BrojGlasova;
            }
            Console.WriteLine("\nGlasanje za stranke: ");
            Console.WriteLine(" - Ukupan broj glasova: " + brojacZaStranke);
            Console.WriteLine(" - Broj glasova za stranke: ");
            foreach (Stranka s in stranke)
            {
                Console.WriteLine($" {s.Naziv}: {s.BrojGlasova}");
            }
        }
        public void IspisStanja(List<Glasac> glasaci, List<Stranka> stranke, List<Kandidat> kandidati, List<Glas> glasovi)
        {
            Console.WriteLine("\n---------------------------------------");
            Console.WriteLine("|             Prikaz stanja           |");
            Console.WriteLine("---------------------------------------\n");
            if (!DaLiJeGlasanjePocelo(glasovi))
            {
                Console.WriteLine("Glasanje jos uvijek nije pocelo!");
                return;
            }
            else if (!UkupniIzborniPragKandidata(glasaci))
            {
                Console.WriteLine("Izbori nisu legalni ili jos uvijek traju!");
                return;
            }
            Console.WriteLine("Ukupan broj registrovanih glasaca: " + glasaci.Count);

            IspisStanjaGradonacelnika(glasaci, kandidati);
            IspisStanjaNacelnika(glasaci, kandidati);
            IspisStanjaVijecnika(glasaci, kandidati);
            IspisStanjaStranke(stranke);
        }
    }
}
