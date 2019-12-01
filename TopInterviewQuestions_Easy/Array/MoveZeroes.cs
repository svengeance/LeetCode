using Xunit;

namespace TopInterviewQuestions_Easy.Array
{
    public class MoveZeroes
    {
        public void Solution(int[] nums)
        {
            int toMove = 0;

            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] == 0)
                {
                    toMove++;
                    continue;
                }

                nums[i - toMove] = nums[i];
            }

            for (int i = nums.Length - toMove; i < nums.Length; i++)
            {
                nums[i] = 0;
            }
        }

        [Theory]
        [InlineData(new[] { 0, 1, 0, 3, 12 }, new[] { 1, 3, 12, 0, 0 })]
        [InlineData(new[] { 0, 0, 0 }, new[] { 0, 0, 0 })]
        [InlineData(new[] { 1, 2, 3, 0, 1, 2, 3, 0, 1 }, new[] { 1, 2, 3, 1, 2, 3, 1, 0, 0 })]
        public void TestSolution(int[] nums, int[] expected)
        {
            // Given

            // When
            Solution(nums);

            // Then
            Assert.Equal(expected, nums);
        }
    }
}