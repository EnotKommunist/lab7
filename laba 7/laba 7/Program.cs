using System;
using System.IO;

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
            Console.WriteLine($"Файл '{task1File}' создан с 15 случайными числами (0-20)");

            Console.Write("Введите число для поиска: ");
            if (int.TryParse(Console.ReadLine(), out int searchNumber))
            {
                bool contains = Class1.ContainsNumber(task1File, searchNumber);
                Console.WriteLine(contains ? "Число найдено в файле!" : "Число не найдено в файле.");
            }

            Console.WriteLine("\n--- ЗАДАНИЕ 2 ---");
            string task2File = "task2.txt";
            Class1.GenerateFileTask2(task2File, 5, 6, 1, 50);
            Console.WriteLine($"Файл '{task2File}' создан: 5 строк по 6 чисел (1-50)");
            Console.WriteLine("Содержимое файла:");
            Console.WriteLine(File.ReadAllText(task2File));

            Console.Write("Введите число k для поиска кратных: ");
            if (int.TryParse(Console.ReadLine(), out int k))
            {
                int sum = Class1.SumMultiplesOfK(task2File, k);
                Console.WriteLine($"Сумма чисел, кратных {k}: {sum}");
            }

            Console.WriteLine("\n--- ЗАДАНИЕ 3 ---");
            string task3Source = "task3_source.txt";
            string task3Dest = "task3_result.txt";

            // Создаём тестовый текстовый файл
            File.WriteAllText(task3Source,
                "Это строка без цифр\n" +
                "Строка с цифрой 123\n" +
                "Ещё одна чистая строка\n" +
                "Цифры 456 и буквы\n" +
                "И снова только текст");

            Console.WriteLine($"Исходный файл '{task3Source}' создан:");
            Console.WriteLine(File.ReadAllText(task3Source));

            Class1.CopyLinesWithoutDigits(task3Source, task3Dest);
            Console.WriteLine($"\nРезультат записан в '{task3Dest}':");
            Console.WriteLine(File.ReadAllText(task3Dest));

            // ========== Задание 4 ==========
            Console.WriteLine("\n--- ЗАДАНИЕ 4 ---");
            string task4Source = "task4_source.bin";
            string task4Dest = "task4_no_duplicates.bin";

            Class1.GenerateBinaryFileTask4(task4Source, 30, 1, 15);
            Console.WriteLine($"Бинарный файл '{task4Source}' создан с 30 числами (1-15)");

            Class1.RemoveDuplicatesBinary(task4Source, task4Dest);
            Console.WriteLine($"Дубликаты удалены, результат в '{task4Dest}'");

            // Показываем исходные данные и результат
            Console.WriteLine("\nИсходные числа:");
            using (BinaryReader reader = new BinaryReader(File.OpenRead(task4Source)))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                    Console.Write(reader.ReadInt32() + " ");
            }

            Console.WriteLine("\n\nБез дубликатов:");
            using (BinaryReader reader = new BinaryReader(File.OpenRead(task4Dest)))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                    Console.Write(reader.ReadInt32() + " ");
            }
            Console.WriteLine();

            Console.WriteLine("\n--- ЗАДАНИЕ 5 ---");
            string task5File = "toys.xml";

            Class1.GenerateXmlFileTask5(task5File, 8);
            Console.WriteLine($"XML файл '{task5File}' создан с 8 игрушками");

            Console.Write("Введите максимальную цену (руб): ");
            if (int.TryParse(Console.ReadLine(), out int maxPrice))
            {
                var suitableToys = Class1.GetAffordableToysForAge5(task5File, maxPrice);

                if (suitableToys.Count > 0)
                {
                    Console.WriteLine($"\nНайдено игрушек для 5 лет с ценой до {maxPrice} руб:");
                    foreach (string toyName in suitableToys)
                        Console.WriteLine($"  • {toyName}");
                }
                else
                {
                    Console.WriteLine($"Подходящих игрушек не найдено.");
                }
            }
        }
    }
}