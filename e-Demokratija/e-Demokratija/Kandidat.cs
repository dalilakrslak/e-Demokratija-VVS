using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Demokratija
{
    public enum Pozicija {
        gradonacelnik, 
        nacelnik, 
        vijecnik
    };
    public class Kandidat: Glasac
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
        public Kandidat(String ime, String prezime, DateTime datumRodjenja, Pozicija pozicija, string opisKandidata, Stranka stranka): base(ime, prezime, datumRodjenja)
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
        }
        public Pozicija Pozicija{ 
            get => pozicija; 
            set => pozicija = value; 
        }
        public Stranka Stranka { 
            get => stranka; 
            set => stranka = value; 
        }
        public string OpisKandidata { 
            get => opisKandidata; 
            set => opisKandidata = value; 
        }
        public int BrojGlasova { 
            get => brojGlasova; 
            set => brojGlasova = value; 
        }
    }
}
