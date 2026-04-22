using System;

namespace laba_7
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== Демонстрация работы всех заданий ===\n");

            Console.WriteLine("--- ЗАДАНИЕ 1 ---");
            string task1File = "task1.txt";
            Class1.GenerateFileTask1(task1File, 15, 0, 20);

            Console.WriteLine("\n--- ЗАДАНИЕ 2 ---");
            string task2File = "task2.txt";
            Class1.GenerateFileTask2(task2File, 5, 6, 1, 50);

            Console.WriteLine("\n--- ЗАДАНИЕ 3 ---");
            string task3Source = "task3_source.txt";
            string task3Dest = "task3_result.txt";
            Class1.GenerateFileTask3(task3Source, task3Dest);

            Console.WriteLine("\n--- ЗАДАНИЕ 4 ---");
            string task4Source = "task4_source.bin";
            string task4Dest = "task4_no_duplicates.bin";
            Class1.GenerateFileTask4(task4Source, task4Dest, 30, 1, 15);

            Console.WriteLine("\n--- ЗАДАНИЕ 5 ---");
            string task5File = "toys.xml";
            Class1.GenerateFileTask5(task5File, 8);
        }
    }
}
