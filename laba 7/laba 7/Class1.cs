using System.Xml.Serialization;

namespace laba_7
{
    internal class Class1
    {

        private static bool CheckFileExists(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine($"Файла нет по пути: {path}");
                return false;
            }
            return true;
        }
        private static int ReadIntFromConsole(string prompt)
        {
            int result;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (int.TryParse(input, out result))
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
        public static void GenerateFileTask1(string path, int count, int min = 0, int max = 100)
        {
            Random random = new Random();
            if (CheckFileExists(path))
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    for (int i = 0; i < count; i++)
                    {
                        writer.WriteLine(random.Next(min, max));
                    }
                }
                Console.WriteLine($"Файл '{path}' заполнен {count} случайными числами ({min}-{max})");

                int searchNumber = ReadIntFromConsole("Введите число для поиска: ");
                bool contains = ContainsNumber(path, searchNumber);
                Console.WriteLine(contains ? "Число найдено в файле!" : "Число не найдено в файле.");
            }
            else
            {
                Console.WriteLine($"Файл '{path}' не существует");
            }
        }

        public static bool ContainsNumber(string path, int b)
        {
            foreach (string line in File.ReadLines(path))
            {
                if (int.TryParse(line.Trim(), out int num) && num == b)
                    return true;
            }
            return false;
        }

        // ЗАДАНИЕ 2
        public static void GenerateFileTask2(string path, int linesCount, int numsPerLine, int min = 0, int max = 100)
        {
            Random random = new Random();
            if (CheckFileExists(path))
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    for (int i = 0; i < linesCount; i++)
                    {
                        var numbers = new List<int>();
                        for (int j = 0; j < numsPerLine; j++)
                        {
                            numbers.Add(random.Next(min, max));
                        }
                        writer.WriteLine(string.Join(" ", numbers));
                    }
                }
                Console.WriteLine($"Файл '{path}' заполнен: {linesCount} строк по {numsPerLine} чисел ({min}-{max})");
                Console.WriteLine("Содержимое файла:");
                Console.WriteLine(File.ReadAllText(path));

                int k = ReadIntFromConsole("Введите число k для поиска кратных: ");
                int sum = SumMultiplesOfK(path, k);
                Console.WriteLine($"Сумма чисел, кратных {k}: {sum}");
            }
            else
            {
                Console.WriteLine($"Файл '{path}' не существует");
            }
        }

        public static int SumMultiplesOfK(string path, int k)
        {
            int sum = 0;
            foreach (string line in File.ReadLines(path))
            {
                var parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string part in parts)
                {
                    if (int.TryParse(part, out int num) && num % k == 0)
                    {
                        sum += num;
                    }
                }
            }
            return sum;
        }

        // ЗАДАНИЕ 3
        public static void GenerateFileTask3(string sourcePath, string destPath)
        {
            if (CheckFileExists(sourcePath))
            {
                File.WriteAllText(sourcePath,
                    "Это строка без цифр\n" +
                    "Строка с цифрой 123\n" +
                    "Ещё одна чистая строка\n" +
                    "Цифры 456 и буквы\n" +
                    "И снова только текст");
                Console.WriteLine($"Исходный файл '{sourcePath}'");
                Console.WriteLine("Содержимое исходного файла:");
                Console.WriteLine(File.ReadAllText(sourcePath));

                CopyLinesWithoutDigits(sourcePath, destPath);
                Console.WriteLine($"\nРезультат записан в '{destPath}':");
                Console.WriteLine(File.ReadAllText(destPath));
            }
            else
            {
                Console.WriteLine($"Исходный файл '{sourcePath}' не существует");
            }
        }

        public static void CopyLinesWithoutDigits(string sourcePath, string destPath)
        {
            using (StreamReader reader = new StreamReader(sourcePath))
            using (StreamWriter writer = new StreamWriter(destPath))
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
        public static void GenerateFileTask4(string sourcePath, string destPath, int count, int min = 0, int max = 100)
        {
            Random random = new Random();
            if (CheckFileExists(sourcePath))
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(sourcePath, FileMode.Create)))
                {
                    for (int i = 0; i < count; i++)
                    {
                        writer.Write(random.Next(min, max));
                    }
                }
                Console.WriteLine($"Бинарный файл '{sourcePath}' создан с {count} числами ({min}-{max})");
                RemoveDuplicatesBinary(sourcePath, destPath);
                Console.WriteLine($"Дубликаты удалены, результат в '{destPath}'");

                Console.WriteLine("\nИсходные числа:");
                using (BinaryReader reader = new BinaryReader(File.OpenRead(sourcePath)))
                {
                    while (reader.BaseStream.Position < reader.BaseStream.Length)
                        Console.Write(reader.ReadInt32() + " ");
                }

                Console.WriteLine("\n\nБез дубликатов:");
                using (BinaryReader reader = new BinaryReader(File.OpenRead(destPath)))
                {
                    while (reader.BaseStream.Position < reader.BaseStream.Length)
                        Console.Write(reader.ReadInt32() + " ");
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine($"Бинарный файл '{sourcePath}' не существует");
            }
        }

        public static void RemoveDuplicatesBinary(string sourcePath, string destPath)
        {
            var seen = new HashSet<int>();
            using (BinaryReader reader = new BinaryReader(File.OpenRead(sourcePath)))
            using (BinaryWriter writer = new BinaryWriter(File.Open(destPath, FileMode.Create)))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
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
        public static void GenerateFileTask5(string path, int count)
        {
            Random random = new Random();
            if (CheckFileExists(path))
            {
                var toys = new List<Toy>();
                string[] names = { "Кукла", "Машинка", "Конструктор", "Пазл", "Мяч", "Робот", "Кубики", "Пирамидка" };

                for (int i = 0; i < count; i++)
                {
                    toys.Add(new Toy
                    {
                        Name = names[random.Next(names.Length)] + $" {i + 1}",
                        Price = random.Next(50, 1500),
                        MinAge = random.Next(1, 5),
                        MaxAge = random.Next(5, 12)
                    });
                }

                XmlSerializer serializer = new XmlSerializer(typeof(List<Toy>), new XmlRootAttribute("Toys"));
                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    serializer.Serialize(fs, toys);
                }
                Console.WriteLine($"XML файл '{path}' создан с {count} игрушками");

                int maxPrice = ReadIntFromConsole("Введите максимальную цену (руб): ");
                var suitableToys = GetAffordableToysForAge5(path, maxPrice);

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
            else
            {
                Console.WriteLine($"XML файл '{path}' не существует");
            }
        }

        public static List<string> GetAffordableToysForAge5(string path, int k)
        {
            var result = new List<string>();
            XmlSerializer serializer = new XmlSerializer(typeof(List<Toy>), new XmlRootAttribute("Toys"));

            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                var toys = (List<Toy>)serializer.Deserialize(fs);
                foreach (var toy in toys)
                {
                    if (toy.Price <= k && toy.MinAge <= 5 && toy.MaxAge >= 5)
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
