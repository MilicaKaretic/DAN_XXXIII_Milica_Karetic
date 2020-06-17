using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAN_XXXIII_Milica_Karetic
{
    class Program
    {
        /// <summary>
        /// Method for threads
        /// </summary>
        public static void Method()
        {
            string currentName = Thread.CurrentThread.Name;

            if (currentName == "Thread_1")
            {
                WriteMatrixToFile();
            }
            else if (currentName == "Thread_22")
            {
                WriteRandomNumbersToFile();
            }
            else if (currentName == "Thread_3")
            {
                GetMatrixFromFile();
            }
            else if (currentName == "Thread_44")
            {
                GetSumFromFile();
            }
        }

        /// <summary>
        /// Method that write matrix 100x100 to file
        /// </summary>
        public static void WriteMatrixToFile()
        {
            string fileName = @"..\..\FileByThread_1.txt";
            try
            {
                using (StreamWriter sw = new StreamWriter(fileName))
                {
                    for (int i = 0; i < 100; i++)
                    {
                        for (int j = 0; j < 100; j++)
                        {
                            if (i == j)
                                sw.Write("1");
                            else
                                sw.Write("0");
                        }
                        sw.WriteLine();
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// Method that write random odd numbers 0-10000 to file
        /// </summary>
        public static void WriteRandomNumbersToFile()
        {
            string fileName = @"..\..\FileByThread_22.txt";
            Random rnd = new Random();
            try
            {
                using (StreamWriter sw = new StreamWriter(fileName))
                {
                    int i = 0;
                    while (i < 1000)
                    {
                        int num = rnd.Next(0, 10000);
                        if (num % 2 != 0)
                        {
                            sw.WriteLine(num);
                            i++;
                        }
                        else
                            continue;
                    }

                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Method that write matrix from file to console
        /// </summary>
        public static void GetMatrixFromFile()
        {
        }

        /// <summary>
        /// Method that get sum of odd numbers from file
        /// </summary>
        public static void GetSumFromFile()
        {
        }

        static void Main(string[] args)
        {
            List<Thread> threads = new List<Thread>();

            for (int i = 1; i < 5; i++)
            {
                if (i % 2 != 0)
                {
                    Thread t = new Thread(new ThreadStart(Method))
                    {
                        Name = string.Format("Thread_{0}", i)
                    };
                    Console.WriteLine(t.Name + " created.");
                    threads.Add(t);
                }
                else
                {
                    Thread t = new Thread(new ThreadStart(Method))
                    {
                        Name = string.Format("Thread_{0}{1}", i, i)
                    };
                    Console.WriteLine(t.Name + " created.");
                    threads.Add(t);
                }
            }


            threads[0].Start();
            threads[1].Start();
            threads[0].Join();
            threads[1].Join();


            threads[2].Start();
            threads[3].Start();

            threads[2].Join();
            threads[3].Join();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
