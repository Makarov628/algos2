using System;
using System.Collections.Generic;
using AlgorithmsDataStructures2;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class BSTTests
    {
        #region DeepAllNodes

        [Test]
        public void InOrder_Empty()
        {
            var tree = new BST<int>(null);

            var res = tree.DeepAllNodes(0);
            
            Assert.That(res.Count, Is.EqualTo(0));
        }

        [Test]
        public void InOrder_OnlyOne()
        {
            var root = new BSTNode<int>(8, 8, null);
            var tree = new BST<int>(root);

            var res = tree.DeepAllNodes(0);
            
            Assert.That(res.Count, Is.EqualTo(1));
            Assert.That(res[0].NodeKey, Is.EqualTo(8));
        }
        
        [Test]
        public void InOrder_Full1()
        {
            // Arrange
            var root = new BSTNode<int>(8, 8, null);
            var tree = new BST<int>(root);
            var child4 = new BSTNode<int>(4, 4, root);
            var child12 = new BSTNode<int>(12, 12, root);
            var child2 = new BSTNode<int>(2, 2, child4);
            var child6 = new BSTNode<int>(6, 6, child4);
            var child1 = new BSTNode<int>(1, 1, child2);
            var child3 = new BSTNode<int>(3, 3, child2);
            var child5 = new BSTNode<int>(5, 5, child6);
            var child7 = new BSTNode<int>(7, 7, child6);
            var child10 = new BSTNode<int>(10, 10, child12);
            var child14 = new BSTNode<int>(14, 14, child12);
            var child9 = new BSTNode<int>(9, 9, child10);
            var child11 = new BSTNode<int>(11, 11, child10);
            var child13 = new BSTNode<int>(13, 13, child14);
            var child15 = new BSTNode<int>(15, 15, child14);
            root.LeftChild = child4;
            root.RightChild = child12;
            child4.LeftChild = child2;
            child4.RightChild = child6;
            child2.LeftChild = child1;
            child2.RightChild = child3;
            child6.LeftChild = child5;
            child6.RightChild = child7;
            child12.LeftChild = child10;
            child12.RightChild = child14;
            child10.LeftChild = child9;
            child10.RightChild = child11;
            child14.LeftChild = child13;
            child14.RightChild = child15;

            var res = tree.DeepAllNodes(0);

            var expected = new List<BSTNode<int>>()
            {
                child1, child2, child3, child4, child5, child6, child7,
                root, child9, child10, child11, child12, child13, child14, child15
            };
            
            Assert.That(res.Count, Is.EqualTo(15));
            CollectionAssert.AreEqual(expected, res);
        }
        
        [Test]
        public void PostOrder_Empty()
        {
            var tree = new BST<int>(null);

            var res = tree.DeepAllNodes(1);
            
            Assert.That(res.Count, Is.EqualTo(0));
        }

        [Test]
        public void PostOrder_OnlyOne()
        {
            var root = new BSTNode<int>(8, 8, null);
            var tree = new BST<int>(root);

            var res = tree.DeepAllNodes(1);
            
            Assert.That(res.Count, Is.EqualTo(1));
            Assert.That(res[0].NodeKey, Is.EqualTo(8));
        }
        
        [Test]
        public void PostOrder_Full1()
        {
            // Arrange
            var root = new BSTNode<int>(8, 8, null);
            var tree = new BST<int>(root);
            var child4 = new BSTNode<int>(4, 4, root);
            var child12 = new BSTNode<int>(12, 12, root);
            var child2 = new BSTNode<int>(2, 2, child4);
            var child6 = new BSTNode<int>(6, 6, child4);
            var child1 = new BSTNode<int>(1, 1, child2);
            var child3 = new BSTNode<int>(3, 3, child2);
            var child5 = new BSTNode<int>(5, 5, child6);
            var child7 = new BSTNode<int>(7, 7, child6);
            var child10 = new BSTNode<int>(10, 10, child12);
            var child14 = new BSTNode<int>(14, 14, child12);
            var child9 = new BSTNode<int>(9, 9, child10);
            var child11 = new BSTNode<int>(11, 11, child10);
            var child13 = new BSTNode<int>(13, 13, child14);
            var child15 = new BSTNode<int>(15, 15, child14);
            root.LeftChild = child4;
            root.RightChild = child12;
            child4.LeftChild = child2;
            child4.RightChild = child6;
            child2.LeftChild = child1;
            child2.RightChild = child3;
            child6.LeftChild = child5;
            child6.RightChild = child7;
            child12.LeftChild = child10;
            child12.RightChild = child14;
            child10.LeftChild = child9;
            child10.RightChild = child11;
            child14.LeftChild = child13;
            child14.RightChild = child15;

            var res = tree.DeepAllNodes(1);

            var expected = new List<BSTNode<int>>()
            {
                child1, child3, child2, child5, child7, child6, child4,
                child9, child11, child10, child13, child15, child14, child12, root
            };
            
            Assert.That(res.Count, Is.EqualTo(15));
            CollectionAssert.AreEqual(expected, res);
        }
        
        [Test]
        public void PreOrder_Empty()
        {
            var tree = new BST<int>(null);

            var res = tree.DeepAllNodes(2);
            
            Assert.That(res.Count, Is.EqualTo(0));
        }

        [Test]
        public void PreOrder_OnlyOne()
        {
            var root = new BSTNode<int>(8, 8, null);
            var tree = new BST<int>(root);

            var res = tree.DeepAllNodes(2);
            
            Assert.That(res.Count, Is.EqualTo(1));
            Assert.That(res[0].NodeKey, Is.EqualTo(8));
        }
        
        [Test]
        public void PreOrder_Full1()
        {
            // Arrange
            var root = new BSTNode<int>(8, 8, null);
            var tree = new BST<int>(root);
            var child4 = new BSTNode<int>(4, 4, root);
            var child12 = new BSTNode<int>(12, 12, root);
            var child2 = new BSTNode<int>(2, 2, child4);
            var child6 = new BSTNode<int>(6, 6, child4);
            var child1 = new BSTNode<int>(1, 1, child2);
            var child3 = new BSTNode<int>(3, 3, child2);
            var child5 = new BSTNode<int>(5, 5, child6);
            var child7 = new BSTNode<int>(7, 7, child6);
            var child10 = new BSTNode<int>(10, 10, child12);
            var child14 = new BSTNode<int>(14, 14, child12);
            var child9 = new BSTNode<int>(9, 9, child10);
            var child11 = new BSTNode<int>(11, 11, child10);
            var child13 = new BSTNode<int>(13, 13, child14);
            var child15 = new BSTNode<int>(15, 15, child14);
            root.LeftChild = child4;
            root.RightChild = child12;
            child4.LeftChild = child2;
            child4.RightChild = child6;
            child2.LeftChild = child1;
            child2.RightChild = child3;
            child6.LeftChild = child5;
            child6.RightChild = child7;
            child12.LeftChild = child10;
            child12.RightChild = child14;
            child10.LeftChild = child9;
            child10.RightChild = child11;
            child14.LeftChild = child13;
            child14.RightChild = child15;

            var res = tree.DeepAllNodes(2);

            var expected = new List<BSTNode<int>>()
            {
                root, child4, child2, child1, child3, child6, child5, child7,
                child12, child10, child9, child11, child14, child13, child15
            };
            
            Assert.That(res.Count, Is.EqualTo(15));
            CollectionAssert.AreEqual(expected, res);
        }
        
        #endregion
        
        #region WideAllNodes

        [Test]
        public void Wide_Full1()
        {
            // Arrange
            var root = new BSTNode<int>(8, 8, null);
            var tree = new BST<int>(root);
            var child4 = new BSTNode<int>(4, 4, root);
            var child12 = new BSTNode<int>(12, 12, root);
            var child2 = new BSTNode<int>(2, 2, child4);
            var child6 = new BSTNode<int>(6, 6, child4);
            var child1 = new BSTNode<int>(1, 1, child2);
            var child3 = new BSTNode<int>(3, 3, child2);
            var child5 = new BSTNode<int>(5, 5, child6);
            var child7 = new BSTNode<int>(7, 7, child6);
            var child10 = new BSTNode<int>(10, 10, child12);
            var child14 = new BSTNode<int>(14, 14, child12);
            var child9 = new BSTNode<int>(9, 9, child10);
            var child11 = new BSTNode<int>(11, 11, child10);
            var child13 = new BSTNode<int>(13, 13, child14);
            var child15 = new BSTNode<int>(15, 15, child14);
            root.LeftChild = child4;
            root.RightChild = child12;
            child4.LeftChild = child2;
            child4.RightChild = child6;
            child2.LeftChild = child1;
            child2.RightChild = child3;
            child6.LeftChild = child5;
            child6.RightChild = child7;
            child12.LeftChild = child10;
            child12.RightChild = child14;
            child10.LeftChild = child9;
            child10.RightChild = child11;
            child14.LeftChild = child13;
            child14.RightChild = child15;

            var res = tree.WideAllNodes();

            var expected = new List<BSTNode<int>>()
            {
                root, child4, child12, child2, child6, child10, child14, child1, child3, child5, child7, child9,
                child11, child13, child15
            };
            
            Assert.That(res.Count, Is.EqualTo(15));
            CollectionAssert.AreEqual(expected, res);
        }

        #endregion
    }
}
