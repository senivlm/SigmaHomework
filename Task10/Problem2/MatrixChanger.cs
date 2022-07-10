using System.Collections;
using System.Text;

namespace Task10.Problem2
{
    //Гірший варіант реалізації завдання, оскільки потрібно розкоментовувати один IEnumerator і закоментовувати інший
    //(для перегляду кращого варіанту (на мою думку) дивіться файл AbstractMatrix.cs)
    internal class MatrixChanger : IEnumerable<int>
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
        public MatrixChanger()
        {
            Matrix = new int[1, 1] { { 0 } };
            Rows = 1;
            Cols = 1;
        }
        public MatrixChanger(int[,] matrix)
        {
            Matrix = (int[,])matrix.Clone() ?? new int[1, 1] { { 0 } };
            Rows = matrix.GetLength(0);
            Cols = matrix.GetLength(1);
        }
        public string GetMatrixString()
        {
            var builder = new StringBuilder();
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _cols; j++)
                {
                    builder.Append($"{Matrix[i, j]}\t");
                }
                builder.Append('\n');
            }
            return builder.ToString();
        }
        //DiagonalSnake
        public IEnumerator<int> GetEnumerator()
        {
            if (_rows != _cols)
            {
                throw new Exception("Matrix must be square to use DiagonatSnakeMatrix method!");
            }
            bool isTopRight = true;
            for (int i = 0; i < _rows + _cols - 1; i++)
            {
                int row = isTopRight ? i : 0;
                int col = isTopRight ? 0 : i;
                do
                {
                    if (row < _rows && col < _cols)
                    {
                        yield return Matrix[row, col];
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
        }

        //HorizontalSnake
        /*public IEnumerator<int> GetEnumerator()
        {
            for (int i = 0; i < _rows; i++)
            {
                if (i % 2 == 0)
                {
                    for (int j = 0; j < _cols; j++)
                    {
                        yield return Matrix[i, j];
                    }
                }
                else
                {
                    for (int j = _cols - 1; j >= 0; j--)
                    {
                        yield return Matrix[i, j];
                    }
                }
            }
        }*/

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
