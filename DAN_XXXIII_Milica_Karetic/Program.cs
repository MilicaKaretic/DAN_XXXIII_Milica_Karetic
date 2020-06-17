using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;


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
            string fileName = @"..\..\FileByThread_1.txt";
            try
            {
                using (StreamReader sw = new StreamReader(fileName))
                {
                    string line;
                    while ((line = sw.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Method that get sum of odd numbers from file
        /// </summary>
        public static void GetSumFromFile()
        {
            string fileName = @"..\..\FileByThread_22.txt";
            Random rnd = new Random();
            try
            {
                int sum = 0;
                using (StreamReader sw = new StreamReader(fileName))
                {
                    string line;
                    while ((line = sw.ReadLine()) != null)
                    {
                        sum = sum + int.Parse(line);
                    }

                }
                Console.WriteLine("Sum of odd numbers from file: " + sum);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
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


            Stopwatch s = new Stopwatch();
            //start stopwatch 
            s.Start();
            threads[0].Start();
            threads[1].Start();
            threads[0].Join();
            threads[1].Join();
            //end stopwatch after two threads finished their job
            s.Stop();

            Console.WriteLine();
            TimeSpan ts = s.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}", ts.Seconds, ts.Milliseconds);
            //write time to console
            Console.WriteLine("RunTime for thread_1 and thread_22 " + elapsedTime);


            threads[2].Start();
            threads[3].Start();

            threads[2].Join();
            threads[3].Join();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
