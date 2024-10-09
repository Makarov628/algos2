using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsDataStructures2
{
  public class aBST
  {
    public int?[] Tree; // массив ключей

    public aBST(int depth)
    {
      int tree_size = depth == 0 ? 1 : (int)Math.Pow(2, depth + 1) - 1;
      Tree = new int?[tree_size];
      for (int i = 0; i < tree_size; i++) Tree[i] = null;
    }

    public int? FindKeyIndex(int key)
    {
      return FindKeyIndex(key, 0);
    }

    private int? FindKeyIndex(int key, int index)
    {
      if (index >= Tree.Length)
        return null;

      if (Tree[index] == null)
        return -index;

      if (Tree[index] == key)
        return index;

      if (Tree[index] > key)
        return FindKeyIndex(key, 2 * index + 1);

      return FindKeyIndex(key, 2 * index + 2);
    }

    public int AddKey(int key)
    {
      int? index = FindKeyIndex(key);

      if (index == null)
        return -1;

      if (index > 0)
        return index.Value;

      int positiveIndex = Math.Abs(index.Value);
      Tree[positiveIndex] = key;
      return positiveIndex;
    }

  }
}