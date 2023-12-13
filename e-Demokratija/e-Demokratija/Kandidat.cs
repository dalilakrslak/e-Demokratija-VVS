using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Demokratija
{
    public enum Pozicija
    {
        gradonacelnik,
        nacelnik,
        vijecnik
    };
    public class Kandidat : Glasac
    {
        private static int trenutniRedniBroj = 1;
        private int redniBroj;
        private Pozicija pozicija;
        private string opisKandidata;
        private Stranka stranka;
        private int brojGlasova;
        public Kandidat()
        {
            redniBroj = trenutniRedniBroj++;
        }
        public Kandidat(String ime, String prezime, DateTime datumRodjenja, Pozicija pozicija, string opisKandidata, Stranka stranka) : base(ime, prezime, datumRodjenja)
        {
            redniBroj = trenutniRedniBroj++;
            this.pozicija = pozicija;
            this.opisKandidata = opisKandidata;
            this.stranka = stranka;
            brojGlasova = 0;
        }
        public int RedniBroj
        {
            get => redniBroj;
            set => redniBroj = value;
        }
        public Pozicija Pozicija
        {
            get => pozicija;
            set => pozicija = value;
        }
        public Stranka Stranka
        {
            get => stranka;
            set => stranka = value;
        }
        public string OpisKandidata
        {
            get => opisKandidata;
            set => opisKandidata = value;
        }
        public int BrojGlasova
        {
            get => brojGlasova;
            set => brojGlasova = value;
        }
        public void DaLiJeOpisIspravan(string opis)
        {
            if (string.IsNullOrWhiteSpace(opis))
                throw new ArgumentException("Opis kandidata ne može biti prazan!");
            if (opis.Split(' ').Length < 3)
            {
                throw new ArgumentException("Opis kandidata treba sadrzavati minimalno 3 rijeci!");
            }
        }
        public bool DaLiJePozicijaIspravna(string pozicija)
        {
            return pozicija.Equals("gradonacelnik") || pozicija.Equals("nacelnik") || pozicija.Equals("vijecnik");
        }
        public void IspisiKandidateZaGradonacelnika(List<Kandidat> kandidati)
        {
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
        }
        public void IspisiKandidateZaNacelnika(List<Kandidat> kandidati)
        {
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
        }
        public void IspisiKandidateZaVijecnike(List<Kandidat> kandidati)
        {
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
        }
        public bool DaLiImaGlas(IProvjera sigurnosnaProvjera)
        {
            if (!sigurnosnaProvjera.DaLiImaGlas(redniBroj))
                return false;
            return true;
        }
    }
}
