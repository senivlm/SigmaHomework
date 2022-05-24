using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixHomework
{
    public class Pair
    {
        private int number;
        private int freq;
        public int Number { get => number; set => number = value; }
        public int Freq { get => freq; set => freq = value; }

        public Pair(int number, int freq)
        {
            Number = number;
            Freq = freq;
        }
        public override string ToString()
        {
            return $"{number} - {freq}";
        }
    }
}
