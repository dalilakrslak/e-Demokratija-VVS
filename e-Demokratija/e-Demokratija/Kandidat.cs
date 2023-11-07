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
        private Pozicija pozicija; 
        private string opisKandidata;
        private Stranka stranka;
        public Kandidat() { }
        public Kandidat(Pozicija pozicija, string opisKandidata, Stranka stranka)
        {
            this.pozicija = pozicija;
            this.opisKandidata = opisKandidata;
            this.stranka = stranka;
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


    }
}
