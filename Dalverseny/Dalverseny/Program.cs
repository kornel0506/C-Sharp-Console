using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;

namespace Dalverseny
{
    class VezerloOsztaly
    {
        private List<Versenyzo> versenyzok = new List<Versenyzo>();
        private int zsuriLetszam = 5;
        private int pontHatar = 10;
        public void Start() 
        {
            AdatBevitel();

            Kiiratas("\nRésztvevők:\n");
            Verseny();
            Kiiratas("\nEredmények:\n");

            Eredmenyek();
            Keresesek();        
        }

        private void AdatBevitel()
        {
            Versenyzo versenyzo;
            string nev, szak;
            int sorszam = 1;

            StreamReader sr = new StreamReader("versenyzok.txt");

            while (!sr.EndOfStream)
            {
                nev = sr.ReadLine();
                szak = sr.ReadLine();

                versenyzo = new Versenyzo(sorszam, nev, szak);

                versenyzok.Add(versenyzo);

                sorszam++;
            }
            sr.Close();
        }

        private void Kiiratas(string cim)
        {
            Console.WriteLine(cim);
            foreach (Versenyzo enekes in versenyzok)
            {
                Console.WriteLine(enekes);
            }
        }

        private void Verseny()
        {
            Random rand = new Random();
            int pont;
            foreach (Versenyzo versenyzo in versenyzok)
            {
                for (int i = 1; i <= zsuriLetszam; i++)
                {
                    pont = rand.Next(pontHatar);
                    versenyzo.PontotKap(pont);
                }
            }
        }

        private void Eredmenyek()
        {
            Nyertes();
            Sorrend();
        }

        private void Nyertes()
        {
            int max = versenyzok[0].PontSzam;

            foreach (Versenyzo enekes in versenyzok)
            {
                if (enekes.pontSzam > max)
                {
                    max = enekes.pontSzam;
                }
            }

            Console.WriteLine("\n A legjobb(ak)\n");
            foreach (Versenyzo enekes in versenyzok)
            {
                if (enekes.pontSzam == max)
                {
                    Console.WriteLine(enekes);
                }
            }
        }

        private void Sorrend()
        { 
            Versenyzo temp;
            for (int i = 0; i < versenyzok.Count - 1; i++)
            {
                for (int j = i + 1; j < versenyzok.Count; j++)
                {
                    if (versenyzok[i].pontSzam < versenyzok[j].pontSzam)
                    {
                        temp = versenyzok[i];
                        versenyzok[i] = versenyzok[j];
                        versenyzok[j] = temp;
                    }
                }
            }

            Kiiratas("\nEredménytábla\n");
        }

        private void Keresesek()
        {
            Console.WriteLine("\nAdott szakhoz tartozó énekesek keresése\n");
            Console.WriteLine("\nKeres valakit? (i/n)");
            char valasz;
            while (!char.TryParse(Console.ReadLine(), out valasz))
            {
                Console.WriteLine("Egy karaktert írjon. ");
            }
            string szak;
            bool vanIlyen;

            while (valasz == 'i' || valasz == 'I')
            {
                Console.WriteLine("Szak: ");
                szak = Console.ReadLine();
                vanIlyen = false;

                foreach (Versenyzo enekes in versenyzok)
                {
                    if (enekes.Szak == szak)
                    {
                        Console.WriteLine(enekes);
                        vanIlyen = true;
                    }
                }

                if (!vanIlyen)
                {
                    Console.WriteLine("Erről a szakról senki sem indult");
                }

                Console.WriteLine("\nKeres még valakit? (i/n) ");
                valasz = char.Parse(Console.ReadLine());
            }
        }
    }
    class Versenyzo
    {
        public int rajtszam;
        public string nev;
        public string szak;
        public int pontSzam;

        public Versenyzo(int rajtszam, string nev, string szak)
        {
            this.rajtszam = rajtszam;
            this.nev = nev;
            this.szak = szak;
        }

        public void PontotKap(int pont)
        {
            pontSzam += pont;
        }

        public override string ToString()
        {
            return String.Format("{0,5}\t{1,-20}{2,-20}{3,-10}",rajtszam,nev,szak,pontSzam + " pont");
            //return $"{rajtszam} \t\t  {nev} \t\t  {szak} \t\t  {pontSzam} pont";
        }

        public int Rajtszam
        {
            get { return rajtszam; }
        }

        public string Nev
        { 
            get { return nev; } 
        }

        public string Szak
        {
            get { return szak; }
        }

        public int PontSzam
        {
            get { return pontSzam; }
        }

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            new VezerloOsztaly().Start();

            Console.ReadKey();
        }
    }
}
