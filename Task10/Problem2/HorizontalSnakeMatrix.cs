using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task10.Problem2
{
    internal class HorizontalSnakeMatrix : AbstractMatrix
    {
        public HorizontalSnakeMatrix() : base() { }
        public HorizontalSnakeMatrix(int[,] matrix, TurnSide turnSide = default) : base(matrix, turnSide) { }
        public override IEnumerator<int> GetEnumerator()
        {
            bool isRight = _turnSide == TurnSide.Right;
            for (int i = 0; i < _rows; i++)
            {
                if (isRight)
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
                isRight = !isRight;
            }
        }
    }
}
