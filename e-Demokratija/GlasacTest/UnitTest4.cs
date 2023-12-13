using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using System.Xml;
using e_Demokratija;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Testovi
{
    [TestClass]

    public class UnitTest4
    {
        private string IspisKonzole(Action action)
        {
            var izvorniIzlaz = Console.Out;
            using (var izlaz = new StringWriter())
            {
                Console.SetOut(izlaz);
                action.Invoke();
                Console.SetOut(izvorniIzlaz);
                return izlaz.ToString();
            }
        }

        [TestMethod]
        public void TestIspravnogKonstruktoraKlaseKandidat()
        {
            string ime = "Vildana";
            string prezime = "Tabaković";
            DateTime datumRodjenja = new DateTime(2001, 2, 7);
            Pozicija pozicija = Pozicija.gradonacelnik;
            string opisKandidata = "Iskusni lider";
            Stranka stranka = new Stranka("VTT", "Neka stranka");
            Kandidat kandidat = new Kandidat(ime, prezime, datumRodjenja, pozicija, opisKandidata, stranka);
            Assert.AreEqual(ime, kandidat.Ime);
            Assert.AreEqual(prezime, kandidat.Prezime);
            Assert.AreEqual(datumRodjenja, kandidat.DatumRodjenja);
            Assert.AreEqual(pozicija, kandidat.Pozicija);
            Assert.AreEqual(opisKandidata, kandidat.OpisKandidata);
            Assert.AreEqual(stranka, kandidat.Stranka);
            Assert.AreEqual(0, kandidat.BrojGlasova);
        }
        [TestMethod]
        public void TestPozicijaSetter()
        {
            Kandidat kandidat = new Kandidat();
            Pozicija ocekivanaPozicija = Pozicija.gradonacelnik;
            kandidat.Pozicija = ocekivanaPozicija;
            Pozicija stvarnaPozicija = kandidat.Pozicija;
            Assert.AreEqual(ocekivanaPozicija, stvarnaPozicija);
        }
        [TestMethod]
        public void OpisKandidataSetGet()
        {
            Kandidat kandidat = new Kandidat();
            string expectedOpisKandidata = "Experienced leader";
            kandidat.OpisKandidata = expectedOpisKandidata;
            string actualOpisKandidata = kandidat.OpisKandidata;
            Assert.AreEqual(expectedOpisKandidata, actualOpisKandidata);
        }

        [TestMethod]
        public void BrojGlasovaSetGet()
        {
            Kandidat kandidat = new Kandidat();
            int expectedBrojGlasova = 42;
            kandidat.BrojGlasova = expectedBrojGlasova;
            int actualBrojGlasova = kandidat.BrojGlasova;
            Assert.AreEqual(expectedBrojGlasova, actualBrojGlasova);
        }
        [TestMethod]
        public void TestDaLiJeOpisIspravan_NeBacaIzuzetak()
        {

            Kandidat kandidat = new Kandidat();
            string validOpis = "Neki opis koji sadrzi vise od tri rijeci";
            try
            {
                kandidat.DaLiJeOpisIspravan(validOpis);
            }
            catch (ArgumentException)
            {
                Assert.Fail("");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDaLiJeOpisIspravan_PrazanOpis_BacaIzuzetak()
        {
            Kandidat kandidat = new Kandidat();
            string pogresanOpis = null;
            kandidat.DaLiJeOpisIspravan(pogresanOpis);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DaLiJeOpisIspravan_OpisSaManjeOdTriRijeci_BacaIzuzetak()
        {
            Kandidat kandidat = new Kandidat();
            string pogresanOpis = "Kratki opis";
            kandidat.DaLiJeOpisIspravan(pogresanOpis);
        }
        [TestMethod]
        public void TestDaLiJePozicijaIspravna_IspravnaPozicija_TrebaVratitiTrue()
        {
            Kandidat kandidat = new Kandidat();
            string Pozicija = "gradonacelnik";
            bool rezultat = kandidat.DaLiJePozicijaIspravna(Pozicija);
            Assert.IsTrue(rezultat);
        }

        [TestMethod]
        public void TestDaLiJePozicijaIspravna_NeispravnaPozicija_TrebaVratitiFalse()
        {
            Kandidat kandidat = new Kandidat();
            string Pozicija = "neka_pozicija";
            bool result = kandidat.DaLiJePozicijaIspravna(Pozicija);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DaLiJePozicijaIspravna_NeispravnoNapisanaPozicija_TrebaVratitiFalse()
        {
            Kandidat kandidat = new Kandidat();
            string Pozicija = "NaCeLnIk";
            bool result = kandidat.DaLiJePozicijaIspravna(Pozicija);
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void IspisiKandidateZaGradonacelnika_TrebaVratitiTrue()
        {
            List<Kandidat> kandidati = new List<Kandidat>
            {
                new Kandidat("John", "Doe", new DateTime(1979, 12, 27), Pozicija.gradonacelnik, "Opis1", new Stranka("Stranka1", "Opis Stranke1")),
                new Kandidat("Jane", "Doe", new DateTime(1979, 12, 27), Pozicija.gradonacelnik, "Opis2", null),
                new Kandidat("Jim", "Smith", new DateTime(1979, 12, 27), Pozicija.gradonacelnik, "Opis3", new Stranka("Stranka2", "Opis Stranke2"))
            };
            var ocekivaniIzlaz = new StringBuilder();
            ocekivaniIzlaz.AppendLine(kandidati[0].RedniBroj + " - John Doe (Stranka1)");
            ocekivaniIzlaz.AppendLine(kandidati[1].RedniBroj + " - Jane Doe (nezavisni kandidat)");
            ocekivaniIzlaz.AppendLine(kandidati[2].RedniBroj + " - Jim Smith (Stranka2)");
            Kandidat kandidat = new Kandidat();
            var izlazKonzole = IspisKonzole(() => kandidat.IspisiKandidateZaGradonacelnika(kandidati));
            StringAssert.Contains(izlazKonzole, ocekivaniIzlaz.ToString());
        }
        [TestMethod]
        public void IspisiKandidateZaNacelnika_TrebaVratitiTrue()
        {
            List<Kandidat> kandidati = new List<Kandidat>
            {
                new Kandidat("John", "Doe", new DateTime(1979, 12, 27), Pozicija.nacelnik, "Opis1", new Stranka("Stranka1", "Opis Stranke1")),
                new Kandidat("Jane", "Doe", new DateTime(1979, 12, 27), Pozicija.nacelnik, "Opis2", null),
                new Kandidat("Jim", "Smith", new DateTime(1979, 12, 27), Pozicija.nacelnik, "Opis3", new Stranka("Stranka2", "Opis Stranke2"))
            };
            var ocekivaniIzlaz = new StringBuilder();
            ocekivaniIzlaz.AppendLine(kandidati[0].RedniBroj + " - John Doe (Stranka1)");
            ocekivaniIzlaz.AppendLine(kandidati[1].RedniBroj + " - Jane Doe (nezavisni kandidat)");
            ocekivaniIzlaz.AppendLine(kandidati[2].RedniBroj + " - Jim Smith (Stranka2)");
            Kandidat kandidat = new Kandidat();
            var izlazKonzole = IspisKonzole(() => kandidat.IspisiKandidateZaNacelnika(kandidati));
            StringAssert.Contains(izlazKonzole, ocekivaniIzlaz.ToString());
        }
        [TestMethod]
        public void IspisiKandidateZaVijecnika_TrebaVratitiTrue()
        {
            List<Kandidat> kandidati = new List<Kandidat>
            {
                new Kandidat("John", "Doe", new DateTime(1979, 12, 27), Pozicija.vijecnik, "Opis1", new Stranka("Stranka1", "Opis Stranke1")),
                new Kandidat("Jane", "Doe", new DateTime(1979, 12, 27), Pozicija.vijecnik, "Opis2", null),
                new Kandidat("Jim", "Smith", new DateTime(1979, 12, 27), Pozicija.vijecnik, "Opis3", new Stranka("Stranka2", "Opis Stranke2"))
            };
            var ocekivaniIzlaz = new StringBuilder();
            ocekivaniIzlaz.AppendLine(kandidati[0].RedniBroj + " - John Doe (Stranka1)");
            ocekivaniIzlaz.AppendLine(kandidati[1].RedniBroj + " - Jane Doe (nezavisni kandidat)");
            ocekivaniIzlaz.AppendLine(kandidati[2].RedniBroj + " - Jim Smith (Stranka2)");
            Kandidat kandidat = new Kandidat();
            var izlazKonzole = IspisKonzole(() => kandidat.IspisiKandidateZaVijecnike(kandidati));
            StringAssert.Contains(izlazKonzole, ocekivaniIzlaz.ToString());
        }

        [TestMethod]
        public void TestZamjenskiObjekat()
        {
            Kandidat k = new Kandidat("Novi", "Kandidat", new DateTime(1978, 11, 10), Pozicija.gradonacelnik, "Opis novog kandidata", null);
            Spy temp = new Spy();
            temp.Glasovi = 0;
            Assert.IsFalse(k.DaLiImaGlas(temp));
            temp.Glasovi = 32;
            Assert.IsTrue(k.DaLiImaGlas(temp));
        }

        public static IEnumerable<object[]> ReadXML()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("..\\..\\..\\Kandidati.xml");
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                List<string> elements = new List<string>();
                foreach (XmlNode innerNode in node)
                {
                    elements.Add(innerNode.InnerText);
                }
                yield return new object[] { elements[0], elements[1] };
            }
        }
        public static IEnumerable<object[]> ReadCSV()
        {
            using (var reader = new StreamReader("..\\..\\..\\Kandidati.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var rows = csv.GetRecords<dynamic>();
                foreach (var row in rows)
                {
                    var values = ((IDictionary<String, Object>)row).Values;
                    var elements = values.Select(elem => elem.ToString()).ToList();
                    yield return new object[] { elements[0], elements[1] };
                }
            }
        }
        static IEnumerable<object[]> KandidatiXML
        {
            get
            {
                return ReadXML();
            }
        }
        static IEnumerable<object[]> KandidatiCSV
        {
            get
            {
                return ReadCSV();
            }
        }

        // DDT TESTS
        [DynamicData(nameof(KandidatiXML))]
        [TestMethod]
        public void TestKonstruktoraGlasacaXML(string ime, string prezime)
        {
            Kandidat kandidat = new Kandidat(ime, prezime, new DateTime(1987, 12, 12), Pozicija.gradonacelnik, "Opis kandidata broj jedan.", null);
        }
        [DynamicData(nameof(KandidatiCSV))]
        [TestMethod]
        public void TestKonstruktoraGlasacaCSV(string ime, string prezime)
        {
            Kandidat kandidat = new Kandidat(ime, prezime, new DateTime(1987, 12, 12), Pozicija.gradonacelnik, "Opis kandidata broj jedan.", null);
        }
    }
}
