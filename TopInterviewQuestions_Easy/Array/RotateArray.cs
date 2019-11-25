using Xunit;

namespace TopInterviewQuestions_Easy.Array
{
    public class RotateArray
    {
        public void Solution(int[] arr, int k)
        {
            var shifted = new int[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                shifted[(i + k) % arr.Length] = arr[i];

            shifted.CopyTo(arr, 0);
        }

        [Theory]
        [InlineData(new[] { 1, 2, 3, 4, 5, 6, 7 }, 3, new[] { 5, 6, 7, 1, 2, 3, 4 })]
        [InlineData(new[] { 1, 2, 3 }, 1, new[] { 3, 1, 2 })]
        [InlineData(new[] { 1, 2, 3 }, 2, new[] { 2, 3, 1})]
        public void TestSolution(int[] arr, int k, int[] expected)
        {
            // Given

            // When
            Solution(arr, k);

            // Then
            Assert.Equal(expected, arr);
        }
    }
}