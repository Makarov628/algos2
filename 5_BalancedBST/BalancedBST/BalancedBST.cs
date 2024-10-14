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
  }
}

