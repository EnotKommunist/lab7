using System.Text;

namespace laba_7_2
{
    internal class CollectionTasks
    {
        //ЗАДАНИЕ 6 (List)
        public static List<T> RemoveAllElements<T>(List<T> list, T value)
        {
            List<T> result = new List<T>();

            foreach (T item in list)
            {
                if (!item.Equals(value))
                {
                    result.Add(item);
                }
            }

            return result;
        }

        //ЗАДАНИЕ 7 (LinkedList)
        public static void ReverseBetweenFirstAndLast<T>(LinkedList<T> list, T element)
        {
            if (list.Count < 2)
                return;

            LinkedListNode<T> firstNode = list.First;
            while (firstNode != null && !firstNode.Value.Equals(element))
            {
                firstNode = firstNode.Next;
            }

            LinkedListNode<T> lastNode = list.Last;
            while (lastNode != null && !lastNode.Value.Equals(element))
            {
                lastNode = lastNode.Previous;
            }

            if (firstNode == null || lastNode == null || firstNode == lastNode)
                return;

            List<T> middleElements = new List<T>();
            LinkedListNode<T> current = firstNode.Next;

            while (current != lastNode)
            {
                middleElements.Add(current.Value);
                current = current.Next;
            }

            for (int i = 0; i < middleElements.Count / 2; i++)
            {
                T temp = middleElements[i];
                middleElements[i] = middleElements[middleElements.Count - 1 - i];
                middleElements[middleElements.Count - 1 - i] = temp;
            }

            current = firstNode.Next;
            for (int i = 0; i < middleElements.Count; i++)
            {
                current.Value = middleElements[i];
                current = current.Next;
            }
        }

        //ЗАДАНИЕ 8 (HashSet)
        public static void AnalyzeMovieViews(
            List<HashSet<string>> viewersMovies,
            List<string> allMovies,
            out HashSet<string> watchedByAll,
            out HashSet<string> watchedBySome,
            out HashSet<string> watchedByNone)
        {
            int n = viewersMovies.Count;

            watchedByAll = new HashSet<string>();
            watchedBySome = new HashSet<string>();
            watchedByNone = new HashSet<string>();

            foreach (string movie in allMovies)
            {
                int count = 0;
                foreach (HashSet<string> viewer in viewersMovies)
                {
                    if (viewer.Contains(movie))
                    {
                        count++;
                    }
                }

                if (count == n)
                {
                    watchedByAll.Add(movie);
                }
                else if (count > 0)
                {
                    watchedBySome.Add(movie);
                }
                else
                {
                    watchedByNone.Add(movie);
                }
            }
        }

        //ЗАДАНИЕ 9 (HashSet с текстом)
        public static List<char> FindFrequentVoicedConsonants(string filePath)
        {
            HashSet<char> voicedConsonants = new HashSet<char>
            {
                'б', 'в', 'г', 'д', 'ж', 'з',
                'Б', 'В', 'Г', 'Д', 'Ж', 'З'
            };

            Dictionary<char, int> letterCount = new Dictionary<char, int>();
            foreach (char c in voicedConsonants)
            {
                letterCount[c] = 0;
                if (char.IsUpper(c))
                {
                    letterCount[char.ToLower(c)] = 0;
                }
            }

            string text = File.ReadAllText(filePath, Encoding.UTF8);
            string[] words = SplitIntoWords(text);

            foreach (string word in words)
            {
                if (string.IsNullOrEmpty(word))
                    continue;

                HashSet<char> lettersInWord = new HashSet<char>();
                foreach (char c in word)
                {
                    char lowerC = char.ToLower(c);
                    if (voicedConsonants.Contains(c) || voicedConsonants.Contains(char.ToUpper(c)))
                    {
                        lettersInWord.Add(lowerC);
                    }
                }

                foreach (char letter in lettersInWord)
                {
                    if (letterCount.ContainsKey(letter))
                    {
                        letterCount[letter] = letterCount[letter] + 1;
                    }
                }
            }

            List<char> result = new List<char>();
            foreach (KeyValuePair<char, int> pair in letterCount)
            {
                if (pair.Value > 1)
                {
                    result.Add(pair.Key);
                }
            }

            SortList(result);

            return result;
        }

        private static string[] SplitIntoWords(string text)
        {
            List<string> words = new List<string>();
            StringBuilder currentWord = new StringBuilder();

            foreach (char c in text)
            {
                if (char.IsLetter(c) || c == 'ё' || c == 'Ё')
                {
                    currentWord.Append(c);
                }
                else
                {
                    if (currentWord.Length > 0)
                    {
                        words.Add(currentWord.ToString());
                        currentWord.Clear();
                    }
                }
            }

            if (currentWord.Length > 0)
            {
                words.Add(currentWord.ToString());
            }

            return words.ToArray();
        }

        private static void SortList(List<char> list)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                for (int j = i + 1; j < list.Count; j++)
                {
                    if (list[i] > list[j])
                    {
                        char temp = list[i];
                        list[i] = list[j];
                        list[j] = temp;
                    }
                }
            }
        }

        //ЗАДАНИЕ 10 (Dictionary/SortedList)
        public static double CalculateAverageEmployeesPerDepartment(string filePath)
        {
            Dictionary<string, int> departmentCount = new Dictionary<string, int>();

            using (StreamReader reader = new StreamReader(filePath, Encoding.UTF8))
            {
                string firstLine = reader.ReadLine();
                int n;
                if (!int.TryParse(firstLine, out n))
                {
                    return 0;
                }

                for (int i = 0; i < n; i++)
                {
                    string line = reader.ReadLine();
                    if (line == null)
                    {
                        break;
                    }

                    string phone = ExtractPhoneNumber(line);
                    string lastTwoDigits = GetLastTwoDigits(phone);

                    if (departmentCount.ContainsKey(lastTwoDigits))
                    {
                        departmentCount[lastTwoDigits]++;
                    }
                    else
                    {
                        departmentCount[lastTwoDigits] = 1;
                    }
                }
            }
            int totalEmployees = 0;
            foreach (int count in departmentCount.Values)
            {
                totalEmployees += count;
            }

            return (double)totalEmployees / departmentCount.Count;
        }

        private static string ExtractPhoneNumber(string line)
        {
            string[] parts = line.Split(' ');

            if (parts.Length < 3)
                return null;

            return parts[parts.Length - 1];
        }

        private static string GetLastTwoDigits(string phone)
        {
            string[] parts = phone.Split('-');
            return parts[parts.Length - 1];
        }

        public static void CreateTestFileTask9(string path)
        {
            string text = "Белый снег, белый мел,\n" +
                         "Белый заяц тоже бел.\n" +
                         "А вот белка не бела,\n" +
                         "Белой даже не была.";

            File.WriteAllText(path, text, Encoding.UTF8);
            Console.WriteLine($"Тестовый файл '{path}' создан.");
        }

        public static void CreateTestFileTask10(string path)
        {
            string[] lines = {
        "5",
        "Иванов П.С. 555-66-77",
        "Петров А.В. 555-66-77",
        "Сидоров М.И. 555-66-88",
        "Козлов Д.А. 555-66-77",
        "Смирнов Е.Б. 555-66-99"
        };

            File.WriteAllLines(path, lines, Encoding.UTF8);
            Console.WriteLine($"Тестовый файл '{path}' создан.");
        }
    }
}
