using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5
{
    public static class SortingAlgorithms
    {
        #region QuickSortAlgorithm
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
        public static void QuickSort(int[] array, int left, int right, PivotElement pivotElement)
        {
            if (left < right)
            {
                int pivot = Partition(array, left, right, pivotElement);

                if (pivot > 1)
                {
                    QuickSort(array, left, pivot - 1, pivotElement);
                }
                if (pivot + 1 < right)
                {
                    QuickSort(array, pivot + 1, right, pivotElement);
                }
            }
        }
        #endregion

        #region MergeSortAlgorithm
        private static void Merge(int[] array, int left, int middle, int right)
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
        public static async Task MergeSplitSort(int[] array, int left, int right)
        {
            if (left < right)
            {
                int middle = (left + right) / 2;
                Task leftPart = MergeSplitSort(array, left, middle);
                Task rightPart = MergeSplitSort(array, middle + 1, right);
                await Task.WhenAll(leftPart, rightPart);
                Merge(array, left, middle + 1, right);
            }
        }
        private static void Merge(int[] array1, int[] array2, string targetFile)
        {
            int firstArrayIndex = 0, secondArrayIndex = 0;

            if (!File.Exists(targetFile))
            {
                using var writer = File.Create(targetFile);
            }

            using var streamWriter = new StreamWriter(targetFile, false);
            while (firstArrayIndex < array1.Length && secondArrayIndex < array2.Length)
            {
                if (array1[firstArrayIndex] <= array2[secondArrayIndex])
                {
                    streamWriter.Write($"{array1[firstArrayIndex++]} ");
                }
                else
                {
                    streamWriter.Write($"{array2[secondArrayIndex++]} ");
                }
            }
            while (firstArrayIndex < array1.Length)
            {
                streamWriter.Write($"{array1[firstArrayIndex++]} ");
            }
            while (secondArrayIndex < array2.Length)
            {
                streamWriter.Write($"{array2[secondArrayIndex++]} ");
            }
        }
        public static void MergeSortFile(string path)
        {
            string? line = "";
            using (StreamReader streamReader = new StreamReader(path))
            {
                line = streamReader.ReadLine();
            }
            int numbersCount = line.Count(c => c == ' ');
            int middleIndex = 0;
            for (int i = 0; i != numbersCount / 2; middleIndex++)
            {
                if (line[middleIndex] == ' ')
                {
                    i++;
                }
            }
            var firstPartOfLine = line[..(middleIndex - 1)].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s)).ToArray();
            var secondPartOfLine = line[middleIndex..].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s)).ToArray();
            Task firstHalf = MergeSplitSort(firstPartOfLine, 0, numbersCount / 2 - 1);
            Task secondHalf = MergeSplitSort(secondPartOfLine, 0, numbersCount - numbersCount / 2 - 1);
            Task.WaitAll(firstHalf, secondHalf);
            string? directory = Path.GetDirectoryName(path);
            Merge(firstPartOfLine, secondPartOfLine, $@"{directory}\SortedArray.txt");
        }
        #endregion

        #region HeapSort
        public static void HeapSort(int[] array)
        {
            for (int i = array.Length / 2 - 1; i >= 0; i--)//Build max heap first time
            {
                Heapify(array, array.Length, i);
            }

            for (int i = array.Length - 1; i >= 0; i--)//Build max heap
            {
                (array[0], array[i]) = (array[i], array[0]);//Swap first and last element of the heap
                Heapify(array, i, 0);//Using this metod, since only first element is out of place
            }
        }
        private static void Heapify(int[] array, int heapSize, int parentIndex)
        {
            var leftChildIndex = 2 * parentIndex + 1;
            var rightChildIndex = 2 * parentIndex + 2;
            int maxNumberIndex = parentIndex;

            if (leftChildIndex < heapSize && array[leftChildIndex] > array[maxNumberIndex])
            {
                maxNumberIndex = leftChildIndex;
            }
            if (rightChildIndex < heapSize && array[rightChildIndex] > array[maxNumberIndex])
            {
                maxNumberIndex = rightChildIndex;
            }

            if (maxNumberIndex != parentIndex)
            {
                (array[parentIndex], array[maxNumberIndex]) = (array[maxNumberIndex], array[parentIndex]);
                Heapify(array, heapSize, maxNumberIndex);
            }
        }
        #endregion
    }
}
