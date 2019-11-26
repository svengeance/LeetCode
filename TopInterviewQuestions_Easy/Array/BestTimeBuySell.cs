using System;
using Xunit;

namespace TopInterviewQuestions_Easy.Array
{
    public class BestTimeBuySell
    {
        public int Solution(int[] prices)
        {
            if (prices.Length < 2)
                return 0;

            int profit = 0;
            int min = prices[0],
                max = int.MinValue;

            for (var i = 1; i < prices.Length; i++)
            {
                var val = prices[i];
                int prevmin = min;
                min = Math.Min(min, val);
                max = Math.Max(max, val);

                if (min == max)
                    max = int.MinValue;

                if ((max > min && val < max) || (i == prices.Length - 1 && max > min))
                {
                    profit += max - prevmin;
                    min = val;
                    max = int.MinValue;
                }
            }

            return profit;
        }

        // Added post-submission for my own benefit. 
        // Note: When doing these graph problems, consider slope & overall goal
        public int Solution2(int[] prices)
        {
            var profit = 0;

            for (var i = 0; i < prices.Length - 1; i++)
                profit += Math.Max(0, prices[i + 1] - prices[i]);

            return profit;
        }

        [Theory]
        [InlineData(new[] { 7, 1, 5, 3, 6, 4 }, 7)]
        [InlineData(new[] { 1, 2, 3, 4, 5 }, 4)]
        [InlineData(new[] { 5, 4, 3, 2, 1 }, 0)]
        [InlineData(new[] { 1, 2, 9, 8, 7, 6, 1, 2 }, 9)]
        public void TestSolution(int[] prices, int expected)
        {
            // Given

            // When
            var output = Solution2(prices);

            // Then
            Assert.Equal(expected, output);
        }
    }
}