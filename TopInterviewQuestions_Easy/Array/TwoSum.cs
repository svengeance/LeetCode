using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace TopInterviewQuestions_Easy.Array
{
    public class TwoSum
    {
        public int[] Solution(int[] arr, int target)
        {
            var previous = new Dictionary<int, int>(arr.Length);
            for (int i = 0; i < arr.Length; i++)
            {
                if (i == 0)
                {
                    previous[arr[i]] = i;
                    continue;
                }
                var seek = target - arr[i];
                if (previous.TryGetValue(seek, out var found))
                {
                    return new[] { found, i };
                }
                previous[arr[i]] = i;
            }

            return default;
        }

        [Theory]
        [InlineData(new[] { 2, 7, 11, 15 }, 9, new[] { 0, 1 })]
        [InlineData(new[] { 5, 10, 3, 1 }, 4, new[] { 2, 3 })]
        [InlineData(new[] { -1, -5, 3, 1, -13 }, -14, new[] { 0, 4 })]
        public void TestTwoSum(int[] arr, int target, int[] expected)
        {
            // Given

            // When
            var components = Solution(arr, target);

            // Then
            Assert.Equal(2, components.Length);
            var second = arr[components[1]];
            var first = arr[components[0]];
            Assert.Equal(expected, new [] { components[0], components[1] });
            Assert.Equal(target, first + second);
        }
    }
}
