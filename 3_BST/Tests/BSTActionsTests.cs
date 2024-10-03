using System;
using System.Collections.Generic;
using System.Linq;
using AlgorithmsDataStructures2;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class BSTActionsTests
    {
        [Test]
        public void InvertTree()
        {
            var root = new BSTNode<int>(1, 1);
            var node2 = new BSTNode<int>(2, 2);
            var node3 = new BSTNode<int>(3, 3);
            var node4 = new BSTNode<int>(4, 4);
            var node5 = new BSTNode<int>(5, 5);
            var node6 = new BSTNode<int>(6, 6);
            var node7 = new BSTNode<int>(7, 7);

            root.AddLeft(node2);
            root.AddRight(node3);
            root.LeftChild.AddLeft(node4);
            root.LeftChild.AddRight(node5);
            root.RightChild.AddLeft(node6);
            root.RightChild.AddRight(node7);

            Assert.That(root.LeftChild, Is.SameAs(node2));
            Assert.That(root.RightChild, Is.SameAs(node3));
            Assert.That(root.LeftChild.LeftChild, Is.SameAs(node4));
            Assert.That(root.LeftChild.RightChild, Is.SameAs(node5));
            Assert.That(root.RightChild.LeftChild, Is.SameAs(node6));
            Assert.That(root.RightChild.RightChild, Is.SameAs(node7));

            var invertedRoot = BSTActions.InvertTree(root);

            Assert.That(invertedRoot.RightChild, Is.SameAs(node2));
            Assert.That(invertedRoot.LeftChild, Is.SameAs(node3));
            Assert.That(invertedRoot.RightChild.RightChild, Is.SameAs(node4));
            Assert.That(invertedRoot.RightChild.LeftChild, Is.SameAs(node5));
            Assert.That(invertedRoot.LeftChild.RightChild, Is.SameAs(node6));
            Assert.That(invertedRoot.LeftChild.LeftChild, Is.SameAs(node7));
        }


        [Test]
        public void FindLevelWithMaxSumDFS()
        {
            var root = new BSTNode<int>(1, 1);
            var node2 = new BSTNode<int>(2, 10000);
            var node3 = new BSTNode<int>(3, 3);
            var node4 = new BSTNode<int>(4, 4);
            var node5 = new BSTNode<int>(5, 5);
            var node6 = new BSTNode<int>(6, 6);
            var node7 = new BSTNode<int>(7, 7);

            root.AddLeft(node2);
            root.AddRight(node3);
            root.LeftChild.AddLeft(node4);
            root.LeftChild.AddRight(node5);
            root.RightChild.AddLeft(node6);
            root.RightChild.AddRight(node7);

            Assert.That(BSTActions.FindLevelWithMaxSumDFS(root), Is.EqualTo(2));
        }

        [Test]
        public void BuildTree()
        {
            var root = new BSTNode<int>(1, 1);
            var node2 = new BSTNode<int>(2, 2);
            var node3 = new BSTNode<int>(3, 3);
            var node4 = new BSTNode<int>(4, 4);
            var node5 = new BSTNode<int>(5, 5);
            var node6 = new BSTNode<int>(6, 6);
            var node7 = new BSTNode<int>(7, 7);

            root.AddLeft(node2);
            root.AddRight(node3);
            root.LeftChild.AddLeft(node4);
            root.LeftChild.AddRight(node5);
            root.RightChild.AddLeft(node6);
            root.RightChild.AddRight(node7);

            Assert.That(root.LeftChild, Is.SameAs(node2));
            Assert.That(root.RightChild, Is.SameAs(node3));
            Assert.That(root.LeftChild.LeftChild, Is.SameAs(node4));
            Assert.That(root.LeftChild.RightChild, Is.SameAs(node5));
            Assert.That(root.RightChild.LeftChild, Is.SameAs(node6));
            Assert.That(root.RightChild.RightChild, Is.SameAs(node7));

            var tree = new BST<int>(root);
            var preorder = tree.DeepAllNodes(2);
            var inorder = tree.DeepAllNodes(0);

            CollectionAssert.AreEqual(preorder, new List<BSTNode>() { root, node2, node4, node5, node3, node6, node7 });
            CollectionAssert.AreEqual(inorder, new List<BSTNode>() { node4, node2, node5, root, node6, node3, node7 });

            var reconstructedRoot = BSTActions.BuildTree(
                preorder.Select(p => p.NodeKey).ToArray(),
                inorder.Select(i => i.NodeKey).ToArray());

            Assert.That(reconstructedRoot.LeftChild.NodeValue, Is.EqualTo(node2.NodeValue));
            Assert.That(reconstructedRoot.RightChild.NodeValue, Is.EqualTo(node3.NodeValue));
            Assert.That(reconstructedRoot.LeftChild.LeftChild.NodeValue, Is.EqualTo(node4.NodeValue));
            Assert.That(reconstructedRoot.LeftChild.RightChild.NodeValue, Is.EqualTo(node5.NodeValue));
            Assert.That(reconstructedRoot.RightChild.LeftChild.NodeValue, Is.EqualTo(node6.NodeValue));
            Assert.That(reconstructedRoot.RightChild.RightChild.NodeValue, Is.EqualTo(node7.NodeValue));
        }
    }
}
