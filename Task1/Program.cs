using System;
using System.Threading;

namespace Task1
{
    class Program
    {
        const int rows = 5;
        const int colums = 5;

        static int[,] garden = new int[rows, colums];
        static object lockObject = new object();

        static void Main(string[] args)
        {
            ThreadStart threadStart = new ThreadStart(Gardener1);
            Thread thread = new Thread(threadStart);
            thread.Start();

            Gardener2();

            thread.Join();

            Console.WriteLine("\nИтоговый сад:");
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < colums; j++)
                {
                    Console.Write($"{garden[i, j]} ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\nДля выхода нажмите любую клавишу:");
            Console.ReadKey();
        }

        static void Gardener1()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < colums; j++)
                {
                    lock (lockObject)
                    {
                        if (garden[i, j] == 0)
                        {
                            garden[i, j] = 1;
                            Console.WriteLine($"Садовник 1: ({i}, {j})");
                        }
                    }
                    Thread.Sleep(100);
                }
            }
        }

        static void Gardener2()
        {
            for (int i = rows - 1; i >= 0; i--)
            {
                for (int j = colums - 1; j >= 0; j--)
                {
                    lock (lockObject)
                    {
                        if (garden[i, j] == 0)
                        {
                            garden[i, j] = 2;
                            Console.WriteLine($"Садовник 2: ({i}, {j})");
                        }
                    }
                    Thread.Sleep(100);
                }
            }
        }
    }
}