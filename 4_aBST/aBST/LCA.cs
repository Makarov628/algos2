using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsDataStructures2
{
  public class LCA
  {
    // public int MaxDepth;
    // public int?[] Tree; // массив ключей

    // public LCA(int depth)
    // {
    //   MaxDepth = depth;
    //   int tree_size = (int)Math.Pow(2, depth + 1) - 1;
    //   Tree = new int?[tree_size];
    //   for (int i = 0; i < tree_size; i++) Tree[i] = null;
    // }

    // public int? FindKeyIndex(int key)
    // {
    //   return FindKeyIndex(key, 0);
    // }

    // private int? FindKeyIndex(int key, int index)
    // {
    //   if (index >= Tree.Length)
    //     return null;

    //   if (Tree[index] == null)
    //     return -index;

    //   if (Tree[index] == key)
    //     return index;

    //   if (Tree[index] < key)
    //     return FindKeyIndex(key, 2 * index + 1);

    //   return FindKeyIndex(key, 2 * index + 2);
    // }

    // public int AddKey(int key)
    // {
    //   int? index = FindKeyIndex(key, 0);

    //   if (index == null)
    //     return -1;

    //   if (index > 0)
    //     return index.Value;

    //   int positiveIndex = Math.Abs(index.Value);
    //   Tree[positiveIndex] = key;
    //   return positiveIndex;
    // }

    // public (int firstIndex, int secondIndex) FindIndexes(int firstKey, int secondKey)
    // {
    //   // TODO:
    //   return (firstKey, secondKey);
    // }

    // public int FindDepthForIndex(int index) =>
    //   (int)Math.Floor(Math.Log2(index + 1));

    // public int GetSideOfMainSubtree(int index)
    // {
    //   // Ищем на какой глубине находится индекс
    //   var depth = FindDepthForIndex(index);

    //   // Определяем начальный и конечный индексы на полученной глубине
    //   var firstIndexOfLeftSubtree = Math.Pow(2, depth) / 2;
    //   var lastIndexOfRightSubtree = Math.Pow(2, depth) - 1;

    //   // Определяем границы главного левого поддерева
    //   var lastIndexOfLeftSubtree = lastIndexOfRightSubtree - firstIndexOfLeftSubtree / 2;
    //   if (firstIndexOfLeftSubtree <= index || index <= lastIndexOfLeftSubtree)
    //     return 1; // Индекс левого поддерева

    //   // Определяем границы главного правого поддерева
    //   var firstIndexOfRightSubtree = lastIndexOfLeftSubtree + 1;
    //   if (firstIndexOfRightSubtree <= index || index <= lastIndexOfRightSubtree)
    //     return 2; // Индекс правого поддерева

    //   return 0;
    // }



    // public (int leftBorderIndex, int rightBorderIndex) FindBordersOfSubtree(int index, int depth)
    // {
    //   // Определяем начальный и конечный индексы на полученной глубине
    //   var LeftSubtreeLeftBorder = Math.Pow(2, depth) / 2;
    //   var RightSubtreeRightBorder = Math.Pow(2, depth) - 1;

    //   // Определяем границы главного левого поддерева
    //   var LeftSubtreeRightBorder = RightSubtreeRightBorder - LeftSubtreeLeftBorder / 2;

    //   // Определяем границы главного правого поддерева
    //   var RightSubtreeLeftBorder = LeftSubtreeRightBorder + 1;

    //   return ()
    // }

    // public bool TryFindAncestorFromTwoIndexes(int firstIndex, int secondIndex, out int ancestorIndex)
    // {
    //   ancestorIndex = -1;

    //   var minIndex = Math.Min(firstIndex, secondIndex);
    //   var maxIndex = Math.Max(firstIndex, secondIndex);

    //   var depthOfMinIndex = FindDepthForIndex(minIndex);
    //   var depthOfMaxIndex = FindDepthForIndex(maxIndex);

    //   if (depthOfMinIndex == depthOfMaxIndex)
    //     return false;



    //   return true;
    // }

    // public int? LCA2(int firstKey, int secondKey)
    // {
    //   var (firstIndex, secondIndex) = FindIndexes(firstKey, secondKey);
    //   if (firstIndex == -1 && secondIndex == -1)
    //     return null;

    //   // Если один ключ не найден, тогда считаем предком другой
    //   if (firstIndex == -1 || secondIndex == -1)
    //     return Tree[Math.Max(firstIndex, secondIndex)];

    //   // Если индексы одинаковы, возвращаем найденный ключ
    //   if (firstIndex == secondIndex)
    //     return Tree[firstIndex];

    //   var minIndex = Math.Min(firstIndex, secondIndex); // 3  // 3
    //   var maxIndex = Math.Max(firstIndex, secondIndex); // 10 // 5
    //   var depthOfMinIndex = FindDepthForIndex(minIndex); // 2 // 2
    //   var depthOfMaxIndex = FindDepthForIndex(maxIndex); // 3 // 2

    //   if (depthOfMinIndex == depthOfMaxIndex)
    //   {

    //   }
      

      

    // }

    // public int? LCA(int firstKey, int secondKey)
    // {
    //   var (firstIndex, secondIndex) = FindIndexes(firstKey, secondKey);
    //   if (firstIndex == -1 && secondIndex == -1)
    //     return null;

    //   // Если один ключ не найден, тогда считаем предком второй
    //   if (firstIndex == -1 || secondIndex == -1)
    //     return Tree[Math.Max(firstIndex, secondIndex)];

    //   // Если индексы одинаковы, возвращаем найденный ключ
    //   if (firstIndex == secondIndex)
    //     return Tree[firstIndex];

    //   // Ищем в каком из главных поддеревьев находятся индексы
    //   var firstIndexDepth = FindDepthForIndex(firstIndex);
    //   var secondIndexDepth = FindDepthForIndex(secondIndex);

    //   // Если индексы в разных главных поддеревьях то общим предком будет корень
    //   if (GetSideOfMainSubtree(firstIndex) != GetSideOfMainSubtree(secondIndex))
    //     return Tree.FirstOrDefault();

    //   // Проверяем не являются ли индексы предками друг другу
    //   if (TryFindAncestorFromTwoIndexes(firstIndex, secondIndex, out var ancestorIndex))
    //     return Tree[ancestorIndex];



    //   return null;
    // }

  }
}