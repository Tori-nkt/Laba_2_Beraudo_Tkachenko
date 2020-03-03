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
            int[,] bud = GetListStudents();
        }
        public static int n, c = 0;

        public static int[,] GetListStudents()
        {
            Console.Write("Введіть назву теки: ");
            string path = Console.ReadLine();
            int[,] sp_budj = new int[30, 5];
            //ArrayList list_bud = new ArrayList();
            //List<Students> students = new List<Students>();
            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                string line;
                int count = 0;
                int n = Convert.ToInt32(sr.ReadLine());
                while (count != n)
                {
                    //students1.csv
                    line = sr.ReadLine();
                    string[] temp = line.Split(",");
                    if (line.EndsWith("FALSE"))
                    {
                        Console.WriteLine(line);
                        for (int j = 0; j < 5; j++)
                        {
                            sp_budj[c, j] = Convert.ToInt32(temp[j + 1]);
                            //Console.Write("{0, 5}", sp_budj[c, j]);
                        }
                        c++;
                        Console.WriteLine();
                    }
                    count++;
                }
            }
            return sp_budj;
        }
    }
}
