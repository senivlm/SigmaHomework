using System.Collections;
using System.Text;

namespace Task10.Problem2
{
    public enum TurnSide
    {
        Right,
        Left
    }
    //Кращий варіант реалізації завдання, оскільки реалізувати IEnumerable двічі в одному класі неможливо
    public abstract class AbstractMatrix : IEnumerable<int>
    {
        protected TurnSide _turnSide;
        protected int _rows;
        protected int _cols;
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
        protected int[,] Matrix;
        public AbstractMatrix()
        {
            Matrix = new int[1, 1] { { 0 } };
            Rows = 1;
            Cols = 1;
        }
        public AbstractMatrix(int[,] matrix, TurnSide turnSide = default)
        {
            Matrix = (int[,])matrix.Clone() ?? new int[1, 1] { { 0 } };
            Rows = matrix.GetLength(0);
            Cols = matrix.GetLength(1);
            _turnSide = turnSide;
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
        public abstract IEnumerator<int> GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
