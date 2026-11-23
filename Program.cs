namespace hw8;

// Part A: Trace by hand
// A1:
//      Insert:         Array after insert:         Swaps:
//      15              [15]                        0
//      40              [40, 15]                    1
//      25              [40, 15, 25]                0
//      50              [50, 40, 25, 15]            2
//      10              [50, 40, 25, 15, 10]        0
//      45              [50, 40, 45, 14, 10, 25]    1
//
// A2:
//      Starting max heap: [50, 40, 45, 15, 10, 25]
//
//      Call:               Returned value:         Array after removal:
//      RemoveMax() #1      50                      [45, 40, 25, 15, 10]
//      RemoveMax() #2      45                      [40, 15, 25, 10]
//
// A3:
//      starting array: [5, 12, 3, 20, 8, 15, 1]
//
//      We start at the last parent leaf
//      Heapify call:               Array after call:
//      Heapify(2)                  [5, 12, 15, 20, 8, 3, 1]
//      Heapify(1)                  [5, 20, 15, 12, 8, 3, 1]
//      Heapify(0)                  [20, 12, 15, 5, 8, 3, 1]
//
// A4:
//      starting max heap: [20, 12, 15, 5, 8, 3, 1]
//
//      Round 1
//      1. Swap 20 with 1
//      2. Heapify the top with heap size 6
//      3. Swap 1 with 15, then 1 with 3
//
//      Array: [15, 12, 3, 5, 8, 1 | 20]
//      Sorted part: [20]
//
//      Round 2
//      1. Swap 15 with 1
//      2. Heapify the top with heap size 5
//      3.Swap 1 with 12, then 1 with 8
//
//      Array: [12, 8, 3, 5, 1 | 15, 20]
//      Sorted part: [15, 20]
//
// A5:
//      Final min heap: [10, 15, 25, 50, 40, 45]
//
// A6:
//      Way to build it         Insert      Peek        RemoveMax
//      Unsorted list           O(1)        O(n)        O(n)
//      Sorted list             O(n)        O(1)        O(1)
//      Binary heap             O(log n)    O(1)        O(log n)
//
//      A binary heap provides fast insert and removal with O(log n) time.
//      It also has O(1) access to the highest-priority item without having to sort the list.
//
// A7:
//      1.  Emergency Room: The items are patients waiting for treatment.
//                          The priority is how serious their medical condition is
//
//      2.  CPU Scheduling: The items processes waitin gto run.
//                          The priority is how urgent each process is.
//

using System;
using System.Collections.Generic;

public class MinPriorityQueue
{
    private readonly List<int> heap = new List<int>();

    public int Count => heap.Count;
    public bool IsEmpty => Count == 0;

    private static int Parent(int i) => (i - 1) / 2;

    public void Insert(int priority)
    {
        heap.Add(priority);
        int current = heap.Count - 1;
        while (current > 0 && heap[current] < heap[Parent(current)])
        {
            Swap(current, Parent(current));
            current = Parent(current);
        }
    }

    public int Peek()
    {
        if (IsEmpty)
            throw new InvalidOperationException("Empty Queue");
        return heap[0];
    }

    public int RemoveMin()
    {
        if (IsEmpty)
            throw new InvalidOperationException("Empty Queue");
        int min = heap[0];
        heap[0] = heap[heap.Count - 1];
        heap.RemoveAt(heap.Count - 1);
        if (heap.Count > 0)
            SinkDown(0);
        return min;
    }

    private void SinkDown(int index)
    {
        while (true)
        {
            int minIndex = index;
            int left = 2 * index + 1,
                right = 2 * index + 2;
            if (left < heap.Count && heap[left] < heap[minIndex])
                minIndex = left;
            if (right < heap.Count && heap[right] < heap[minIndex])
                minIndex = right;
            if (minIndex == index)
                return;
            Swap(index, minIndex);
            index = minIndex;
        }
    }

    private void Swap(int a, int b) => (heap[a], heap[b]) = (heap[b], heap[a]);
}

class Program
{
    static bool IsMaxHeap(int[] arr)
    {
        for (int i = 1; i < arr.Length; i++)
            if (arr[i] > arr[(i - 1) / 2])
                return false;
        return true;
    }

    static void Heapify(int[] arr, int size, int i)
    {
        int largest = i;
        int left = 2 * i + 1;
        int right = 2 * i + 2;

        if (left < size && arr[left] > arr[largest])
            largest = left;
        if (right < size && arr[right] > arr[largest])
            largest = right;

        if (largest != i)
        {
            (arr[i], arr[largest]) = (arr[largest], arr[i]);
            Heapify(arr, size, largest);
        }
    }

    static void BuildMaxHeap(int[] arr)
    {
        for (int i = arr.Length / 2 - 1; i >= 0; i--)
            Heapify(arr, arr.Length, i);
    }

    static void HeapSort(int[] arr)
    {
        BuildMaxHeap(arr);
        for (int end = arr.Length - 1; end > 0; end--)
        {
            (arr[0], arr[end]) = (arr[end], arr[0]);
            Heapify(arr, end, 0);
        }
    }

    static int[] TopK(int[] scores, int k)
    {
        var pq = new PriorityQueue<int, int>();

        foreach (int s in scores)
        {
            pq.Enqueue(s, s);
            if (pq.Count > k)
                pq.Dequeue();
        }

        var result = new int[pq.Count];
        for (int i = result.Length - 1; i >= 0; i--)
            result[i] = pq.Dequeue();
        return result;
    }

    static void Main(string[] args)
    {
        // B1
        var pq = new MinPriorityQueue();
        foreach (int x in new[] { 15, 40, 25, 50, 10, 45 })
            pq.Insert(x);

        var removed = new List<int>();
        while (!pq.IsEmpty)
            removed.Add(pq.RemoveMin());
        Console.WriteLine(string.Join(", ", removed));

        try
        {
            pq.RemoveMin();
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine(e.Message);
        }
        Console.WriteLine();

        // B2
        Console.WriteLine(IsMaxHeap(new[] { 90, 70, 80, 30, 60, 50 }));
        Console.WriteLine(IsMaxHeap(new[] { 50, 40, 30, 45, 10 }));
        Console.WriteLine(IsMaxHeap(new[] { 10, 20, 30 }));
        Console.WriteLine(IsMaxHeap(new int[0]));
        Console.WriteLine();

        // B3
        int[] data = { 5, 12, 3, 20, 8, 15, 1 };

        int[] copy = (int[])data.Clone();
        BuildMaxHeap(copy);
        Console.WriteLine(string.Join(", ", copy));
        Console.WriteLine(IsMaxHeap(copy));

        HeapSort(data);
        Console.WriteLine(string.Join(", ", data));
        Console.WriteLine();

        // B4
        int[] top = TopK(new[] { 42, 17, 99, 63, 8, 75, 31 }, 3);
        Console.WriteLine(string.Join(", ", top));
    }
}
