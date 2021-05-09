using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using AmazonQuestions.Easy;

namespace AmazonQuestions.Medium
{
    // https://leetcode.com/problems/connecting-cities-with-minimum-cost/
    public class MinimumCost: TestQuestion<int, int[][], int>
    {
        public override (int arg1, int[][] arg2)[] TestCases => new[]
        {
            (
                3,
                new[]
                {
                    new[] { 1, 2, 5 },
                    new[] { 1, 3, 6 },
                    new[] { 2, 3, 1 },
                }
            ),
            (
                4,
                new[]
                {
                    new[] { 1, 2, 3 },
                    new[] { 3, 4, 4 },
                }
            ),
            (
                6,
                new[]
                {
                    new[] { 1, 2, 1 },
                    new[] { 1, 6, 14 },
                    new[] { 6, 5, 9 },
                    new[] { 5, 4, 6 },
                    new[] { 4, 2, 15 },
                    new[] { 3, 6, 2 },
                    new[] { 3, 1, 9 },
                    new[] { 3, 2, 10 },
                    new[] { 3, 4, 11 },
                }
            ),
            (
                5,
                new[]
                {
                    new[] { 2, 1, 3267 },
                    new[] { 3, 2, 25910 },
                    new[] { 4, 1, 30518 }
                }
            )
        };

        public override int[] TestAnswers => new[] { 6, -1, 27, -1};

        /*
         * Solution obtained and ported from: https://shareablecode.com/snippets/connecting-cities-with-minimum-cost-python-solution-leetcode-7Uuq-8mfn
         * Watched/read some tutorials on UnionFind/DisjointSet data structures and how they are
         * an efficient algorithm for monitoring the connectivity of sets with one another
         */

        private class UnionFind
        {
            public int[] Set;
            public int Count;

            public UnionFind(int count)
            {
                Count = count;
                Set = Enumerable.Range(0, count).ToArray();
            }

            public int Find(int node)
            {
                if (Set[node] != node)
                    Set[node] = Find(Set[node]);

                return Set[node];
            }

            public bool MakeUnion(int x, int y)
            {
                var (px, py) = (Find(x), Find(y));
                if (px == py)
                    return false;

                Set[Math.Min(px, py)] = Math.Max(px, py);
                Count--;

                return true;
            }
        }

        public override int Solution(int N, int[][] connections)
        {
            Array.Sort(connections, (l, r) => l[2].CompareTo(r[2]));
            var uf = new UnionFind(N);
            var cost = 0;
            foreach (var connection in connections)
                if (uf.MakeUnion(connection[0] - 1, connection[1] - 1))
                    cost += connection[2];

            return uf.Count == 1 ? cost : -1;
        }
    }
}