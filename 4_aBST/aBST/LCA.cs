using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace AlgorithmsDataStructures2
{
  public static class LCA
  {
    public static int? LowestCommonAncestor(this aBST bst, int firstIndex, int secondIndex)
    {
      if (firstIndex < 0 && secondIndex < 0)
        return null;

      if (firstIndex >= bst.Tree.Length && secondIndex >= bst.Tree.Length)
        return null;

      // Если один ключ не найден, тогда считаем предком другой
      if (firstIndex < 0 || secondIndex < 0)
        return Math.Max(firstIndex, secondIndex);

      if (firstIndex >= bst.Tree.Length || secondIndex >= bst.Tree.Length)
        return Math.Min(firstIndex, secondIndex);

      // Если индексы одинаковы, возвращаем найденный ключ
      if (firstIndex == secondIndex)
        return firstIndex;

      return LowestCommonAncestor(firstIndex, secondIndex);
    }

    private static int CountOfBinaryDigits(int index) =>
      (int)Math.Floor(Math.Log2(index));

    private static int LowestCommonAncestor(int firstIndex, int secondIndex)
    {
      var minNode = Math.Min(firstIndex, secondIndex) + 1;
      var maxNode = Math.Max(firstIndex, secondIndex) + 1;

      // Считаем на какой глубине находятся индексы
      var depthOfMin = CountOfBinaryDigits(minNode);
      var depthOfMax = CountOfBinaryDigits(maxNode);

      // С помощью побитового сдвига поднимаемся на глубину минимального индекса и получаем родителя
      var parentNode = maxNode >> (depthOfMax - depthOfMin);

      // Если найденный родитель равен минимальному индексу, значит он является предком
      if (parentNode == minNode)
        return parentNode - 1;

      // Подсчитываем расстояние до предка
      var distance = CountOfBinaryDigits(parentNode ^ minNode);
      
      // Сдвигаем и возращаем индекс
      return (parentNode >> distance) - 1;
    }

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

    public static List<int> WideAllNodes(this aBST aBST) => 
      aBST.Tree.Where(node => node.HasValue).Select(node => (int)node).ToList();

  }
}
