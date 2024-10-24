using System;
using System.Collections.Generic;
using AlgorithmsDataStructures2;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class RemoveKeyTests
    {
        [Test]
        public void Remove()
        {
            var bst = new aBST(3);
            FillTree(bst.Tree);

            // Если у узла два дочерних узла
            Assert.That(bst.Tree[1], Is.EqualTo(25));
            Assert.That(bst.Tree[9], Is.EqualTo(31));
            bst.Remove(25);
            Assert.That(bst.Tree[1], Is.EqualTo(31));
            Assert.That(bst.Tree[9], Is.EqualTo(null));

            // Если узел - лист
            Assert.That(bst.Tree[14], Is.EqualTo(92));
            bst.Remove(92);
            Assert.That(bst.Tree[14], Is.EqualTo(null));

            // Если у узла один дочерний узел
            Assert.That(bst.Tree[5], Is.EqualTo(62));
            Assert.That(bst.Tree[11], Is.EqualTo(55));
            bst.Remove(62);
            Assert.That(bst.Tree[5], Is.EqualTo(55));
            Assert.That(bst.Tree[11], Is.EqualTo(null));
        }

        private void FillTree(int?[] tree)
        {
            var list = new List<int?>()
            {
                50, 25, 75, 20, 37, 62, 84, null, null, 31, 43, 55, null, null, 92
            };

            for (int i = 0; i < list.Count; i++)
            {
                tree[i] = list[i];
            }
        }

    }
}
