using System;
using System.Linq;

namespace AlgorithmsDataStructures2
{
    public static class HeapFunctions
    {

        public static int FindMaxInRange(this Heap heap, uint startIndex, uint endIndex)
        {
            if (heap.lastPointer == 0)
                return -1;

            if (startIndex > endIndex)
                return -1;

            if (endIndex > heap.lastPointer - 1)
                return -1;

            if (startIndex == 0)
                return heap.HeapArray[0];

            var depthOfStart = CalculateDepth(startIndex);
            var depthOfEnd = CalculateDepth(endIndex);

            // Если индексы на одном уровне - ищем
            if (depthOfStart == depthOfEnd)
                return FindMax(heap.HeapArray, startIndex, endIndex);

            // Если начальный индекс это первый индекс на уровне
            // Значит нет смысла искать максимум на уровнях ниже
            if (CalculateLeftBorderIndex(depthOfStart) == startIndex)
                return FindMax(heap.HeapArray, startIndex, CalculateRightBorderIndex(depthOfStart));

            // Если индексы расположены далеко друг друга (больше чем на 1 уровень)
            // То имеет смысл искать максимум всего на один уровень ниже 
            if (depthOfEnd - depthOfStart > 1)
                return FindMax(heap.HeapArray, startIndex, CalculateRightBorderIndex(depthOfStart + 1));

            return FindMax(heap.HeapArray, startIndex, endIndex);
        }

        private static int FindMax(int[] array, uint startIndex, uint endIndex)
        {
            int max = int.MinValue;

            for (uint i = startIndex; i <= endIndex; i++)
                if (array[i] > max)
                    max = array[i];

            return max;
        }

        public static uint CalculateLeftBorderIndex(int depth) =>
            (uint)Math.Pow(2, depth) - 1;

        public static uint CalculateRightBorderIndex(int depth) =>
            (uint)Math.Pow(2, depth + 1) - 1 - 1;

        public static int CalculateDepth(uint index) =>
            (int)Math.Floor(Math.Log2(index + 1));

        public static int FindMax(int[] array)
        {
            int newHeapDepth = (int)Math.Floor(Math.Log2(array.Length));
            var heapForSort = new Heap();
            heapForSort.MakeHeap(new int[0], newHeapDepth);

            foreach (var value in array)
                heapForSort.Add(value);

            return heapForSort.GetMax();
        }

        public static int FindElementLessThanKey(this Heap heap, int key)
        {
            if (heap.HeapArray == null || heap.HeapArray.Length == 0 || heap.lastPointer == 0)
                return -1;

            var clonedHeap = new Heap()
            {
                HeapArray = new int[heap.HeapArray.Length],
                lastPointer = heap.lastPointer
            };
            Array.Copy(heap.HeapArray, clonedHeap.HeapArray, heap.lastPointer);

            while (clonedHeap.lastPointer > 0)
            {
                var element = clonedHeap.GetMax();
                if (element < key)
                    return element;
            }

            return -1;
        }

    
        public static Heap MergeHeaps(params Heap[] heaps)
        {
            int elementsTotalCount = heaps.Sum(heap => heap.lastPointer);
            int newHeapDepth = (int)Math.Floor(Math.Log2(elementsTotalCount));

            var newHeap = new Heap();
            newHeap.MakeHeap(new int[0], newHeapDepth);
            
            int iterationsCount = heaps.Max(heap => heap.lastPointer);

            IOrderedEnumerable<int> sortedElements = Enumerable.Range(0, iterationsCount)
                .SelectMany(_ => heaps
                    .Select(heap => heap.GetMax())
                    .Where(element => element != -1))
                .OrderByDescending(e => e);

            foreach (var element in sortedElements) 
                newHeap.Add(element);
        
            return newHeap;
        }

        public static Heap MergeHeapUsingArray(this Heap first, Heap second)
        {
            int totalCount = first.lastPointer + second.lastPointer;
            int depth = (int)Math.Floor(Math.Log2(totalCount));
            int size = (int)(Math.Pow(2, depth + 1) - 1);

            if (first.HeapArray.Length < size)
            {
                int[] newHeapArray = new int[size];
                Array.Copy(first.HeapArray, newHeapArray, first.lastPointer);
                first.HeapArray = newHeapArray;
            }

            while (second.lastPointer > 0)
                first.Add(second.GetMax());

            return first;
        }
    }
}