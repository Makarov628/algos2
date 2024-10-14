using System;
using System.Collections.Generic;
using AlgorithmsDataStructures2;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class BalancedBSTTests
    {
        [Test]
        public void GenerateBBSTArray_Test()
        {
            int[] a = { 5, 4, 9, 12, 13, 6, 7, 1, 8, 15, 2, 10, 3, 14, 11 };
            int[] aBST = { 8, 4, 12, 2, 6, 10, 14, 1, 3, 5, 7, 9, 11, 13, 15 };
            CollectionAssert.AreEqual(BalancedBST.GenerateBBSTArray(a), aBST);
        }

        [Test]
        public void GenerateBBSTArray_Empty()
        {
            int[] a = new int[0];
            int[] aBST = new int[0];
            CollectionAssert.AreEqual(BalancedBST.GenerateBBSTArray(a), aBST);
        }
    }
}
