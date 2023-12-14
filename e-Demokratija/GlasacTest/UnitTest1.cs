using CsvHelper;
using e_Demokratija;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;

namespace Testovi
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestIspravnogKonstruktoraKlaseGlasac1()
        {
            Glasac g = new Glasac("Dalila", "Kršlak", new DateTime(2001, 11, 23));
            g.DaLiJeImeIspravno(g.Ime);
            g.DaLiJePrezimeIspravno(g.Prezime);
            g.DaLiJeDatumaRodjenjaIspravan(g.DatumRodjenja);
            Assert.AreEqual("Dalila", g.Ime);
            Assert.AreEqual("Kršlak", g.Prezime);
            Assert.AreEqual(new DateTime(2001, 11, 23), g.DatumRodjenja);
        }
        [TestMethod]
        public void TestIspravnogKonstruktoraKlaseGlasac2()
        {
            Glasac g = new Glasac("Dalila", "Kršlak", new DateTime(2001, 11, 23));
            Assert.AreEqual("Dalila", g.Ime);
            Assert.AreEqual("Kršlak", g.Prezime);
            Assert.AreEqual(new DateTime(2001, 11, 23), g.DatumRodjenja);
            Assert.IsFalse(g.DaLiJeGlasaoZaGradonacelnika);
            Assert.IsFalse(g.DaLiJeGlasaoZaNacelnika);
            Assert.IsFalse(g.DaLiJeGlasaoZaVijecnika);
        }
        [TestMethod]
        public void TestIspravneDodjeleKoda1()
        {
            Glasac g = new Glasac("Dalila", "Krslak", new DateTime(2001, 11, 23));
            Assert.AreEqual("dkrslak231101", g.Kod);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DaLiJeImeIspravno_KratkoIme_BacaIzuzetak()
        {
            Glasac g = new Glasac("D", "Kršlak", new DateTime(2001, 11, 23));
            g.DaLiJeImeIspravno(g.Ime);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DaLiJeImeIspravno_DugoIme_BacaIzuzetak()
        {
            Glasac g = new Glasac("OvoJeNekoJakooooDugoImeKojeNiNePostoji", "Kršlak", new DateTime(2001, 11, 23));
            g.DaLiJeImeIspravno(g.Ime);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DaLiJeImeIspravno_PraznoIme_BacaIzuzetak()
        {
            Glasac g = new Glasac();
            string ime = "";
            g.DaLiJeImeIspravno(ime);
            g = new Glasac(ime, "Kršlak", new DateTime(2001, 11, 23));
        }
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void DaLiJeImeIspravno_Null_BacaIzuzetak()
        {
            Glasac g = new Glasac(null, "Kršlak", new DateTime(2001, 11, 23));
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DaLiJeImeIspravno_ZnakoviUImenu_BacaIzuzetak()
        {
            Glasac g = new Glasac("Dalila2311", "Kršlak", new DateTime(2001, 11, 23));
            g.DaLiJeImeIspravno(g.Ime);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DaLiJeImeIspravno_ImeSaCrticama_BacaIzuzetak()
        {
            Glasac g = new Glasac("Dalila-Daki-Dals", "Kršlak", new DateTime(2001, 11, 23));
            g.DaLiJeImeIspravno(g.Ime);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DaLiJePrezimeIspravno_KratkoPrezime_BacaIzuzetak()
        {
            Glasac g = new Glasac("Dalila", "K", new DateTime(2001, 11, 23));
            g.DaLiJePrezimeIspravno(g.Prezime);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DaLiJePrezimeIspravno_DugoPrezime_BacaIzuzetak()
        {
            Glasac g = new Glasac("Dalila", "OvoJeNekoJakooooDugoPrezimeKojeNiNePostoji", new DateTime(2001, 11, 23));
            g.DaLiJePrezimeIspravno(g.Prezime);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DaLiJePrezimeIspravno_PraznoPrezime_BacaIzuzetak()
        {
            Glasac g = new Glasac();
            string prezime = "";
            g.DaLiJePrezimeIspravno(prezime);
            g = new Glasac("Dalila", prezime, new DateTime(2001, 11, 23));
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DaLiJePrezimeIspravno_ZnakoviUPrezimenu_BacaIzuzetak()
        {
            Glasac g = new Glasac("Dalila", "Kršlak2311", new DateTime(2001, 11, 23));
            g.DaLiJePrezimeIspravno(g.Prezime);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DaLiJePrezimeIspravno_PrezimeSaCrticama_BacaIzuzetak()
        {
            Glasac g = new Glasac("Dalila", "Kršlak-Kršlak-Kršlak", new DateTime(2001, 11, 23));
            g.DaLiJePrezimeIspravno(g.Prezime);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DaLiJeDatumaRodjenjaIspravan_Buducnost_BacaIzuzetak()
        {
            Glasac g = new Glasac("Dalila", "Kršlak", new DateTime(2025, 11, 23));
            g.DaLiJeDatumaRodjenjaIspravan(g.DatumRodjenja);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DaLiJeDatumaRodjenjaIspravan_Maloljetnost_BacaIzuzetak()
        {
            Glasac g = new Glasac("Dalila", "Kršlak", new DateTime(2007, 11, 23));
            g.DaLiJeDatumaRodjenjaIspravan(g.DatumRodjenja);
        }
        [TestMethod]
        public void TestSetterImena()
        {
            Glasac g = new Glasac("Dalida", "Kršlak", new DateTime(2001, 11, 23));
            g.Ime = "Dalila";
            Assert.AreEqual("Dalila", g.Ime);
            StringAssert.StartsWith(g.Ime, "D");
            StringAssert.EndsWith(g.Ime, "a");
            StringAssert.Contains(g.Ime, "alil");
        }
        [TestMethod]
        public void TestSetterPrezimena()
        {
            Glasac g = new Glasac("Dalila", "Kršl", new DateTime(2001, 11, 23));
            g.Prezime = "Kršlak";
            Assert.AreEqual("Kršlak", g.Prezime);
            StringAssert.StartsWith(g.Prezime, "K");
            StringAssert.EndsWith(g.Prezime, "k");
            StringAssert.Contains(g.Prezime, "ršla");
        }
        [TestMethod]
        public void TestSetterDatumaRodjenja()
        {
            Glasac g = new Glasac("Dalila", "Kršlak", new DateTime(2004, 11, 23));
            g.DatumRodjenja = new DateTime(2001, 11, 23);
            Assert.AreEqual(new DateTime(2001, 11, 23), g.DatumRodjenja);
        }
        [TestMethod]
        public void TestSetterKoda()
        {
            Glasac g = new Glasac("Dalila", "Kršlak", new DateTime(2001, 11, 23));
            g.Kod = "dalilakrslak231101";
            Assert.AreEqual("dalilakrslak231101", g.Kod);
        }
        [TestMethod]
        public void TestSetterDaLiJeGlasaoZaGradonacelnika()
        {
            Glasac g = new Glasac("Dalila", "Kršlak", new DateTime(2001, 11, 23));
            g.DaLiJeGlasaoZaGradonacelnika = true;
            Assert.IsTrue(g.DaLiJeGlasaoZaGradonacelnika);
        }
        [TestMethod]
        public void TestSetterDaLiJeGlasaoZaNacelnika()
        {
            Glasac g = new Glasac("Dalila", "Kršlak", new DateTime(2001, 11, 23));
            g.DaLiJeGlasaoZaNacelnika = true;
            Assert.IsTrue(g.DaLiJeGlasaoZaNacelnika);
        }
        [TestMethod]
        public void TestSetterDaLiJeGlasaoZaVijecnika() 
        { 
            Glasac g = new Glasac("Dalila", "Kršlak", new DateTime(2001, 11, 23));
            g.DaLiJeGlasaoZaVijecnika = true;
            Assert.IsTrue(g.DaLiJeGlasaoZaVijecnika);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestZamjenskiObjekat()
        {
            Glasac g = new Glasac("Dalila", "Kršlak", new DateTime(2001, 11, 23));
            Spy temp = new Spy();
            temp.Glasao = false;
            Assert.IsTrue(g.VjerodostojnostGlasaca(temp));
            temp.Glasao = true;
            Assert.IsTrue(g.VjerodostojnostGlasaca(temp));
        }
        public static IEnumerable<object[]> ReadXML()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("..\\..\\..\\Glasaci.xml");
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
            using (var reader = new StreamReader("..\\..\\..\\Glasaci.csv"))
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
        static IEnumerable<object[]> GlasaciXML
        {
            get
            {
                return ReadXML();
            }
        }
        static IEnumerable<object[]> GlasaciCSV
        {
            get
            {
                return ReadCSV();
            }
        }

        // DDT TESTS
        [DynamicData(nameof(GlasaciXML))]
        [TestMethod]
        public void TestKonstruktoraGlasacaXML(string ime, string prezime)
        {
            Glasac g = new Glasac(ime, prezime, new DateTime(2001, 11, 23));
            
        }
        [DynamicData(nameof(GlasaciCSV))]
        [TestMethod]
        public void TestKonstruktoraGlasacaCSV(string ime, string prezime)
        {
            Glasac g = new Glasac(ime, prezime, new DateTime(2001, 11, 23));

        }
    }
}
