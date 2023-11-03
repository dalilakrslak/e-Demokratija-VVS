using System;
using System.Collections.Generic;
using System.Linq;
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

        public Glasac(String ime, String prezime, DateTime datumRodjenja, String adresa, String brojLicneKarte, string jmbg)
        {
            this.ime = ime;
            this.prezime = prezime;
            this.datumRodjenja = datumRodjenja;
            FormirajKodGlasaca();
        }
        public string Ime
        {
            get => ime;
            set
            {
                ime = value;
                FormirajKodGlasaca();
            }
        }
        public string Prezime
        {
            get => prezime;
            set
            {
                prezime = value;
                FormirajKodGlasaca();
            }
        }
        public DateTime DatumRodjenja
        {
            get => datumRodjenja;
            set
            {
                datumRodjenja = value;
                FormirajKodGlasaca();
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
