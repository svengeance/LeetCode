using Xunit;

namespace TopInterviewQuestions_Easy.Array
{
    public class RotateImage
    {
        public void Solution(int[][] matrix)
        {

        }

        [Theory]
        [InlineData()]
        public void TestSolution()
        {
            // Given
            var matrix = new int[][]
            {
                new int[] { 5, 1, 9, 11 },
                new int[] { 2, 4, 8, 10 },
                new int[] { 13, 3, 6, 7 },
                new int[] { 15, 14, 12, 16 }
            };

            // When
            Solution(matrix);

            // Then
            var solution = new int[][]
            {
                new int[] { 15, 13, 2, 5 },
                new int[] { 14, 3, 4, 1 },
                new int[] { 12, 6, 8, 9 },
                new int[] { 16, 7, 10, 11 },
            };

            Assert.Equal(solution, matrix);
        }
    }
}