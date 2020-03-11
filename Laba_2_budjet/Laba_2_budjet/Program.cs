using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace Laba_2_budjet
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            string[] budj = GetListStudents();
            int[,] bud = Rate_bud(budj);
            double[] sr = Sredniy_bal(bud, budj);
            
            double[] stepend = Sort_sp(sr);

            Write_file(stepend, sr, budj);


        }
        public static int n, c = 0;


        public static string[] GetListStudents()
        {
            
            Console.Write("Введіть назву теки: ");    // давайте употреблять малоизвестные слова
            string storagePath = Console.ReadLine();
            string[] list_bud = new string[60];       // простите, бомбит немножко
            
            var fileStorage = new List<string[]>();
            try
            {
                var filePaths = Directory.GetFiles(storagePath, "*.csv"); //string[]
                foreach (string path in filePaths)
                {
                    string[] fileLines = System.IO.File.ReadAllLines(path);
                    fileStorage.Add(fileLines);
                }
                foreach (string[] fileLines in fileStorage)
                {
                    foreach (string fileLine in fileLines)
                    {
                        if (fileLine.EndsWith("TRUE"))
                        {
                            list_bud[c] = fileLine;
                            c++;
                        }
                    }
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return list_bud;//создаём список бюджетников            
        }
        static int[,] Rate_bud(string[] budj)       //создаём список оценок студентов       
        {
            int[,] sp_budj = new int[c, 5];
            for (int i = 0; i < c; i++)
            {
                string line = budj[i];
                string[] temp = line.Split(",");
                if (line.EndsWith("TRUE"))    // на счёт false не уверенна, но суть не меняется
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
        static double[] Sredniy_bal(int[,] budj, string[] bud)    //узнаём средний балл каждого бюджетника
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
                Console.Write("{0} ", tem[0]);      // для наглядности го попринтим)
                Console.WriteLine("{0:f3}", sredniy[i]);  // {0:f3} форматированый вывод double (3 знака после запятой)
            }
            return sredniy;
        }
        static int Find_Percent(int k)
        {
            int percent = c * k / 100;   // с - количество бюджетников, k - процент которій мы хотим узнать
            return percent;
        }
        static double[] Sort_sp(double[] sp)    //находим тех, кто получает стипендию
        {
            double temp;
            for (int i = 0; i < c; i++)         // сортируем список сред. баллов, по спаданию
            {
                for (int j = i + 1; j < 5; j++)
                {
                    if (sp[i] > sp[j])
                    {
                        temp = sp[i];
                        sp[i] = sp[j];
                        sp[j] = temp;
                    }
                }
            }                  
            int per = Find_Percent(40);            // находим КОЛ-СТВО умников, что получают стипендию 
            double[] stependia = new double[per];  // с помощью отсортированного списка узнаём студентов 
            for (int i = 0; i < per; i++)          // которые получают стипендию
            {
                stependia[i] = sp[i];
            }
            
            return stependia;
        }
        static void Write_file(double[] step, double[] sred_b, string[] bud)
 
        {
            Console.Write("Введіть назву теки для запису: ");
            string path = Console.ReadLine();
            try
            {
                               // создание файла
                using (FileStream fs = File.Create(path))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes("Список бюджетников в формате (имя) (средний бал) (на стипендии)");
                              // Add some information
                    fs.Write(info, 0, info.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            try
            {
                using (StreamWriter sw = new StreamWriter(path))
                {
                    int per = Find_Percent(40);
                    for (int i = 0; i < c; i++)
                    {

                        string[] temp = (bud[i]).Split(",");
                        sw.Write(temp[0]);
                        sw.Write("  ср. балл: {0}  ", sred_b[i]);

                        for (int j = 0; j < per; j++)
                        {
                            if (sred_b[i] == step[j])
                            {
                                sw.Write(" На стипендии");

                                break;
                            }
                        }
                        sw.WriteLine();
                    }
                }
                using (StreamReader sr = new StreamReader(path))
                {
                    string line;
                    int count = 0;
                    while (count != c + 1)
                    {
                        line = sr.ReadLine();
                        Console.WriteLine(line);
                        count++;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
