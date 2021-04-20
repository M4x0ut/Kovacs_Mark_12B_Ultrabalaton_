using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kovacs_Mark_12B_Ultrabalaton
{
    class Program
    {
        struct maraton
        {
            public string nev;
            public int rajtszam;
            public string nem;
            public int ora;
            public int perc;
            public int sec;
            public int szazalek;
        }
        static maraton[] futok = new maraton[500];
        static double idoorabanfuggveny(int eredmenyora, int eredmenyperc, int eredmenysec)
        {
            double eredmenyoraban = eredmenyora + eredmenyperc / 60 + eredmenysec / 3600;
            return eredmenyoraban;
        }
        static void Main(string[] args)
        {
            string[] fajlbol = File.ReadAllLines("ub2017egyeni.txt");
            int sorokszama = 0;
            int i;
            for (int k = 1; k < fajlbol.Count(); k++)
            {
                string[] egysordarabolva = fajlbol[k].Split(';');
                futok[sorokszama].nev = egysordarabolva[0];
                futok[sorokszama].rajtszam = Convert.ToInt32(egysordarabolva[1]);
                futok[sorokszama].nem = egysordarabolva[2];
                string[] egysordarabolva2 = egysordarabolva[3].Split(':');
                futok[sorokszama].ora = Convert.ToInt32(egysordarabolva2[0]);
                futok[sorokszama].perc = Convert.ToInt32(egysordarabolva2[1]);
                futok[sorokszama].sec = Convert.ToInt32(egysordarabolva2[2]);
                futok[sorokszama].szazalek = Convert.ToInt32(egysordarabolva[4]);
                sorokszama++;
            }

            int futokszama = sorokszama;
            Console.WriteLine("3. feladat: Egyéni indulók: "+ futokszama + " fő");
            int noisportolokszama = 0;
            for (i = 0; i < futokszama; i++)
            {
                if (futok[i].nem == "Noi" && futok[i].szazalek == 100)
                {
                    noisportolokszama++;
                }
            }
            Console.WriteLine("4. feladat: Célba érkező női sportolók: "+ noisportolokszama + " fő" );




            Console.Write("5. feladat: Kérem a sportoló nevét: ");
            string sportoloneve = Console.ReadLine();

            i = 0;
            while (i < futokszama && sportoloneve != futok[i].nev)
            {
                i++;

            }


            if (i < futokszama)
            {
                Console.WriteLine("\tIndult egyéniben  a sportoló? Igen");
                if (futok[i].szazalek == 100)
                    Console.WriteLine("\tTeljesítette a teljes távot? Igen");
                else Console.WriteLine("\tTeljesítette a teljes távot? Nem");
            }

            else
                Console.WriteLine("\tIndult egyéniben  a sportoló? Nem");
            double idooraban = 0;
            int ferfifutokszama = 0;
            foreach (var vizsgalt in futok)
            {
                if (vizsgalt.nem == "Ferfi" && vizsgalt.szazalek == 100)
                {
                    idooraban += idoorabanfuggveny(vizsgalt.ora, vizsgalt.perc, vizsgalt.sec);
                    ferfifutokszama++;


                }
            }

            Console.WriteLine("7. feladat: Átlagos idő: " + idooraban / ferfifutokszama + " óra");
            string gyoztesferfi = futok[0].nev;
            string gyoztesno = futok[0].nev;
            int gyoztesferfisorszam = 0;
            int gyoztesnoisorszam = 0;
            double gyoztesidoferfi = 100;
            double gyoztesidonoi = 100;
            for (i = 0; i < futokszama; i++)
            {
                if (futok[i].nem == "Ferfi" && futok[i].szazalek == 100)
                {
                    if ((idoorabanfuggveny(futok[i].ora, futok[i].perc, futok[i].sec)) < gyoztesidoferfi)
                    {
                        gyoztesidoferfi = idoorabanfuggveny(futok[i].ora, futok[i].perc, futok[i].sec);
                        gyoztesferfisorszam = i;
                        gyoztesferfi = futok[i].nev;
                    }
                }
                if (futok[i].nem == "Noi" && futok[i].szazalek == 100)
                {
                    if ((idoorabanfuggveny(futok[i].ora, futok[i].perc, futok[i].sec)) < gyoztesidonoi)
                    {
                        gyoztesidonoi = idoorabanfuggveny(futok[i].ora, futok[i].perc, futok[i].sec);
                        gyoztesnoisorszam = i;
                        gyoztesno = futok[i].nev;
                    }
                }
            }
            Console.WriteLine("8. feladat: Verseny győztesei\n\tNők " + gyoztesno + " " + futok[gyoztesnoisorszam].rajtszam + " " + futok[gyoztesnoisorszam].ora + " " + futok[gyoztesnoisorszam].perc + " " + futok[gyoztesnoisorszam].sec);
            Console.WriteLine("\tFérfiak "+ gyoztesferfi + " " + futok[gyoztesferfisorszam].rajtszam + " " + futok[gyoztesferfisorszam].ora + " " + futok[gyoztesferfisorszam].perc + " " + futok[gyoztesferfisorszam].sec);

            Console.ReadKey();
        }
    }
}