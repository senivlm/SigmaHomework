using System.Text;

namespace Task5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Vector vector = new Vector(15);
                vector.ReadFromFile(@"D:\C# projects\SigmaHomework\Task5\UnsortedArray.txt");
                Console.WriteLine($"Unsorted array: {vector}");

                vector.MergeSortFile(@"D:\C# projects\SigmaHomework\Task5\UnsortedArray.txt", Order.Descending);//Merge sort files
                vector.ReadFromFile(@"D:\C# projects\SigmaHomework\Task5\SortedArray.txt");//Reading sorted array from file
                Console.WriteLine($"Sorted with FileMerge array in {SortingAlgorithms.OrderOfSorting} order: {vector}");

                vector.InitRand(-15, 15);
                Console.WriteLine($"Unsorted array: {vector}");

                vector.HeapSort(Order.Descending);//Heap sort
                Console.WriteLine($"Sorted with HeapSort array in {SortingAlgorithms.OrderOfSorting} order: {vector}");

                /*string myString = "Hello my beautiful world! SigmaSoftware";
                string newStr = myString.Replace(" ", $" {myString[(myString.LastIndexOf(' ') + 1)..]} ");
                Console.WriteLine(newStr);

                string str = "   Hello, World! I am   a   student  of SigmaSoftware     Camp  ";
                var words = str.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                str = char.ToLower(str[0]) + str[1..].ToUpper();
                foreach (var word in words)
                {
                    Console.WriteLine(word);
                }*/
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            /*Console.WriteLine();
            var students = new List<(string, int, double)>();

            try
            {
                using (StreamReader reader = new StreamReader(@"D:\C# projects\SigmaHomework\Task5\data.txt"))
                {
                    while (!reader.EndOfStream)
                    {
                        try
                        {
                            string line = reader.ReadLine();
                            students.Add(Parse(line));
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }

            foreach (var student in students)
            {
                Console.WriteLine($"{student.Item1} - {student.Item2} - {student.Item3}");
            }*/

            Console.ReadLine();
        }
        public static (string, int, double) Parse(string line)
        {
            var data = line.Split(' ');
            string exception = "";
            double mark = 0;
            /*if (data.Length != 3)
            {
                throw new FormatException($"String format is invalid: {line}");
            }*/
            if (!int.TryParse(data[1], out int year))
            {
                exception += "Invalid year format ";
            }
            try//If third element is epsent, we will initialize it with 0
            {
                if (!double.TryParse(data[2], out mark))
                {
                    exception += "Invalid mark format";
                }
            }
            catch (IndexOutOfRangeException)
            {
                mark = 0;
            }
            if (exception.Length != 0)
            {
                throw new FormatException(exception);
            }
            return (data[0], year, mark);
        }
    }
}
