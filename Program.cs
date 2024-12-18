using System;
using System.Diagnostics;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        int[] mergeSortData = GenerateRandomArray(200000);
        int[] heapSortData = GenerateRandomArray(300000);

        CompareSorts("Merge Sort", MergeSortSequential, MergeSortParallel, mergeSortData);
        CompareSorts("Heap Sort", HeapSortSequential, HeapSortParallel, heapSortData);

        Console.WriteLine("Comparison Complete");
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey(); // Added display stop line
    }

    static int[] GenerateRandomArray(int size)
    {
        Random random = new Random();
        int[] array = new int[size];
        for (int i = 0; i < size; i++)
        {
            array[i] = random.Next(1, 100000);
        }
        return array;
    }

    static void CompareSorts(string sortName, Action<int[]> sequentialSort, Action<int[]> parallelSort, int[] originalData)
    {
        Console.WriteLine($"\n{sortName}:");

        // Sequential Sort
        int[] sequentialData = (int[])originalData.Clone();
        var watch = Stopwatch.StartNew();
        long beforeMemory = GC.GetTotalMemory(true);
        sequentialSort(sequentialData);
        long afterMemory = GC.GetTotalMemory(false);
        watch.Stop();
        Console.WriteLine($"Sequential - Time: {watch.ElapsedMilliseconds} ms, Memory: {afterMemory - beforeMemory} bytes");

        // Parallel Sort
        int[] parallelData = (int[])originalData.Clone();
        watch.Restart();
        beforeMemory = GC.GetTotalMemory(true);
        parallelSort(parallelData);
        afterMemory = GC.GetTotalMemory(false);
        watch.Stop();
        Console.WriteLine($"Parallel - Time: {watch.ElapsedMilliseconds} ms, Memory: {afterMemory - beforeMemory} bytes");

        // Ensure both sorted results are the same
        Console.WriteLine($"Are results identical? {IsSortedEqual(sequentialData, parallelData)}");
    }

    static bool IsSortedEqual(int[] arr1, int[] arr2)
    {
        if (arr1.Length != arr2.Length) return false;
        for (int i = 0; i < arr1.Length; i++)
        {
            if (arr1[i] != arr2[i]) return false;
        }
        return true;
    }

    static void MergeSortSequential(int[] array)
    {
        MergeSort(array, 0, array.Length - 1);
    }

    static void MergeSortParallel(int[] array)
    {
        if (array.Length <= 10000) // Optimize for smaller arrays to avoid thread overhead
        {
            MergeSort(array, 0, array.Length - 1);
            return;
        }

        Parallel.Invoke(
            () => MergeSort(array, 0, array.Length / 2),
            () => MergeSort(array, array.Length / 2 + 1, array.Length - 1));
        Merge(array, 0, array.Length / 2, array.Length - 1);
    }

    static void MergeSort(int[] array, int left, int right)
    {
        if (left < right)
        {
            int middle = (left + right) / 2;
            MergeSort(array, left, middle);
            MergeSort(array, middle + 1, right);
            Merge(array, left, middle, right);
        }
    }

    static void Merge(int[] array, int left, int middle, int right)
    {
        int[] leftArray = new int[middle - left + 1];
        int[] rightArray = new int[right - middle];
        Array.Copy(array, left, leftArray, 0, middle - left + 1);
        Array.Copy(array, middle + 1, rightArray, 0, right - middle);

        int i = 0, j = 0, k = left;
        while (i < leftArray.Length && j < rightArray.Length)
        {
            if (leftArray[i] <= rightArray[j])
            {
                array[k++] = leftArray[i++];
            }
            else
            {
                array[k++] = rightArray[j++];
            }
        }

        while (i < leftArray.Length)
        {
            array[k++] = leftArray[i++];
        }

        while (j < rightArray.Length)
        {
            array[k++] = rightArray[j++];
        }
    }

    static void HeapSortSequential(int[] array)
    {
        int n = array.Length;
        for (int i = n / 2 - 1; i >= 0; i--)
            Heapify(array, n, i);

        for (int i = n - 1; i > 0; i--)
        {
            (array[0], array[i]) = (array[i], array[0]);
            Heapify(array, i, 0);
        }
    }

    static void HeapSortParallel(int[] array)
    {
        int n = array.Length;

        if (n <= 10000) // Optimize for smaller arrays to avoid thread overhead
        {
            HeapSortSequential(array);
            return;
        }

        Parallel.For(0, n / 2, i => Heapify(array, n, n / 2 - 1 - i));

        for (int i = n - 1; i > 0; i--)
        {
            (array[0], array[i]) = (array[i], array[0]);
            Heapify(array, i, 0);
        }
    }

    static void Heapify(int[] array, int n, int i)
    {
        int largest = i;
        int left = 2 * i + 1;
        int right = 2 * i + 2;

        if (left < n && array[left] > array[largest])
            largest = left;

        if (right < n && array[right] > array[largest])
            largest = right;

        if (largest != i)
        {
            (array[i], array[largest]) = (array[largest], array[i]);
            Heapify(array, n, largest);
        }
    }
}