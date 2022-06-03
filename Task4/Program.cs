namespace Task4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Vector vector = new Vector(15);

                vector.InitRand(-15, 15);
                Console.WriteLine($"Unsorted array: {vector}");

                vector.BubbleSort(Order.Descending);//Bubble sort
                Console.WriteLine($"Sorted with BubbleSort array in {SortingAlgorithms.OrderOfSorting} order: {vector}");

                vector.InitShuffle();
                Console.WriteLine($"\nUnsorted array: {vector}");

                vector.QuickSort(PivotElement.Middle, Order.Descending);//Quick sort
                Console.WriteLine($"Sorted with QuickSort array in {SortingAlgorithms.OrderOfSorting} order: {vector}");

                vector.InitRand(-15, 15);
                Console.WriteLine($"\nUnsorted array: {vector}");

                vector.QuickSortWithDuplicates(Order.Descending);//Quick sort with duplicates
                Console.WriteLine($"Sorted with QuickSortWithDuplicates array in {SortingAlgorithms.OrderOfSorting} order: {vector}");

                vector.InitRand(-15, 15);
                Console.WriteLine($"\nUnsorted array: {vector}");

                vector.MergeSort(Order.Descending);//Merge sort
                Console.WriteLine($"Sorted with MergeSort array in {SortingAlgorithms.OrderOfSorting} order: {vector}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
    }
}
