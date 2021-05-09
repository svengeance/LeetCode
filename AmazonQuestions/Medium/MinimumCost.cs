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

        private class UnionSet
        {
            public int[] Set;
            public int Count;

            public UnionSet(int count)
            {
                Count = count;
                Set = Enumerable.Range(0, count).ToArray();
            }
        }

        public override int Solution(int N, int[][] connections)
        {

        }
    }
}