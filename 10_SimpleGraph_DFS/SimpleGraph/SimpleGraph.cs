using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures2
{
    public class Vertex
    {
        public bool Hit; // для обхода в глубину - true - если посещённая вершина
        public int Value;
        public Vertex(int val)
        {
            Value = val;
            Hit = false;
        }
    }
  
    public class SimpleGraph
    {
        public Vertex[] vertex; // список, хранящий вершины
        public int [,] m_adjacency; // матрица смежности
        public int max_vertex;
        private Stack<Vertex> _stack;
        private Queue<Vertex> _queue;
        private int _operationLimit;
        private List<List<Vertex>> _pathes;

        public SimpleGraph(int size)
        {
            max_vertex = size;
            m_adjacency = new int [size,size];
            vertex = new Vertex [size];
            _stack = new Stack<Vertex>();
            _queue = new Queue<Vertex>();
            _pathes = new List<List<Vertex>>();
        }
	
        public void AddVertex(int value)
        {
            // код добавления новой вершины 
            // с значением value 
            // в свободную позицию массива vertex

            for (int i = 0; i < max_vertex; i++)
            {
                if (vertex[i] != null)
                    continue;
                
                var newVertex = new Vertex(value);
                vertex[i] = newVertex;
                break;
            }
        }

        // здесь и далее, параметры v -- индекс вершины
        // в списке  vertex
        public void RemoveVertex(int v)
        {
            // ваш код удаления вершины со всеми её рёбрами
            if (!IsIndexCorrect(v))
                return;

            vertex[v] = null;
            for (int i = 0; i < vertex.Length; i++)
            {
                m_adjacency[v, i] = 0;
            }
            
            for (int j = 0; j < vertex.Length; j++)
            {
                m_adjacency[j, v] = 0;
            }
        }
	
        public bool IsEdge(int v1, int v2)
        {
            // true если есть ребро между вершинами v1 и v2
            return IsIndexCorrect(v1) && IsIndexCorrect(v2) && m_adjacency[v1, v2] == 1;
        }
	
        public void AddEdge(int v1, int v2)
        {
            // добавление ребра между вершинами v1 и v2
            if (!IsIndexCorrect(v1) || !IsIndexCorrect(v2))
                return;

            m_adjacency[v1, v2] = 1;
            m_adjacency[v2, v1] = 1;
        }
	
        public void RemoveEdge(int v1, int v2)
        {
            // удаление ребра между вершинами v1 и v2
            if (!IsIndexCorrect(v1) || !IsIndexCorrect(v2))
                return;
            
            m_adjacency[v1, v2] = 0;
            m_adjacency[v2, v1] = 0;
        }
        
        public List<Vertex> DepthFirstSearch(int VFrom, int VTo)
        {
            // Узлы задаются позициями в списке vertex.
            // Возвращается список узлов -- путь из VFrom в VTo.
            // Список пустой, если пути нету.
            
            var path = new List<Vertex>();
            if (!IsIndexCorrect(VFrom) || !IsIndexCorrect(VTo))
                return path;
            
            // 0. Prepare for search
            ClearDataStructures();
            
            // 1. Выбираем текущую вершину
            var res = GetPathDepth(VFrom, VTo);
            Array.Reverse(res);

            foreach (var el in res)
            {
                path.Add(el);
            }

            return path;
        }
        
        private Vertex[] GetPathDepth(int X, int VTo)
        {
            vertex[X].Hit = true;
            _stack.Push(vertex[X]);

            while (_stack.Count > 0)
            {

                for (int i = 0; i < max_vertex; i++)
                {
                    if (X != i && m_adjacency[X, i] == 1 && i == VTo)
                    {
                        _stack.Push(vertex[i]);
                        return _stack.ToArray();
                    }
                }

                X = GetNotVisited(X);
                if (X != -1)
                    return GetPathDepth(X, VTo);
            
                _stack.Pop();
                if (_stack.Count == 0)
                    break;

                var newCurrent = _stack.Peek();
                newCurrent.Hit = true;
                X = Array.IndexOf(vertex, newCurrent);
            }
            
            return _stack.ToArray();
        }

        private int GetNotVisited(int currentV)
        {
            for (int i = 0; i < max_vertex; i++)
            {
                if (i != currentV && m_adjacency[currentV, i] == 1 && !vertex[i].Hit)
                    return i;
            }

            return -1;
        }

        private void ClearDataStructures()
        {
            _stack.Clear();
            _queue.Clear();
            foreach (var v in vertex)
            {
                if (v != null)
                    v.Hit = false;
            }

            _pathes.Clear();

            _operationLimit = 500;
        }

        private bool IsIndexCorrect(int i) =>
            i >= 0 && i < vertex.Length;
    }
}

