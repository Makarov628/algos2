using System;
using System.Collections.Generic;
using AlgorithmsDataStructures2;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class HeapFuntionsTests
    {
        [Test]
        public void FindMaxInRange()
        {
            Heap heap = new Heap()
            {
                /*                          0   1    2    3   4   5    6   7  8   9  10  11 12       */
                HeapArray = new int[15] { 521, 409, 200, 300, 56, 84, 111, 1, 29, 8, 38, 4, 35, 0, 0 },
                lastPointer = 13
            };

            uint startIndex = 8;
            uint endIndex = 12;

            // Индексы на одном уровне
            Assert.That(HeapFunctions.CalculateDepth(startIndex), Is.EqualTo(HeapFunctions.CalculateDepth(endIndex)));
            Assert.That(heap.FindMaxInRange(startIndex, endIndex), Is.EqualTo(38));

            startIndex = 2;
            endIndex = 12;

            // Индексы на расстоянии друг от друга на больше чем на 1 уровень
            Assert.That(HeapFunctions.CalculateDepth(endIndex) - HeapFunctions.CalculateDepth(startIndex), Is.EqualTo(2));
            Assert.That(heap.FindMaxInRange(startIndex, endIndex), Is.EqualTo(300));
            // Отсекаем уровни ниже
            Assert.That(HeapFunctions.CalculateRightBorderIndex(HeapFunctions.CalculateDepth(startIndex) + 1), Is.EqualTo(6));
            Assert.That(heap.FindMaxInRange(startIndex, endIndex), Is.EqualTo(heap.FindMaxInRange(startIndex, 6)));

            startIndex = 1;
            endIndex = 12;

            // Если стартовый индекс совпадает с начальным индексом на уровне
            // Проверяем только один уровень
            Assert.That(HeapFunctions.CalculateLeftBorderIndex(HeapFunctions.CalculateDepth(startIndex)), Is.EqualTo(startIndex));
            Assert.That(HeapFunctions.CalculateRightBorderIndex(HeapFunctions.CalculateDepth(startIndex)), Is.EqualTo(2));
            Assert.That(heap.FindMaxInRange(startIndex, endIndex), Is.EqualTo(heap.FindMaxInRange(startIndex, 2)));

            startIndex = 2;
            endIndex = 5;

            // Индексы на расстоянии друг от друга на 1 уровень
            Assert.That(HeapFunctions.CalculateDepth(endIndex) - HeapFunctions.CalculateDepth(startIndex), Is.EqualTo(1));
            Assert.That(heap.FindMaxInRange(startIndex, endIndex), Is.EqualTo(300));
        }

        [Test]
        public void FindElementLessThanKey()
        {
            Heap heap = new Heap()
            {
                /*                          0   1    2    3   4   5    6   7  8   9  10  11 12       */
                HeapArray = new int[15] { 521, 409, 200, 300, 56, 84, 111, 1, 29, 8, 38, 4, 35, 0, 0 },
                lastPointer = 13
            };

            Assert.That(heap.FindElementLessThanKey(522), Is.EqualTo(521));
            Assert.That(heap.FindElementLessThanKey(521), Is.EqualTo(409));
            Assert.That(heap.FindElementLessThanKey(409), Is.EqualTo(300));
            Assert.That(heap.FindElementLessThanKey(300), Is.EqualTo(200));
            Assert.That(heap.FindElementLessThanKey(200), Is.EqualTo(111));
            Assert.That(heap.FindElementLessThanKey(111), Is.EqualTo(84));
            Assert.That(heap.FindElementLessThanKey(84), Is.EqualTo(56));
            Assert.That(heap.FindElementLessThanKey(56), Is.EqualTo(38));
            Assert.That(heap.FindElementLessThanKey(38), Is.EqualTo(35));
            Assert.That(heap.FindElementLessThanKey(35), Is.EqualTo(29));
            Assert.That(heap.FindElementLessThanKey(29), Is.EqualTo(8));
            Assert.That(heap.FindElementLessThanKey(8), Is.EqualTo(4));
            Assert.That(heap.FindElementLessThanKey(4), Is.EqualTo(1));
            Assert.That(heap.FindElementLessThanKey(1), Is.EqualTo(-1));
        }

        [Test]
        public void MergeHeaps()
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

            var orderedList = new int[] { 56, 35, 26, 25, 21, 17, 12, 10, 8 };
            foreach (var item in orderedList)
            {
                Assert.That(newMergedHeap.GetMax(), Is.EqualTo(item));
            }
            Assert.That(firstHeap.GetMax(), Is.EqualTo(-1));
            Assert.That(secondHeap.GetMax(), Is.EqualTo(-1));

        }

    }
}
