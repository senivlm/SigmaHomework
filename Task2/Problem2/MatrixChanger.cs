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
        private int _rows;
        private int _cols;
        public int Rows
        {
            get => _rows;
            private set
            {
                if (value <= 0)
                {
                    throw new Exception("Number of _rows can not be negative or zero!");
                }
                _rows = value;
            }
        }
        public int Cols
        {
            get => _cols;
            private set
            {
                if (value <= 0)
                {
                    throw new Exception("Number of columns can not be negative or zero!");
                }
                _cols = value;
            }
        }
        private int[,] Matrix;
        public MatrixChanger(int _rows, int columns)
        {
            Rows = _rows;
            Cols = columns;
            Matrix = new int[_rows, columns];
        }
        public MatrixChanger() : this(1, 1)
        {

        }
        public void OutputMatrix()
        {
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _cols; j++)
                {
                    Console.Write($"{Matrix[i, j]}\t");
                }
                Console.WriteLine();
            }
        }
        public void VerticalSnakeMatrix()
        {
            int counter = 1;
            for (int j = 0; j < _cols; j++, counter += _rows)
            {
                for (int i = 0; i < _rows; i++)
                {
                    Matrix[i, j] = counter;
                    if (j % 2 == 0 && i != _rows - 1)
                    {
                        counter++;
                    }
                    else if (j % 2 == 1 && i != _rows - 1)
                    {
                        counter--;
                    }
                }
            }
            OutputMatrix();
        }
        public void DiagonalSnakeMatrix(TurnSide turnSide)
        {
            if (_rows != _cols)
            {
                throw new Exception("Matrix must be square to use DiagonatSnakeMatrix method!");
            }
            int counter = 1;
            bool isTopRight = turnSide == TurnSide.Right ? true : false;
            for (int i = 0; counter <= _rows * _cols; i++)
            {
                int row = isTopRight ? i : 0;
                int col = isTopRight ? 0 : i;
                do
                {
                    if (row < _rows && col < _cols)
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
            for (int c = 0; counter <= _cols * _rows; c++)
            {
                for (; i < _rows - c; i++, counter++)
                {
                    Matrix[i, j] = counter;
                }
                i--;
                j++;
                for (; j < _cols - c; j++, counter++)
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
        public void ReadMatrixFromFile(StreamReader reader)
        {
            string line = reader.ReadLine();
            var parts = line.Split(' ');

            _rows = int.Parse(parts[0]);
            _cols = int.Parse(parts[1]);

            Matrix = new int[_rows, _cols];

            for (int i = 0; i < _rows; i++)
            {
                var items = reader.ReadLine().Split(' ');
                for (int j = 0; j < _cols; j++)
                {
                    Matrix[i, j] = int.Parse(items[j]);
                }
            }
        }
    }
}
