using System;
using System.Collections;
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

        public List<Vertex<T>> WeakVertices()
        {
            // возвращает список узлов вне треугольников
            var list = new List<Vertex<T>>();

            // 0. начинаем итерацию по всем узлам vertex, для каждого:
            foreach (var v in vertex)
            {
                if (IsWeak(v))
                    list.Add(v);
            }

            return list;
        }

        private bool IsWeak(Vertex<T> current)
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



        private List<Vertex<T>> GetNeighbours(Vertex<T> current)
        {
            var list = new List<Vertex<T>>();
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

        private bool HaveLink(List<Vertex<T>> neighboors, Vertex<T> current)
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


        private IEnumerable<int> GetVerticles() =>
            Enumerable.Range(0, max_vertex).Where(v => vertex[v] != null);

        private IEnumerable<int> GetNeighbours(int v) =>
            GetVerticles()
                .Where(vi => vi != v && IsEdge(v, vi));

        private IEnumerable<int> GetNeighboursIfStrong(int v)
        {
            var neighbours = GetNeighbours(v).ToArray();
            if (neighbours.Length >= 2)
                return neighbours;

            return Array.Empty<int>();
        }

        private bool IsTriangle(int v1, int v2, int v3) =>
            IsEdge(v1, v2) && IsEdge(v2, v3) && IsEdge(v3, v1);

        public int CountTriangles()
        {
            var trianglePaths = new HashSet<string>();

            foreach (var v1 in GetVerticles())
                foreach (var v2 in GetNeighboursIfStrong(v1))
                    foreach (var v3 in GetNeighbours(v2))
                        if (IsTriangle(v1, v2, v3))
                            trianglePaths.Add(string.Join("-", new[] { v1, v2, v3 }.OrderBy(x => x)));

            return trianglePaths.Count;
        }

        public List<Vertex<T>> WeakVerticesStrassen()
        {
            // 1. Получаем A2 = A * A
            int n = max_vertex;
            int[,] A = m_adjacency;
            int[,] A2 = StrassenMultiply(A, A, n);

            // 2. Получаем A3 = A2 * A
            int[,] A3 = StrassenMultiply(A2, A, n);

            // 3. Проверяем диагональ A3. Если A3[i,i] == 0, вершина i слабая.
            var result = new List<Vertex<T>>();
            for (int i = 0; i < n; i++)
            {
                if (vertex[i] != null && A3[i, i] == 0)
                    result.Add(vertex[i]);
            }

            return result;
        }

        // Предполагаем, что n - степень двойки.
        // Если не степень двойки, нужно дополнять матрицу до нужного размера.
        private int[,] StrassenMultiply(int[,] A, int[,] B, int n)
        {
            // если n=1, просто умножаем скалярно
            if (n == 1)
            {
                int[,] C = new int[1, 1];
                C[0, 0] = A[0, 0] * B[0, 0];
                return C;
            }

            int newSize = n / 2;

            // Разбиваем матрицу на 4 подматрицы
            int[,] A11 = new int[newSize, newSize];
            int[,] A12 = new int[newSize, newSize];
            int[,] A21 = new int[newSize, newSize];
            int[,] A22 = new int[newSize, newSize];

            int[,] B11 = new int[newSize, newSize];
            int[,] B12 = new int[newSize, newSize];
            int[,] B21 = new int[newSize, newSize];
            int[,] B22 = new int[newSize, newSize];

            Split(A, A11, 0 , 0);
            Split(A, A12, 0 , newSize);
            Split(A, A21, newSize, 0);
            Split(A, A22, newSize, newSize);

            Split(B, B11, 0 , 0);
            Split(B, B12, 0 , newSize);
            Split(B, B21, newSize, 0);
            Split(B, B22, newSize, newSize);

            // Вычисляем 7 произведений по Штрассену:
            var M1 = StrassenMultiply(Add(A11, A22, newSize), Add(B11, B22, newSize), newSize);
            var M2 = StrassenMultiply(Add(A21, A22, newSize), B11, newSize);
            var M3 = StrassenMultiply(A11, Sub(B12, B22, newSize), newSize);
            var M4 = StrassenMultiply(A22, Sub(B21, B11, newSize), newSize);
            var M5 = StrassenMultiply(Add(A11, A12, newSize), B22, newSize);
            var M6 = StrassenMultiply(Sub(A21, A11, newSize), Add(B11, B12, newSize), newSize);
            var M7 = StrassenMultiply(Sub(A12, A22, newSize), Add(B21, B22, newSize), newSize);

            // C11, C12, C21, C22
            var C11 = Add(Sub(Add(M1, M4, newSize), M5, newSize), M7, newSize);
            var C12 = Add(M3, M5, newSize);
            var C21 = Add(M2, M4, newSize);
            var C22 = Add(Sub(Add(M1, M3, newSize), M2, newSize), M6, newSize);

            int[,] Cn = new int[n,n];
            Join(C11, Cn, 0 , 0);
            Join(C12, Cn, 0 , newSize);
            Join(C21, Cn, newSize, 0);
            Join(C22, Cn, newSize, newSize);

            return Cn;
        }

        private void Split(int[,] P, int[,] C, int iB, int jB) 
        {
            for (int i = 0; i < C.GetLength(0); i++)
                for (int j = 0; j < C.GetLength(1); j++)
                    C[i,j] = P[iB + i, jB + j];
        }

        private void Join(int[,] C, int[,] P, int iB, int jB)
        {
            for (int i = 0; i < C.GetLength(0); i++)
                for (int j = 0; j < C.GetLength(1); j++)
                    P[iB + i, jB + j] = C[i,j];
        }

        private int[,] Add(int[,] A, int[,] B, int n)
        {
            int[,] R = new int[n,n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    R[i,j] = A[i,j] + B[i,j];
            return R;
        }

        private int[,] Sub(int[,] A, int[,] B, int n)
        {
            int[,] R = new int[n,n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    R[i,j] = A[i,j] - B[i,j];
            return R;
        }
    }

    public static class GraphHelper
    {
        public static List<Vertex<T>> WeakVerticles<T>(SimpleGraph<T> graph)
        {
            var weakVerticles = new List<Vertex<T>>();

            for (int v = 0; v < graph.max_vertex; v++)
            {
                if (IsWeak(graph, v))
                    weakVerticles.Add(graph.vertex[v]);
            }

            return weakVerticles;
        }

        private static bool IsWeak<T>(SimpleGraph<T> graph, int vertexIndex)
        {
            var neighbours = GetNeighbours(graph, vertexIndex);

            if (neighbours.Count < 2)
                return true;

            foreach (var n in neighbours)
                if (HaveLink(graph, neighbours, n))
                    return false;

            return true;
        }


        private static List<int> GetNeighbours<T>(SimpleGraph<T> graph, int vertexIndex)
        {
            var neighbours = new List<int>();

            for (int i = 0; i < graph.max_vertex; i++)
                if (i != vertexIndex && graph.IsEdge(vertexIndex, i))
                    neighbours.Add(i);

            return neighbours;
        }

        private static bool HaveLink<T>(SimpleGraph<T> graph, List<int> neighbours, int neighbourIndex)
        {
            for (int i = 0; i < graph.max_vertex; i++)
                // если текущий имеет ребро и neighbours содержит этот узел - значит треугольник есть
                if (i != neighbourIndex && graph.IsEdge(neighbourIndex, i) && neighbours.Contains(i))
                    return true;

            return false;
        }
    }
}