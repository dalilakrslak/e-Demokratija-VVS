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
        public Supervizor(string password)
        {
            this.password = password;
        }
        public string Password
        {
            get => password;
            set => password = value;
        }
        public bool DaLiJePasswordIspravan (string password)
        {
            return password == "admin";
        }
        public string GenerisiNoviPassword()
        {
            Random random = new Random();
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            int length = 8;

            string noviPassword = new string(Enumerable.Repeat(characters, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());

            password = noviPassword; 
            return noviPassword;
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
        public bool DaLiPostojiKandidatSaImenom(List<Kandidat> kandidati, string ime)
        {
            foreach (Kandidat k in kandidati)
            {
                if (k.Ime.Equals(ime))
                {
                    return true;
                }
            }
            return false;
        }
    }
}