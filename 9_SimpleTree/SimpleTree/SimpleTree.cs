using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsDataStructures2
{
  public class SimpleTreeNode<T>
  {
    public T NodeValue;
    public SimpleTreeNode<T> Parent;
    public List<SimpleTreeNode<T>> Children;
    public int Level;

    public SimpleTreeNode(T val, SimpleTreeNode<T> parent)
    {
      NodeValue = val;
      Parent = parent;
      Children = null;
    }

    public void SetParent(SimpleTreeNode<T> parent)
    {
      Parent = parent;
      Level = parent == null ? 1 : parent.Level + 1;
      RecalculateChildLevels();
    }

    public bool IsLeaf() => Children == null;

    private void RecalculateChildLevels() => RecalculateLevels(this, Level);

    private void RecalculateLevels(SimpleTreeNode<T> node, int level)
    {
      node.Level = level;
      if (node.Children == null)
        return;

      foreach (var item in node.Children)
        RecalculateLevels(item, level + 1);
    }
  }

  public class SimpleTree<T>
  {
    public SimpleTreeNode<T> Root; // корень, может быть null

    public SimpleTree(SimpleTreeNode<T> root)
    {
      Root = root;
      Root.SetParent(null);
    }

    public void AddChild(SimpleTreeNode<T> ParentNode, SimpleTreeNode<T> NewChild)
    {
      NewChild.SetParent(ParentNode);

      if (ParentNode.IsLeaf())
        ParentNode.Children = new List<SimpleTreeNode<T>>();

      ParentNode.Children.Add(NewChild);
    }

    public void DeleteNode(SimpleTreeNode<T> NodeToDelete)
    {
      if (!NodeToDelete.IsLeaf())
        NodeToDelete.Children.Clear();

      DeleteNodeFromParent(NodeToDelete);
    }

    public List<SimpleTreeNode<T>> GetAllNodes() => GetNodes(Root);

    public List<SimpleTreeNode<T>> FindNodesByValue(T val) => GetNodesByValue(Root, val);

    public void MoveNode(SimpleTreeNode<T> OriginalNode, SimpleTreeNode<T> NewParent)
    {
      DeleteNodeFromParent(OriginalNode);
      AddChild(NewParent, OriginalNode);
    }

    public int Count() => GetAllNodes().Count();

    public int LeafCount() => GetAllNodes().Count(node => node.IsLeaf());

    public bool IsSymmetric()
    {
      if (Root == null || Root.IsLeaf())
        return true;

      if (Root.Children.Count != 2)
        return false;

      return IsNodeSymmetric(Root.Children[0], Root.Children[1]);
    }

    public List<T> EvenTrees()
    {
      var res = new List<T>();
      // если количество всех узлов нечётное - значит мы не может получить четных деревьев
      if (Root == null || Root.Children == null || GetChildrens(Root).Count % 2 != 0)
        return res;

      return GetEvenTrees(Root);
    }

    private List<T> GetEvenTrees(SimpleTreeNode<T> currentNode)
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

    private List<SimpleTreeNode<T>> GetChildrens(SimpleTreeNode<T> currentNode)
    {
      var list = new List<SimpleTreeNode<T>>();
      list.Add(currentNode);

      if (currentNode.Children == null || currentNode.Children.Count == 0)
        return list;

      foreach (var child in currentNode.Children)
        list.AddRange(GetChildrens(child));

      return list;
    }

    // Получить узлы пропустив ноду со всеми потомками
    private List<SimpleTreeNode<T>> GetChildrensSkipNode(SimpleTreeNode<T> currentNode, SimpleTreeNode<T> nodeToSkip)
    {
      var list = new List<SimpleTreeNode<T>>();
      if (currentNode == null || nodeToSkip == null || currentNode.Equals(nodeToSkip))
        return list;

      list.Add(currentNode);

      if (currentNode.Children == null || currentNode.Children.Count == 0)
        return list;

      foreach (var child in currentNode.Children)
        list.AddRange(GetChildrens(child));

      return list;
    }

    private bool IsNodeSymmetric(SimpleTreeNode<T> leftNode, SimpleTreeNode<T> rightNode)
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

    private List<SimpleTreeNode<T>> GetNodes(SimpleTreeNode<T> node)
    {
      List<SimpleTreeNode<T>> nodes = new() { node };

      if (node.IsLeaf())
        return nodes;

      foreach (var item in node.Children)
        nodes.AddRange(GetNodes(item));

      return nodes;
    }

    private List<SimpleTreeNode<T>> GetNodesByValue(SimpleTreeNode<T> node, T value)
    {
      List<SimpleTreeNode<T>> nodes = new();

      if (node.NodeValue.Equals(value))
        nodes.Add(node);

      if (node.IsLeaf())
        return nodes;

      foreach (var item in node.Children)
        nodes.AddRange(GetNodesByValue(item, value));

      return nodes;
    }

    private void DeleteNodeFromParent(SimpleTreeNode<T> node)
    {
      if (node.Parent.IsLeaf())
        return;

      var parentNodes = node.Parent.Children;
      if (parentNodes.IndexOf(node) >= 0)
        parentNodes.Remove(node);

      if (!parentNodes.Any())
        node.Parent.Children = null;
    }

  }

}

