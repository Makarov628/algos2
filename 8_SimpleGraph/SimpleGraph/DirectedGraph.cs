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
            // Массив статусов вершин:
            // 0 - не посещена (white)
            // 1 - в процессе посещения (gray)
            // 2 - посещена (black)
            var state = new int[max_vertex];
            
            for (var v = 0; v < max_vertex; v++)
            {
                if (vertex[v] == null || state[v] != 0)
                    continue;

                if (IsCyclicRecursive(v, state))
                    return true;
            }

            return false;
        }

        private bool IsCyclicRecursive(int v, int[] state)
        {
            state[v] = 1; // Начинаем посещение 

            foreach (var neighbor in GetNeighbours(v))
            {
                if (state[neighbor] == 1)
                    return true; // Обнаружен цикл

                if (state[neighbor] == 2)
                    continue; // Игнорируем посещенные узлы

                if (IsCyclicRecursive(neighbor, state))
                    return true; // Если обнаружен цикл в поддереве, возвращаемся
            }

            state[v] = 2; // Заканчиваем посещение
            return false;
        }

        private IEnumerable<int> GetNeighbours(int v)
        {
            for (int i = 0; i < max_vertex; i++)
                if (m_adjacency[v, i] == 1)
                    yield return i;
        }
        
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
            for (var i = 0; i < vertex.Length; i++)
                m_adjacency[v, i] = 0;


            for (var j = 0; j < vertex.Length; j++)
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