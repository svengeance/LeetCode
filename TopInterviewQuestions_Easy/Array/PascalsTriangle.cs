using System.Collections.Generic;
using Xunit;

namespace TopInterviewQuestions_Easy.Array
{
    public class PascalsTriangle
    {
        // Note: code was slower than the average _probably_ because I had branching in my inner loop
        public List<List<int>> Solution(int numRows)
        {
            if (numRows == 0)
                return new List<List<int>>();

            var start = new List<int> {1};
            var result = new List<List<int>> { start };
            for (int i = 1; i < numRows; i++)
            {
                var tmp = new List<int>();
                var prev = result[i - 1];
                for (int j = 0; j <= prev.Count; j++)
                {
                    if (j == 0 || j == prev.Count)
                        tmp.Add(prev[0]);
                    else
                        tmp.Add(prev[j] + prev[j - 1]);
                }
                result.Add(tmp);
            }

            return result;
        }

        [Theory]
        [InlineData()]
        public void TestSolution()
        {
            // Given
            var expected = new List<List<int>>
            {
                new List<int> {1},
                new List<int> {1, 1},
                new List<int> {1, 2, 1},
                new List<int> {1, 3, 3, 1},
                new List<int> {1, 4, 6, 4, 1},
                new List<int> {1, 5, 10, 10, 5, 1},
                new List<int> {1, 6, 15, 20, 15, 6, 1},
            };
            // When
            var output = Solution(7);
            // Then
            Assert.Equal(expected, output);
        }
    }
}