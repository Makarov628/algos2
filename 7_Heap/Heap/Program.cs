using System;

namespace AlgorithmsDataStructures2
{

    public class Program
    {
        public static void Main(string[] args)
        {
            Heap firstHeap = new Heap()
            {
                HeapArray = new int[7] { 56, 25, 10, 17, 0, 0, 0 },
                lastPointer = 4
            };

            Heap secondHeap = new Heap()
            {
                HeapArray = new int[7] { 35, 21, 26, 8, 12, 0, 0 },
                lastPointer = 5
            };

            var newMergedHeap = HeapFunctions.MergeHeaps(firstHeap, secondHeap);
        }
    }
}