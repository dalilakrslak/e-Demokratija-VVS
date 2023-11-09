using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Demokratija
{
    public class Glas
    {
        private Glasac glasac;
        private Kandidat kandidat;

        public Glas(Glasac glasac, Kandidat kandidat)
        {
            this.glasac = glasac;
            this.kandidat = kandidat;

            if (kandidat.Stranka != null && kandidat.Pozicija.Equals(Pozicija.vijecnik))
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
        public Glasac Glasac { 
            get => glasac; 
            set => glasac = value; 
        }
        public Kandidat Kandidat { 
            get => kandidat; 
            set => kandidat = value; 
        }
    }
}
