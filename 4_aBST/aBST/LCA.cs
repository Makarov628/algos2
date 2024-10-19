using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace AlgorithmsDataStructures2
{
  public static class LCA
  {
    public static int? LowestCommonAncestor(this aBST bst, int node1, int node2)
    {
      int direction;
      for (int index = 0; index < bst.Tree.Length; index = GetNextIndex(index, direction))
      {
        // Если узел пустой, то LCA не найден
        if (bst.Tree[index] == null)
          return null;

        int currentValue = bst.Tree[index].Value;

        // Определяем, нужно ли двигаться влево или вправо
        bool isMoveToLeft = node1 < currentValue && node2 < currentValue;
        bool isMoveToRight = node1 > currentValue && node2 > currentValue;

        // Если не нужно двигаться ни влево, ни вправо, значит это LCA
        if (!isMoveToLeft && !isMoveToRight)
          return currentValue;

        direction = isMoveToLeft ? 1 : isMoveToRight ? 2 : 0;
      }

      return null;
    }

    private static int GetNextIndex(int index, int direction)
    {
      return 2 * index + direction;
    }


    public static int? LowestCommonAncestorIndex(this aBST bst, int firstIndex, int secondIndex)
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

      return LowestCommonAncestorIndex(firstIndex, secondIndex);
    }

    private static int LowestCommonAncestorIndex(int firstIndex, int secondIndex)
    {
      var minNode = Math.Min(firstIndex, secondIndex) + 1;
      var maxNode = Math.Max(firstIndex, secondIndex) + 1;

      // Индекс в бинарном виде хранит в себе маршрут (пример: 13 -> 1101 -> Корень-Право-Лево-Правый)
      // Количество разрядов у индекса = глубина дерева + 1

      // Считаем на какой глубине находятся индексы
      var depthOfMin = CalculateDepth(minNode);
      var depthOfMax = CalculateDepth(maxNode);

      // С помощью побитового сдвига поднимаемся на глубину минимального индекса и получаем родителя
      var parentNode = maxNode >> (depthOfMax - depthOfMin);

      // Если найденный родитель равен минимальному индексу, значит он является предком
      if (parentNode == minNode)
        return parentNode - 1;

      // Через оператор XOR выявляем разницу в маршрутах
      var difference = parentNode ^ minNode;

      // Расстоянием до общего предка будет количество разрядов 
      // в полученной разнице маршрутов
      var distance = CountOfBinaryDigits(difference);

      // Сдвигаем и возращаем индекс
      return (parentNode >> distance) - 1;
    }

    private static int CalculateDepth(int index) =>
      (int)Math.Floor(Math.Log2(index));

    private static int CountOfBinaryDigits(int value) =>
      Convert.ToString(value, 2).Length;

    public static List<int> WideAllNodes(this aBST aBST) =>
      aBST.Tree.Where(node => node.HasValue).Select(node => (int)node).ToList();

  }
}
