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

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}
