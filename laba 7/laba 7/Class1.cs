using System.Xml.Serialization;

namespace laba_7
{
    public static class Tasks
    {
        public static bool CheckFileExists(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine($"Файла нет по пути: {path}");
                return false;
            }
            return true;
        }

        public static int ReadIntFromConsole(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (int.TryParse(input, out int result))
                {
                    return result;
                }
                else
                {
                    Console.WriteLine("Ошибка: введите целое число!");
                }
            }
        }

        // ЗАДАНИЕ 1
        public static void ProcessTask1Logic(string path)
        {
            if (Tasks.CheckFileExists(path))
            {
                int searchNumber = ReadIntFromConsole("Введите" +
                    " число для поиска: ");
                bool contains = ContainsNumber(path, searchNumber);
                Console.WriteLine(contains ? "Число найдено в файле!"
                    : "Число не найдено в файле.");
            }
        }

        public static void GenerateFileTask1(string path, int count)
        {
            if (Tasks.CheckFileExists(path))
            {
                Random random = new Random();
                using (StreamWriter writer 
                    = new StreamWriter(path))
                {
                    for (int i = 0; i < count; i++)
                    {
                        writer.WriteLine(random.Next(0, 50));
                    }
                }
                Console.WriteLine($"Файл '{path}'" +
                    $" заполнен {count} случайными числами (0-50)");
            }
        }

        public static bool ContainsNumber(string path, int b)
        {
            foreach (string line in File.ReadLines(path))
            {
                if (int.TryParse(line, out int num) && num == b)
                    return true;
            }
            return false;
        }

        // ЗАДАНИЕ 2
        public static void ProcessTask2Logic(string path)
        {
            if (CheckFileExists(path))
            {
                Console.WriteLine("Содержимое файла:");
                Console.WriteLine(File.ReadAllText(path));

                int k = ReadIntFromConsole("Введите число" +
                    " k для поиска кратных: ");
                int sum = SumMultiplesOfK(path, k);
                Console.WriteLine($"Сумма чисел," +
                    $" кратных {k}: {sum}");
            }
        }

        public static void GenerateFileTask2(string path,
            int linesCount, int numsPerLine)
        {
            if (Tasks.CheckFileExists(path))
            {
                Random random = new Random();
                using (StreamWriter writer =
                    new StreamWriter(path))
                {
                    for (int i = 0; i < linesCount; i++)
                    {
                        var numbers = new List<int>();
                        for (int j = 0; j < numsPerLine; j++)
                        {
                            numbers.Add(random.Next(0, 50));
                        }
                        writer.WriteLine(string.Join(" ", numbers));
                    }
                }
                Console.WriteLine($"Файл '{path}' заполнен:" +
                    $" {linesCount} строк по {numsPerLine}" +
                    $" чисел (0-50)");
            }
        }

        public static int SumMultiplesOfK(string path, int k)
        {
            int sum = 0;
            foreach (string line in File.ReadLines(path))
            {
                var parts = line.Split(new[] { ' ' },
                    StringSplitOptions.RemoveEmptyEntries);
                foreach (string part in parts)
                {
                    if (int.TryParse(part,
                        out int num) && num % k == 0)
                    {
                        sum += num;
                    }
                }
            }
            return sum;
        }

        // ЗАДАНИЕ 3
        public static void ProcessTask3Logic(string sourcePath,
                                             string destPath)
        {
            if (CheckFileExists(sourcePath) &&
                CheckFileExists(destPath))
            {
                Console.WriteLine("Содержимое исходного файла:");
                Console.WriteLine(File.ReadAllText(sourcePath));

                CopyLinesWithoutDigits(sourcePath, destPath);
                Console.WriteLine($"\nРезультат записан в '{destPath}':");
                Console.WriteLine(File.ReadAllText(destPath));
            }
        }

        public static void GenerateFileTask3(string sourcePath)
        {
            if (CheckFileExists(sourcePath))
            {
                File.WriteAllText(sourcePath,
                    "Это строка без цифр\n" +
                    "Строка с цифрой 123\n" +
                    "Ещё одна чистая строка\n" +
                    "Цифры 456 и буквы\n" +
                    "И снова только текст");
                Console.WriteLine($"Исходный файл" +
                    $" '{sourcePath}' создан");
            }
        }

        public static void CopyLinesWithoutDigits(string sourcePath,
                                                  string destPath)
        {
            using (StreamReader reader =
                new StreamReader(sourcePath))
            using (StreamWriter writer =
                new StreamWriter(destPath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (!line.Any(char.IsDigit))
                    {
                        writer.WriteLine(line);
                    }
                }
            }
        }

        // ЗАДАНИЕ 4
        public static void ProcessTask4Logic(string sourcePath, string destPath)
        {
            if (CheckFileExists(sourcePath))
            {
                RemoveDuplicatesBinary(sourcePath, destPath);
                Console.WriteLine($"Дубликаты удалены," +
                    $" результат в '{destPath}'");

                Console.WriteLine("\nИсходные числа:");
                using (BinaryReader reader =
                    new BinaryReader(File.OpenRead(sourcePath)))
                {
                    while (reader.BaseStream.Position <
                           reader.BaseStream.Length)
                        Console.Write(reader.ReadInt32() + " ");
                }

                Console.WriteLine("\n\nБез дубликатов:");
                using (BinaryReader reader =
                    new BinaryReader(File.OpenRead(destPath)))
                {
                    while (reader.BaseStream.Position <
                           reader.BaseStream.Length)
                        Console.Write(reader.ReadInt32() + " ");
                }
                Console.WriteLine();
            }
        }

        public static void GenerateFileTask4(string sourcePath, int count)
        {
            if (CheckFileExists(sourcePath))
            {
                Random random = new Random();
                using (BinaryWriter writer =
                    new BinaryWriter(File.Open(sourcePath,
                                               FileMode.Create)))
                {
                    for (int i = 0; i < count; i++)
                    {
                        writer.Write(random.Next(1, 50));
                    }
                }
                Console.WriteLine($"Бинарный файл " +
                    $"'{sourcePath}' создан с {count} " +
                    $"числами (1-50)");
            }
            
        }

        public static void RemoveDuplicatesBinary(string sourcePath, string destPath)
        {
            var seen = new HashSet<int>();
            using (BinaryReader reader =
                new BinaryReader(File.OpenRead(sourcePath)))
            using (BinaryWriter writer =
                new BinaryWriter(File.Open(destPath,
                                 FileMode.Create)))
            {
                while (reader.BaseStream.Position <
                       reader.BaseStream.Length)
                {
                    int num = reader.ReadInt32();
                    if (!seen.Contains(num))
                    {
                        seen.Add(num);
                        writer.Write(num);
                    }
                }
            }
        }

        // ЗАДАНИЕ 5
        public static void ProcessTask5Logic(string path)
        {
            if (CheckFileExists(path))
            {
                int maxPrice =
                    ReadIntFromConsole("Введите максимальную цену (руб): ");
                var suitableToys =
                    GetAffordableToysForAge5(path, maxPrice);

                if (suitableToys.Count > 0)
                {
                    Console.WriteLine($"\nНайдено игрушек" +
                                      $" для 5 лет с ценой" +
                                      $" до {maxPrice} руб:");
                    foreach (string toyName in suitableToys)
                        Console.WriteLine($"  • {toyName}");
                }
                else
                {
                    Console.WriteLine($"Подходящих игрушек не найдено.");
                }
            }
        }

        public static void GenerateFileTask5(string path, int count)
        {
            if (CheckFileExists(path))
            {
                Random random = new Random();
                var toys = new List<Toy>();
                string[] names = { "Кукла", "Машинка",
                                   "Конструктор", "Пазл",
                                   "Мяч", "Робот",
                                   "Кубики", "Пирамидка" };

                for (int i = 0; i < count; i++)
                {
                    toys.Add(new Toy
                    {
                        Name = names[random.Next(names.Length)]
                        + $" {i + 1}",
                        Price = random.Next(50, 1500),
                        MinAge = random.Next(1, 5),
                        MaxAge = random.Next(5, 12)
                    });
                }

                XmlSerializer serializer =
                    new XmlSerializer(typeof(List<Toy>), 
                    new XmlRootAttribute("Toys"));
                using (FileStream fs =
                    new FileStream(path, FileMode.Create))
                {
                    serializer.Serialize(fs, toys);
                }
                Console.WriteLine($"XML файл '{path}'" +
                    $" создан с {count} игрушками");
            }
        }

        public static List<string> GetAffordableToysForAge5(string path, int k)
        {
            var result = new List<string>();
            XmlSerializer serializer =
                new XmlSerializer(typeof(List<Toy>),
                new XmlRootAttribute("Toys"));

            using (FileStream fs =
                new FileStream(path, FileMode.Open))
            {
                var toys = (List<Toy>)serializer.Deserialize(fs);
                foreach (var toy in toys)
                {
                    if (toy.Price <= k &&
                        toy.MinAge <= 5 &&
                        toy.MaxAge >= 5)
                    {
                        result.Add(toy.Name);
                    }
                }
            }
            return result;
        }
    }

    public class Toy
    {
        private string name;
        private int price;
        private int minAge;
        private int maxAge;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        public int Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
            }
        }
        public int MinAge
        {
            get
            {
                return minAge;
            }
            set
            {
                minAge = value;
            }
        }
        public int MaxAge
        {
            get
            {
                return maxAge;
            }
            set
            {
                maxAge = value;
            }
        }
    }
}
