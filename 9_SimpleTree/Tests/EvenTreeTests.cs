using System;
using System.Collections.Generic;
using AlgorithmsDataStructures2;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class EvenTreeTests
    {
        private EvenTree<int> tree;

        [SetUp]
        public void SetUp()
        {
            tree = new EvenTree<int>(new EvenTreeNode<int>(1, null));

            tree.AddChild(tree.Root, new EvenTreeNode<int>(2, null));
            tree.AddChild(tree.Root.Children[0], new EvenTreeNode<int>(5, null));
            tree.AddChild(tree.Root.Children[0], new EvenTreeNode<int>(7, null));

            tree.AddChild(tree.Root, new EvenTreeNode<int>(3, null));
            tree.AddChild(tree.Root.Children[1], new EvenTreeNode<int>(4, null));

            tree.AddChild(tree.Root, new EvenTreeNode<int>(6, null));
            tree.AddChild(tree.Root.Children[2], new EvenTreeNode<int>(8, null));
            tree.AddChild(tree.Root.Children[2].Children[0], new EvenTreeNode<int>(9, null));
            tree.AddChild(tree.Root.Children[2].Children[0], new EvenTreeNode<int>(10, null));
        }

        [Test]
        public void TestRegressionEvenTrees()
        {
            var result = tree.EvenTrees();

            var expected = new List<int> { 1, 3, 1, 6 };
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void TestRegressionCountEvenSubtrees()
        {
            int count = tree.CountEvenSubtrees();
            Assert.That(count, Is.EqualTo(3));
        }

        [Test]
        public void TestBinaryEven()
        {
            // Удаляем узел с NodeValue == 3 (tree.Root.Children[1])
            tree.DeleteNode(tree.Root.Children[1]);

            tree.BalanceBinaryEven();

            Assert.That(tree.Root.NodeValue, Is.EqualTo(7));

            Assert.That(tree.Root.Children[0].NodeValue, Is.EqualTo(5));
            Assert.That(tree.Root.Children[1].NodeValue, Is.EqualTo(9));

            Assert.That(tree.Root.Children[0].Children[0].NodeValue, Is.EqualTo(2));
            Assert.That(tree.Root.Children[0].Children[1].NodeValue, Is.EqualTo(6));

            Assert.That(tree.Root.Children[0].Children[0].Children[0].NodeValue, Is.EqualTo(1));
            Assert.That(tree.Root.Children[0].Children[0].Children.Count, Is.EqualTo(1));

            Assert.That(tree.Root.Children[0].Children[1].Children, Is.Null.Or.Empty);

            Assert.That(tree.Root.Children[1].Children[0].NodeValue, Is.EqualTo(8));
            Assert.That(tree.Root.Children[1].Children[0].Children, Is.Null.Or.Empty);

            Assert.That(tree.Root.Children[1].Children[1].NodeValue, Is.EqualTo(10));
            Assert.That(tree.Root.Children[1].Children[1].Children, Is.Null.Or.Empty);
        }
    }
}