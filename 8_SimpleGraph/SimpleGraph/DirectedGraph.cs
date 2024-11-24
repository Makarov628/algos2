using System;
using System.Linq;
using System.Collections.Generic;

namespace AlgorithmsDataStructures2
{
    public class DirectGraphVertex
    {
        public int Value;
        public DirectGraphVertex(int value)
        {
            Value = value;
        }
    }

    public class DirectedGraph
    {
        public DirectGraphVertex[] vertex; // список, хранящий вершины
        public int[,] m_adjacency; // матрица смежности
        public int max_vertex;

        public DirectedGraph(int size)
        {
            max_vertex = size;
            m_adjacency = new int[size, size];
            vertex = new DirectGraphVertex[size];
        }

        public bool IsCyclic()
        {
            for (int v = 0; v < max_vertex; v++)
                if (vertex[v] != null && IsCyclicRecursive(v, new HashSet<int>()))
                    return true;

            return false;
        }

        private bool IsCyclicRecursive(int v, HashSet<int> visited)
        {
            if (visited.Contains(v))
                return true;

            visited.Add(v);
            foreach (var vToVisit in GetNeighbours(v))
                if (IsCyclicRecursive(vToVisit, CloneVisitedCollection(visited)))
                    return true;

            return false;
        }

        private IEnumerable<int> GetNeighbours(int v)
        {
            for (int i = 0; i < max_vertex; i++)
                if (m_adjacency[v, i] == 1)
                    yield return i;

            yield break;
        }

        private HashSet<int> CloneVisitedCollection(HashSet<int> fromCollection) =>
            new HashSet<int>(fromCollection);
             

        public void AddVertex(int value)
        {
            for (int i = 0; i < max_vertex; i++)
            {
                if (vertex[i] != null)
                    continue;

                var newVertex = new DirectGraphVertex(value);
                vertex[i] = newVertex;
                break;
            }
        }

        public void RemoveVertex(int v)
        {
            if (!IsIndexCorrect(v))
                return;

            vertex[v] = null;
            for (int i = 0; i < vertex.Length; i++)
                m_adjacency[v, i] = 0;


            for (int j = 0; j < vertex.Length; j++)
                m_adjacency[j, v] = 0;
        }

        public bool IsEdge(int v1, int v2) =>
            IsIndexCorrect(v1) && IsIndexCorrect(v2) && m_adjacency[v1, v2] == 1;

        public void AddEdge(int v1, int v2) => ChangeEdgeState(v1, v2, 1);

        public void RemoveEdge(int v1, int v2) => ChangeEdgeState(v1, v2, 0);

        private void ChangeEdgeState(int v1, int v2, int state)
        {
            if (!IsIndexCorrect(v1) || !IsIndexCorrect(v2))
                return;

            m_adjacency[v1, v2] = state;
        }

        private bool IsIndexCorrect(int i) =>
           i >= 0 && i < vertex.Length;
    }
}