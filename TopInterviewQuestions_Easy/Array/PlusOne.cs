using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Xunit;

namespace TopInterviewQuestions_Easy.Array
{
    public class PlusOne
    {
        public int[] Solution(int[] arr)
        {
            bool carry = false;

            var result = new int[arr.Length];

            for (int i = arr.Length - 1; i >= 0; i--)
            {
                var val = arr[i] + (carry ? 1 : 0) + (i == arr.Length - 1 ? 1 : 0);
                carry = val > 9;
                result[i] = val - (carry ? 10 : 0);
            }

            if (carry)
            {
                var tmp = new int[result.Length + 1];
                result.CopyTo(tmp, 1);
                tmp[0] = 1;
                result = tmp;
            }

            return result;
        }

        [Theory]
        [InlineData(new[] { 1, 2, 3 }, new[] { 1, 2, 4 })]
        [InlineData(new[] { 9, 8, 1, 2, 3 }, new[] { 9, 8, 1, 2, 4 })]
        [InlineData(new[] { 9 }, new[] { 1, 0 })]
        [InlineData(new[] { 5 }, new[] { 6 })]
        public void TestSolution(int[] arr, int[] expected)
        {
            // Given

            // When
            var result = Solution(arr);

            // Then
            Assert.Equal(expected, result);
        }
    }
}