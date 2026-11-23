namespace hw8;

using System;
using System.Collections.Generic;

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

public class MinPriorityQueue
{
    private readonly List<int> heap = new List<int>();

    public int Count => heap.Count;
    public int IsEmpty => heap.Count == 0;

    private static int Parent(int i)
    {
        return (i - 1) / 2;
    }

    public void Swap(int a, int b)
    {
        int temp = heap[a];
        heap[a] = heap[b];
        heap[b] = temp;
    }

    // O(log n): add at the end, then bubble up while smaller than parent
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

    // O(1)
    public int Peek()
    {
        if (IsEmpty)
            throw new InvalidOperationException("Empty queue");
        return heap[0];
    }

    // O(log n): move last item to the top, then sink it toward the smaller child
    public int RemoveMin()
    {
        if (IsEmpty)
            throw new InvalidOperationException("Empty queque");
        int min = heap[0];
        heap[0] = heap[heap.Count - 1];
        heap.RemoveAt(heap.Count - 1);
        if (heap.Count > 0)
            SinkDown(0);
        return min;
    }

    private void SinkDown(int index)
    {
        while (true) { }
    }
}

class Program
{
    static void Main(string[] args)
    {
        MinPriorityQueue pq = new MinPriorityQueue();

        pq.Insert(15);
        pq.Insert(40);
        pq.Insert(25);
        pq.Insert(50);
        pq.Insert(10);
        pq.Insert(45);

        while (!pq.IsEmpty)
        {
            Console.Write(pq.RemoveMin());

            if (!pq.IsEmpty)
                Console.Write(", ");
        }

        Console.WriteLine();

        try
        {
            pq.RemoveMin();
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
