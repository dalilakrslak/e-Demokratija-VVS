﻿using System;
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
        private bool daLiJeGlasaoZaGradonacelnika = false;
        private bool daLiJeGlasaoZaNacelnika = false;
        private bool daLiJeGlasaoZaVijecnika = false;

        public Glasac() { }

        public Glasac(String ime, String prezime, DateTime datumRodjenja)
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
        public bool DaLiJeGlasaoZaGradonacelnika
        {
            get => daLiJeGlasaoZaGradonacelnika;
            set => daLiJeGlasaoZaGradonacelnika = value;
        }
        public bool DaLiJeGlasaoZaNacelnika
        {
            get => daLiJeGlasaoZaNacelnika;
            set => daLiJeGlasaoZaNacelnika = value;
        }
        public bool DaLiJeGlasaoZaVijecnika
        {
            get => daLiJeGlasaoZaVijecnika;
            set => daLiJeGlasaoZaVijecnika = value;
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

            kod = ime.Substring(0, 1) + prezime + dan + mjesec + datumRodjenja.Year.ToString().Substring(2, 2);
            kod = kod.ToLower();
        }
        public void DaLiJeImeIspravno(string ime)
        {
            if (ime.Length == 0)
                throw new ArgumentException("Ime ne može biti prazna riječ!");

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

        public void DaLiJePrezimeIspravno(string prezime)
        {
            if (prezime.Length == 0)
                throw new ArgumentException("Prezime ne može biti prazna riječ!");

            bool samoCrtice = true;
            int brojCrtica = 0;

            foreach (char c in prezime.ToCharArray())
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

            if (prezime.Length < 3 || prezime.Length > 20 || !samoCrtice || brojCrtica > 1)
                throw new ArgumentOutOfRangeException("Upisano prezime nije validno!");
        }

        public void DaLiJeDatumaRodjenjaIspravan(DateTime datum)
        {
            if (datum > DateTime.Now)
                throw new ArgumentOutOfRangeException("Datum rođenja ne može biti veći od današnjeg datuma!");

            if (DateTime.Now.Date < datum.AddYears(18))
            {
                throw new ArgumentOutOfRangeException("Glasač nije punoljetan!");
            }
        }
    }
}
