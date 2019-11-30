using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using Xunit;

namespace TopInterviewQuestions_Easy.Array
{
    public class ContainsDuplicate
    {
        // Probably too cheeky
        public bool Solution(int[] nums)
        {
            return new HashSet<int>(nums).Count != nums.Length;
        }

        public bool Solution2(int[] nums)
        {
            var set = new HashSet<int>(nums.Length);
            foreach (var num in nums)
            {
                if (set.Contains(num))
                    return true;

                set.Add(num);
            }

            return false;
        }

        [Theory]
        [InlineData(new[] { 1, 2, 3, 1 }, true )]
        [InlineData(new[] { 1, 2, 3, 4 }, false )]
        public void TestSolution(int[] nums, bool expected)
        {
            // Given

            // When
            var answer = Solution2(nums);
            // Then
            Assert.Equal(expected, answer);
        }
    }
}