namespace Task4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Vector vector = new Vector(1);
                vector.ReadFromFile(@"D:\C# projects\SigmaHomework\Task4\ArrayInitializer.txt");
                Console.WriteLine(vector);
                //vector.QuickSort(PivotElement.First);
                vector.MergeSort();
                Console.WriteLine(vector);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}
