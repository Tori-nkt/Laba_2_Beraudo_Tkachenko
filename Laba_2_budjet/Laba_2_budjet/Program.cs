using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;

namespace Laba_2_budjet
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            string[] budj = GetListStudents();
            int[,] bud = Rate_bud(budj);
            int perc = Find_Percent(40);
            Console.WriteLine("{0} студентов получают стипендию.", perc);
            double[] sr = Sredniy_bal(bud, budj);
            //Stepuha(sr, perc, budj);
        }
        public static int n, c = 0;


        public static string[] GetListStudents()
        {
            Console.Write("Введіть назву теки: ");
            string path = Console.ReadLine();
            string[] list_bud = new string[30];
            //List<Students> students = new List<Students>();
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                int count = 0;
                int n = Convert.ToInt32(sr.ReadLine());
                while (count != n)
                {
                    //students1.csv
                    line = sr.ReadLine();
                    if (line.EndsWith("FALSE"))
                    {
                        list_bud[c] = line;
                        c++;
                    }
                    count++;
                }
            }
            return list_bud;
        }
        static int[,] Rate_bud(string[] budj)
        {
            int[,] sp_budj = new int[c, 5];
            for (int i = 0; i < c; i++)
            {
                string line = budj[i];
                string[] temp = line.Split(",");
                if (line.EndsWith("FALSE"))
                {
                    Console.WriteLine(line);
                    for (int j = 0; j < 5; j++)
                    {
                        sp_budj[i, j] = Convert.ToInt32(temp[j + 1]);
                    }
                    Console.WriteLine();
                }
            }
            return sp_budj;
        }
        static double[] Sredniy_bal(int[,] budj, string[] bud)
        {
            double sum;

            double[] sredniy = new double[c];
            for (int i = 0; i < c; i++)
            {
                string[] tem = bud[i].Split(",");
                sum = 0;
                for (int j = 0; j < 5; j++)
                {
                    sum += budj[i, j];
                }
                sredniy[i] = sum / 5;
                Console.Write("{0} ", tem[0]);
                Console.WriteLine("{0:f3}", sredniy[i]);
            }
            return sredniy;
        }
        static int Find_Percent(int k)
        {
            int percent = c * k / 100;
            return percent;
        }
    }
}
