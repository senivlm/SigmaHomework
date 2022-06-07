using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5
{
    public static class SortingAlgorithms
    {
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
            {
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
            {
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
        private static void Merge(string file1, string file2, string targetFile)
        {
            if (!File.Exists(targetFile))
            {
                using var writer = File.Create(targetFile);
            }

            using var streamReader1 = new StreamReader(file1);
            using var streamReader2 = new StreamReader(file2);
            using var streamWriter = new StreamWriter(targetFile, false);

            bool isElementInFirstFile = false, isFirstIteration = true;
            int first = 0;
            int second = 0;

            while (!streamReader1.EndOfStream && !streamReader2.EndOfStream)
            {//На кожній ітерації циклу перевіряти? Не доцільно!
                if (isFirstIteration)
                {
                    first = int.Parse(streamReader1.ReadLine());
                    second = int.Parse(streamReader2.ReadLine());
                    isFirstIteration = false;
                }
                else if (isElementInFirstFile)
                {
                    first = int.Parse(streamReader1.ReadLine());
                }
                else
                {
                    second = int.Parse(streamReader2.ReadLine());
                }
                if (first.CompareTo(second) * (int)OrderOfSorting <= 0)
                {
                    streamWriter.Write($"{first} ");
                    isElementInFirstFile = true;
                }
                else
                {
                    streamWriter.Write($"{second} ");
                    isElementInFirstFile = false;
                }
            }

            streamWriter.Write($"{(isElementInFirstFile ? second : first)} ");

            while (!streamReader1.EndOfStream)
            {
                streamWriter.Write($"{streamReader1.ReadLine()} ");
            }
            while (!streamReader2.EndOfStream)
            {
                streamWriter.Write($"{streamReader2.ReadLine()} ");
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

            string? directory = Path.GetDirectoryName(path);

            var PartOfLine = line[..(middleIndex - 1)].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s)).ToArray();
            MergeSplitSort(PartOfLine, 0, numbersCount / 2 - 1);
            using (var creator = File.Create($@"{directory}\leftPart.txt")) { }
            using (StreamWriter writer = new StreamWriter($@"{directory}\leftPart.txt"))
            {
                foreach (var part in PartOfLine)
                {
                    writer.WriteLine(part);
                }
            }

            PartOfLine = line[middleIndex..].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s)).ToArray();
            MergeSplitSort(PartOfLine, 0, numbersCount - numbersCount / 2 - 1);
            using (var creator = File.Create($@"{directory}\rightPart.txt")) { }
            using (StreamWriter writer = new StreamWriter($@"{directory}\rightPart.txt"))
            {
                foreach (var part in PartOfLine)
                {
                    writer.WriteLine(part);
                }
            }

            Merge($@"{directory}\leftPart.txt", $@"{directory}\rightPart.txt", $@"{directory}\SortedArray.txt");

            File.Delete($@"{directory}\leftPart.txt");
            File.Delete($@"{directory}\rightPart.txt");
        }
        #endregion

        #region HeapSort
        public static void HeapSort(int[] array)
        {
            for (int i = array.Length / 2 - 1; i >= 0; i--)//Build max/min heap first time
            {
                Heapify(array, array.Length, i);
            }

            for (int i = array.Length - 1; i >= 0; i--)//Build max/min heap
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

            if (leftChildIndex < heapSize && array[leftChildIndex].CompareTo(array[maxNumberIndex]) * (int)OrderOfSorting > 0)
            {
                maxNumberIndex = leftChildIndex;
            }
            if (rightChildIndex < heapSize && array[rightChildIndex].CompareTo(array[maxNumberIndex]) * (int)OrderOfSorting > 0)
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
