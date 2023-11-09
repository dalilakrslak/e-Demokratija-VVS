using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;

namespace e_Demokratija
{
    public class CSVMaker
    {
        private List<Glasac> glasaci = new List<Glasac>();
        private List<Stranka> stranke = new List<Stranka>();
        private List<Kandidat> kandidati = new List<Kandidat>();

        public CSVMaker()
        {
            var putanjaGlasaci = Path.Combine(Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.Parent.FullName, "e-Demokratija"), "glasaci.csv");
            var putanjaKandidati = Path.Combine(Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.Parent.FullName, "e-Demokratija"), "kandidati.csv");
            var putanjaStranke = Path.Combine(Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.Parent.FullName, "e-Demokratija"), "stranke.csv");
            var putanjaGlasovi = Path.Combine(Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.Parent.FullName, "e-Demokratija"), "glasovi.csv");

            if (!File.Exists(putanjaGlasaci))
            {
                File.Create(putanjaGlasaci).Close();
            }

            if (!File.Exists(putanjaKandidati))
            {
                File.Create(putanjaKandidati).Close();
            }

            if (!File.Exists(putanjaStranke))
            {
                File.Create(putanjaStranke).Close();
            }

            if (!File.Exists(putanjaGlasovi))
            {
                File.Create(putanjaGlasovi).Close();
            }
        }

        public List<Glasac> CitajGlasaceIzCSV(string putanjaDoCSV)
        {
            using (var reader = new StreamReader(putanjaDoCSV))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                glasaci = csv.GetRecords<Glasac>().ToList();
                return glasaci;
            }
        }
        public List<Stranka> CitajStrankeIzCSV(string putanjaDoCSV)
        {
            using (var reader = new StreamReader(putanjaDoCSV))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                stranke = csv.GetRecords<Stranka>().ToList();
                return stranke;
            }
        }
        public List<Kandidat> CitajKandidateIzCSV(string putanjaDoCSV)
        {
            using (var reader = new StreamReader(putanjaDoCSV))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                kandidati = csv.GetRecords<Kandidat>().ToList();
                return kandidati;
            }
        }

        public void DodajGlasaca(Glasac noviGlasac)
        {
            glasaci.Add(noviGlasac);
            using (var writer = new StreamWriter(Path.Combine(Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.Parent.FullName, "e-Demokratija"), "glasaci.csv")))
            using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csv.WriteRecords(glasaci);
            }
        }
        public void DodajStranku(Stranka novaStranka)
        {
            stranke.Add(novaStranka);
            using (var writer = new StreamWriter(Path.Combine(Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.Parent.FullName, "e-Demokratija"), "stranke.csv")))
            using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csv.WriteRecords(stranke);
            }
        }
        public void DodajKandidata(Kandidat noviKandidat)
        {
            kandidati.Add(noviKandidat);
            using (var writer = new StreamWriter(Path.Combine(Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.Parent.FullName, "e-Demokratija"), "kandidati.csv")))
            using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csv.WriteRecords(kandidati);
            }
        }
        public void AzurirajStrankeIzCSV(List<Stranka> stranke)
        {
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture);
            csvConfig.HasHeaderRecord = true; 

            using (var writer = new StreamWriter(Path.Combine(Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.Parent.FullName, "e-Demokratija"), "stranke.csv")))
            using (var csv = new CsvWriter(writer, csvConfig))
            {
                csv.WriteRecords(stranke);
            }
        }
        public void AzurirajKandidateIzCSV(List<Kandidat> kandidati)
        {
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture);
            csvConfig.HasHeaderRecord = true;

            using (var writer = new StreamWriter(Path.Combine(Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.Parent.FullName, "e-Demokratija"), "kandidati.csv")))
            using (var csv = new CsvWriter(writer, csvConfig))
            {
                csv.WriteRecords(kandidati);
            }
        }
    }
}