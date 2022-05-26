namespace Task3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Vector vector = new Vector(new int[] { 1, 2, 4, 3, 3, 2, 1 });

                //Paragraph 1
                Console.WriteLine(vector.IsPalindrom());

                //Paragraph 2
                Console.WriteLine($"Array before my reversing: {vector}");
                vector.MyReverseArray();
                Console.WriteLine($"Array after my reversing: {vector}");
                vector.BuiltInReverseArray();
                Console.WriteLine($"Array after built-in reversing: {vector}");

                //Paragraph 3
                Console.WriteLine("Longest subarray:");
                vector.LongestSubarray();

                //Paragraph 4
                Console.Write("Enter number of rows: ");
                int rows = int.Parse(Console.ReadLine());
                Console.Write("Enter number of columns: ");
                int cols = int.Parse(Console.ReadLine());

                MatrixChanger matrixChanger = new MatrixChanger(rows, cols);
                matrixChanger.DiagonalSnakeMatrix(TurnSide.Right);

                //Paragraph 5
                vector.InitShuffle();
                Console.WriteLine($"Shuffled array: {vector}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
    }
}