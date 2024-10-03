using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsDataStructures2
{
    public static class BSTActions
    {
        public static BSTNode<T> InvertTree<T>(BSTNode<T> root)
        {
            if (root == null)
                return null;

            var temp = root.LeftChild;
            root.LeftChild = InvertTree(root.RightChild);
            root.RightChild = InvertTree(temp);

            return root;
        }



        public static int FindLevelWithMaxSumDFS(BSTNode<int> root)
        {
            int maxLevel = MaxLevel(root);
            int[] sum = new int[maxLevel];

            MaxLevelSum(root, sum, 0);

            int findedLevelIndex = 0;
            for (int i = 0; i < maxLevel; i++)
                if (sum[findedLevelIndex] < sum[i])
                    findedLevelIndex = i;

            return findedLevelIndex + 1;
        }

        private static int MaxLevel(BSTNode<int> root) =>
            root != null
                ? 1 + Math.Max(MaxLevel(root.LeftChild), MaxLevel(root.RightChild))
                : 0;

        private static void MaxLevelSum(BSTNode<int> root, int[] sum, int levelIndex)
        {
            if (root == null)
                return;

            sum[levelIndex] += root.NodeValue;
            MaxLevelSum(root.LeftChild, sum, levelIndex + 1);
            MaxLevelSum(root.RightChild, sum, levelIndex + 1);
        }



        public static BSTNode<int> BuildTree(int[] preorder, int[] inorder)
        {
            Dictionary<int, int> inorderIndexByValues = new Dictionary<int, int>();
            for (int i = 0; i < inorder.Length; i++)
                inorderIndexByValues[inorder[i]] = i;

            return BuildTree(preorder, inorder, 0, 0, inorder.Length - 1, inorderIndexByValues);
        }

        private static BSTNode<int> BuildTree(int[] preorder, int[] inorder, int preorderStartIndex, int inorderStartIndex, int inorderEndIndex, Dictionary<int, int> inorderIndexByValues)
        {
            if (preorderStartIndex >= preorder.Length || inorderStartIndex > inorderEndIndex)
                return null;

            int value = preorder[preorderStartIndex];
            var root = new BSTNode<int>(value, value, null);

            int inorderIndex = inorderIndexByValues[value];
            int leftSubtreeSize = inorderIndex - inorderStartIndex + 1;

            root.AddLeft(BuildTree(preorder, inorder, preorderStartIndex + 1, inorderStartIndex, inorderIndex - 1, inorderIndexByValues));
            root.AddRight(BuildTree(preorder, inorder, preorderStartIndex + leftSubtreeSize, inorderIndex + 1, inorderEndIndex, inorderIndexByValues));

            return root;
        }

    }
}