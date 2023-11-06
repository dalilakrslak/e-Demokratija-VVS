using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Demokratija
{
    public class Stranka
    {
        private string naziv;
        private string opis;
        private int brojGlasova;
        private List<Kandidat> rukovodstvo;

        public Stranka(string naziv, string opis)
        {
            this.naziv = naziv;
            this.opis = opis;
            brojGlasova = 0;
            rukovodstvo = new List<Kandidat>();
        }

        public string Naziv { 
            get => naziv; 
            set => naziv = value; 
        }
        public string Opis { 
            get => opis; 
            set => opis = value; 
        }
        public int BrojGlasova { 
            get => brojGlasova; 
            set => brojGlasova = value; 
        }
        public List<Kandidat> Rukovodstvo { 
            get => rukovodstvo; 
            set => rukovodstvo = value; 
        }

    }
}
