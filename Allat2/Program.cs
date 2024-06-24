using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Allat
{
    class Allat
    {
        private string nev;
        private int szuletesiEv;

        private int szepsegPont, viselkedesPont;
        private int pontSzam;

        private static int aktualisEv;
        private static int korHatar;

        public Allat(string nev, int szuletesiEv)
        {
            this.nev = nev;
            this.szuletesiEv = szuletesiEv;
        }

        public int Kor()
        {
            return aktualisEv - szuletesiEv;
        }

        public void Pontozzak(int szepsegPont, int viselkedesPont)
        {
            this.szepsegPont = szepsegPont;
            this.viselkedesPont = viselkedesPont;
            if (Kor() <= korHatar)
            {
                pontSzam = viselkedesPont * Kor() + szepsegPont * (korHatar - Kor());
            }
            else
            {
                pontSzam = 0;
            }
        }

        public override string ToString()
        {
            return nev + " pontszáma:" + pontSzam + " pont";
        }

        public string Nev
        {
            get { return nev; }
        }
        public int SzuletesiEv
        {
            get { return szuletesiEv; }
        }
        public int SzepsegPont
        {
            get { return szepsegPont; }
        }
        public int ViselkedesPont
        {
            get { return viselkedesPont; }
        }
        public int PontSzam
        {
            get { return pontSzam; }
        }


        public static int AktualisEv
        {
            get { return aktualisEv; }
            set { aktualisEv = value; }
        }
        public static int KorHatar
        {
            get { return korHatar; }
            set { korHatar = value; }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {

            int aktEv = 2015, korhatar = 10;

            Allat.AktualisEv = aktEv;
            Allat.KorHatar = korhatar;

            AllatVerseny();

            Console.ReadKey();
        }

        private static void AllatVerseny()
        {
            Allat allat;

            string nev;
            int szulEv;
            int szepseg, viselkedes;
            int veletlenPontHatar = 10;

            Random rand = new Random();

            int osszesVersenyzo = 0;
            int osszesPont = 0;
            int legtobbPont = 0;

            Console.WriteLine("Kezdődik a verseny");

            char tovabb = 'i';
            while (tovabb == 'i')
            {
                Console.WriteLine("Az állat neve: ");
                nev = Console.ReadLine();

                Console.WriteLine("Születése éve: ");
                while (!int.TryParse(Console.ReadLine(), out szulEv)
                            || szulEv <= 0
                            || szulEv > Allat.AktualisEv)
                {
                    Console.WriteLine("Hibás adat, kérem újra.");
                }

                szepseg = rand.Next(veletlenPontHatar + 1);
                viselkedes = rand.Next(veletlenPontHatar + 1);

                allat = new Allat(nev, szulEv);

                allat.Pontozzak(szepseg, viselkedes);

                Console.WriteLine(allat);

                osszesVersenyzo++;
                osszesPont += allat.PontSzam;
                if (legtobbPont < allat.PontSzam)
                {
                    legtobbPont = allat.PontSzam;
                }

                Console.WriteLine("Van még állat? (i/n) ");

                tovabb = char.Parse(Console.ReadLine());
            }

            Console.WriteLine($"\nÖsszesen {osszesVersenyzo} versenyző volt, " +
                              $"\nÖsszpontszámuk: {osszesPont} pont," +
                              $"\nLegnagyobb pontszám: {legtobbPont}");


        }
    }
}
