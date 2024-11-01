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
        
        public List<Vertex> BreadthFirstSearch(int VFrom, int VTo)
        {
            // узлы задаются позициями в списке vertex.
            // возвращает список узлов -- путь из VFrom в VTo
            // или пустой список, если пути нету
            var path = new List<Vertex>();
            if (!IsIndexCorrect(VFrom) || !IsIndexCorrect(VTo))
                return path;
            
            ClearDataStructures();

            vertex[VFrom].Hit = true;
            // если false - путь не найден, возвращаем пустой массив
            if (!GetPathWidth(VFrom, VTo))
                return path;

            return CalculatePath(VFrom, VTo);
        }
        
        public List<Vertex> WeakVertices()
        {
            // возвращает список узлов вне треугольников
            var list = new List<Vertex>();

            // 0. начинаем итерацию по всем узлам vertex, для каждого:
            foreach (var v in vertex)
            {
                if (IsWeak(v))
                    list.Add(v);
            }

            return list;
        }

        private bool IsWeak(Vertex current)
        {
            // 1. получить все узлы соседи текущего
            var neighbours = GetNeighbours(current);

            // 2. если узлов меньше 2 - сразу возвращаем true
            if (neighbours.Count < 2)
                return true;

            // 3. для каждого узла из п.1 проверяем, есть ли сосед из списка 1, связанный ребром. Если есть - возвращаем false и передаем в эту ф-ию новый узел
            foreach (var n in neighbours)
            {
                if (HaveLink(neighbours, n))
                    return false;
            }

            return true;
        }

        private List<Vertex> GetNeighbours(Vertex current)
        {
            var list = new List<Vertex>();
            var currentIndex = Array.IndexOf(vertex, current);
            if (currentIndex == -1)
                return list;
            
            for (int i = 0; i < max_vertex; i++)
            {
                if (i != currentIndex && m_adjacency[currentIndex, i] == 1)
                    list.Add(vertex[i]);
            }

            return list;
        }

        private bool HaveLink(List<Vertex> neighboors, Vertex current)
        {
            var currentIndex = Array.IndexOf(vertex, current);
            if (currentIndex == -1)
                return false;

            for (int i = 0; i < max_vertex; i++)
            {
                // если текущий имеет ребро и neighbours содержит этот узел - значит треугольник есть
                if (i != currentIndex && m_adjacency[currentIndex, i] == 1 && neighboors.Contains(vertex[i]))
                    return true;
            }

            return false;
        }

        // Рассчитаем путь
        private List<Vertex> CalculatePath(int VFrom, int VTo)
        {
            foreach (var path in _pathes)
            {
                if (path[0] == vertex[VFrom] && path[path.Count - 1] == vertex[VTo])
                    return path;
            }

            return new List<Vertex>();
        }

        /// <summary>
        /// False if path not found, else - true
        /// </summary>
        /// <param name="X"></param>
        /// <param name="VTo"></param>
        /// <returns></returns>
        private bool GetPathWidth(int X, int VTo)
        {
            bool initial = true;
            
            while (_operationLimit > 0)
            {
                // 2) Из всех смежных с X вершин выбираем любую (например первую) непосещённую Y
                var Y = GetNotVisited(X);

                // Если выбранная вершина равна искомой, значит цель найдена, заканчиваем работу
                if (Y == VTo)
                {
                    AddToPathOrCreateNew(vertex[X], vertex[Y], initial);
                    return true;
                }

                if (Y != -1)
                {
                    // 3) Помечаем найденную смежную вершину как посещённую, помещаем в очередь. Переходим к п.2
                    vertex[Y].Hit = true;
                    _queue.Enqueue(vertex[Y]);
                    AddToPathOrCreateNew(vertex[X], vertex[Y], initial);
                    continue;
                }
                
                //Если таких вершин нет, проверяем очередь:
                // Если очередь пуста, заканчиваем работу (путь до цели не найден).
                if (_queue.Count == 0)
                    return false;
                
                // извлекаем из очереди очередной элемент, делаем его текущим X, и переходим обратно к данному п.2.
                var nextToCheck = _queue.Dequeue();
                X = Array.IndexOf(vertex, nextToCheck);
                _operationLimit--;
                initial = false;
            }

            return false;
        }

        private void AddToPathOrCreateNew(Vertex x, Vertex y, bool initial)
        {
            if (initial)
            {
                _pathes.Add(new List<Vertex>() {x, y});
                return;
            }

            // находим путь, у которого последний элемент == x (ранее добавленный y) и добавляем новый y
            foreach (var path in _pathes)
            {
                var index = path.IndexOf(x);
                
                // если элемент - последний, добавляем в текущий путь
                if (index == path.Count - 1)
                {
                    path.Add(y);
                    return;
                }
                
                // если элемент есть, но он не последний, значит пути в данном месте расходятся (уже был добавлен элемент в нужный путь)
                // поэтому нужно добавить новый путь в список путей
                if (index != -1)
                {
                    // создаем новый путь, копируя старый до нужного элемента включительно
                    var newPath = path.GetRange(0, index + 1);
                    // добавляем y в новый путь
                    newPath.Add(y);
                    // и добавляем новый путь в список путей
                    _pathes.Add(newPath);
                    return;
                }
            }
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

