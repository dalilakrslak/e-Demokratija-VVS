using CsvHelper;
using e_Demokratija;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Testovi
{
    [TestClass]
    public class TDD
    {
        [TestMethod]
        public void ResetGlasanjaZaVijecnika_GlasacNijeGlasaoZaVijecnika_BrojGlasovaSeNeMijenja()
        {
            var glasac = new Glasac();
            var kandidati = new List<Kandidat> { 
                new Kandidat("Kandidat", "Prvi", new DateTime(1977,10,12), Pozicija.gradonacelnik, "Opis", null)
            };
            var glasovi = new List<Glas> { new Glas(glasac, kandidati[0]) };
            var originalniBrojGlasova = kandidati[0].BrojGlasova;
            Glas glas = new Glas();
            glas.ResetGlasanjaZaVijecnika(glasac, glasovi, kandidati);

            Assert.AreEqual(originalniBrojGlasova, kandidati[0].BrojGlasova);
        }

        [TestMethod]
        public void ResetGlasanjaZaVijecnika_GlasacGlasaoZaVijecnika_BrojGlasovaSeSmanjuje()
        {
            var glasac = new Glasac();
            var vijecnik = new Kandidat("Kandidat", "Prvi", new DateTime(1977, 10, 12), Pozicija.vijecnik, "Opis", null);
            var drugiKandidat = new Kandidat("Kandidat", "Prvi", new DateTime(1977, 10, 12), Pozicija.gradonacelnik, "Opis", null);
            var kandidati = new List<Kandidat> { vijecnik, drugiKandidat };
            var glasovi = new List<Glas> { new Glas(glasac, vijecnik), new Glas(new Glasac(), drugiKandidat) };
            Glas glas = new Glas();

            glas.ResetGlasanjaZaVijecnika(glasac, glasovi, kandidati);

            Assert.AreEqual(0, vijecnik.BrojGlasova);
        }

        [TestMethod]
        public void ResetGlasanjaZaVijecnika_NemaGlasovaZaVijecnika_BrojGlasovaSeNeMijenja()
        {
            var glasac = new Glasac();
            var vijecnik = new Kandidat("Kandidat", "Prvi", new DateTime(1977, 10, 12), Pozicija.vijecnik, "Opis", null);
            var drugiKandidat = new Kandidat("Kandidat", "Prvi", new DateTime(1977, 10, 12), Pozicija.gradonacelnik, "Opis", null);
            var kandidati = new List<Kandidat> { vijecnik, drugiKandidat };
            var glasovi = new List<Glas> { new Glas(new Glasac(), drugiKandidat) };
            var originalniBrojGlasova = vijecnik.BrojGlasova;

            Glas glas = new Glas();

            glas.ResetGlasanjaZaVijecnika(glasac, glasovi, kandidati);

            Assert.AreEqual(originalniBrojGlasova, vijecnik.BrojGlasova);
        }
        [TestMethod]
        public void DaLiJeOpisStrankeIspravan_ValidanOpis_OcekivaniRezultatTrue()
        {
            Stranka stranka = new Stranka();
            string opis = "Puni naziv stranke Ne-Ka Stranka je NekaStranka. NekaStranka je osnovana 2020. godine.";

            bool rezultat = stranka.DaLiJeOpisStrankeIspravan(opis);

            Assert.IsTrue(rezultat);
        }

        [TestMethod]
        public void DaLiJeOpisStrankeIspravan_NevalidanOpisBezTacke_OcekivaniRezultatFalse()
        {
            Stranka stranka = new Stranka();
            string opis = "Puni naziv stranke LosaStranka je LosaStranka je osnovana 2021 godine";

            bool rezultat = stranka.DaLiJeOpisStrankeIspravan(opis);

            Assert.IsFalse(rezultat);
        }

        [TestMethod]
        public void DaLiJeOpisStrankeIspravan_NevalidanOpisSamoPola_OcekivaniRezultatFalse()
        {
            Stranka stranka = new Stranka();
            string opis = "Ne-Ka Stranka je NekaStranka.";

            bool rezultat = stranka.DaLiJeOpisStrankeIspravan(opis);

            Assert.IsFalse(rezultat);
        }

        [TestMethod]
        public void DaLiJeOpisStrankeIspravan_NevalidanOpisPrazan_OcekivaniRezultatFalse()
        {
            Stranka stranka = new Stranka();
            string opis = "";

            bool rezultat = stranka.DaLiJeOpisStrankeIspravan(opis);

            Assert.IsFalse(rezultat);
        }
        [TestMethod]
        public void DaLiJeOpisStrankeIspravan_GodinaUBuducnosti_OcekivaniRezultatFalse()
        {
            Stranka stranka = new Stranka();
            string opis = "Puni naziv stranke Ne-Ka Stranka je NekaStranka. NekaStranka je osnovana 2100. godine.";

            bool result = stranka.DaLiJeOpisStrankeIspravan(opis);

            Assert.IsFalse(result);
        }
        [TestMethod]
        public void DaLiJeOpisStrankeIspravan_NeispravnaGodina_OcekivaniRezultatFalse()
        {
            Stranka stranka = new Stranka();
            string opis = "Puni naziv stranke Ne-Ka Stranka je NekaStranka. NekaStranka je osnovana 12345. godine.";

            bool result = stranka.DaLiJeOpisStrankeIspravan(opis);

            Assert.IsFalse(result);
        }
    }
}
