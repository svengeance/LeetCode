using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;

namespace AmazonQuestions.Medium
{
    // https://leetcode.com/problems/number-of-islands
    public class NumIslands: TestQuestion<char[][], int>
    {
        public override char[][][] TestCases => new[]
        {
            new[]
            {
                new[] { '1', '1', '1', '1', '0' },
                new[] { '1', '1', '0', '1', '0' },
                new[] { '1', '1', '0', '0', '0' },
                new[] { '0', '0', '0', '0', '0' },
            },
            new[]
            {
                new[] { '1', '1', '0', '0', '0' },
                new[] { '1', '1', '0', '0', '0' },
                new[] { '0', '0', '1', '0', '0' },
                new[] { '0', '0', '0', '1', '1' },
            },
            new []
            {
                new [] { '0'}
            },
            new []
            {
                new [] { '1'}
            },
            new []
            {
                new [] { '1', '0', '1' }
            },
            new []
            {
                new [] { '1', '0', '1' },
                new [] { '0', '1', '0' },
                new [] { '1', '0', '1' }
            }
        };
        public override int[] TestAnswers => new[] { 1, 3, 0, 1, 2, 5 };

        // Feels good to have so many uses for this.
        // The trick to this problem was recognizing that it's a disjoint set question,
        // but having this class memorized is pretty nifty.
        private class DisjointSet
        {
            public int Count;
            public int InitialCount;
            public int[] Roots;

            public DisjointSet(int count)
            {
                InitialCount = count;
                Count = count;
                Roots = Enumerable.Range(0, count).ToArray();
            }

            public int Find(int x)
            {
                //Console.WriteLine(JsonConvert.SerializeObject(Roots));
                if (Roots[x] != x)
                    Roots[x] = Find(Roots[x]);

                return Roots[x];
            }

            public void Union(int x, int y)
            {
                var (px, py) = (Find(x), Find(y));

                if (px == py)
                    return;

                Roots[Math.Min(px, py)] = Math.Max(px, py);
                Count--;
            }
        }

        public override int Solution(char[][] grid)
        {
            var landCount = 0;
            var set = new DisjointSet(grid.Length * grid[0].Length);

            for (int y = 0; y < grid.Length; y++)
            for (int x = 0; x < grid[0].Length; x++)
            {
                var index = (y * grid[0].Length) + x;
                var piece = grid[y][x];
                if (piece == '0')
                    continue;

                landCount++;

                var upIndex = index - grid[0].Length;
                var downIndex = index + grid[0].Length;
                var leftIndex = index - 1;
                var rightIndex = index + 1;

                // Look up
                if (y > 0 && grid[y - 1][x] == '1')
                    set.Union(index, upIndex);
                // Look Down
                if (y < grid.Length - 1 && grid[y + 1][x] == '1')
                    set.Union(index, downIndex);
                // Look Left
                if (x > 0 && grid[y][x -1] == '1')
                    set.Union(index, leftIndex);
                if (x < grid[0].Length - 1 && grid[y][x + 1] == '1')
                    set.Union(index, rightIndex);
            }

            var numJoins = set.InitialCount - set.Count;
            var numConnections = landCount - numJoins;
            return numConnections;
        }
    }
}