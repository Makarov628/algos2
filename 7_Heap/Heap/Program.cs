using System;

namespace AlgorithmsDataStructures2
{

    public class Program
    {
        public static void Main(string[] args)
        {
            Heap heap = new Heap()
            {
                /*                          0   1    2    3   4   5    6   7  8   9  10  11 12       */
                HeapArray = new int[15] { 521, 409, 200, 300, 56, 84, 111, 1, 29, 8, 38, 4, 35, 0, 0 },
                lastPointer = 13
            };

            heap.FindElementLessThanKey(300);
        }
    }
}