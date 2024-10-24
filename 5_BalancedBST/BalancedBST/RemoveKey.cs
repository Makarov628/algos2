using System;

namespace AlgorithmsDataStructures2
{
    public static class RemoveKey
    {
        public static void Remove(this aBST bst, int key)
        {
            var index = bst.FindKeyIndex(key);
            if (!index.HasValue || index < 0)
                return;

            if (IsLeaf(bst, index.Value))
            {
                bst.Tree[index.Value] = null;
                return;
            }

            if (TryToGetOnlyOneChild(bst, index.Value, out int singleChildIndex))
            {
                bst.Tree[index.Value] = bst.Tree[singleChildIndex];
                bst.Tree[singleChildIndex] = null;
                return;
            }

            var minIndex = FindMinIndexOfRightSubtree(bst, index.Value);
            bst.Tree[index.Value] = bst.Tree[minIndex]; 
            bst.Tree[minIndex] = null;
        }

        private static bool IsLeaf(this aBST bst, int index)
        {
            if (CalculateDepth(index + 1) == GetMaxDepth(bst))
                return true;

            return IsEmpty(bst, GetNextLeftIndex(index))
                && IsEmpty(bst, GetNextRightIndex(index));
        }

        private static bool TryToGetOnlyOneChild(this aBST bst, int index, out int singleChildIndex)
        {
            var isLeft = !IsEmpty(bst, GetNextLeftIndex(index));
            var isRight = !IsEmpty(bst, GetNextRightIndex(index));
            singleChildIndex = -1;

            if (isLeft && isRight)
                return false;

            if (isLeft)
                singleChildIndex = GetNextLeftIndex(index);

            if (isRight)
                singleChildIndex = GetNextRightIndex(index);

            return isLeft || isRight;
        }

        private static int FindMinIndexOfRightSubtree(this aBST bst, int index)
        {
            var rightSubtreeIndex = GetNextRightIndex(index) + 1;
            
            // Считаем глубину правого поддерева
            var subtreeDepth = GetMaxDepth(bst) - CalculateDepth(index + 1);

            // Опускаемся до самого наименьшего узла на максимальную глубину поддерева
            var leftMinimumIndex = rightSubtreeIndex << subtreeDepth;

            // Если самый глубокий узел пустой, поднимаемся выше
            while (IsEmpty(bst, leftMinimumIndex))
                leftMinimumIndex >>= 1;

            return leftMinimumIndex - 1;
        }

        private static int GetNextLeftIndex(int index) =>
            GetNextIndex(index, 1);

        private static int GetNextRightIndex(int index) =>
            GetNextIndex(index, 2);

        private static int GetNextIndex(int index, int direction) =>
            2 * index + direction;

        private static int CalculateDepth(int index) =>
            (int)Math.Floor(Math.Log2(index));

        private static int GetMaxDepth(this aBST bst) =>
            CalculateDepth(bst.Tree.Length);

        private static bool IsEmpty(this aBST bst, int index) =>
            index >= bst.Tree.Length || bst.Tree[index] == null;
    }
}