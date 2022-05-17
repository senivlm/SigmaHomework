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
                matrixChanger.VerticalSnakeMatrix();
                Console.WriteLine();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
    internal class MatrixChanger
    {
        private int rows;
        private int cols;
        public int Rows
        {
            get => rows;
            private set
            {
                if (value <= 0)
                {
                    throw new Exception("Number of rows can not be negative or zero!");
                }
                rows = value;
            }
        }
        public int Cols
        {
            get => cols;
            private set
            {
                if (value <= 0)
                {
                    throw new Exception("Number of columns can not be negative or zero!");
                }
                cols = value;
            }
        }
        private int[,] Matrix;
        public MatrixChanger(int rows, int columns)
        {
            Rows = rows;
            Cols = columns;
            Matrix = new int[rows, columns];
        }
        private void OutputMatrix()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write($"{Matrix[i, j]}\t");
                }
                Console.WriteLine();
            }
        }
        public void VerticalSnakeMatrix()
        {
            int counter = 1;
            for (int j = 0; j < cols; j++, counter += rows)
            {
                for (int i = 0; i < rows; i++)
                {
                    Matrix[i, j] = counter;
                    if (j % 2 == 0 && i != rows - 1)
                    {
                        counter++;
                    }
                    else if (j % 2 == 1 && i != rows - 1)
                    {
                        counter--;
                    }
                }
            }
            OutputMatrix();
        }
        public void DiagonalSnakeMatrix()
        {
            if (rows != cols)
            {
                throw new Exception("Matrix must be square to use DiagonatSnakeMatrix method!");
            }
            int counter = 1;
            bool topright = true;
            for (int i = 0; counter <= rows * cols; i++)
            {
                int row = topright ? i : 0;
                int col = topright ? 0 : i;
                do
                {
                    if (row < rows && col < cols)
                    {
                        Matrix[row, col] = counter;
                        counter++;
                    }
                    if (topright)
                    {
                        row--;
                        col++;
                    }
                    else
                    {
                        row++;
                        col--;
                    }
                } while (topright ? row >= 0 : col >= 0);
                topright = !topright;
            }
            OutputMatrix();
        }
        public void SpiralSnake()
        {
            int counter = 1;
            int i = 0, j = 0;
            for (int c = 0; counter <= cols * rows; c++)
            {
                for (; i < rows - c; i++, counter++)
                {
                    Matrix[i, j] = counter;
                }
                i--;
                j++;
                for (; j < cols - c; j++, counter++)
                {
                    Matrix[i, j] = counter;
                }
                j--;
                i--;
                for (; i >= 0 + c; i--, counter++)
                {
                    Matrix[i, j] = counter;
                }
                i++;
                j--;
                for (; j >= 1 + c; j--, counter++)
                {
                    Matrix[i, j] = counter;
                }
                j++;
                i++;
            }
            OutputMatrix();
        }
    }
}
