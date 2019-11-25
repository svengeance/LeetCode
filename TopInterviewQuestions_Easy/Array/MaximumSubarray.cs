using System;
using System.Linq;
using System.Threading.Tasks.Sources;
using Xunit;
using Xunit.Sdk;

namespace TopInterviewQuestions_Easy.Array
{
    public class MaximumSubarray
    {
        public int Solution(int[] arr)
        {
            if (arr.Length == 0)
                return 0;

            if (arr.Length == 1)
                return arr[0];

            var max = arr[0];
            var localMax = arr[0];

            for (var i = 1; i < arr.Length; i++)
            {
                var curr = arr[i];

                localMax = Math.Max(curr, localMax + curr);
                max = Math.Max(localMax, max);
            }

            return max;
        }

        public int Solution2(int[] arr)
        {
            int sum = 0;
            int tmp = int.MinValue;
            foreach (int i in arr)
            {
                sum += i;
                if (sum > tmp)
                    tmp = sum;

                if (sum < 0)
                    sum = 0;
            }

            return tmp;
        }

        [Theory]
        [InlineData(new[] { -2, 1, -3, 4, -1, 2, 1, -5, 4 }, 6)]
        [InlineData(new[] { 5, 1, -5, 2, 3, -6, -6, 10 }, 10)]
        [InlineData(new[] { -1, -5, -19, -1 }, -1)]
        [InlineData(new[] { -1 }, -1)]
        [InlineData(new[] { -1, -2 }, -1)]
        [InlineData(new[] { 10, 10, -20, 5 }, 20)]
        public void TestSolution(int[] arr, int sum)
        {
            // Given

            // When
            var calculated = Solution2(arr);

            // Then
            Assert.Equal(sum, calculated);
        }
    }
}