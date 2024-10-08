using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures2
{
  public abstract class BSTNode
  {
    public int NodeKey; // ключ узла
  }

  public class BSTNode<T> : BSTNode
  {
    public T NodeValue; // значение в узле
    public BSTNode<T> Parent; // родитель или null для корня
    public BSTNode<T> LeftChild; // левый потомок
    public BSTNode<T> RightChild; // правый потомок	

    public BSTNode(int key, T val, BSTNode<T> parent)
    {
      NodeKey = key;
      NodeValue = val;
      Parent = parent;
      LeftChild = null;
      RightChild = null;
    }

    public BSTNode(int key, T val) : this(key, val, null)
    { }

    public void AddLeft(BSTNode<T> node)
    {
      if (node == null)
        return;

      LeftChild = node;
      node.Parent = this;
    }

    public void AddRight(BSTNode<T> node)
    {
      if (node == null)
        return;

      RightChild = node;
      node.Parent = this;
    }

    public bool IsHasLeft() => LeftChild != null;

    public bool IsHasRight() => RightChild != null;

    public bool IsLeaf() => !(IsHasLeft() && IsHasRight());

  }

  // промежуточный результат поиска
  public class BSTFind<T>
  {
    // null если в дереве вообще нету узлов
    public BSTNode<T> Node;

    // true если узел найден
    public bool NodeHasKey;

    // true, если родительскому узлу надо добавить новый левым
    public bool ToLeft;

    public BSTFind() { Node = null; }
  }

  public class BST<T>
  {
    BSTNode<T> Root; // корень дерева, или null

    public BST(BSTNode<T> node)
    {
      Root = node;
    }

    public BSTFind<T> FindNodeByKey(int key) =>
      Root == null ? new BSTFind<T>() : FindByKey(Root, null, key);

    public bool AddKeyValue(int key, T val)
    {
      var node = FindNodeByKey(key);
      if (node.NodeHasKey)
        return false;

      if (node.Node == null)
      {
        Root = new BSTNode<T>(key, val);
        return true;
      }

      var newNode = new BSTNode<T>(key, val, node.Node);
      if (node.ToLeft)
        node.Node.LeftChild = newNode;
      else
        node.Node.RightChild = newNode;

      return true;
    }

    public BSTNode<T> FinMinMax(BSTNode<T> FromNode, bool FindMax) =>
      GetMinMax(FromNode, FindMax);


    public bool DeleteNodeByKey(int key)
    {
      // удаляем узел по ключу
      var node = FindNodeByKey(key);
      if (!node.NodeHasKey)
        return false; // если узел не найден

      var parent = node.Node.Parent;
      // если удаляем корень
      if (parent == null)
      {
        Root = null;
        return true;
      }

      // флаг каким был у родителя - правым или левым
      bool deletedWasLeft = key < parent.NodeKey;
      var leftChild = node.Node.LeftChild;
      var rightChild = node.Node.RightChild;

      // находим узел-преемник, который встает вместо удаляемого
      var nodeToChange = GetNodeToChange(node.Node);

      if (deletedWasLeft)
        parent.LeftChild = nodeToChange;
      else
        parent.RightChild = nodeToChange;

      node.Node = nodeToChange;

      // если удаляем лист
      if (nodeToChange == null)
        return true;

      // Если мы находим лист, то его и надо поместить вместо удаляемого узла.
      // делаем узел-приемник потомком родителя удаляемого узла
      if (nodeToChange.IsLeaf())
      {
        if (leftChild != null && !nodeToChange.Equals(leftChild))
          leftChild.Parent = nodeToChange;
        if (rightChild != null && !nodeToChange.Equals(rightChild))
          rightChild.Parent = nodeToChange;

        nodeToChange.Parent.LeftChild = null;
        nodeToChange.Parent = parent;

        if (!nodeToChange.Equals(leftChild))
          nodeToChange.LeftChild = leftChild;
        if (!nodeToChange.Equals(rightChild))
          nodeToChange.RightChild = rightChild;

        return true;
      }

      // Если мы находим узел, у которого есть только правый потомок,
      // то преемником берём этот узел, а вместо него помещаем его правого потомка.
      // делаем узел-приемник потомком родителя удаляемого узла
      if (leftChild != null)
        leftChild.Parent = nodeToChange;

      if (!nodeToChange.Equals(leftChild))
        nodeToChange.LeftChild = leftChild;
      nodeToChange.Parent = parent;
      return true;
    }

    public int Count() =>
      Root == null ? 0 : GetAllNodes(Root).Count;

    private BSTNode<T> GetNodeToChange(BSTNode<T> node)
    {
      if (node.IsLeaf())
        return null;

      if (!node.IsHasRight())
        return FinMinMax(node.LeftChild, true);

      return FinMinMax(node.RightChild, false);
    }

    private List<BSTNode<T>> GetAllNodes(BSTNode<T> currentNode)
    {
      var list = new List<BSTNode<T>> { currentNode };

      if (currentNode.IsHasLeft())
        list.AddRange(GetAllNodes(currentNode.LeftChild));

      if (currentNode.IsHasRight())
        list.AddRange(GetAllNodes(currentNode.RightChild));

      return list;
    }

    private BSTFind<T> FindByKey(BSTNode<T> current, BSTNode<T> parent, int key)
    {
      if (current == null && parent.NodeKey > key)
        return new BSTFind<T>()
        {
          Node = parent,
          NodeHasKey = false,
          ToLeft = true
        };

      if (current == null)
        return new BSTFind<T>()
        {
          Node = parent,
          NodeHasKey = false,
          ToLeft = false
        };

      if (current.NodeKey.Equals(key))
        return new BSTFind<T>()
        {
          Node = current,
          NodeHasKey = true
        };

      if (current.NodeKey > key)
        return FindByKey(current.LeftChild, current, key);

      return FindByKey(current.RightChild, current, key);
    }

    public BSTNode<T> GetMinMax(BSTNode<T> currentNode, bool findMax)
    {
      if (!currentNode.IsHasRight() && findMax)
        return currentNode;

      if (!currentNode.IsHasLeft() && !findMax)
        return currentNode;

      if (findMax)
        return GetMinMax(currentNode.RightChild, findMax);

      return GetMinMax(currentNode.LeftChild, findMax);
    }

    public List<BSTNode> WideAllNodes()
    {
      if (Root == null)
        return new List<BSTNode>();

      return WideAllNodes(Root);
    }

    private List<BSTNode> WideAllNodes(BSTNode<T> root)
    {
      var queue = new Queue<BSTNode>();
      queue.Enqueue(root);
      var allNodes = new List<BSTNode>();

      while (queue.Count != 0)
      {
        BSTNode current = queue.Dequeue();
        if (!(current is BSTNode<T> currentT))
          throw new Exception();

        allNodes.Add(currentT);

        if (currentT.IsHasLeft())
          queue.Enqueue(currentT.LeftChild);
        if (currentT.IsHasRight())
          queue.Enqueue(currentT.RightChild);
      }

      return allNodes;
    }

    public List<BSTNode> DeepAllNodes(int o)
    {
      if (Root == null)
        return new List<BSTNode>();

      if (o == 0)
        return InOrder(Root);

      if (o == 1)
        return PostOrder(Root);

      if (o == 2)
        return PreOrder(Root);

      throw new ArgumentException("Parameter should be: 0, 1, 2");
    }

    private List<BSTNode> InOrder(BSTNode<T> current)
    {
      var nodes = new List<BSTNode>();

      if (current.IsHasLeft())
        nodes.AddRange(InOrder(current.LeftChild));

      nodes.Add(current);

      if (current.IsHasRight())
        nodes.AddRange(InOrder(current.RightChild));

      return nodes;
    }

    private List<BSTNode> PostOrder(BSTNode<T> current)
    {
      var nodes = new List<BSTNode>();

      if (current.IsHasLeft())
        nodes.AddRange(PostOrder(current.LeftChild));

      if (current.IsHasRight())
        nodes.AddRange(PostOrder(current.RightChild));

      nodes.Add(current);

      return nodes;
    }

    private List<BSTNode> PreOrder(BSTNode<T> current)
    {
      var nodes = new List<BSTNode>();
      nodes.Add(current);

      if (current.IsHasLeft())
        nodes.AddRange(PreOrder(current.LeftChild));

      if (current.IsHasRight())
        nodes.AddRange(PreOrder(current.RightChild));

      return nodes;
    }

  }
}

