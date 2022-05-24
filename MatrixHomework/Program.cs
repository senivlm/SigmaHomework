using System;

namespace MatrixHomework
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Write("Enter number of rows: ");
                int rows = int.Parse(Console.ReadLine());
                Console.Write("Enter number of columns: ");
                int cols = int.Parse(Console.ReadLine());

                MatrixChanger matrixChanger = new MatrixChanger(rows, cols);
                matrixChanger.DiagonalSnakeMatrix(TurnSide.Right);

                Vector vector = new Vector(new int[] { 1, 2, 4, 3, 3, 2, 1 });
                Console.WriteLine(vector.IsPalindrom());
                Console.WriteLine($"Array before reversing: {vector}");
                vector.ReverseArray();
                Console.WriteLine($"Array after reversing: {vector}");
                Console.WriteLine("Longest subarray:");
                vector.LongestSubarray();
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
