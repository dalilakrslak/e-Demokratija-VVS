using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace e_Demokratija
{
    public class Stranka
    {
        private static int trenutniRedniBroj = 1;
        private string naziv;
        private string opis;
        private int brojGlasova;
        private int redniBroj;
        public Stranka()
        {
            redniBroj = trenutniRedniBroj++;
        }

        public Stranka(string naziv, string opis)
        {
            this.naziv = naziv;
            this.opis = opis;
            brojGlasova = 0;
            redniBroj = trenutniRedniBroj++;
        }

        public string Naziv
        {
            get => naziv;
            set => naziv = value;
        }
        public string Opis
        {
            get => opis;
            set => opis = value;
        }
        public int BrojGlasova
        {
            get => brojGlasova;
            set => brojGlasova = value;
        }
        public int RedniBroj
        {
            get => redniBroj;
        }
        public bool DaLiJeOpisStrankeIspravan(string opis)
        {
            if (string.IsNullOrEmpty(opis))
            {
                return false;
            }
            else if (!opis.EndsWith("."))
            {
                return false;
            }
            else if (!opis.Contains("naziv stranke") || !opis.Contains("osnovana"))
            {
                return false;
            }
            else if (!Regex.Match(opis, @"\b(\d{4})\b").Success)
            {
                return false;
            }
            else if (int.Parse(Regex.Match(opis, @"\b(\d{4})\b").Value) > DateTime.Now.Year)
            {
                return false;
            }
            return true;
        }
    }
}
