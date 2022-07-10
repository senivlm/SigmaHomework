namespace Task10.Problem2
{
    public class DiagonalSnakeMatrix : AbstractMatrix
    {
        public DiagonalSnakeMatrix() : base() { }
        public DiagonalSnakeMatrix(int[,] matrix, TurnSide turnSide = default) : base(matrix, turnSide) { }
        public override IEnumerator<int> GetEnumerator()
        {
            if (Rows != Cols)
            {
                throw new Exception("Matrix must be square to use DiagonatSnakeMatrix method!");
            }
            bool isTopRight = _turnSide == TurnSide.Right;
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
    }
}
