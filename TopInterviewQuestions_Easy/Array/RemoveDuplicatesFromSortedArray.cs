using System;
using System.Linq;
using Xunit;

namespace TopInterviewQuestions_Easy
{
    public class RemoveDuplicatesFromSortedArray
    {
        public int Solution(int[] arr)
        {
            if (arr.Length == 0)
                return 0;

            var lastSeen = arr[0];
            var numDuplicates = 0;
            var freshIndex = 1;
            for (var i = 1; i < arr.Length; i++)
            {
                if (lastSeen != arr[i])
                    arr[freshIndex++] = arr[i];
                else
                    numDuplicates++;
                lastSeen = arr[i];
            }

            return freshIndex;
        }

        [Theory]
        [InlineData(new[] { 1, 2, 3 }, 3)]
        [InlineData(new[] { 1, 1, 2 }, 2)]
        [InlineData(new[] { 0 }, 1)]
        [InlineData(new[] { 0, 0, 1, 1, 1, 2, 2, 3, 3, 4 }, 5)]
        public void TestRemoveDuplicatesFromSortedArray(int[] arr, int expected)
        {
            // Given

            // When
            var len = Solution(arr);

            // Then
            Assert.Equal(expected, len);
            Assert.True(len < arr.Length + 1);
            Assert.All(arr.Take(len), i => Assert.Contains(i, arr));
            Assert.Equal(len, arr.Take(len).ToHashSet().Count);
        }
    }
}
