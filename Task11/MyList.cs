using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task11
{
    internal class MyList : IList<int>
    {
        private List<int> _list;
        public MyList()
        {
            _list = new List<int>();
        }
        public MyList(params int[] parameters)
        {

        }
        public void Sort()
        {
            _list.Sort((int number1, int number2) => number2.CompareTo(number1));
        }
        private int BinarySearch(int number, int leftIndex, int rightIndex)
        {
            if (rightIndex < leftIndex)
            {
                return -1;
            }
            int middleIndex = (leftIndex + rightIndex) / 2;
            if (_list[middleIndex] == number)
            {
                return middleIndex;
            }
            else
            {
                if (_list[middleIndex] < number)
                {
                    return BinarySearch(number, middleIndex + 1, rightIndex);
                }
                else
                {
                    return BinarySearch(number, leftIndex, middleIndex - 1);
                }
            }
        }
        public int Find(int number)
        {
            _list.Sort();
            return BinarySearch(number, 0, _list.Count - 1);
        }
        public int this[int index]
        {
            get => _list[index];
            set => _list[index] = value;
        }
        public int Count => throw new NotImplementedException();

        public bool IsReadOnly => throw new NotImplementedException();

        public void Add(int item) => _list.Add(item);
        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(int item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(int[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<int> GetEnumerator()
        {
            _list.Sort();
            return _list.GetEnumerator();
        }

        public int IndexOf(int item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, int item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(int item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
        public override string ToString()
        {
            return string.Join(", ", _list);
        }
    }
}
