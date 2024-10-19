using System;
using System.Collections.Generic;
using AlgorithmsDataStructures2;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class LCATests
    {


        [Test]
        public void LCA()
        {
            /*
                               50
                              / \
                             /   \
                            /     \
                           /       \
                          /         \
                         /           \
                        /             \
                       /               \
                      /                 \
                     25                  75
                    / \                 / \
                   /   \               /   \
                  /     \             /     \
                 /       \           /       \
                20       37         62        84
                / \      / \       / \       / \
               /   \    /   \     /   \     /   \
              10    22 31    43   55   64  83    92
            */

            var tree = new aBST(3);
            FillTreeFull(tree.Tree);

            Assert.That(tree.LowestCommonAncestor(25, 75), Is.EqualTo(50));
            Assert.That(tree.LowestCommonAncestor(20, 84), Is.EqualTo(50));
            Assert.That(tree.LowestCommonAncestor(10, 92), Is.EqualTo(50));
            Assert.That(tree.LowestCommonAncestor(43, 55), Is.EqualTo(50));

            Assert.That(tree.LowestCommonAncestor(20, 43), Is.EqualTo(25));
            Assert.That(tree.LowestCommonAncestor(10, 37), Is.EqualTo(25));

            Assert.That(tree.LowestCommonAncestor(92, 62), Is.EqualTo(75));

            Assert.That(tree.LowestCommonAncestor(10000, 300000), Is.EqualTo(null));
        }


        [Test]
        public void LCA_Index()
        {
            /*
                               0
                              / \
                             /   \
                            /     \
                           /       \
                          /         \
                         /           \
                        /             \
                       /               \
                      /                 \
                     1                   2
                    / \                 / \
                   /   \               /   \
                  /     \             /     \
                 /       \           /       \
                3         4         5         6
               / \       / \       / \       / \
              /   \     /   \     /   \     /   \
             7     8   9    10   11   12  13    14
            */

            var tree = new aBST(3);
            FillTreeFull(tree.Tree);

            Assert.That(tree.LowestCommonAncestorIndex(1, 2), Is.EqualTo(0));
            Assert.That(tree.LowestCommonAncestorIndex(3, 6), Is.EqualTo(0));
            Assert.That(tree.LowestCommonAncestorIndex(10, 11), Is.EqualTo(0));

            Assert.That(tree.LowestCommonAncestorIndex(7, 10), Is.EqualTo(1));
            Assert.That(tree.LowestCommonAncestorIndex(3, 4), Is.EqualTo(1));

            Assert.That(tree.LowestCommonAncestorIndex(11, 14), Is.EqualTo(2));

            Assert.That(tree.LowestCommonAncestorIndex(22, 23), Is.EqualTo(null));
        }

        [Test]
        public void WideAllNodes()
        {
            var tree = new aBST(3);
            FillTree(tree.Tree);

            CollectionAssert.AreEqual(new List<int>() { 50, 25, 75, 37, 62, 84, 31, 43, 55, 92 }, tree.WideAllNodes());
        }

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
