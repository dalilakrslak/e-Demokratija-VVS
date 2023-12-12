using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Demokratija
{
    public class Supervizor
    {
        private string password;
        public Supervizor()
        {
            password = "admin";
        }
        public string Password
        {
            get => password;
        }
        public bool DaLiJePasswordIspravan (string password)
        {
            return password == "admin";
        }
        public bool DaLiStrankaPostoji(List<Stranka> stranke, string naziv)
        {
            foreach (Stranka s in stranke)
            {
                if (s.Naziv.Equals(naziv))
                {
                    return true;
                }
            }
            return false;
        }
        public bool DaLiKandidatPostoji(List<Kandidat> kandidati, int redniBroj)
        {
            foreach (Kandidat k in kandidati)
            {
                if (k.RedniBroj.Equals(redniBroj))
                {
                    return true;
                }
            }
            return false;
        }
        public void IzmijeniStranku(CSVMaker csvMaker, List<Stranka> stranke, string naziv, string noviNaziv, string noviOpis)
        {
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
        }
        public void ObrisiKandidata(CSVMaker csvMaker, List<Kandidat> kandidati, int redniBroj)
        {
            for (int i = 0; i < kandidati.Count; i++)
            {
                if (kandidati[i].RedniBroj.Equals(redniBroj))
                {
                    kandidati.RemoveAt(i);
                    break;
                }
            }
            csvMaker.AzurirajKandidateIzCSV(kandidati);
        }
        public void ObrisiStranku(CSVMaker csvMaker, List<Stranka> stranke, string naziv)
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
        }
    }
}