using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using Newtonsoft.Json;

namespace AmazonQuestions.Medium
{
    public class OrangesRotting: TestQuestion<int[][], int>
    {
        public override int[][][] TestCases => new[]
        {
            new[]
            {
                new[] { 2, 1, 1 },
                new[] { 1, 1, 0 },
                new[] { 0, 1, 1 },
            },
            new[]
            {
                new[] { 2, 1, 1 },
                new[] { 0, 1, 1 },
                new[] { 1, 0, 1 }
            },
            new[]
            {
                new[] { 0, 2 }
            },
            new[]
            {
                new[] { 0, 0, 0 }
            },
            new []
            {
                new [] { 1, 0, 2 }
            },
            new []
            {
                new [] { 2, 0 },
                new [] { 0, 1 }
            },
            new []
            {
                new [] { 2, 1 },
                new [] { 0, 1 }
            },
            new []
            {
                new [] { 1 },
                new [] { 2 }
            },
            new []
            {
                new[] { 2,0,1,1,1,1,1,1,1,1 },
                new[] { 1,0,1,0,0,0,0,0,0,1 },
                new[] { 1,0,1,0,1,1,1,1,0,1 },
                new[] { 1,0,1,0,1,0,0,1,0,1 },
                new[] { 1,0,1,0,1,0,0,1,0,1 },
                new[] { 1,0,1,0,1,1,0,1,0,1 },
                new[] { 1,0,1,0,0,0,0,1,0,1 },
                new[] { 1,0,1,1,1,1,1,1,0,1 },
                new[] { 1,0,0,0,0,0,0,0,0,1 },
                new[] { 1,1,1,1,1,1,1,1,1,1 }
            }
        };

        public override int[] TestAnswers => new[] { 4, -1, 0, 0, -1, -1, 2, 1, 58 };

        private class Orange
        {
            public readonly int x, y;
            public bool IsRotten;

            public static Orange NullOrange = new Orange(-1, -1, false);

            public Orange(int x, int y, bool isRotten)
            {
                this.x = x;
                this.y = y;
                IsRotten = isRotten;
            }

            public static bool operator ==(Orange o1, Orange o2) => (o1.x, o1.y, o1.IsRotten) == (o2.x, o2.y, o2.IsRotten);
            public static bool operator !=(Orange o1, Orange o2) => !(o1 == o2);

        }
        
        /*
         * Needed to sleep on this one after a failed first attempt.
         * The trick I need to learn is that - just because I have "a" hammer (disjoint set), not everything is a nail.
         *
         * A first approach that worked for 30% of the test cases - but failed the rest - wasted so much of my time.
         * I need to think about graph traversal problems FIRST as a BFS/DFS problem, and
         * really recognize WHEN it's about sets. Lesson learned.
         */
        public override int Solution(int[][] grid)
        {
            var directions = new[] { new[] { 0, 1 }, new[] { 0, -1 }, new[] { -1, 0 }, new[] { 1, 0 } };
            int rows = grid.Length;
            int cols = grid[0].Length;
            var orangesByLocation = new Dictionary<(int x, int y), Orange>(rows*cols);

            for (int y = 0; y < rows; y++)
            for (int x = 0; x < cols; x++)
            {
                var num = grid[y][x];
                if (num == 0)
                    continue;

                orangesByLocation[(x, y)] = new Orange(x, y, num == 2);
            }

            var bfsQueue = new Queue<Orange>(orangesByLocation.Values.Where(w => w.IsRotten));
            bfsQueue.Enqueue(Orange.NullOrange);

            int numDays = 0;
            while (bfsQueue.TryDequeue(out var orange))
            {
                if (orange == Orange.NullOrange && bfsQueue.Count > 0)
                {
                    numDays++;
                    bfsQueue.Enqueue(Orange.NullOrange);
                    continue;
                }

                foreach (var dir in directions)
                {
                    var newCoords = (orange.x + dir[0], orange.y + dir[1]);
                    if (orangesByLocation.TryGetValue(newCoords, out var potentialOrange))
                        if (!potentialOrange.IsRotten)
                        {
                            potentialOrange.IsRotten = true;
                            bfsQueue.Enqueue(potentialOrange);
                        }
                }
            }

            if (orangesByLocation.Values.Any(a => !a.IsRotten))
                return -1;

            return numDays;
        }
    }
}