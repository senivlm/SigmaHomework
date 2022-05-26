namespace Task4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Vector vector = new Vector(new int[] { 10, 5, 8, 9, 16, 6, 15, 12, 3 });

                vector.Quicksort(PivotElement.First);
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
