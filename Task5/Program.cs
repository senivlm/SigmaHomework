namespace Task5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Vector vector = new Vector(1);
                vector.ReadFromFile(@"D:\C# projects\SigmaHomework\Task5\UnsortedArray.txt");
                Console.WriteLine($"Unsorted array: {vector}");

                SortingAlgorithms.MergeSortFile(@"D:\C# projects\SigmaHomework\Task5\UnsortedArray.txt");//Merge sort 
                vector.ReadFromFile(@"D:\C# projects\SigmaHomework\Task5\SortedArray.txt");//Reading sorted array from file
                Console.WriteLine($"Sorted with FileMerge array: {vector}");

                vector.InitRand(-15, 15);
                Console.WriteLine($"\nUnsorted array: {vector}");

                vector.HeapSort();//Heap sort
                Console.WriteLine($"Sorted with HeapSort array: {vector}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
    }
}
