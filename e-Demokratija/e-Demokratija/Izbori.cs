using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Demokratija
{
    public class Izbori
    {
        private List<Glasac> glasaci;
        private List<Kandidat> kandidati;
        private List<Stranka> stranke;
        private List<Glas> glasovi;
        public Izbori() 
        {
            //glasaci = new List<Glasac>();
            kandidati = new List<Kandidat>();
            stranke = new List<Stranka>();
            glasovi = new List<Glas>();
        }
        public Izbori(List<Glasac> glasac, List<Kandidat> kandidati, List<Stranka> stranke, List<Glas> glasovi) { 
            this.glasaci = glasac;
            this.kandidati = kandidati;
            this.stranke = stranke;
            this.glasovi = glasovi;
        }
        public List<Glasac> Glasaci
        {
            get => glasaci;
            set => glasaci = value;
        }
        public List<Kandidat> Kandidati
        {
            get => kandidati;
            set => kandidati = value;
        }
        public List<Stranka> Stranke
        {
            get => stranke;
            set => stranke = value;
        }
        public List<Glas> Glasovi { 
            get => glasovi; 
            set => glasovi = value; 
        }
        public void KreirajIzbore()
        {
            Stranka sda = new Stranka("SDA", "ovo je opis stranke");
            Stranka sds = new Stranka("SDS", "opis");
            Stranka hdz = new Stranka("HDZ", "opis");
            Stranka snsd = new Stranka("SNSD", "opis");
            Stranka osmorka = new Stranka("OSMORKA", "opis");

            stranke.Add(sda);
            stranke.Add(sds);
            stranke.Add(hdz);
            stranke.Add(snsd);
            stranke.Add(osmorka);
            Kandidat gradonacelnik1 = new Kandidat("Mujo", "Mujić", new DateTime(1980, 11, 23), Pozicija.gradonacelnik, "Kandidat je bio član stranke sda od 1.1.1994 do 1.1.1997, član stranke sdp od 1.1.1997 do 2.3.1999", sda);
            Kandidat gradonacelnik2 = new Kandidat("Pero", "Perić", new DateTime(1980, 8, 15), Pozicija.gradonacelnik, "Kandidat je bio član stranke sda od 1.1.1994 do 1.1.1997, član stranke sdp od 1.1.1997 do 2.3.1999", osmorka);
            Kandidat gradonacelnik3 = new Kandidat("Duro", "Durić", new DateTime(1980, 1, 3), Pozicija.gradonacelnik, "Kandidat je bio član stranke sda od 1.1.1994 do 1.1.1997, član stranke sdp od 1.1.1997 do 2.3.1999", hdz);
            kandidati.Add(gradonacelnik1);
            kandidati.Add(gradonacelnik2);
            kandidati.Add(gradonacelnik3);
        }
    }
}
