using System;
using System.IO;
using System.Text;

namespace NeedlemanWunsch
{
    class Program
    {
        static int match = 1, mismatch = -1 , gap = -2;
        static int[,] matris;
        static String nükleotit1, nükleotit2;
        static String dizi1 = "", dizi2 = "";

        static void Main(string[] args)
        {
            try
            {
                using (var okuma1 = new StreamReader("D:\\vs project new\\NeedlemanWunsch\\NeedlemanWunsch\\dizilim1.txt"))
                {
                    nükleotit1 = okuma1.ReadToEnd();
                }

                using (var okuma2 = new StreamReader("D:\\vs project new\\NeedlemanWunsch\\NeedlemanWunsch\\dizilim2.txt"))
                {
                    nükleotit2 = okuma2.ReadToEnd();
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Dosya Okuma Hatalı Yapıldı:", e.Message);
            }

            matris2D();
            matrisGoster();
            dizilimHizalama();
            dizilimYazdir();
        }
        public static int[,] matris2D()
        {
            char[] nükleotit_1 = nükleotit1.ToCharArray();
            char[] nükleotit_2 = nükleotit2.ToCharArray();

            matris = new int[nükleotit1.Length + 1 , nükleotit2.Length + 1];

            for (int i = 1; i < nükleotit1.Length + 1; i++)
            {
                matris[i , 0] = matris[i - 1 , 0] + gap;
            }

            for (int j = 1; j < nükleotit2.Length + 1; j++)
            {
                matris[0 , j] = matris[0 , j - 1] + gap;
            }

            for (int i = 1; i < nükleotit1.Length + 1; i++)
            {
                for (int j = 1; j < nükleotit2.Length + 1; j++)
                {
                    if (nükleotit_1[i - 1] != ' ' || nükleotit_2[j - 1] != ' ')
                    {
                        if (nükleotit_1[i - 1] == nükleotit_2[j - 1])
                        {
                            matris[i , j] = maks(matris[i - 1 , j] + gap, matris[i - 1 , j - 1] + match, matris[i , j - 1] + gap);
                        }
                        else
                        {
                            matris[i , j] = maks(matris[i - 1 , j] + gap, matris[i - 1 , j - 1] + mismatch, matris[i , j - 1] + gap);
                        }
                    }
                    else
                    {
                        matris[i , j] = maks(matris[i - 1 , j] + gap, matris[i - 1 , j - 1] + gap, matris[i , j - 1] + gap);
                    }
                }
            }
            return matris;
        }
        public static void dizilimHizalama()
        {
            int nu1 = nükleotit1.Length + 1;
            int nu2 = nükleotit2.Length + 1;

            int n1 = nu1 - 1;
            int n2 = nu2 - 1;
            
            char[] nükleotit_1 = nükleotit1.ToCharArray();
            char[] nükleotit_2 = nükleotit2.ToCharArray();

            while (n1 > 0 && n2 > 0)
            {
                int skor = 0;

                if (nükleotit_1[n1 - 1] == nükleotit_2[n2 - 1])
                {
                    skor = 1;
                }
                else
                {
                    skor = -1;
                }

                if (n1 > 0 && n2 > 0 && matris[n1, n2] == matris[n1 - 1, n2 - 1] + skor)
                {
                    dizi1 = nükleotit_2[n2 - 1] + dizi1;
                    dizi2 = nükleotit_1[n1 - 1] + dizi2;
                    n1--;
                    n2--;
                }
                else if (n2 > 0 && matris[n1, n2] == matris[n1, n2 - 1] - 2)
                {
                    dizi1 = nükleotit_2[n2 - 1] + dizi1;
                    dizi2 = "-" + dizi2;
                    n2--;
                }
                else
                {
                    dizi1 = "-" + dizi1;
                    dizi2 = nükleotit_1[n1 - 1] + dizi2;
                    n1--;
                } 
            }
        }
        public static int weight(int w1, int w2)
        {
            char[] nükleotit_1 = nükleotit1.ToCharArray();
            char[] nükleotit_2 = nükleotit2.ToCharArray();

            if (nükleotit_1[w1 - 1] == nükleotit_2[w2 - 1])
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
        public static void dizilimYazdir()
        {
            Console.WriteLine("En iyi Dizilim \n1. Dizilim: " + dizi1 + "\n" + "2. Dizilim: " + dizi2);
        }
        public static int maks(int m1, int m2, int m3)
        {
            return Math.Max(m3, Math.Max(m1, m2));
        }

        public static void matrisGoster()
        {
            String gecici = "";
            for (int i = 0; i < nükleotit1.Length + 1; i++)
            {
                for (int j = 0; j < nükleotit2.Length + 1; j++)
                {
                    gecici += matris[i , j] + " | ";
                }
                gecici += "\n";
            }
            Console.WriteLine(gecici);
        }
    }
}
