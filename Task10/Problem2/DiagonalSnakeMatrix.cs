using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            int counter = 1;
            bool isTopRight = _turnSide == TurnSide.Right;
            for (int i = 0; counter <= _rows * _cols; i++)
            {
                int row = isTopRight ? i : 0;
                int col = isTopRight ? 0 : i;
                do
                {
                    if (row < _rows && col < _cols)
                    {
                        yield return Matrix[row, col];
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
        }
    }
}
