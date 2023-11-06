using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace e_Demokratija
{
    public class Glasac
    {
        private string ime;
        private string prezime;
        private DateTime datumRodjenja;
        private string kod;

        public Glasac(String ime, String prezime, DateTime datumRodjenja)
        {
            DaLiJeImeIspravno(ime);
            this.ime = ime;
            DaLiJePrezimeIspravno(prezime);
            this.prezime = prezime;
            DaLiJeDatumaRodjenjaIspravan(datumRodjenja);
            this.datumRodjenja = datumRodjenja;
            FormirajKodGlasaca();
        }
        public string Ime
        {
            get => ime;
            set
            {
                ime = value;
            }
        }
        public string Prezime
        {
            get => prezime;
            set
            {
                prezime = value;
            }
        }
        public DateTime DatumRodjenja
        {
            get => datumRodjenja;
            set
            {
                datumRodjenja = value;
            }
        }
        public string Kod
        {
            get => kod;
            set
            {
                kod = value.ToLower();
            }
        }

        private void DaLiJeImeIspravno(string ime) 
        {
            if (ime.Length == 0)
                throw new ArgumentException("Ime ne može biti prazna riječ!");

            if (ime == null)
                throw new ArgumentNullException("Glasač mora imati ime - ime ne smije biti NULL!");

            bool samoCrtice = true;
            int brojCrtica = 0;

            foreach (char c in ime.ToCharArray())
            {
                if (!Char.IsLetter(c))
                {
                    if (c == '-')
                    {
                        brojCrtica++;
                        continue;
                    }
                    else
                    {
                        samoCrtice = false;
                        break;
                    }
                }
            }

            if (ime.Length < 3 || ime.Length > 20 || !samoCrtice || brojCrtica > 1)
                throw new ArgumentOutOfRangeException("Upisano ime nije validno!");
        }

        private void DaLiJePrezimeIspravno(string prezime)
        {
            if (ime.Length == 0)
                throw new ArgumentException("Prezime ne može biti prazna riječ!");

            if (ime == null)
                throw new ArgumentNullException("Glasač mora imati prezime - prezime ne smije biti NULL!");

            bool samoCrtice = true;
            int brojCrtica = 0;

            foreach (char c in ime.ToCharArray())
            {
                if (!Char.IsLetter(c))
                {
                    if (c == '-')
                    {
                        brojCrtica++;
                        continue;
                    }
                    else
                    {
                        samoCrtice = false;
                        break;
                    }
                }
            }

            if (ime.Length < 3 || ime.Length > 20 || !samoCrtice || brojCrtica > 1)
                throw new ArgumentOutOfRangeException("Upisano prezime nije validno!");
        }

        private void DaLiJeDatumaRodjenjaIspravan(DateTime datum)
        {
            if (datum > DateTime.Now)
                throw new ArgumentOutOfRangeException("Datum rođenja ne može biti veći od današnjeg datuma!");

            int godine = DateTime.Now.Year - datumRodjenja.Year;
            if (DateTime.Now.Year - datum.Year < 18 || (DateTime.Now.Year - datumRodjenja.Year == 18 && DateTime.Now.Date < datum.AddYears(18).Date))
            {
                throw new ArgumentOutOfRangeException("Glasač nije punoljetan!");
            }
        }

        void FormirajKodGlasaca()
        {
            string dan = "";
            if (datumRodjenja.Day < 10)
            {
                dan = "0" + datumRodjenja.Day.ToString();
            }
            else
            {
                dan = datumRodjenja.Day.ToString();
            }

            string mjesec = "";
            if (datumRodjenja.Month < 10)
            {
                mjesec = "0" + datumRodjenja.Month.ToString();
            }
            else
            {
                mjesec = datumRodjenja.Month.ToString();
            }

            kod = ime.Substring(0, 1) + prezime + dan + mjesec + datumRodjenja.Year.ToString().Substring(2,2);
            kod = kod.ToLower();
        }
    }
}
