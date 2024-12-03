using System;

namespace AlgorithmsDataStructures2
{

    public class Program
    {
        public static void Main(string[] args)
        {
            var tree = new EvenTree<int>(new EvenTreeNode<int>(1, null));

            tree.AddChild(tree.Root, new EvenTreeNode<int>(2, null));
            tree.AddChild(tree.Root.Children[0], new EvenTreeNode<int>(5, null));
            tree.AddChild(tree.Root.Children[0], new EvenTreeNode<int>(7, null));

            tree.AddChild(tree.Root, new EvenTreeNode<int>(3, null));
            tree.AddChild(tree.Root.Children[1], new EvenTreeNode<int>(4, null));

            tree.AddChild(tree.Root, new EvenTreeNode<int>(6, null));
            tree.AddChild(tree.Root.Children[2], new EvenTreeNode<int>(8, null));
            tree.AddChild(tree.Root.Children[2].Children[0], new EvenTreeNode<int>(9, null));
            tree.AddChild(tree.Root.Children[2].Children[0], new EvenTreeNode<int>(10, null));

            tree.CountEvenSubtrees();
        }
    }
}