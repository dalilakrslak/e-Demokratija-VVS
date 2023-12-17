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
    public class UnitTest3
    {
        public class DummyGlas : List<Glas>
        {
            public new void Add(Glas glas)
            {
                base.Add(glas);
            }
        }
        private XmlReaderSettings settings = new XmlReaderSettings();
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
        public void TestPovecanjaGlasova1()
        {
            var glasac = new Glasac();
            var stranka = new Stranka { Naziv = "SDP" };
            var kandidat = new Kandidat { Stranka = stranka };
            var glas = new Glas(glasac, kandidat);
            Assert.AreEqual(1, kandidat.BrojGlasova);
            Assert.AreEqual(1, stranka.BrojGlasova);
        }
        [TestMethod]
        public void TestPovecanjaGlasova2()
        {
            var glasac = new Glasac();
            var kandidat = new Kandidat { Stranka = null };
            var glas = new Glas(glasac, kandidat);
            Assert.AreEqual(1, kandidat.BrojGlasova);
        }
        [TestMethod]
        public void TestPovecanjaGlasova3()
        {
            var glasac = new Glasac();
            var kandidat = new Kandidat { Pozicija = Pozicija.nacelnik };
            var glas = new Glas(glasac, kandidat);
            Assert.IsFalse(glasac.DaLiJeGlasaoZaGradonacelnika);
            Assert.IsTrue(glasac.DaLiJeGlasaoZaNacelnika);
            Assert.IsFalse(glasac.DaLiJeGlasaoZaVijecnika);
        }
        [TestMethod]
        public void TestPovecanjaGlasova4()
        {
            var glasac = new Glasac();
            var kandidat = new Kandidat { Pozicija = Pozicija.vijecnik };
            var glas = new Glas(glasac, kandidat);
            Assert.IsFalse(glasac.DaLiJeGlasaoZaGradonacelnika);
            Assert.IsFalse(glasac.DaLiJeGlasaoZaNacelnika);
            Assert.IsTrue(glasac.DaLiJeGlasaoZaVijecnika);
        }
        [TestMethod]
        public void TestGlasacSetter()
        {
            var glas = new Glas();
            var newGlasac = new Glasac();

            glas.Glasac = newGlasac;

            Assert.AreEqual(newGlasac, glas.Glasac);
        }

        [TestMethod]
        public void TestKandidatSetter()
        {
            var glas = new Glas();
            var newKandidat = new Kandidat();

            glas.Kandidat = newKandidat;

            Assert.AreEqual(newKandidat, glas.Kandidat);
        }
        [TestMethod]
        public void TestDaLiJeGlasanjePocelo1()
        {
            var glas = new Glas();
            var glasovi = new List<Glas>();
            var result = glas.DaLiJeGlasanjePocelo(glasovi);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestDaLiJeGlasanjePocelo2()
        {
            var glas = new Glas();
            var glasovi = new List<Glas> { new Glas(), new Glas() };
            var result = glas.DaLiJeGlasanjePocelo(glasovi);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void TestUkupnogIzbornogPragaKandidata1()
        {
            var glas = new Glas();
            var glasaci = new List<Glasac>
            {
                new Glasac { DaLiJeGlasaoZaGradonacelnika = true },
                new Glasac { DaLiJeGlasaoZaNacelnika = true },
                new Glasac { DaLiJeGlasaoZaVijecnika = true }
            };
            var result = glas.UkupniIzborniPragKandidata(glasaci);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestUkupnogIzbornogPragaKandidata2()
        {
            var glas = new Glas();
            var glasaci = new List<Glasac>
                {
                    new Glasac { DaLiJeGlasaoZaGradonacelnika = false }
                };
            var result = glas.UkupniIzborniPragKandidata(glasaci);
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void TestSortiranjaKandidata()
        {
            var glas = new Glas();
            var kandidati = new List<Kandidat>
            {
                new Kandidat { BrojGlasova = 3 },
                new Kandidat { BrojGlasova = 1 },
                new Kandidat { BrojGlasova = 2 }
            };
            glas.SortiranjeKandidata(kandidati);
            CollectionAssert.AreEqual(new int[] { 3, 2, 1 }, kandidati.Select(k => k.BrojGlasova).ToArray());
        }
        [TestMethod]
        public void TestIspisStanjaGradonacelnika()
        {
            var glas = new Glas();
            var glasaci = new List<Glasac>
            {
                new Glasac { DaLiJeGlasaoZaGradonacelnika = true },
                new Glasac { DaLiJeGlasaoZaGradonacelnika = true },
                new Glasac { DaLiJeGlasaoZaGradonacelnika = true }
            };
            var kandidati = new List<Kandidat>
            {
                new Kandidat { Pozicija = Pozicija.gradonacelnik, BrojGlasova = 5, Ime = "K1", Prezime = "Prezime1", Stranka = new Stranka { Naziv = "StrankaA" } },
                new Kandidat { Pozicija = Pozicija.gradonacelnik, BrojGlasova = 4, Ime = "K2", Prezime = "Prezime2", Stranka = new Stranka { Naziv = "StrankaA" } },
                new Kandidat { Pozicija = Pozicija.gradonacelnik, BrojGlasova = 3, Ime = "K3", Prezime = "Prezime3", Stranka = new Stranka { Naziv = "StrankaB" } }
            };
            var ocekivaniIzlaz = new StringBuilder();
            ocekivaniIzlaz.AppendLine("Glasanje za gradonacelnika: ");
            ocekivaniIzlaz.AppendLine(" - Ukupan broj glasova: 3");
            ocekivaniIzlaz.AppendLine(" - Prva tri mjesta: ");
            ocekivaniIzlaz.AppendLine($"     K1 Prezime1 (StrankaA) - 5 glasova");
            ocekivaniIzlaz.AppendLine($"     K2 Prezime2 (StrankaA) - 4 glasova");
            ocekivaniIzlaz.AppendLine($"     K3 Prezime3 (StrankaB) - 3 glasova");
            var izlazKonzole = IspisKonzole(() => glas.IspisStanjaGradonacelnika(glasaci, kandidati));
            StringAssert.Contains(izlazKonzole, ocekivaniIzlaz.ToString());
        }
        [TestMethod]
        public void TestIspisStanjaNacelnika()
        {
            var glas = new Glas();
            var glasaci = new List<Glasac>
            {
                new Glasac { DaLiJeGlasaoZaNacelnika = true },
                new Glasac { DaLiJeGlasaoZaNacelnika = true },
                new Glasac { DaLiJeGlasaoZaNacelnika = true }
            };
            var kandidati = new List<Kandidat>
            {
                new Kandidat { Pozicija = Pozicija.nacelnik, BrojGlasova = 5, Ime = "K1", Prezime = "Prezime1", Stranka = new Stranka { Naziv = "StrankaA" } },
                new Kandidat { Pozicija = Pozicija.nacelnik, BrojGlasova = 4, Ime = "K2", Prezime = "Prezime2", Stranka = new Stranka { Naziv = "StrankaA" } },
                new Kandidat { Pozicija = Pozicija.nacelnik, BrojGlasova = 3, Ime = "K3", Prezime = "Prezime3", Stranka = new Stranka { Naziv = "StrankaB" } }
            };
            var ocekivaniIzlaz = new StringBuilder();
            ocekivaniIzlaz.AppendLine("Glasanje za nacelnika: ");
            ocekivaniIzlaz.AppendLine(" - Ukupan broj glasova: 3");
            ocekivaniIzlaz.AppendLine(" - Prva tri mjesta: ");
            ocekivaniIzlaz.AppendLine($"     K1 Prezime1 (StrankaA) - 5 glasova");
            ocekivaniIzlaz.AppendLine($"     K2 Prezime2 (StrankaA) - 4 glasova");
            ocekivaniIzlaz.AppendLine($"     K3 Prezime3 (StrankaB) - 3 glasova");
            var izlazKonzole = IspisKonzole(() => glas.IspisStanjaNacelnika(glasaci, kandidati));
            StringAssert.Contains(izlazKonzole, ocekivaniIzlaz.ToString());
        }
        [TestMethod]
        public void TestIspisStanjaVijecnika()
        {
            var glas = new Glas();
            var glasaci = new List<Glasac>
            {
                new Glasac { DaLiJeGlasaoZaVijecnika = true },
                new Glasac { DaLiJeGlasaoZaVijecnika = true },
                new Glasac { DaLiJeGlasaoZaVijecnika = true },
                new Glasac { DaLiJeGlasaoZaVijecnika = true },
                new Glasac { DaLiJeGlasaoZaVijecnika = true },
                new Glasac { DaLiJeGlasaoZaVijecnika = true }
            };
            var kandidati = new List<Kandidat>
            {
                new Kandidat { Pozicija = Pozicija.vijecnik, BrojGlasova = 5, Ime = "K1", Prezime = "Prezime1", Stranka = new Stranka { Naziv = "StrankaA" } },
                new Kandidat { Pozicija = Pozicija.vijecnik, BrojGlasova = 4, Ime = "K2", Prezime = "Prezime2", Stranka = new Stranka { Naziv = "StrankaA" } },
                new Kandidat { Pozicija = Pozicija.vijecnik, BrojGlasova = 3, Ime = "K3", Prezime = "Prezime3", Stranka = new Stranka { Naziv = "StrankaB" } },
                new Kandidat { Pozicija = Pozicija.vijecnik, BrojGlasova = 2, Ime = "K4", Prezime = "Prezime4", Stranka = new Stranka { Naziv = "StrankaB" } },
                new Kandidat { Pozicija = Pozicija.vijecnik, BrojGlasova = 1, Ime = "K5", Prezime = "Prezime5", Stranka = new Stranka { Naziv = "StrankaC" } }
            };
            var ocekivaniIzlaz = new StringBuilder();
            ocekivaniIzlaz.AppendLine("Glasanje za vijecnika: ");
            ocekivaniIzlaz.AppendLine(" - Ukupan broj glasova: 6");
            ocekivaniIzlaz.AppendLine(" - Prvih pet mjesta: ");
            ocekivaniIzlaz.AppendLine($"     K1 Prezime1 (StrankaA) - 5 glasova");
            ocekivaniIzlaz.AppendLine($"     K2 Prezime2 (StrankaA) - 4 glasova");
            ocekivaniIzlaz.AppendLine($"     K3 Prezime3 (StrankaB) - 3 glasova");
            ocekivaniIzlaz.AppendLine($"     K4 Prezime4 (StrankaB) - 2 glasova");
            ocekivaniIzlaz.AppendLine($"     K5 Prezime5 (StrankaC) - 1 glasova");
            var izlazKonzole = IspisKonzole(() => glas.IspisStanjaVijecnika(glasaci, kandidati));
            StringAssert.Contains(izlazKonzole, ocekivaniIzlaz.ToString());
        }
        [TestMethod]
        public void TestIspisaStanjaStranke()
        {
            var glas = new Glas();
            var stranke = new List<Stranka>
            {
                new Stranka { Naziv = "StrankaA", BrojGlasova = 10 },
                new Stranka { Naziv = "StrankaB", BrojGlasova = 7 },
                new Stranka { Naziv = "StrankaC", BrojGlasova = 5 }
            };
            var izlazKonzole = IspisKonzole(() => glas.IspisStanjaStranke(stranke));
            StringAssert.Contains(izlazKonzole, "Ukupan broj glasova: 22");
            StringAssert.Contains(izlazKonzole, "Broj glasova za stranke:");
            StringAssert.Contains(izlazKonzole, " StrankaA: 10");
            StringAssert.Contains(izlazKonzole, " StrankaB: 7");
            StringAssert.Contains(izlazKonzole, " StrankaC: 5");
        }
        [TestMethod]
        public void TestIspisaStanja_GlasanjeNijePocelo()
        {
            var glas = new Glas();
            var glasaci = new List<Glasac>();
            var kandidati = new List<Kandidat>();
            var stranke = new List<Stranka>();
            var glasovi = new List<Glas>();
            var izlazKonzole = IspisKonzole(() => glas.IspisStanja(glasaci, stranke, kandidati, glasovi));
            StringAssert.Contains(izlazKonzole, "Glasanje jos uvijek nije pocelo!");
        }
        [TestMethod]
        public void TestIspisStanja_NelegalniIzbori()
        {
            var glas = new Glas();
            var glasaci = new List<Glasac> { new Glasac() };
            var kandidati = new List<Kandidat> { new Kandidat() };
            var stranke = new List<Stranka> { new Stranka() };
            var glasovi = new List<Glas> { new Glas() };
            var izlazKonzole = IspisKonzole(() => glas.IspisStanja(glasaci, stranke, kandidati, glasovi));
            StringAssert.Contains(izlazKonzole, "Izbori nisu legalni ili jos uvijek traju!");
        }
        [TestMethod]
        public void TestIspisStanja_Izbori()
        {
            var glas = new Glas();
            var glasaci = new List<Glasac> {
                new Glasac { DaLiJeGlasaoZaGradonacelnika = true, DaLiJeGlasaoZaVijecnika = true },
                new Glasac { DaLiJeGlasaoZaGradonacelnika = true, DaLiJeGlasaoZaNacelnika = true },
                new Glasac { DaLiJeGlasaoZaNacelnika = true },
            };
            var kandidati = new List<Kandidat> { new Kandidat() };
            var stranke = new List<Stranka> { new Stranka() };
            var glasovi = new List<Glas> { new Glas() };
            var izlazKonzole = IspisKonzole(() => glas.IspisStanja(glasaci, stranke, kandidati, glasovi));
            StringAssert.Contains(izlazKonzole, "Ukupan broj registrovanih glasaca: " + glasaci.Count);
        }
        [TestMethod]
        public void TestDaLiJeGlasanjePocelo_SaDummyObjektom()
        {
            var dummyGlas = new DummyGlas();
            dummyGlas.Add(new Glas());
            Glas glas = new Glas();
            bool rezultat = glas.DaLiJeGlasanjePocelo(dummyGlas);
            Assert.IsTrue(rezultat);
        }
        public static IEnumerable<object[]> ReadXML()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("..\\..\\..\\Glasovi.xml");
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                string imeGlasaca = node["Glasac"]["Ime"].InnerText;
                string prezimeGlasaca = node["Glasac"]["Prezime"].InnerText;
                string imeKandidata = node["Kandidat"]["Ime"].InnerText;
                string prezimeKandidata = node["Kandidat"]["Prezime"].InnerText;

                yield return new object[] { imeGlasaca, prezimeGlasaca, imeKandidata, prezimeKandidata };
            }
        }
        public static IEnumerable<object[]> ReadCSV()
        {
            using (var reader = new StreamReader("..\\..\\..\\Glasovi.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var rows = csv.GetRecords<dynamic>();
                foreach (var row in rows)
                {
                    var values = ((IDictionary<String, Object>)row).Values;
                    var elements = values.Select(elem => elem.ToString()).ToList();
                    yield return new object[] { elements[0], elements[1], elements[2], elements[3] };
                }
            }
        }
        static IEnumerable<object[]> GlasXML
        {
            get
            {
                return ReadXML();
            }
        }
        static IEnumerable<object[]> GlasCSV
        {
            get
            {
                return ReadCSV();
            }
        }

        [DynamicData(nameof(GlasXML))]
        [TestMethod]
        public void TestKonstruktoraGlasaXML(string imeGlasaca, string prezimeGlasaca, string imeKandidata, string prezimeKandidata)
        {
            Glasac glasac = new Glasac(imeGlasaca, prezimeGlasaca, new DateTime(1979, 12, 27));
            Kandidat kandidat = new Kandidat(imeKandidata, prezimeKandidata, new DateTime(1966, 10, 15), Pozicija.gradonacelnik, "Ovo je opis", null);
            Glas g = new Glas(glasac, kandidat);
        }

        [DynamicData(nameof(GlasCSV))]
        [TestMethod]
        public void TestKonstruktoraGlasaCSV(string imeGlasaca, string prezimeGlasaca, string imeKandidata, string prezimeKandidata)
        {
            Glasac glasac = new Glasac(imeGlasaca, prezimeGlasaca, new DateTime(1979, 12, 27));
            Kandidat kandidat = new Kandidat(imeKandidata, prezimeKandidata, new DateTime(1966, 10, 15), Pozicija.gradonacelnik, "Ovo je opis", null);
            Glas g = new Glas(glasac, kandidat);
        }

        //White box
        [TestMethod]
        public void TestIspisStanjaGradonacelnika_Put1()
        {
            var glas = new Glas();
            var glasaci = new List<Glasac>();
            var kandidati = new List<Kandidat>();
            var ocekivaniIzlaz = new StringBuilder();
            ocekivaniIzlaz.AppendLine("Glasanje za gradonacelnika: ");
            ocekivaniIzlaz.AppendLine(" - Ukupan broj glasova: 0");
            ocekivaniIzlaz.AppendLine(" - Prva tri mjesta: ");

            var izlazKonzole = IspisKonzole(() => glas.IspisStanjaGradonacelnika(glasaci, kandidati));

            StringAssert.Contains(izlazKonzole, ocekivaniIzlaz.ToString());
        }

        [TestMethod]
        public void TestIspisStanjaGradonacelnika_Put3()
        {
            var glas = new Glas();
            var glasaci = new List<Glasac>
             {
                 new Glasac { DaLiJeGlasaoZaGradonacelnika = true },
                 new Glasac { DaLiJeGlasaoZaGradonacelnika = true },
                    new Glasac { DaLiJeGlasaoZaGradonacelnika = true }
             };

            var kandidati = new List<Kandidat>
             {
                 new Kandidat { Pozicija = Pozicija.vijecnik, BrojGlasova = 5, Ime = "K1", Prezime = "Prezime1", Stranka = new Stranka { Naziv = "StrankaA" } },
                 new Kandidat { Pozicija = Pozicija.nacelnik, BrojGlasova = 4, Ime = "K2", Prezime = "Prezime2", Stranka = new Stranka { Naziv = "StrankaA" } },
                 new Kandidat { Pozicija = Pozicija.nacelnik, BrojGlasova = 3, Ime = "K3", Prezime = "Prezime3", Stranka = new Stranka { Naziv = "StrankaB" } }
             };

            var ocekivaniIzlaz = new StringBuilder();
            ocekivaniIzlaz.AppendLine("Glasanje za gradonacelnika: ");
            ocekivaniIzlaz.AppendLine(" - Ukupan broj glasova: 3");
            ocekivaniIzlaz.AppendLine(" - Prva tri mjesta: ");

            var izlazKonzole = IspisKonzole(() => glas.IspisStanjaGradonacelnika(glasaci, kandidati));

            StringAssert.Contains(izlazKonzole, ocekivaniIzlaz.ToString());
        }

        [TestMethod]
        public void TestIspisStanjaGradonacelnika_Put2()
        {
            var glas = new Glas();
            var glasaci = new List<Glasac>
            {
                new Glasac { DaLiJeGlasaoZaGradonacelnika = true },
                new Glasac { DaLiJeGlasaoZaGradonacelnika = true },
                new Glasac { DaLiJeGlasaoZaGradonacelnika = true }
            };

            var kandidati = new List<Kandidat>
            {
                 new Kandidat { Pozicija = Pozicija.nacelnik, BrojGlasova = 3, Ime = "K3", Prezime = "Prezime3", Stranka = new Stranka { Naziv = "StrankaB" } }
            };

            var ocekivaniIzlaz = new StringBuilder();
            ocekivaniIzlaz.AppendLine("Glasanje za gradonacelnika: ");
            ocekivaniIzlaz.AppendLine(" - Ukupan broj glasova: 3");
            ocekivaniIzlaz.AppendLine(" - Prva tri mjesta: ");

            var izlazKonzole = IspisKonzole(() => glas.IspisStanjaGradonacelnika(glasaci, kandidati));

            StringAssert.Contains(izlazKonzole, ocekivaniIzlaz.ToString());
        }
        [TestMethod]
        public void TestIspisStanjaGradonacelnika_Put4()
        {
            var glas = new Glas();
            var glasaci = new List<Glasac>
            {
                 new Glasac { DaLiJeGlasaoZaGradonacelnika = false },
                 new Glasac { DaLiJeGlasaoZaGradonacelnika = false },
                    new Glasac { DaLiJeGlasaoZaGradonacelnika = false }
            };

            var kandidati = new List<Kandidat>
            {
                 new Kandidat { Pozicija = Pozicija.gradonacelnik, BrojGlasova = 5, Ime = "K1", Prezime = "Prezime1", Stranka = new Stranka { Naziv = "StrankaA" } },
                 new Kandidat { Pozicija = Pozicija.gradonacelnik, BrojGlasova = 5, Ime = "K1", Prezime = "Prezime1", Stranka = new Stranka { Naziv = "StrankaA" } },
                 new Kandidat { Pozicija = Pozicija.gradonacelnik, BrojGlasova = 4, Ime = "K2", Prezime = "Prezime2", Stranka = new Stranka { Naziv = "StrankaA" } }
            };

            var ocekivaniIzlaz = new StringBuilder();
            ocekivaniIzlaz.AppendLine("Glasanje za gradonacelnika: ");
            ocekivaniIzlaz.AppendLine(" - Ukupan broj glasova: 0");
            ocekivaniIzlaz.AppendLine(" - Prva tri mjesta: ");

            var izlazKonzole = IspisKonzole(() => glas.IspisStanjaGradonacelnika(glasaci, kandidati));

            StringAssert.Contains(izlazKonzole, ocekivaniIzlaz.ToString());
        }
    }
}
