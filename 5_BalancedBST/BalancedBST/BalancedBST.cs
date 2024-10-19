using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures2
{
  public static class BalancedBST
  {
    public static int[] GenerateBBSTArray(int[] a)
    {
      if (a == null || a.Length == 0)
        return new int[0];

      Array.Sort(a);
      var aBst = new int[a.Length];
      Generate(a, aBst, 0, 0, a.Length - 1);

      return aBst;
    }

    private static void Generate(int[] input, int[] aBST, int i, int left, int right)
    {
      if (left > right || i > aBST.Length - 1)
        return;

      int centralIndex = (left + right) / 2;
      aBST[i] = input[centralIndex];

      Generate(input, aBST, 2 * i + 1, left, centralIndex - 1);
      Generate(input, aBST, 2 * i + 2, centralIndex + 1, right);
    }

    /*
        public static bool DeleteSubtree(this aBST bst, int key)
    {
      var rootIndex = bst.FindKeyIndex(key) ?? -1;
      if (rootIndex == -1)
        return false;

      var rootNumber = rootIndex + 1;

      // Считаем на какой глубине находится индекс относительно всего дерева
      var depthOfIndex = (int)Math.Floor(Math.Log2(rootNumber));

      // Глубина всего дерева
      var maxDepth = (int)Math.Floor(Math.Log2(bst.Tree.Length));

      // Определяем глубину поддерева 
      var subtreeDepth = maxDepth - depthOfIndex;

      // Выделяем границы на максимальной глубине поддерева
      var leftBorder = rootNumber << subtreeDepth;
      var rightBorder = leftBorder + (int)Math.Pow(2, subtreeDepth) - 1;

      // Удаляем элементы и двигаемся к корню поддерева побитовым сдвигом
      while (leftBorder != rootNumber && rightBorder != rootNumber)
      {
        for (int i = leftBorder - 1; i < rightBorder; i++)
          bst.Tree[i] = null;

        leftBorder >>= 1;
        rightBorder >>= 1;
      }

      bst.Tree[rootIndex] = null;
      return true;
    }
    */
  }
}

