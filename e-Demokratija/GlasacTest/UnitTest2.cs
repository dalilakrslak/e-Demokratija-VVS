using CsvHelper;
using e_Demokratija;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Xml;

namespace Testovi
{
    [TestClass]
    public class UnitTest2
    {
        public class DummyStranke : List<Stranka>
        {
            public new void Add(Stranka stranka)
            {
                base.Add(stranka);
            }

        }
        private XmlReaderSettings settings = new XmlReaderSettings();
        [TestMethod]
        public void TestIspravnogKonstruktoraKlaseSupervizor()
        {
            Supervizor s = new Supervizor();
            Assert.AreEqual("admin", s.Password);
        }
        [TestMethod]
        public void TestIspravnogKonstruktoraKlaseSupervizor2()
        {
            Supervizor s = new Supervizor("pass");
            Assert.AreEqual("pass", s.Password);
        }
        [TestMethod]
        public void TestDaLiJePasswordIspravan_VracaTrue()
        {
            Supervizor s = new Supervizor();
            Assert.IsTrue(s.DaLiJePasswordIspravan("admin"));
        }
        [TestMethod]
        public void TestDaLiStrankaPostoji_VracaFalseZaPraznuListu()
        {
            Supervizor s = new Supervizor();
            List<Stranka> praznaLista = new List<Stranka>();
            Assert.IsFalse(s.DaLiStrankaPostoji(praznaLista, "SDA"));
        }

        [TestMethod]
        public void TestDaLiStrankaPostoji_VracaFalseZaNeispravanNaziv()
        {
            Supervizor s = new Supervizor();
            List<Stranka> stranke = new List<Stranka>
            {
                new Stranka { Naziv = "SDA" },
                new Stranka { Naziv = "HDZ" },
            };
            Assert.IsFalse(s.DaLiStrankaPostoji(stranke, "NeispravanNaziv"));
        }

        [TestMethod]
        public void TestDaLiStrankaPostoji_VracaTrueZaPravilanNaziv()
        {
            Supervizor s = new Supervizor();
            List<Stranka> stranke = new List<Stranka>
            {
                new Stranka { Naziv = "SDA" },
                new Stranka { Naziv = "HDZ" },
            };
            Assert.IsTrue(s.DaLiStrankaPostoji(stranke, "SDA"));
        }
        [TestMethod]
        public void TestDaLiKandidatPostoji_VracaFalseZaPraznuListu()
        {
            Supervizor s = new Supervizor();
            List<Kandidat> praznaLista = new List<Kandidat>();
            Assert.IsFalse(s.DaLiKandidatPostoji(praznaLista, 1));
        }

        [TestMethod]
        public void TestDaLiKandidatPostoji_VracaFalseZaNeispravanRedniBroj()
        {
            Supervizor s = new Supervizor();
            List<Kandidat> kandidati = new List<Kandidat>
            {
                new Kandidat { RedniBroj = 1 },
                new Kandidat { RedniBroj = 2 },
            };
            Assert.IsFalse(s.DaLiKandidatPostoji(kandidati, 3));
        }

        [TestMethod]
        public void TestDaLiKandidatPostoji_VracaTrueZaPravilanRedniBroj()
        {
            Supervizor s = new Supervizor();
            List<Kandidat> kandidati = new List<Kandidat>
            {
                new Kandidat { RedniBroj = 1 },
                new Kandidat { RedniBroj = 2 },
            };
            Assert.IsTrue(s.DaLiKandidatPostoji(kandidati, 1));
        }
        [TestMethod]
        public void TestGenerisiNoviPassword_PasswordSeAzurira()
        {
            Supervizor supervizor = new Supervizor();

            string stariPassword = supervizor.Password;
            string noviPassword = supervizor.GenerisiNoviPassword();
            supervizor.Password = noviPassword;
            Assert.AreNotEqual(stariPassword, noviPassword);
            Assert.AreNotEqual(stariPassword, supervizor.Password);
        }
        [TestMethod]
        public void DaLiPostojiKandidatSaImenom_KandidatPostoji_VracaTrue()
        {
            Supervizor supervizor = new Supervizor();
            List<Kandidat> kandidati = new List<Kandidat>
            {
                new Kandidat { Ime = "John" },
                new Kandidat { Ime = "Jane" },
                new Kandidat { Ime = "Bob" }
            };
            string imeKandidata = "Jane";

            bool rezultat = supervizor.DaLiPostojiKandidatSaImenom(kandidati, imeKandidata);

            Assert.IsTrue(rezultat);
        }

        [TestMethod]
        public void DaLiPostojiKandidatSaImenom_KandidatNePostoji_VracaFalse()
        {
            Supervizor supervizor = new Supervizor();
            List<Kandidat> kandidati = new List<Kandidat>
            {
                new Kandidat { Ime = "John" },
                new Kandidat { Ime = "Jane" },
                new Kandidat { Ime = "Bob" }
            };
            string imeKandidata = "Alice";

            bool rezultat = supervizor.DaLiPostojiKandidatSaImenom(kandidati, imeKandidata);

            Assert.IsFalse(rezultat);
        }
        public static IEnumerable<object[]> ReadXML()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("..\\..\\..\\Supervizor.xml");
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                List<string> elements = new List<string>();
                foreach (XmlNode innerNode in node)
                {
                    elements.Add(innerNode.InnerText);
                }
                yield return new object[] { elements[0] };
            }
        }
        public static IEnumerable<object[]> ReadCSV()
        {
            using (var reader = new StreamReader("..\\..\\..\\Supervizor.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var rows = csv.GetRecords<dynamic>();
                foreach (var row in rows)
                {
                    var values = ((IDictionary<String, Object>)row).Values;
                    var elements = values.Select(elem => elem.ToString()).ToList();
                    yield return new object[] { elements[0] };
                }
            }
        }
        static IEnumerable<object[]> SupervizorXML
        {
            get
            {
                return ReadXML();
            }
        }
        static IEnumerable<object[]> SupervizorCSV
        {
            get
            {
                return ReadCSV();
            }
        }

        // DDT TESTS
        [DynamicData(nameof(SupervizorXML))]
        [TestMethod]
        public void TestKonstruktoraSupervizoraXML(string password)
        {
            Supervizor s = new Supervizor(password);
        }
        [DynamicData(nameof(SupervizorXML))]
        [TestMethod]
        public void TestKonstruktoraSupervizoraCSV(string password)
        {
            Supervizor s = new Supervizor(password);
        }
        [TestMethod]
        public void TestDaLiStrankaPostoji_SaDummyObjektom()
        {
            var dummyStranke = new DummyStranke();
            dummyStranke.Add(new Stranka { Naziv = "SDA" });
            Supervizor supervizor = new Supervizor();
            bool rezultat = supervizor.DaLiStrankaPostoji(dummyStranke, "SDA");
            Assert.IsTrue(rezultat);
        }
    }
}
