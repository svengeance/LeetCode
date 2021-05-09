using System;
using System.Linq;

namespace AmazonQuestions.Medium
{
    // https://leetcode.com/problems/number-of-connected-components-in-an-undirected-graph/
    public class CountComponents: TestQuestion<int, int[][], int>
    {
        public override (int arg1, int[][] arg2)[] TestCases => new[]
        {
            (
                5,
                new[]
                {
                    new[] { 0, 1 },
                    new[] { 1, 2 },
                    new[] { 3, 4 },
                }
            ),
            (
                5,
                new[]
                {
                    new[] { 0, 1 },
                    new[] { 1, 2 },
                    new[] { 2, 3 },
                    new[] { 3, 4 },
                }
            ),
        };

        public override int[] TestAnswers => new[] { 2, 1 };

        private class UnionFind
        {
            public int Count;
            private int[] Roots;

            public UnionFind(int count)
            {
                Count = count;
                Roots = Enumerable.Range(0, count).ToArray();
            }

            public int Find(int x)
            {
                if (Roots[x] != x)
                    Roots[x] = Find(Roots[x]);

                return Roots[x];
            }

            public void Union(int x, int y)
            {
                var (px, py) = (Find(x), Find(y));

                if (px == py)
                    return;

                Count--;
                Roots[Math.Min(px, py)] = Math.Max(px, py);
            }
        }

        public override int Solution(int n, int[][] edges)
        {
            var uf = new UnionFind(n);
            foreach (var edge in edges)
                uf.Union(edge[0], edge[1]);

            return uf.Count;
        }
    }
}