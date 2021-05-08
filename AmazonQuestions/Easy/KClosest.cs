using System;
using System.Collections.Generic;

namespace AmazonQuestions.Easy
{
    // https://leetcode.com/problems/k-closest-points-to-origin/submissions/
    public class KClosest: TestQuestion<int[][], int, int[][]>
    {
        public override (int[][] arg1, int arg2)[] TestCases => new[]
        {
            (
                new[]
                {
                    new[] { 1, 3 }, new[] { -2, 2 }
                },
                1
            ),
            (
                new[]
                {
                    new[] { 3, 3 }, new[] { 5, -1 }, new[] { -2, 4 }
                },
                2
            )
        };

        public override int[][][] TestAnswers => new[]
        {
            new[]
            {
                new[] { -2, 2 }
            },
            new[]
            {
                new[] { 3, 3 }, new[] { -2, 4 }
            }
        };

        public override int[][] Solution(int[][] points, int k)
        {
            if (k == 0)
                return Array.Empty<int[]>();

            double DistanceToOrigin(int[] point) => Math.Sqrt((point[0] * point[0]) + (point[1] * point[1]));
            Array.Sort(points, (left, right) => DistanceToOrigin(left).CompareTo(DistanceToOrigin(right)));

            return points.AsSpan(..k).ToArray();
        }
    }
}