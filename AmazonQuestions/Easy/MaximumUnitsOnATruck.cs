using System;

namespace AmazonQuestions.Easy
{
    // https://leetcode.com/problems/maximum-units-on-a-truck
    public class MaximumUnitsOnATruck: TestQuestion<int[][], int, int>
    {
        public override (int[][] arg1, int arg2)[] TestCases => new[]
        {
            (
                new[]
                {
                    new[] { 1, 3 }, new[] { 2, 2 }, new[] { 3, 1 }
                },
                4
            ),
            (
                new[]
                {
                    new[] { 5, 10 }, new[] { 2, 5 }, new[] { 4, 7 }, new[] { 3, 9 }
                },
                10
            )
        };

        public override int[] TestAnswers => new[] { 8, 91 };

        public override int Solution(int[][] arg1, int arg2)
        {
            int totalUnits = 0;
            Array.Sort(arg1, (left, right) => left[1].CompareTo(right[1]));
            for (int i = arg1.Length; i > 0; i--)
            {
                var box = arg1[i - 1];
                arg2 -= box[0];
                if (arg2 < 0) // If we passed our mark, add how many units we "could have" taken
                    return totalUnits + ((box[0] - Math.Abs(arg2)) * box[1]);
                else
                    totalUnits += (box[0] * box[1]);
            }

            return totalUnits;
        }
    }
}