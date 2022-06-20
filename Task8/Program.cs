using System.Collections;
using System.Text.RegularExpressions;
using Task8.Problem1;
using Task8.Problem2;
using Task8.Problem3;
using Task8.Problem3.Products;

namespace Task8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Problem 1
            var quarter1 = new Quarter();
            var quarter2 = new Quarter();
            try
            {
                using var reader1 = new StreamReader(@"D:\C# projects\SigmaHomework\Task8\Problem1\ReportOnElectricityConsumption1.txt");
                using var reader2 = new StreamReader(@"D:\C# projects\SigmaHomework\Task8\Problem1\ReportOnElectricityConsumption2.txt");
                quarter1 = new Quarter(reader1, 1.44m);
                quarter2 = new Quarter(reader2, 1.44m);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
//Цей оператор краще після 22 стрічки вставити
            var apartmentData = quarter1 + quarter2;

            foreach (var apartment in apartmentData)
            {
                Console.Write(apartment == null ? "" : apartment);
                Console.WriteLine(new String('-', 80));
            }
            Console.ReadLine();

            var quarter = quarter1 - quarter2;

            Console.WriteLine("Printing all report to file after subtraction...");
            quarter.PrintReportToFile(@"D:\C# projects\SigmaHomework\Task8\Problem1\UserFriendlyReport.txt", false);
            Console.WriteLine("Success!");
            Console.ReadLine();

            //Problem 2
            /*using (var writer = new StreamWriter(@"D:\C# projects\SigmaHomework\Task8\Problem2\SiteStatisticsFile.txt", false))
            {
                Randomizer(writer, 1000);
            }*/

            var statistics = new SiteTrafficStatistics();
            using (var reader = new StreamReader(@"D:\C# projects\SigmaHomework\Task8\Problem2\SiteStatisticsFile.txt"))
            {
                statistics = new SiteTrafficStatistics(reader);
            }

            OutputDictionary(statistics.GetNumberOfVisitsPerWeekForEveryUser());
            Console.WriteLine(new string('-', 80));

            OutputDictionary(statistics.GetMostPopularDayOfWeekForEveryUser());
            Console.WriteLine(new string('-', 80));

            OutputDictionary(statistics.GetMostPopularTimeIntervalForEveryUser());
            Console.WriteLine(new string('-', 80));

            Console.WriteLine($"Most popular time interval for site: {statistics.GetMostPopularTimeIntervalForSite()}");

            Console.ReadLine();

            //Problem 3
            try
            {
                var storage1 = new Storage();
                var storage2 = new Storage(
                    new Product("Apples", 23.97m, 1.0m),
                    new Product("Cola", 33.99m, 1.0m),
                    new Meat("Chicken", 55.90m, 1.0m, MeatCategory.FirstGrade, MeatType.Chicken),
                    new DairyProduct("Milk", 32.00m, 0.5m, 20));

                storage1.AddProductsFromFile(@"D:\C# projects\SigmaHomework\Task8\Problem3\StorageDB.txt");//Valid path: D:\C# projects\SigmaHomework\Task7\StorageDB.txt

                storage1.OutputStorageInformation();
                storage2.OutputStorageInformation();

                Console.WriteLine("Except operation:");
                foreach (var product in Storage.StorageExcept(storage1, storage2))
                {
                    Console.WriteLine(product);
                }

                Console.WriteLine("Intersect operation:");
                foreach (var product in Storage.StorageIntersect(storage1, storage2))
                {
                    Console.WriteLine(product);
                }

                Console.WriteLine("Union operation:");
                foreach (var product in Storage.StorageUnion(storage1, storage2))
                {
                    Console.WriteLine(product);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();

            //Lecture things
            /*HashSet<int> set = new HashSet<int>();
            set.Add(5);
            set.Add(1);
            set.Add(1);
            set.Add(2);
            set.Add(2);
            set.Add(3);
            set.Add(3);*/

            /*SortedSet<int> set1 = new SortedSet<int>(new Comparator());
            set1.Add(5);
            set1.Add(1);
            set1.Add(1);
            set1.Add(2);
            set1.Add(2);
            set1.Add(3);
            set1.Add(3);

            SortedSet<int> set2 = new SortedSet<int>();
            set2.Add(6);
            set2.Add(1);
            set2.Add(4);
            set2.Add(2);
            set2.Add(2);
            set2.Add(8);
            set2.Add(3);

            var list = new List<int>().ToHashSet();

            string text = "Hello, my name is Mike!";

            var punctuation = text
                .Where(char.IsPunctuation)
                .Distinct()
                .ToArray();

            var words = Regex
                    .Split(text, @"\d*\s+\d*")
                    .Select(w => w.Trim(punctuation))
                    .Where(w => w != "")
                    .Distinct()
                    .Where(word => word.Where(symbol => symbol == 'a' || symbol == 'i').Any());

            foreach (var element in words)
            {
                Console.WriteLine(element);
            }

            Console.ReadLine();*/
        }
        private static void Randomizer(StreamWriter targetWriter, int numberOfRecords)
        {
            var valueGenerator = new Random();

            for (int i = 0; i < numberOfRecords; i++)
            {
                string IP = string.Join('.',
                    valueGenerator.Next(197, 201),
                    valueGenerator.Next(197, 201),
                    valueGenerator.Next(197, 201),
                    valueGenerator.Next(197, 201));
                var time = new TimeOnly(valueGenerator.Next(0, 24), valueGenerator.Next(0, 60), valueGenerator.Next(0, 60));
                var day = (DayOfWeek)valueGenerator.Next(0, 7);

                targetWriter.WriteLine($"{IP} {time.ToLongTimeString()} {day}");
            }
        }
        private static void OutputDictionary<TKey, TValue>(Dictionary<TKey, TValue> dictionary) where TKey : notnull
        {
            foreach (var keyValuePair in dictionary)
            {
                Console.WriteLine($"Key: {keyValuePair.Key} Value: {keyValuePair.Value}");
            }
        }
    }
    public class Comparator : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            return y.CompareTo(x);
        }
    }
}
