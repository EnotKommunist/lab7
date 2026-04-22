using System;
using System.Collections.Generic;

namespace laba_7_2
{
    class Program
    {
        static void Main()
        {
            // ==================== ЗАДАНИЕ 6 ====================
            Console.WriteLine("--- ЗАДАНИЕ 6 (List) ---");
            List<int> numbers = new List<int> { 1, 2, 3, 2, 4, 2, 5 };
            Console.WriteLine("Исходный список: " + ListToString(numbers));

            List<int> result6 = CollectionTasks.RemoveAllElements(numbers, 2);
            Console.WriteLine("После удаления всех 2: " + ListToString(result6));

            // ==================== ЗАДАНИЕ 7 ====================
            Console.WriteLine("\n--- ЗАДАНИЕ 7 (LinkedList) ---");
            LinkedList<int> linkedList = new LinkedList<int>();
            int[] values = { 1, 3, 5, 2, 7, 5, 9, 10, 5, 12 };
            foreach (int val in values)
            {
                linkedList.AddLast(val);
            }

            Console.WriteLine("Исходный список: " + LinkedListToString(linkedList));
            Console.WriteLine("Переворот между первым и последним вхождением 5");

            CollectionTasks.ReverseBetweenFirstAndLast(linkedList, 5);
            Console.WriteLine("Результат:      " + LinkedListToString(linkedList));

            // ==================== ЗАДАНИЕ 8 ====================
            Console.WriteLine("\n--- ЗАДАНИЕ 8 (HashSet - фильмы) ---");

            // Список всех фильмов
            List<string> allMovies = new List<string>
            {
                "Матрица", "Аватар", "Титаник", "Интерстеллар", "Начало", "Гладиатор"
            };

            // Списки просмотренных фильмов для каждого зрителя
            List<HashSet<string>> viewersMovies = new List<HashSet<string>>();

            // Зритель 1
            HashSet<string> viewer1 = new HashSet<string> { "Матрица", "Аватар", "Титаник", "Начало" };
            viewersMovies.Add(viewer1);

            // Зритель 2
            HashSet<string> viewer2 = new HashSet<string> { "Матрица", "Аватар", "Интерстеллар", "Гладиатор" };
            viewersMovies.Add(viewer2);

            // Зритель 3
            HashSet<string> viewer3 = new HashSet<string> { "Матрица", "Титаник", "Гладиатор" };
            viewersMovies.Add(viewer3);

            Console.WriteLine("Всего зрителей: " + viewersMovies.Count);
            Console.WriteLine("Всего фильмов: " + allMovies.Count);

            HashSet<string> watchedByAll;
            HashSet<string> watchedBySome;
            HashSet<string> watchedByNone;

            CollectionTasks.AnalyzeMovieViews(
                viewersMovies,
                allMovies,
                out watchedByAll,
                out watchedBySome,
                out watchedByNone);

            Console.WriteLine("\nПосмотрели все: " + SetToString(watchedByAll));
            Console.WriteLine("Посмотрели некоторые: " + SetToString(watchedBySome));
            Console.WriteLine("Не посмотрел никто: " + SetToString(watchedByNone));

            // ==================== ЗАДАНИЕ 9 ====================
            Console.WriteLine("\n--- ЗАДАНИЕ 9 (HashSet - звонкие согласные) ---");
            string task9File = "task9_text.txt";
            CollectionTasks.CreateTestFileTask9(task9File);

            Console.WriteLine("Содержимое файла:");
            Console.WriteLine(System.IO.File.ReadAllText(task9File));

            List<char> frequentConsonants = CollectionTasks.FindFrequentVoicedConsonants(task9File);

            Console.Write("Звонкие согласные, входящие более чем в одно слово: ");
            foreach (char c in frequentConsonants)
            {
                Console.Write(c + " ");
            }
            Console.WriteLine();

            // ==================== ЗАДАНИЕ 10 ====================
            Console.WriteLine("\n--- ЗАДАНИЕ 10 (Dictionary - телефоны) ---");
            string task10File = "task10_phones.txt";
            CollectionTasks.CreateTestFileTask10(task10File);

            Console.WriteLine("Содержимое файла:");
            string[] lines = System.IO.File.ReadAllLines(task10File);
            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }

            double average = CollectionTasks.CalculateAverageEmployeesPerDepartment(task10File);
            Console.WriteLine($"\nСреднее количество сотрудников в подразделении: {average:F2}");
        }

        // Вспомогательные методы для вывода
        static string ListToString<T>(List<T> list)
        {
            if (list.Count == 0) return "[]";

            string result = "[";
            for (int i = 0; i < list.Count; i++)
            {
                result += list[i].ToString();
                if (i < list.Count - 1)
                    result += ", ";
            }
            result += "]";
            return result;
        }

        static string LinkedListToString<T>(LinkedList<T> list)
        {
            if (list.Count == 0) return "[]";

            string result = "[";
            LinkedListNode<T> current = list.First;
            while (current != null)
            {
                result += current.Value.ToString();
                current = current.Next;
                if (current != null)
                    result += ", ";
            }
            result += "]";
            return result;
        }

        static string SetToString<T>(HashSet<T> set)
        {
            if (set.Count == 0) return "{}";

            string result = "{";
            int count = 0;
            foreach (T item in set)
            {
                result += item.ToString();
                count++;
                if (count < set.Count)
                    result += ", ";
            }
            result += "}";
            return result;
        }
    }
}
