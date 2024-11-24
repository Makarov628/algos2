using System.Collections.Generic;
using System.Linq;
using AlgorithmsDataStructures2;
using NUnit.Framework;

namespace AlgorithmDataStructures2Tests
{
    [TestFixture]
    public class DirectedGraphTests
    {

        [Test]
        public void Test_AddVertex()
        {
            var graph = new DirectedGraph(3);
            graph.AddVertex(3);

            Assert.That(graph.vertex[0].Value, Is.EqualTo(3));
            for (int i = 0; i < graph.m_adjacency.GetLength(0); i++)
            {
                for (int j = 0; j < graph.m_adjacency.GetLength(1); j++)
                {
                    Assert.That(graph.m_adjacency[i, j], Is.EqualTo(0));
                }
            }
        }

        [Test]
        public void Test_AddEdge()
        {
            var graph = new DirectedGraph(4);
            graph.AddVertex(0);
            graph.AddVertex(10);
            graph.AddVertex(20);
            graph.AddVertex(30);

            graph.AddEdge(1, 3);
            graph.AddEdge(2, 1);
            graph.AddEdge(2, 2);

            Assert.That(graph.m_adjacency[1, 3], Is.EqualTo(1));
            Assert.That(graph.m_adjacency[2, 1], Is.EqualTo(1));
            Assert.That(graph.m_adjacency[2, 2], Is.EqualTo(1));
        }

        [Test]
        public void Test_IsEdge()
        {
            var graph = new DirectedGraph(4);
            graph.AddVertex(0);
            graph.AddVertex(10);
            graph.AddVertex(20);
            graph.AddVertex(30);

            graph.AddEdge(1, 3);
            graph.AddEdge(2, 1);
            graph.AddEdge(2, 2);

            Assert.That(graph.IsEdge(1, 3), Is.True);
            Assert.That(graph.IsEdge(2, 1), Is.True);
            Assert.That(graph.IsEdge(2, 2), Is.True);
            Assert.That(graph.IsEdge(0, 1), Is.False);
        }

        [Test]
        public void Test_RemoveEdge()
        {
            var graph = new DirectedGraph(4);
            graph.AddVertex(0);
            graph.AddVertex(10);
            graph.AddVertex(20);
            graph.AddVertex(30);

            graph.AddEdge(1, 3);
            graph.AddEdge(2, 1);
            graph.AddEdge(2, 2);

            graph.RemoveEdge(1, 3);

            Assert.That(graph.m_adjacency[1, 3], Is.EqualTo(0));
            Assert.That(graph.m_adjacency[2, 1], Is.EqualTo(1));
            Assert.That(graph.m_adjacency[2, 2], Is.EqualTo(1));
        }

        [Test]
        public void Test_RemoveVertex()
        {
            var graph = new DirectedGraph(4);
            graph.AddVertex(0);
            graph.AddVertex(10);
            graph.AddVertex(20);
            graph.AddVertex(30);

            graph.AddEdge(1, 3);
            graph.AddEdge(2, 1);
            graph.AddEdge(2, 2);

            graph.RemoveVertex(2);

            Assert.That(graph.vertex[2], Is.Null);
            Assert.That(graph.m_adjacency[2, 1], Is.EqualTo(0));
            Assert.That(graph.m_adjacency[2, 2], Is.EqualTo(0));
        }

        [Test]
        public void Test_IsCyclic()
        {
            var graph = new DirectedGraph(4);
            graph.AddVertex(0);
            graph.AddVertex(10);
            graph.AddVertex(20);
            graph.AddVertex(30);

            graph.AddEdge(1, 3);
            graph.AddEdge(2, 1);
            Assert.That(graph.IsCyclic(), Is.False);

            graph.AddEdge(0, 0);
            Assert.That(graph.IsCyclic(), Is.True);

            graph.RemoveEdge(0, 0);
            graph.AddEdge(2, 3);
            Assert.That(graph.IsCyclic(), Is.False);
        }
    }
}