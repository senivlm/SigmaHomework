using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4
{
    public static class SortingAlgorithms
    {
        #region QuickSortAlgorithm
        private static int Partition(ref int[] array, int left, int right, PivotElement pivotElement)
        {
            int pivot = pivotElement switch
            {
                PivotElement.Middle => array[(left + right) / 2],
                PivotElement.First => array[left],
                PivotElement.Last => array[right],
                _ => array[left]
            };
            while (true)
            {
                while (array[left] < pivot)
                {
                    left++;
                }
                while (array[right] > pivot)
                {
                    right--;
                }
                if (left < right)
                {
                    if (array[left] == array[right])
                    {
                        return right;
                    }
                    (array[left], array[right]) = (array[right], array[left]);
                }
                else
                {
                    return right;
                }
            }
        }
        public static void QuickSort(ref int[] array, int left, int right, PivotElement pivotElement)
        {
            if (left < right)
            {
                int pivot = Partition(ref array, left, right, pivotElement);

                if (pivot > 1)
                {
                    QuickSort(ref array, left, pivot - 1, pivotElement);
                }
                if (pivot + 1 < right)
                {
                    QuickSort(ref array, pivot + 1, right, pivotElement);
                }
            }
        }
        #endregion

        #region MergeSortAlgorithm
        private static void Merge(ref int[] array, int left, int middle, int right)
        {
            int firstPartStart = left;
            int secondPartStart = middle;
            int currentTemp = 0;
            int[] temp = new int[right - left + 1];
            while (firstPartStart <= middle - 1 && secondPartStart <= right)
            {
                if (array[firstPartStart] <= array[secondPartStart])
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
        public static void MergeSplitSort(ref int[] array, int left, int right)
        {
            if (left < right)
            {
                int middle = (left + right) / 2;
                MergeSplitSort(ref array, left, middle);
                MergeSplitSort(ref array, middle + 1, right);
                Merge(ref array, left, middle + 1, right);
            }
        }
        #endregion
    }
}
