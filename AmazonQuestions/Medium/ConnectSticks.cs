using System;
using System.Collections.Generic;
using System.Linq;

namespace AmazonQuestions.Medium
{
    //https://leetcode.com/problems/minimum-cost-to-connect-sticks/submissions/
    public class ConnectSticks: TestQuestion<int[], int>
    {
        public override int[][] TestCases => new[]
        {
            new[] { 2, 4, 3 },
            new[] { 1, 8, 3, 5 },
            new[] { 5 },
            new[] { 3354, 4316, 3259, 4904, 4598, 474, 3166, 6322, 8080, 9009 }
        };
        // 2, 2 + 3 + (4 + 2 + 3)
        // 1 + 3 + (5 + 1 + 3) + (8 + 3 + 5 + 1)
        // x + x+y + x+y+z

        public override int[] TestAnswers => new[]
        {
            14, 30, 0, 151646
        };

        public override int Solution(int[] sticks)
        {
            if (sticks.Length == 0)
                return 0;

            var list = sticks.ToList(); // Worst priority queue ever

            list.Sort();
            
            var cost = 0;

            while (list.Count> 1)
            {
                var first = list[0];
                var second = list[1];
                var newCost = first + second;
                cost += newCost;
                list[1] = newCost;
                list = list.Skip(1).ToList();
                list.Sort();
            }

            return cost;
        }
    }
}