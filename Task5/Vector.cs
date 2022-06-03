using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5
{
    public enum PivotElement
    {
        Middle,
        First,
        Last
    }
    public enum Order
    {
        Ascending = 1,
        Descending = -1
    }
    public class Vector
    {
        private int[] _array;

        #region Constructors
        public Vector(int n)
        {
            _array = new int[n];
        }
        public Vector(int[] _array)
        {
            this._array = _array;
        }
        #endregion

        #region SortingAlgorithms
        public void BubbleSort(Order order = Order.Ascending)
        {
            SortingAlgorithms.OrderOfSorting = order;
            SortingAlgorithms.BubbleSort(_array);
        }
        public void QuickSort(PivotElement pivotElement, Order order = Order.Ascending)
        {
            SortingAlgorithms.OrderOfSorting = order;
            SortingAlgorithms.QuickSort(_array, 0, _array.Length - 1, pivotElement);
        }
        public void QuickSortWithDuplicates(Order order = Order.Ascending)
        {
            SortingAlgorithms.OrderOfSorting = order;
            SortingAlgorithms.QuickSortWithDuplicates(_array, 0, _array.Length - 1);
        }
        public void MergeSort(Order order = Order.Ascending)
        {
            SortingAlgorithms.OrderOfSorting = order;
            SortingAlgorithms.MergeSplitSort(_array, 0, _array.Length - 1);
        }
        public void HeapSort(Order order = Order.Ascending)
        {
            SortingAlgorithms.OrderOfSorting = order;
            SortingAlgorithms.HeapSort(_array);
        }
        public void MergeSortFile(string path, Order order = Order.Ascending)
        {
            SortingAlgorithms.OrderOfSorting = order;
            SortingAlgorithms.MergeSortFile(path);
        }
        #endregion

        #region OtherMethods
        public void ReadFromFile(string path)
        {
            using StreamReader streamReader = new StreamReader(path);
            _array = streamReader.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s)).ToArray();
        }
        public bool IsPalindrom()
        {
            for (int i = 0; i < _array.Length / 2; i++)
            {
                if (_array[i] != _array[_array.Length - i - 1])
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
                (_array[i], _array[_array.Length - i - 1]) = (_array[_array.Length - i - 1], _array[i]);
            }
        }
        public void BuiltInReverseArray()
        {
            _array = _array.Reverse().ToArray();
        }
        public void LongestSubarray(out int longestNum, out int longestCount)
        {
            int count = 1;
            longestNum = _array[0];
            longestCount = 1;

            for (int i = 0; i < _array.Length; i++, count++)
            {
                if (i == _array.Length - 1 || _array[i] != _array[i + 1])
                {
                    if (count > longestCount)
                    {
                        longestCount = count;
                        longestNum = _array[i];
                    }
                    count = 0;
                }
            }
        }
        public void InitShuffle()
        {
            for (int i = 0; i < _array.Length; i++)
            {
                _array[i] = i + 1;
            }
            Random random = new Random();
            int randomNumberOfTimes = new Random().Next(15, 20);
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
        public override string ToString()
        {
            return string.Join(' ', _array);
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
        #endregion
    }
}
