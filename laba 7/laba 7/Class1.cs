using System.Xml.Serialization;
namespace laba_7
{
    internal class Class1
    {
        private static readonly Random random = new Random();

        public static void GenerateFileTask1(string path, int count, int min = 0, int max = 100)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                for (int i = 0; i < count; i++)
                {
                    writer.WriteLine(random.Next(min, max));
                }
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

        public static void GenerateFileTask2(string path, int linesCount, int numsPerLine, int min = 0, int max = 100)
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
        }

        public static int SumMultiplesOfK(string path, int k)
        {
            int sum = 0;
            foreach (string line in File.ReadLines(path))
            {
                var parts = line.Split(new[] { ' ' });
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
        public static void GenerateBinaryFileTask4(string path, int count, int min = 0, int max = 100)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
            {
                for (int i = 0; i < count; i++)
                {
                    writer.Write(random.Next(min, max));
                }
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
        public static void GenerateXmlFileTask5(string path, int count)
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

