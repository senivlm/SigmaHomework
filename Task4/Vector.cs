using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4
{
    public enum PivotElement
    {
        Middle,
        First,
        Last
    }
    public class Vector
    {
        private int[] _array;
        public Vector(int n)
        {
            _array = new int[n];
        }
        public Vector(int[] _array)
        {
            this._array = _array;
        }
        private int Partition(int left, int right, PivotElement pivotElement)
        {
            int pivot = pivotElement switch
            {
                PivotElement.Middle => _array[(left + right) / 2],
                PivotElement.First => _array[left],
                PivotElement.Last => _array[right],
                _ => _array[left]
            };
            while (true)
            {
                while (_array[left] < pivot)
                {
                    left++;
                }
                while (_array[right] > pivot)
                {
                    right--;
                }
                if (left < right)
                {
                    if (_array[left] == _array[right])
                    {
                        return right;
                    }
                    (_array[left], _array[right]) = (_array[right], _array[left]);
                }
                else
                {
                    return right;
                }
            }
        }
        public void Quicksort(int left, int right, PivotElement pivotElement)
        {
            if (left < right)
            {
                int pivot = Partition(left, right, pivotElement);

                if (pivot > 1)
                {
                    Quicksort(left, pivot - 1, pivotElement);
                }
                if (pivot + 1 < right)
                {
                    Quicksort(pivot + 1, right, pivotElement);
                }
            }
        }
        public void Quicksort(PivotElement pivotElement)
        {
            Quicksort(0, _array.Length - 1, pivotElement);
        }
        public bool IsPalindrom()
        {
            for (int i = 0; i < _array.Length / 2; i++)
            {
                if (_array[i] != _array[^(i + 1)])
                {
                    return false;
                }
            }
            return true;
        }
        public void MyReverseArray()
        {
            for (int i = 0; i < _array.Length / 2; i++)
            {
                (_array[i], _array[^(i + 1)]) = (_array[^(i + 1)], _array[i]);
            }
        }
        public void BuiltInReverseArray()
        {
            _array = _array.Reverse().ToArray();
        }
        public void LongestSubarray()
        {
            int count = 1;
            int longestNum = _array[0];
            int longestCount = 1;

            for (int i = 1; i < _array.Length; i++)
            {
                if (_array[i] != _array[i - 1])
                {
                    count = 0;
                }
                count++;
                if (count > longestCount)
                {
                    longestCount = count;
                    longestNum = _array[i];
                }
            }
            Console.WriteLine($"Number: {longestNum}, Subsequence length: {longestCount}");
        }
        public void InitShuffle()
        {
            for (int i = 0; i < _array.Length; i++)
            {
                _array[i] = i + 1;
            }
            Random random = new Random();
            int randomNumberOfTimes = new Random().Next(5, 10);
            int random1, random2;
            for (int i = 0; i < randomNumberOfTimes; i++)
            {
                random1 = random.Next(0, _array.Length);
                random2 = random.Next(0, _array.Length);
                (_array[random1], _array[random2]) = (_array[random2], _array[random1]);
            }
        }
        public Pair[] CalculateFreq()
        {

            Pair[] pairs = new Pair[_array.Length];

            for (int i = 0; i < _array.Length; i++)
            {
                pairs[i] = new Pair(0, 0);

            }
            int countDifference = 0;

            for (int i = 0; i < _array.Length; i++)
            {
                bool isElement = false;
                for (int j = 0; j < countDifference; j++)
                {
                    if (_array[i] == pairs[j].Number)
                    {
                        pairs[j].Freq++;
                        isElement = true;
                        break;
                    }
                }
                if (!isElement)
                {
                    pairs[countDifference].Freq++;
                    pairs[countDifference].Number = _array[i];
                    countDifference++;
                }
            }

            Pair[] result = new Pair[countDifference];
            for (int i = 0; i < countDifference; i++)
            {
                result[i] = pairs[i];
            }

            return result;
        }
        public void InitRand(int a, int b)
        {
            var random = new Random();
            for (int i = 0; i < _array.Length; i++)
            {
                _array[i] = random.Next(a, b);
            }
        }
        public int this[int index]
        {
            get => (index >= 0 && index < _array.Length) ? _array[index] : throw new IndexOutOfRangeException();
            set
            {
                if (index >= 0 && index < _array.Length)
                {
                    _array[index] = value;
                }
            }
        }

        public override string ToString()
        {
            return string.Join(' ', _array);
        }
    }
}
