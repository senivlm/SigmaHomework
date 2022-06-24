using System.Collections;

namespace Task11.Problem2
{
    internal class GenericList<T> : IList<T> where T : IComparable<T>//Не певен, що це саме той клас,
                                                                     //для якого потрібно було зробити узагальнення
    {
        private List<T> _values;
        public GenericList(params T[] values)
        {
            _values = values?.ToList() ?? new List<T>();
        }
        public T this[int index]
        {
            get => _values[index];
            set => _values[index] = value;
        }
        public int BinarySearch(T item)
        {
            int left = 0;
            int right = _values.Count - 1;

            _values.Sort();

            while (left <= right)
            {
                int middle = (left + right) / 2;
                if (item.CompareTo(_values[middle]) == 0)
                {
                    return middle;
                }
                else if (item.CompareTo(_values[middle]) < 0)
                {
                    right = middle - 1;
                }
                else
                {
                    left = middle + 1;
                }
            }

            return -1;
        }
        public int Count => _values.Count;

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            _values.Add(item);
        }

        public void Clear()
        {
            _values = new List<T>();
        }

        public bool Contains(T item)
        {
            return _values.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            for (int i = arrayIndex, j = 0; i < arrayIndex + _values.Count; i++, j++)
            {
                array[i] = _values[j];
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _values.GetEnumerator();
        }

        public int IndexOf(T item)
        {
            return _values.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            _values.Insert(index, item);
        }

        public bool Remove(T item)
        {
            return _values.Remove(item);
        }

        public void RemoveAt(int index)
        {
            _values.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
