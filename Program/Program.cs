using System;
using Microsoft.VisualBasic.FileIO;
using System.Collections.Generic;

namespace Versenyzok
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = "pilotak.csv";
            List<Pilota> pilotaLista = BeolvasPilotak(fileName);

            Console.WriteLine("3. feladat: Az állomány {0} adatsort tartalmaz.", pilotaLista.Count);
            Console.WriteLine("4. feladat: Az utolsó sorban szereplő pilóta neve: {0}", pilotaLista[pilotaLista.Count - 1].Nev);
            Console.WriteLine("5. feladat: A XIX. században született pilóták:");
            foreach (Pilota pilota in pilotaLista)
            {
                if (pilota.SzuletesiDatum.Year < 1901)
                {
                    Console.WriteLine("{0} - {1}", pilota.Nev, pilota.SzuletesiDatum.ToString("yyyy. MM. dd"));
                }
            }

            Pilota legkisebbRajtszamPilota = LegkisebbRajtszamPilota(pilotaLista);
            if (legkisebbRajtszamPilota != null)
            {
                Console.WriteLine("6. feladat: A legkisebb értékű rajtszám pilótájának nemzetisége: {0}", legkisebbRajtszamPilota.Nemzetiseg);
            }

            List<string> rajtszamok = MegkapottRajtszamok(pilotaLista);
            Console.WriteLine("7. feladat: A megkapott rajtszámok:");
            foreach (string rajtszam in rajtszamok)
            {
                Console.WriteLine(rajtszam);
            }

            Console.ReadLine();
        }

        static List<Pilota> BeolvasPilotak(string fileLocation)
        {
            List<Pilota> pilotaLista = new List<Pilota>();
            using (TextFieldParser parser = new TextFieldParser(fileLocation))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(";");

                bool firstLine = true;
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();

                    
                    if (firstLine)
                    {
                        firstLine = false;
                        continue;
                    }

                    string nev = fields[0];
                    DateTime szuletesiDatum = DateTime.Parse(fields[1]);
                    string nemzetiseg = fields[2];
                    string rajtszam = fields[3];

                    Pilota pilota = new Pilota(nev, szuletesiDatum, nemzetiseg, rajtszam);
                    pilotaLista.Add(pilota);
                }
            }
            return pilotaLista;
        }

        static Pilota LegkisebbRajtszamPilota(List<Pilota> pilotaLista)
        {
            Pilota legkisebbRajtszamPilota = null;
            foreach (Pilota pilota in pilotaLista)
            {
                if (!string.IsNullOrEmpty(pilota.Rajtszam))
                {
                    if (legkisebbRajtszamPilota == null || int.Parse(pilota.Rajtszam) < int.Parse(legkisebbRajtszamPilota.Rajtszam))
                    {
                        legkisebbRajtszamPilota = pilota;
                    }
                }
            }
            return legkisebbRajtszamPilota;
        }

        static List<string> MegkapottRajtszamok(List<Pilota> pilotaLista)
        {
            List<string> rajtszamok = new List<string>();
            foreach (Pilota pilota in pilotaLista)
            {
                if (!string.IsNullOrEmpty(pilota.Rajtszam) && !rajtszamok.Contains(pilota.Rajtszam))
                {
                    rajtszamok.Add(pilota.Rajtszam);
                }
            }
            return rajtszamok;
        }
    }

    class Pilota
    {
        public string Nev { get; }
        public DateTime SzuletesiDatum { get; }
        public string Nemzetiseg { get; }
        public string Rajtszam { get; }

        public Pilota(string nev, DateTime szuletesiDatum, string nemzetiseg, string rajtszam)
        {
            Nev = nev;
            SzuletesiDatum = szuletesiDatum;
            Nemzetiseg = nemzetiseg;
            Rajtszam = rajtszam;
        }
    }
}
