using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsDataStructures2
{
    public class Vertex<T>
    {
        public bool Hit; // для обхода в глубину - true - если посещённая вершина
        public T Value;
        public Vertex(T val)
        {
            Value = val;
            Hit = false;
        }
    }

    public class SimpleGraph<T>
    {
        public Vertex<T>[] vertex; // список, хранящий вершины
        public int[,] m_adjacency; // матрица смежности
        public int max_vertex;
        private Stack<Vertex<T>> _stack;
        private Queue<Vertex<T>> _queue;
        private int _operationLimit;
        private List<List<Vertex<T>>> _pathes;

        public SimpleGraph(int size)
        {
            max_vertex = size;
            m_adjacency = new int[size, size];
            vertex = new Vertex<T>[size];
            _stack = new Stack<Vertex<T>>();
            _queue = new Queue<Vertex<T>>();
            _pathes = new List<List<Vertex<T>>>();
        }

        public void AddVertex(T value)
        {
            // код добавления новой вершины 
            // с значением value 
            // в свободную позицию массива vertex

            for (int i = 0; i < max_vertex; i++)
            {
                if (vertex[i] != null)
                    continue;

                var newVertex = new Vertex<T>(value);
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

        public List<Vertex<T>> DepthFirstSearch(int VFrom, int VTo)
        {
            // Узлы задаются позициями в списке vertex.
            // Возвращается список узлов -- путь из VFrom в VTo.
            // Список пустой, если пути нету.

            var path = new List<Vertex<T>>();
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

        public List<Vertex<T>> BreadthFirstSearch(int VFrom, int VTo)
        {
            // узлы задаются позициями в списке vertex.
            // возвращает список узлов -- путь из VFrom в VTo
            // или пустой список, если пути нету
            var path = new List<Vertex<T>>();
            if (!IsIndexCorrect(VFrom) || !IsIndexCorrect(VTo))
                return path;

            ClearDataStructures();

            vertex[VFrom].Hit = true;
            // если false - путь не найден, возвращаем пустой массив
            if (!GetPathWidth(VFrom, VTo))
                return path;

            return CalculatePath(VFrom, VTo);
        }


        // Рассчитаем путь
        private List<Vertex<T>> CalculatePath(int VFrom, int VTo)
        {
            foreach (var path in _pathes)
            {
                if (path[0] == vertex[VFrom] && path[path.Count - 1] == vertex[VTo])
                    return path;
            }

            return new List<Vertex<T>>();
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

        private void AddToPathOrCreateNew(Vertex<T> x, Vertex<T> y, bool initial)
        {
            if (initial)
            {
                _pathes.Add(new List<Vertex<T>>() { x, y });
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

        private Vertex<T>[] GetPathDepth(int X, int VTo)
        {
            // 2. Фиксируем вершину X как посещённую.
            vertex[X].Hit = true;
            // 3. Помещаем вершину X в стек.
            _stack.Push(vertex[X]);

            while (_stack.Count > 0)
            {
                // 4. Ищем среди смежных вершин вершины X целевую вершину VTo. Если она найдена, записываем её в стек и возвращаем сам стек как результат работы
                for (int i = 0; i < max_vertex; i++)
                {
                    if (X != i && m_adjacency[X, i] == 1 && i == VTo)
                    {
                        _stack.Push(vertex[i]);
                        return _stack.ToArray();
                    }
                }

                // Если целевой вершины среди смежных нету, то выбираем среди смежных такую вершину, которая ещё не была посещена. Если такая вершина найдена, делаем её текущей X и переходим к п. 2.
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

            return _stack.ToArray(); // возвращаем пустой
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

        /// <summary>
        /// Очищаем все дополнительные структуры данных: делаем стек пустым, а все вершины графа отмечаем как непосещённые
        /// </summary>
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

        private void UnhitAllVertices()
        {
            foreach (var v in vertex)
            {
                if (v != null)
                    v.Hit = false;
            }
        }

        public int FindMaxDistanceInTree()
        {
            int start = -1;
            for (int i = 0; i < max_vertex; i++)
            {
                if (vertex[i] != null)
                {
                    start = i;
                    break;
                }
            }

            if (start == -1) return 0;

            // Находим самую удалённую вершину от start
            var (farthestNode, _) = BFSFarthestNode(start);
            var (_, maxDistance) = BFSFarthestNode(farthestNode);
 
            return maxDistance;
        }

        private (int node, int dist) BFSFarthestNode(int start)
        {
            UnhitAllVertices();
            var dist = Enumerable.Repeat(-1, max_vertex).ToArray();
            var queue = new Queue<int>();

            dist[start] = 0;
            vertex[start].Hit = true;
            queue.Enqueue(start);

            int farthestNode = start;
            int maxDist = 0;

            while (queue.Count > 0)
            {
                int current = queue.Dequeue();
                for (int i = 0; i < max_vertex; i++)
                {
                    if (m_adjacency[current, i] != 1 || vertex[i] == null || vertex[i].Hit)
                        continue;

                    vertex[i].Hit = true;
                    dist[i] = dist[current] + 1;
                    if (dist[i] > maxDist)
                    {
                        maxDist = dist[i];
                        farthestNode = i;
                    }
                    queue.Enqueue(i);
                }
            }

            return (farthestNode, maxDist);
        }

        private bool IsGraphEmpty() => vertex.All(v => v == null);

        public List<List<int>> FindAllCycles()
        {
            if (IsGraphEmpty())
                return new List<List<int>>();

            var all_cycles = new List<List<int>>();
            for (int i = 0; i < vertex.Length; i++)
            {
                if (vertex[i] == null)
                    continue;

                var queue = new Queue<(int nodeIndex, List<int> path)>();
                queue.Enqueue((i, new List<int> { i }));

                var cyclesFromNode = FindAllCyclesFromNode(i, queue);
                if (cyclesFromNode.Any())
                    all_cycles.AddRange(cyclesFromNode);
            }

            return all_cycles;
        }

        private List<List<int>> FindAllCyclesFromNode(int start_index, Queue<(int nodeIndex, List<int> path)> queue)
        {
            var cycles = new List<List<int>>();

            while (queue.Count > 0)
            {
                var (currentNodeIndex, currentPath) = queue.Dequeue();

                for (int i = 0; i < max_vertex; i++)
                {
                    int relation = m_adjacency[currentNodeIndex, i];
                    if (relation == 1 && i == start_index && currentPath.Count > 2)
                    {
                        var newCycle = new List<int>(currentPath);
                        newCycle.Add(start_index);
                        cycles.Add(newCycle);
                    }

                    if (relation == 1 && !currentPath.Contains(i))
                    {
                        var newPath = new List<int>(currentPath) { i };
                        queue.Enqueue((i, newPath));
                    }
                }
            }

            return cycles;
        }
    }
}