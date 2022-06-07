using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4
{
    public static class SortingAlgorithms
    {// Тут сервісний клас доречний
        public static Order OrderOfSorting { get; set; } = Order.Ascending;

        #region BubbleSort
        public static void BubbleSort(int[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = 0; j < array.Length - i - 1; j++)
                {
                    if (array[j].CompareTo(array[j + 1]) * (int)OrderOfSorting > 0)
                    {
                        (array[j], array[j + 1]) = (array[j + 1], array[j]);
                    }
                }
            }
        }
        #endregion

        #region QuickSort
        private static int Partition(int[] array, int left, int right, PivotElement pivotElement)
        {
            int pivot = pivotElement switch
            {
                PivotElement.Middle => array[(left + right) / 2],
                PivotElement.First => array[left],
                PivotElement.Last => array[right],
                _ => array[left]
            };
            while (true)
            {// приведення до int погана ідея
                while (array[left].CompareTo(pivot) * (int)OrderOfSorting < 0)
                {
                    left++;
                }
                while (array[right].CompareTo(pivot) * (int)OrderOfSorting > 0)
                {
                    right--;
                }
                if (left >= right)
                {
                    return right;
                }
                (array[left], array[right]) = (array[right], array[left]);
            }
        }
        public static void QuickSort(int[] array, int left, int right, PivotElement pivotElement)
        {
            if (left < right)
            {
                int pivot = Partition(array, left, right, pivotElement);
                QuickSort(array, left, pivot - 1, pivotElement);
                QuickSort(array, pivot + 1, right, pivotElement);
            }
        }

        private static void ThreeWayPartitioning(int[] array, int left, int right, out int leftEqual, out int rightEqual)
        {
            int pivot = array[left];
            leftEqual = left;
            rightEqual = right;

            for (int iterator = leftEqual + 1; iterator <= rightEqual; iterator++)
            {
                if (array[iterator].CompareTo(pivot) * (int)OrderOfSorting < 0)
                {
                    (array[iterator], array[leftEqual]) = (array[leftEqual], array[iterator]);
                    leftEqual++;
                }
                else if (array[iterator].CompareTo(pivot) * (int)OrderOfSorting > 0)
                {
                    (array[iterator], array[rightEqual]) = (array[rightEqual], array[iterator]);
                    rightEqual--;
                    iterator--;
                }
            }
        }
        public static void QuickSortWithDuplicates(int[] arr, int left, int right)
        {
            if (left < right)
            {
                ThreeWayPartitioning(arr, left, right, out int leftEqual, out int rightEqual);
                QuickSortWithDuplicates(arr, left, leftEqual - 1);
                QuickSortWithDuplicates(arr, rightEqual + 1, right);
            }
        }
        #endregion

        #region MergeSort
        private static void Merge(int[] array, int left, int middle, int right)
        {
            int firstPartStart = left;
            int secondPartStart = middle;
            int currentTemp = 0;
            int[] temp = new int[right - left + 1];
            while (firstPartStart <= middle - 1 && secondPartStart <= right)
            {//Що Вам дає виклик методу? Якщо б тип рередавався як параметр, то це б дало результат. А коли тип int, то це лишнє навантаження
                if (array[firstPartStart].CompareTo(array[secondPartStart]) * (int)OrderOfSorting <= 0)
                {
                    temp[currentTemp++] = array[firstPartStart++];
                }
                else
                {
                    temp[currentTemp++] = array[secondPartStart++];
                }
            }
            while (firstPartStart <= middle - 1)
            {
                temp[currentTemp++] = array[firstPartStart++];
            }
            while (secondPartStart <= right)
            {
                temp[currentTemp++] = array[secondPartStart++];
            }
            for (int i = 0; i < temp.Length; i++)
            {
                array[i + left] = temp[i];
            }
        }
        public static void MergeSplitSort(int[] array, int left, int right)
        {
            if (left < right)
            {
                int middle = (left + right) / 2;
                MergeSplitSort(array, left, middle);
                MergeSplitSort(array, middle + 1, right);
                Merge(array, left, middle + 1, right);
            }
        }
        #endregion
    }
}
