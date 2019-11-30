using System;
using Xunit;

namespace TopInterviewQuestions_Easy.Array
{
    public class SingleNumber
    {
        public int Solution(int[] nums)
        {
            if (nums.Length == 1)
                return nums[0];

            System.Array.Sort(nums);
            for (int i = 0; i < nums.Length; i += 2)
            {
                if (i == nums.Length - 1 || nums[i] != nums[i + 1])
                    return nums[i];
            }

            return 0;
        }

        // Added posthumously - XOR operator. Interesting way to do remove duplicates
        public int Solution2(int[] nums)
        {
            int answer = 0;
            foreach (var num in nums)
                answer ^= num;

            return answer;
        }

        [Theory]
        [InlineData(new [] { 2, 2, 1 }, 1)]
        [InlineData(new [] { 4, 1, 2, 1, 2 }, 4)]
        public void TestSolution(int[] nums, int expected)
        {
            // Given

            // When
            var answer = Solution2(nums);
            // Then
            Assert.Equal(expected, answer);
        }
    }
}