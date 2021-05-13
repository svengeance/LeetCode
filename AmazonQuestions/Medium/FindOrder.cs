using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using NUnit.Framework;

namespace AmazonQuestions.Medium
{
    // https://leetcode.com/problems/course-schedule-ii
    public class FindOrder: TestQuestion<int, int[][], int[]>
    {
        public override (int arg1, int[][] arg2)[] TestCases => new[]
        {
            (
                2,
                new[]
                {
                    new[] { 1, 0 }
                }
            ),
            (
                4,
                new[]
                {
                    new[] { 1, 0 },
                    new[] { 2, 0 },
                    new[] { 3, 1 },
                    new[] { 3, 2 }
                }
            ),
            (
                1,
                new int[0][]
            ),
            (
                6,
                new[]
                {
                    new[] { 1, 5 },
                    new[] { 2, 4 }
                }
            ),
            (
                3,
                new[]
                {
                    new[] { 0, 1 },
                    new[] { 1, 2 },
                    new[] { 2, 0 }
                }
            ),
            (
                4,
                new[]
                {
                    new[] { 3, 0 },
                    new[] { 0, 1 }
                }
            ),
            (
                3,
                new[]
                {
                    new[] { 1, 0 },
                    new[] { 2, 0 },
                    new[] { 0, 2 }
                }
            )
        };

        public override int[][] TestAnswers => new[]
        {
            new[] { 0, 1 },
            new[] { 0, 2, 1, 3 },
            new[] { 0 },
            new[] { 5, 4, 3, 2, 1, 0 },
            Array.Empty<int>(),
            new[] { 2, 1, 0, 3 },
            Array.Empty<int>()
        };

        public class Node
        {
            public bool Visited;
            public int Id;

            public List<Edge> Edges = new List<Edge>();

            public Node(int id) => Id = id;

            public void AddEdge(Node toNode) => Edges.Add(new Edge(this, toNode));
        }

        public class Edge
        {
            public bool Visited = false;

            public Node From;
            public Node To;

            public Edge(Node @from, Node to)
            {
                From = @from;
                To = to;
            }

            public override string ToString() => $"{From.Id} ({Visited}) -> {To.Id} ({Visited})";
        }

        public override int[] Solution(int numCourses, int[][] prerequisites)
        {
            var nodesById = Enumerable.Range(0, numCourses)
                                      .Select(s => new Node(s))
                                      .ToDictionary(k => k.Id);

            foreach (var prereq in prerequisites)
                nodesById[prereq[1]].AddEdge(nodesById[prereq[0]]);

            // Perform cycle check
            bool HasCycle (Node node, HashSet<int> visitedRecursive)
            {
                node.Visited = true;
                visitedRecursive.Add(node.Id);

                foreach (var edge in node.Edges)
                {
                    if (!edge.To.Visited && HasCycle(edge.To, visitedRecursive))
                        return true;
                    else if (visitedRecursive.Contains(edge.To.Id))
                        return true;
                }

                visitedRecursive.Remove(node.Id);
                return false;
            }

            var recursiveVisited = new HashSet<int>();
            foreach (var node in nodesById.Values)
                if (HasCycle(node, recursiveVisited))
                    return Array.Empty<int>();

            // Perform topsort
            foreach (var node in nodesById.Values)
                node.Visited = false;

            var topSort = new Stack<Node>(numCourses);

            void TopSort(Node n)
            {
                n.Visited = true;
                foreach (var edge in n.Edges)
                {
                    if (edge.To.Visited)
                        continue;

                    TopSort(edge.To);
                }

                topSort.Push(n);
            }

            foreach (var node in nodesById.Values)
                if (!node.Visited)
                    TopSort(node);

            return topSort.Select(s => s.Id).ToArray();
        }
    }
}