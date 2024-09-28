using System;
using System.Collections.Generic;
using AlgorithmsDataStructures2;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class SimpleTreeTests
    {
        [Test]
        public void AddEmptyTreeShouldAddChild()
        {
            var root = new SimpleTreeNode<int>(5, null);
            var tree = new SimpleTree<int>(root);

            tree.AddChild(tree.Root, new SimpleTreeNode<int>(6, tree.Root));
            Assert.NotNull(tree.Root);

            Assert.That(tree.Root.NodeValue, Is.EqualTo(5));
            Assert.NotNull(tree.Root.Children);
            Assert.That(tree.Root.Children.Count, Is.EqualTo(1));

            var children = tree.Root.Children[0];
            Assert.NotNull(children);
            Assert.That(children.NodeValue, Is.EqualTo(6));
            Assert.Null(children.Children);

            Assert.That(tree.Count(), Is.EqualTo(2));
            Assert.That(tree.LeafCount(), Is.EqualTo(1));
        }


        [Test]
        public void AddFourNodesShouldAddChild()
        {
            var root = new SimpleTreeNode<int>(5, null);
            var tree = new SimpleTree<int>(root);

            var addNode = new SimpleTreeNode<int>(6, tree.Root);
            tree.AddChild(tree.Root, addNode);

            tree.AddChild(addNode, new SimpleTreeNode<int>(7, addNode));
            tree.AddChild(addNode, new SimpleTreeNode<int>(8, addNode));

            Assert.That(tree.Root.NodeValue, Is.EqualTo(5));
            Assert.NotNull(tree.Root.Children);

            var children = tree.Root.Children[0];
            Assert.NotNull(children);
            Assert.That(children.NodeValue, Is.EqualTo(6));
            Assert.NotNull(children.Children);
            Assert.That(children.Children.Count, Is.EqualTo(2));
            Assert.That(children.Children[0].NodeValue, Is.EqualTo(7));
            Assert.That(children.Children[1].NodeValue, Is.EqualTo(8));

            Assert.That(tree.Count(), Is.EqualTo(4));
            Assert.That(tree.LeafCount(), Is.EqualTo(2));
        }

        [Test]
        public void AddSevenNodesShouldAddChild()
        {
            var root = new SimpleTreeNode<int>(9, null);
            var tree = new SimpleTree<int>(root);

            var node1 = new SimpleTreeNode<int>(4, tree.Root);
            tree.AddChild(tree.Root, node1);

            var node2 = new SimpleTreeNode<int>(17, tree.Root);
            tree.AddChild(tree.Root, node2);

            tree.AddChild(node2, new SimpleTreeNode<int>(3, node2));
            tree.AddChild(node2, new SimpleTreeNode<int>(6, node2));

            tree.AddChild(node1, new SimpleTreeNode<int>(17, node1));
            tree.AddChild(node1, new SimpleTreeNode<int>(22, node1));

            Assert.That(tree.Root.NodeValue, Is.EqualTo(9));
            var treeCh = tree.Root.Children;
            Assert.NotNull(treeCh);
            Assert.That(treeCh.Count, Is.EqualTo(2));
            Assert.That(treeCh[0].NodeValue, Is.EqualTo(4));
            Assert.That(treeCh[1].NodeValue, Is.EqualTo(17));

            var ch1 = treeCh[0];
            Assert.NotNull(ch1.Children);
            Assert.That(ch1.Children.Count, Is.EqualTo(2));
            Assert.That(ch1.Children[0].NodeValue, Is.EqualTo(17));
            Assert.That(ch1.Children[1].NodeValue, Is.EqualTo(22));

            var ch2 = treeCh[1];
            Assert.NotNull(ch2.Children);
            Assert.That(ch2.Children.Count, Is.EqualTo(2));
            Assert.That(ch2.Children[0].NodeValue, Is.EqualTo(3));
            Assert.That(ch2.Children[1].NodeValue, Is.EqualTo(6));

            Assert.That(tree.Count(), Is.EqualTo(7));
            Assert.That(tree.LeafCount(), Is.EqualTo(4));
        }

        [Test]
        public void DeleteNodeNoChildrenShouldBeCorrectTree()
        {
            var root = new SimpleTreeNode<int>(5, null);
            var tree = new SimpleTree<int>(root);
            tree.AddChild(tree.Root, new SimpleTreeNode<int>(6, tree.Root));

            tree.DeleteNode(tree.Root.Children[0]);

            Assert.Null(tree.Root.Children);
            Assert.That(tree.Count(), Is.EqualTo(1));
            Assert.That(tree.LeafCount(), Is.EqualTo(1));
        }

        [Test]
        public void DeleteNode_WithChildren_ShouldBeCorrectTree()
        {
            var root = new SimpleTreeNode<int>(5, null);
            var tree = new SimpleTree<int>(root);

            var addNode = new SimpleTreeNode<int>(6, tree.Root);
            tree.AddChild(addNode, new SimpleTreeNode<int>(5, addNode));
            tree.AddChild(addNode, new SimpleTreeNode<int>(6, addNode));

            tree.AddChild(tree.Root, addNode);

            tree.DeleteNode(tree.Root.Children[0]);

            Assert.Null(tree.Root.Children);
            Assert.That(tree.Count(), Is.EqualTo(1));
            Assert.That(tree.LeafCount(), Is.EqualTo(1));
        }

        [Test]
        public void DeleteNodeWithChildrenAndSubTreeShouldBeCorrectTree()
        {
            //Arrange
            var root = new SimpleTreeNode<int>(5, null);
            var tree = new SimpleTree<int>(root);

            var addNode = new SimpleTreeNode<int>(6, tree.Root);
            tree.AddChild(addNode, new SimpleTreeNode<int>(5, addNode));

            var subNode = new SimpleTreeNode<int>(6, addNode);
            tree.AddChild(subNode, new SimpleTreeNode<int>(8, subNode));
            tree.AddChild(subNode, new SimpleTreeNode<int>(9, subNode));

            tree.AddChild(addNode, subNode);
            tree.AddChild(tree.Root, addNode);

            tree.DeleteNode(tree.Root.Children[0]);

            Assert.Null(tree.Root.Children);
            Assert.That(tree.Count(), Is.EqualTo(1));
            Assert.That(tree.LeafCount(), Is.EqualTo(1));
        }

        [Test]
        public void DeleteNodeSubTreeShouldBeCorrectTree()
        {
            var root = new SimpleTreeNode<int>(5, null);
            var tree = new SimpleTree<int>(root);

            var addNode = new SimpleTreeNode<int>(6, tree.Root);
            tree.AddChild(addNode, new SimpleTreeNode<int>(7, addNode));

            var subNode = new SimpleTreeNode<int>(10, addNode);
            tree.AddChild(subNode, new SimpleTreeNode<int>(8, subNode));
            tree.AddChild(subNode, new SimpleTreeNode<int>(9, subNode));

            tree.AddChild(addNode, subNode);
            tree.AddChild(tree.Root, addNode);

            tree.DeleteNode(subNode);

            Assert.That(tree.Root.Children.Count, Is.EqualTo(1));

            var rootCh = tree.Root.Children[0];
            Assert.That(rootCh.NodeValue, Is.EqualTo(6));
            Assert.That(rootCh.Children.Count, Is.EqualTo(1));
            Assert.That(rootCh.Children[0].NodeValue, Is.EqualTo(7));

            Assert.That(tree.Count(), Is.EqualTo(3));
            Assert.That(tree.LeafCount(), Is.EqualTo(1));
        }

        [Test]
        public void DeleteNodeDeleteThreeTimesFromRootShouldBeCorrectTree()
        {
            var root = new SimpleTreeNode<int>(5, null);
            var tree = new SimpleTree<int>(root);

            tree.AddChild(tree.Root, new SimpleTreeNode<int>(8, tree.Root));
            tree.AddChild(tree.Root, new SimpleTreeNode<int>(9, tree.Root));
            tree.AddChild(tree.Root, new SimpleTreeNode<int>(10, tree.Root));

            tree.DeleteNode(tree.Root.Children[0]);
            Assert.That(tree.Root.Children.Count, Is.EqualTo(2));
            Assert.That(tree.Count(), Is.EqualTo(3));
            Assert.That(tree.LeafCount(), Is.EqualTo(2));

            tree.DeleteNode(tree.Root.Children[0]);
            Assert.That(tree.Root.Children.Count, Is.EqualTo(1));
            Assert.That(tree.Count(), Is.EqualTo(2));
            Assert.That(tree.LeafCount(), Is.EqualTo(1));

            tree.DeleteNode(tree.Root.Children[0]);
            Assert.Null(tree.Root.Children);
            Assert.That(tree.Count(), Is.EqualTo(1));
            Assert.That(tree.LeafCount(), Is.EqualTo(1));
        }

        [Test]
        public void FillLevelsEmptyTreeShouldReturnRoot()
        {
            var root = new SimpleTreeNode<int>(5, null);
            var tree = new SimpleTree<int>(root);

            AssetNodeLevel(tree.GetAllNodes(), 1, new List<(int, int)> { (5, 1) });
        }

        [Test]
        public void FillLevelsTwoEqualNodesShouldReturnList()
        {
            var root = new SimpleTreeNode<int>(5, null);
            var tree = new SimpleTree<int>(root);

            tree.AddChild(tree.Root, new SimpleTreeNode<int>(2, tree.Root));
            tree.AddChild(tree.Root, new SimpleTreeNode<int>(2, tree.Root));

            AssetNodeLevel(tree.GetAllNodes(), 3, new List<(int, int)> { (5, 1), (2, 2), (2, 2) });
        }

        [Test]
        public void FillLevelsTwoNodesShouldReturnList()
        {
            var root = new SimpleTreeNode<int>(5, null);
            var tree = new SimpleTree<int>(root);

            tree.AddChild(tree.Root, new SimpleTreeNode<int>(2, tree.Root));
            tree.AddChild(tree.Root, new SimpleTreeNode<int>(3, tree.Root));

            AssetNodeLevel(tree.GetAllNodes(), 3, new List<(int, int)> { (5, 1), (2, 2), (3, 2) });
        }

        [Test]
        public void FillLevelsTwoNodesOneWithSubtreeShouldReturnList()
        {
            var root = new SimpleTreeNode<int>(5, null);
            var tree = new SimpleTree<int>(root);

            var node1 = new SimpleTreeNode<int>(2, tree.Root);
            tree.AddChild(node1, new SimpleTreeNode<int>(4, node1));

            tree.AddChild(tree.Root, node1);
            tree.AddChild(tree.Root, new SimpleTreeNode<int>(3, tree.Root));

            AssetNodeLevel(tree.GetAllNodes(), 4, new List<(int, int)> { (5, 1), (2, 2), (4, 3), (3, 2) });
        }

        [Test]
        public void FillLevelsTwoNodesWithSubtreeShouldReturnList()
        {
            var root = new SimpleTreeNode<int>(5, null);
            var tree = new SimpleTree<int>(root);

            var node1 = new SimpleTreeNode<int>(2, tree.Root);
            tree.AddChild(node1, new SimpleTreeNode<int>(4, node1));

            tree.AddChild(tree.Root, node1);

            var node2 = new SimpleTreeNode<int>(3, tree.Root);
            tree.AddChild(node2, new SimpleTreeNode<int>(6, node2));
            tree.AddChild(tree.Root, node2);

            AssetNodeLevel(tree.GetAllNodes(), 5, new List<(int, int)> { (5, 1), (2, 2), (4, 3), (3, 2), (6, 3) });
        }

        [Test]
        public void FillLevelsNodesWithSubtreeShouldReturnList()
        {
            var root = new SimpleTreeNode<int>(5, null);
            var tree = new SimpleTree<int>(root);

            var node1 = new SimpleTreeNode<int>(2, tree.Root);
            var subNode1 = new SimpleTreeNode<int>(4, node1);
            tree.AddChild(subNode1, new SimpleTreeNode<int>(8, subNode1));
            tree.AddChild(node1, subNode1);

            tree.AddChild(tree.Root, node1);

            var node2 = new SimpleTreeNode<int>(3, tree.Root);
            tree.AddChild(node2, new SimpleTreeNode<int>(6, node2));
            tree.AddChild(tree.Root, node2);

            AssetNodeLevel(tree.GetAllNodes(), 6, new List<(int, int)> { (5, 1), (2, 2), (4, 3), (8, 4), (3, 2), (6, 3) });
        }

        [Test]
        public void FillLevelsFromExampleShouldReturnList()
        {
            var root = new SimpleTreeNode<int>(9, null);
            var tree = new SimpleTree<int>(root);

            var node1 = new SimpleTreeNode<int>(4, tree.Root);
            var subsubNode1 = new SimpleTreeNode<int>(6, node1);
            tree.AddChild(subsubNode1, new SimpleTreeNode<int>(5, subsubNode1));
            tree.AddChild(subsubNode1, new SimpleTreeNode<int>(7, subsubNode1));
            tree.AddChild(node1, new SimpleTreeNode<int>(3, node1));
            tree.AddChild(node1, subsubNode1);

            tree.AddChild(tree.Root, node1);

            var node2 = new SimpleTreeNode<int>(17, tree.Root);
            var subNode2 = new SimpleTreeNode<int>(22, node2);
            tree.AddChild(subNode2, new SimpleTreeNode<int>(20, subNode2));
            tree.AddChild(node2, subNode2);
            tree.AddChild(tree.Root, node2);

            AssetNodeLevel(tree.GetAllNodes(), 9, new List<(int, int)> { (9, 1), (4, 2), (3, 3), (6, 3), (5, 4), (7, 4), (17, 2), (22, 3), (20, 4) });
        }

        public static void AssetNodeLevel(List<SimpleTreeNode<int>> res, int count, List<(int, int)> equal)
        {
            Assert.That(res.Count, Is.EqualTo(count));
            int idx = 0;
            foreach (var item in res)
            {
                Assert.That(item.NodeValue, Is.EqualTo(equal[idx].Item1));
                Assert.That(item.Level, Is.EqualTo(equal[idx].Item2));
                idx++;
            }
        }

        [Test]
        public void FindNodesByValueEmptyTreeShouldReturnRoot()
        {
            var root = new SimpleTreeNode<int>(5, null);
            var tree = new SimpleTree<int>(root);
            var res = tree.FindNodesByValue(5);

            AssertFind(res, 1, 5);
        }

        [Test]
        public void FindNodesByValueFourNodesShouldReturnEmptyList()
        {
            var root = new SimpleTreeNode<int>(5, null);
            var tree = new SimpleTree<int>(root);

            var addNode = new SimpleTreeNode<int>(6, tree.Root);
            tree.AddChild(tree.Root, addNode);

            tree.AddChild(addNode, new SimpleTreeNode<int>(7, addNode));
            tree.AddChild(addNode, new SimpleTreeNode<int>(8, addNode));

            var find = tree.FindNodesByValue(15);

            AssertFind(find, 0, 15);
        }

        [Test]
        public void FindNodesByValueFourNodesShouldFindNode()
        {
            var root = new SimpleTreeNode<int>(5, null);
            var tree = new SimpleTree<int>(root);
            var addNode = new SimpleTreeNode<int>(6, tree.Root);
            tree.AddChild(tree.Root, addNode);

            tree.AddChild(addNode, new SimpleTreeNode<int>(7, addNode));
            tree.AddChild(addNode, new SimpleTreeNode<int>(8, addNode));

            var find = tree.FindNodesByValue(5);

            AssertFind(find, 1, 5);
        }

        [Test]
        public void FindNodesByValueSevenNodesShouldFindNode()
        {
            var root = new SimpleTreeNode<int>(9, null);
            var tree = new SimpleTree<int>(root);
            var node1 = new SimpleTreeNode<int>(4, tree.Root);
            tree.AddChild(tree.Root, node1);

            var node2 = new SimpleTreeNode<int>(17, tree.Root);
            tree.AddChild(tree.Root, node2);

            tree.AddChild(node2, new SimpleTreeNode<int>(3, node2));
            tree.AddChild(node2, new SimpleTreeNode<int>(6, node2));

            tree.AddChild(node1, new SimpleTreeNode<int>(17, node1));
            tree.AddChild(node1, new SimpleTreeNode<int>(17, node1));

            var found = tree.FindNodesByValue(17);

            AssertFind(found, 3, 17);
        }

        [Test]
        public void FindNodesByValueSevenNodesShouldReturnEmptyList()
        {
            var root = new SimpleTreeNode<int>(9, null);
            var tree = new SimpleTree<int>(root);
            var node1 = new SimpleTreeNode<int>(4, tree.Root);
            tree.AddChild(tree.Root, node1);

            var node2 = new SimpleTreeNode<int>(17, tree.Root);
            tree.AddChild(tree.Root, node2);

            tree.AddChild(node2, new SimpleTreeNode<int>(3, node2));
            tree.AddChild(node2, new SimpleTreeNode<int>(6, node2));

            tree.AddChild(node1, new SimpleTreeNode<int>(17, node1));
            tree.AddChild(node1, new SimpleTreeNode<int>(22, node1));

            var found = tree.FindNodesByValue(33);

            AssertFind(found, 0, 33);
        }

        private static void AssertFind(List<SimpleTreeNode<int>> res, int count, int value)
        {
            Assert.That(res.Count, Is.EqualTo(count));
            foreach (var item in res)
                Assert.That(item.NodeValue, Is.EqualTo(value));
        }

        [Test]
        public void GetAllNodesEmptyTreeShouldReturnRoot()
        {
            var root = new SimpleTreeNode<int>(5, null);
            var tree = new SimpleTree<int>(root);
            var res = tree.GetAllNodes();

            AssetGetAll(res, 1, new List<int> { 5 });
        }

        [Test]
        public void GetAllNodesTwoEqualNodesShouldReturnList()
        {
            var root = new SimpleTreeNode<int>(5, null);
            var tree = new SimpleTree<int>(root);

            tree.AddChild(tree.Root, new SimpleTreeNode<int>(2, tree.Root));
            tree.AddChild(tree.Root, new SimpleTreeNode<int>(2, tree.Root));

            var res = tree.GetAllNodes();

            AssetGetAll(res, 3, new List<int> { 5, 2, 2 });
        }

        [Test]
        public void GetAllNodesTwoNodesShouldReturnList()
        {
            var root = new SimpleTreeNode<int>(5, null);
            var tree = new SimpleTree<int>(root);

            tree.AddChild(tree.Root, new SimpleTreeNode<int>(2, tree.Root));
            tree.AddChild(tree.Root, new SimpleTreeNode<int>(3, tree.Root));

            var res = tree.GetAllNodes();

            AssetGetAll(res, 3, new List<int> { 5, 2, 3 });
        }

        [Test]
        public void GetAllNodesTwoNodesOneWithSubtreeShouldReturnList()
        {
            var root = new SimpleTreeNode<int>(5, null);
            var tree = new SimpleTree<int>(root);

            var node1 = new SimpleTreeNode<int>(2, tree.Root);
            tree.AddChild(node1, new SimpleTreeNode<int>(4, node1));

            tree.AddChild(tree.Root, node1);
            tree.AddChild(tree.Root, new SimpleTreeNode<int>(3, tree.Root));

            var res = tree.GetAllNodes();

            AssetGetAll(res, 4, new List<int> { 5, 2, 4, 3 });
        }

        [Test]
        public void GetAllNodesTwoNodesWithSubtreeShouldReturnList()
        {
            var root = new SimpleTreeNode<int>(5, null);
            var tree = new SimpleTree<int>(root);

            var node1 = new SimpleTreeNode<int>(2, tree.Root);
            tree.AddChild(node1, new SimpleTreeNode<int>(4, node1));

            tree.AddChild(tree.Root, node1);

            var node2 = new SimpleTreeNode<int>(3, tree.Root);
            tree.AddChild(node2, new SimpleTreeNode<int>(6, node2));
            tree.AddChild(tree.Root, node2);

            var res = tree.GetAllNodes();

            AssetGetAll(res, 5, new List<int> { 5, 2, 4, 3, 6 });
        }

        [Test]
        public void GetAllNodesNodesWithSubtreeShouldReturnList()
        {
            var root = new SimpleTreeNode<int>(5, null);
            var tree = new SimpleTree<int>(root);

            var node1 = new SimpleTreeNode<int>(2, tree.Root);
            var subNode1 = new SimpleTreeNode<int>(4, node1);
            tree.AddChild(subNode1, new SimpleTreeNode<int>(8, subNode1));
            tree.AddChild(node1, subNode1);

            tree.AddChild(tree.Root, node1);

            var node2 = new SimpleTreeNode<int>(3, tree.Root);
            tree.AddChild(node2, new SimpleTreeNode<int>(6, node2));
            tree.AddChild(tree.Root, node2);

            var res = tree.GetAllNodes();

            AssetGetAll(res, 6, new List<int> { 5, 2, 4, 8, 3, 6 });
        }

        [Test]
        public void GetAllNodesFromExampleShouldReturnList()
        {
            var root = new SimpleTreeNode<int>(9, null);
            var tree = new SimpleTree<int>(root);

            var node1 = new SimpleTreeNode<int>(4, tree.Root);
            var subsubNode1 = new SimpleTreeNode<int>(6, node1);
            tree.AddChild(subsubNode1, new SimpleTreeNode<int>(5, subsubNode1));
            tree.AddChild(subsubNode1, new SimpleTreeNode<int>(7, subsubNode1));
            tree.AddChild(node1, new SimpleTreeNode<int>(3, node1));
            tree.AddChild(node1, subsubNode1);

            tree.AddChild(tree.Root, node1);

            var node2 = new SimpleTreeNode<int>(17, tree.Root);
            var subNode2 = new SimpleTreeNode<int>(22, node2);
            tree.AddChild(subNode2, new SimpleTreeNode<int>(20, subNode2));
            tree.AddChild(node2, subNode2);
            tree.AddChild(tree.Root, node2);

            var res = tree.GetAllNodes();

            AssetGetAll(res, 9, new List<int> { 9, 4, 3, 6, 5, 7, 17, 22, 20 });
            Assert.That(tree.LeafCount(), Is.EqualTo(4));
            Assert.That(tree.Count(), Is.EqualTo(9));

            foreach (var item in res)
                Assert.That(tree.FindNodesByValue(item.NodeValue).Count, Is.EqualTo(1));
        }

        private static void AssetGetAll(List<SimpleTreeNode<int>> res, int count, List<int> equal)
        {
            Assert.That(res.Count, Is.EqualTo(count));
            int idx = 0;
            foreach (var item in res)
            {
                Assert.That(equal[idx], Is.EqualTo(item.NodeValue));
                idx++;
            }
        }

        [Test]
        public void MoveNodeMoveFromCurrentPlaceToCurrentPlaceShouldMove()
        {
            var root = new SimpleTreeNode<int>(5, null);
            var tree = new SimpleTree<int>(root);
            var node = new SimpleTreeNode<int>(6, tree.Root);
            tree.AddChild(tree.Root, node);

            tree.MoveNode(node, tree.Root);

            Assert.That(tree.Root.NodeValue, Is.EqualTo(5));
            var treeCh = tree.Root.Children;
            Assert.That(treeCh.Count, Is.EqualTo(1));
            Assert.That(treeCh[0].NodeValue, Is.EqualTo(6));
            Assert.That(tree.Count(), Is.EqualTo(2));
            Assert.That(tree.LeafCount(), Is.EqualTo(1));

            AssetNodeLevel(tree.GetAllNodes(), 2, new List<(int, int)> { (5, 1), (6, 2) });
        }

        [Test]
        public void MoveNodeMoveFromSubtreeShouldMove()
        {
            var root = new SimpleTreeNode<int>(5, null);
            var tree = new SimpleTree<int>(root);

            var node = new SimpleTreeNode<int>(6, tree.Root);
            tree.AddChild(tree.Root, node);

            var node1 = new SimpleTreeNode<int>(7, node);
            tree.AddChild(node1, new SimpleTreeNode<int>(8, node1));
            tree.AddChild(node, node1);

            tree.MoveNode(node1, tree.Root);

            Assert.That(tree.Root.NodeValue, Is.EqualTo(5));
            var treeCh = tree.Root.Children;
            Assert.That(treeCh.Count, Is.EqualTo(2));

            Assert.That(treeCh[0].NodeValue, Is.EqualTo(6));
            Assert.Null(treeCh[0].Children);

            Assert.That(treeCh[1].NodeValue, Is.EqualTo(7));
            Assert.That(treeCh[1].Children.Count, Is.EqualTo(1));

            Assert.That(treeCh[1].Children[0].NodeValue, Is.EqualTo(8));
            Assert.Null(treeCh[1].Children[0].Children);

            Assert.That(tree.Count(), Is.EqualTo(4));
            Assert.That(tree.LeafCount(), Is.EqualTo(2));

            AssetNodeLevel(tree.GetAllNodes(), 4, new List<(int, int)> { (5, 1), (6, 2), (7, 2), (8, 3) });
        }

        [Test]
        public void IsSymmetric()
        {
            var root = new SimpleTreeNode<int>(1, null);
            var tree = new SimpleTree<int>(root);

            var leftNode = new SimpleTreeNode<int>(2, tree.Root);
            var rightNode = new SimpleTreeNode<int>(2, tree.Root);
            tree.AddChild(tree.Root, leftNode);
            tree.AddChild(tree.Root, rightNode);

            Assert.IsTrue(tree.IsSymmetric());

            var leftNodeLeftSubNode = new SimpleTreeNode<int>(3, leftNode);
            var leftNodeRightSubNode = new SimpleTreeNode<int>(4, leftNode);
            tree.AddChild(leftNode, leftNodeLeftSubNode);
            tree.AddChild(leftNode, leftNodeRightSubNode);

            Assert.IsFalse(tree.IsSymmetric());

            var rightNodeLeftSubNode = new SimpleTreeNode<int>(4, rightNode);
            var rightNodeRightSubNode = new SimpleTreeNode<int>(3, rightNode);
            tree.AddChild(rightNode, rightNodeLeftSubNode);
            tree.AddChild(rightNode, rightNodeRightSubNode);

            Assert.IsTrue(tree.IsSymmetric());
        }
    }
}
