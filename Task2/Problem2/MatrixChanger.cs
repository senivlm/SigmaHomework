using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    public enum TurnSide
    {
        Down,
        Right
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
        public void DiagonalSnakeMatrix(TurnSide turnSide)
        {
            if (rows != cols)
            {
                throw new Exception("Matrix must be square to use DiagonatSnakeMatrix method!");
            }
            int counter = 1;
            bool isTopRight = turnSide == TurnSide.Right ? true : false;
            for (int i = 0; counter <= rows * cols; i++)
            {
                int row = isTopRight ? i : 0;
                int col = isTopRight ? 0 : i;
                do
                {
                    if (row < rows && col < cols)
                    {
                        Matrix[row, col] = counter;
                        counter++;
                    }
                    if (isTopRight)
                    {
                        row--;
                        col++;
                    }
                    else
                    {
                        row++;
                        col--;
                    }
                } while (isTopRight ? row >= 0 : col >= 0);
                isTopRight = !isTopRight;
            }
            OutputMatrix();
        }
        public void SpiralSnakeMatrix()
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
