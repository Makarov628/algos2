using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsDataStructures2
{
    public class EvenTreeNode<T> where T : IComparable<T>
    {
        public T NodeValue;
        public EvenTreeNode<T> Parent;
        public List<EvenTreeNode<T>> Children;
        public int Level;

        public EvenTreeNode(T val, EvenTreeNode<T> parent)
        {
            NodeValue = val;
            Parent = parent;
            Children = null;
        }

        public void SetParent(EvenTreeNode<T> parent)
        {
            Parent = parent;
            Level = parent == null ? 1 : parent.Level + 1;
            RecalculateChildLevels();
        }

        public bool IsLeaf() => Children == null || !Children.Any();

        private void RecalculateChildLevels() => RecalculateLevels(this, Level);

        private void RecalculateLevels(EvenTreeNode<T> node, int level)
        {
            node.Level = level;
            if (node.Children == null)
                return;

            foreach (var item in node.Children)
                RecalculateLevels(item, level + 1);
        }
    }

    public class EvenTree<T> where T : IComparable<T>
    {
        public EvenTreeNode<T> Root; // корень, может быть null

        public EvenTree(EvenTreeNode<T> root)
        {
            Root = root;
            if (Root != null)
                Root.SetParent(null);
        }

        public void AddChild(EvenTreeNode<T> ParentNode, EvenTreeNode<T> NewChild)
        {
            NewChild.SetParent(ParentNode);

            if (ParentNode.Children == null)
                ParentNode.Children = new List<EvenTreeNode<T>>();

            ParentNode.Children.Add(NewChild);
        }

        public void DeleteNode(EvenTreeNode<T> NodeToDelete)
        {
            if (NodeToDelete.Children != null)
                NodeToDelete.Children.Clear();

            DeleteNodeFromParent(NodeToDelete);
        }

        public List<EvenTreeNode<T>> GetAllNodes()
        {
            var result = new List<EvenTreeNode<T>>();
            if (Root != null)
                GetNodes(Root, result);
            return result;
        }

        public List<EvenTreeNode<T>> FindNodesByValue(T val)
        {
            var result = new List<EvenTreeNode<T>>();
            if (Root != null)
                GetNodesByValue(Root, val, result);
            return result;
        }

        public void MoveNode(EvenTreeNode<T> OriginalNode, EvenTreeNode<T> NewParent)
        {
            DeleteNodeFromParent(OriginalNode);
            AddChild(NewParent, OriginalNode);
        }

        public int Count() => GetAllNodes().Count;

        public int LeafCount() => GetAllNodes().Count(node => node.IsLeaf());

        public void BalanceBinaryEven()
        {
            var nodesSorted = GetAllNodes();
            if (nodesSorted.Count == 0)
                return;

            nodesSorted.Sort((a, b) => a.NodeValue.CompareTo(b.NodeValue));

            foreach (var node in nodesSorted)
            {
                node.Parent = null;
                node.Children = null;
            }

            Root = BalanceBinaryEvenRecursive(nodesSorted);
            if (Root != null)
                Root.SetParent(null);
        }

        private EvenTreeNode<T> BalanceBinaryEvenRecursive(List<EvenTreeNode<T>> nodesSorted)
        {
            if (nodesSorted.Count == 0)
                return null;

            int centralElementIndex = nodesSorted.Count / 2;
            var centralNode = nodesSorted[centralElementIndex];

            var leftSide = nodesSorted.GetRange(0, centralElementIndex);
            var rightSide = nodesSorted.GetRange(centralElementIndex + 1, nodesSorted.Count - centralElementIndex - 1);

            var leftChild = BalanceBinaryEvenRecursive(leftSide);
            var rightChild = BalanceBinaryEvenRecursive(rightSide);

            if (leftChild != null)
                AddChild(centralNode, leftChild);

            if (rightChild != null)
                AddChild(centralNode, rightChild);

            return centralNode;
        }

        // Метод для подсчёта общего количества чётных поддеревьев
        public int CountEvenSubtrees()
        {
            if (Root == null)
                return 0;

            var countEvenSubtrees = 0;
            CountEvenSubtrees(Root, ref countEvenSubtrees);
            return countEvenSubtrees;
        }


        private int CountEvenSubtrees(EvenTreeNode<T> subtreeRoot, ref int countEvenSubtrees)
        {
            var nodeCount = 1;

            if (subtreeRoot.IsLeaf())
                return nodeCount;

            foreach (var child in subtreeRoot.Children)
                nodeCount += CountEvenSubtrees(child, ref countEvenSubtrees);

            if (nodeCount % 2 == 0)
                countEvenSubtrees += 1;
            
            return nodeCount;
        }

        private void GetNodes(EvenTreeNode<T> node, List<EvenTreeNode<T>> nodes)
        {
            nodes.Add(node);

            if (node.IsLeaf())
                return;

            foreach (var child in node.Children)
                GetNodes(child, nodes);
        }

        private void GetNodesByValue(EvenTreeNode<T> node, T value, List<EvenTreeNode<T>> nodes)
        {
            if (node.NodeValue.CompareTo(value) == 0)
                nodes.Add(node);

            if (node.IsLeaf())
                return;

            foreach (var child in node.Children)
                GetNodesByValue(child, value, nodes);
        }

        private void DeleteNodeFromParent(EvenTreeNode<T> node)
        {
            if (node.Parent == null || node.Parent.Children == null)
                return;

            node.Parent.Children.Remove(node);
            if (!node.Parent.Children.Any())
                node.Parent.Children = null;
        }

        public List<T> EvenTrees()
        {
            var res = new List<T>();
            // если количество всех узлов нечётное - значит мы не может получить четных деревьев
            if (Root == null || Root.Children == null || GetChildrens(Root).Count % 2 != 0)
                return res;

            return GetEvenTrees(Root);
        }

        private List<T> GetEvenTrees(EvenTreeNode<T> currentNode)
        {
            var res = new List<T>();

            if (currentNode.Children == null || currentNode.Children.Count == 0)
                return res;

            // если потомки есть - проверяем что их количество (всех в текущем поддереве) - чётное
            if (GetChildrens(currentNode).Count % 2 == 0 && currentNode.Parent != null && GetChildrensSkipNode(Root, currentNode).Count % 2 == 0)
                res.AddRange(new[] { currentNode.Parent.NodeValue, currentNode.NodeValue });

            foreach (var node in currentNode.Children)
                res.AddRange(GetEvenTrees(node));

            return res;
        }

        private List<EvenTreeNode<T>> GetChildrens(EvenTreeNode<T> currentNode)
        {
            var list = new List<EvenTreeNode<T>>();
            list.Add(currentNode);

            if (currentNode.IsLeaf())
                return list;

            foreach (var child in currentNode.Children)
                list.AddRange(GetChildrens(child));

            return list;
        }

        // Получить узлы пропустив ноду со всеми потомками
        private List<EvenTreeNode<T>> GetChildrensSkipNode(EvenTreeNode<T> currentNode, EvenTreeNode<T> nodeToSkip)
        {
            var list = new List<EvenTreeNode<T>>();
            if (currentNode == null || nodeToSkip == null || currentNode.Equals(nodeToSkip))
                return list;

            list.Add(currentNode);

            if (currentNode.IsLeaf())
                return list;

            foreach (var child in currentNode.Children)
                list.AddRange(GetChildrens(child));

            return list;
        }

        public bool IsSymmetric()
        {
            if (Root == null || Root.IsLeaf())
                return true;

            if (Root.Children.Count != 2)
                return false;

            return IsNodeSymmetric(Root.Children[0], Root.Children[1]);
        }

        private bool IsNodeSymmetric(EvenTreeNode<T> leftNode, EvenTreeNode<T> rightNode)
        {
            if (!leftNode.NodeValue.Equals(rightNode.NodeValue))
                return false;

            if (leftNode.IsLeaf() && rightNode.IsLeaf())
                return true;

            if (leftNode.Children?.Count != 2 || rightNode.Children?.Count != 2)
                return false;

            return IsNodeSymmetric(leftNode.Children[0], rightNode.Children[1])
                && IsNodeSymmetric(leftNode.Children[1], rightNode.Children[0]);
        }
    }
}
