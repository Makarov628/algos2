using System;
using System.Collections.Generic;
using AlgorithmsDataStructures2;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class LCATests
    {
            
        
        private void FillTree(int?[] tree)
        {
            var list = new List<int?>()
            {
                50, 25, 75, null, 37, 62, 84, null, null, 31, 43, 55, null, null, 92
            };

            for (int i = 0; i < list.Count; i++)
            {
                tree[i] = list[i];
            }
        }

        private void FillTreeFull(int?[] tree)
        {
            var list = new List<int?>()
            {
                50, 25, 75, 20, 37, 62, 84, 10, 22, 31, 43, 55, 64, 83, 92
            };

            for (int i = 0; i < list.Count; i++)
            {
                tree[i] = list[i];
            }
        }
    }
}
