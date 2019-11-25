using Xunit;

namespace TopInterviewQuestions_Easy.Array
{
    public class MergeSortedArray
    {
        public void Solution(int[] first, int firstLen, int[] second, int secondLen)
        {
            if (secondLen == 0)
                return;

            if (firstLen == 0)
            {
                System.Array.Copy(second, first, secondLen);
                return;
            }

            var result = new int[firstLen + secondLen];
            int idx = 0;
            int jdx = 0;
            while (idx + jdx < result.Length)
            {
                if (jdx == secondLen)
                {
                    result[idx + jdx] = first[idx];
                    idx++;
                    continue;
                }
                if (idx == firstLen)
                {
                    result[idx + jdx] = second[jdx];
                    jdx++;
                    continue;
                }
                var val1 = first[idx];
                var val2 = second[jdx];
                if (val1 > val2)
                {
                    result[idx + jdx] = val2;
                    jdx++;
                }
                else
                {
                    result[idx + jdx] = val1;
                    idx++;
                }
            }

            System.Array.Copy(result, first, result.Length);
        }

        [Theory]
        [InlineData(new[] { 1, 2, 3, 0, 0, 0 }, 3, new[] { 2, 5, 6 }, 3, new[] { 1, 2, 2, 3, 5, 6 })]
        [InlineData(new[] { 1, 2, 0 }, 2, new[] { 3 }, 1, new[] { 1, 2, 3 })]
        [InlineData(new[] { 1, 2, 3 }, 3, new int[] { }, 0, new[] { 1, 2, 3 })]
        [InlineData(new[] { 0, 0, 0 }, 0, new[] { 4, 12, 15 }, 3, new[] { 4, 12, 15 })]
        [InlineData(new[] { 2, 0 }, 1, new[] { 1 }, 1, new[] { 1, 2 })]
        public void TestSolution(int[] first, int firstLen, int[] second, int secondLen, int[] expected)
        {
            // Given

            // When
            Solution(first, firstLen, second, secondLen);

            // Then
            Assert.Equal(expected, first);
        }
    }
}