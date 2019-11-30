using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Xunit;

namespace TopInterviewQuestions_Easy.Array
{
    public class ArrayIntersection
    {
        // Was getting worried about the verbosity of the solution but apparently I'm not far off
        // Could have done better by subtracting the occurrences of the first dictionary rather than constructing
        //      a second one
        public int[] Solution(int[] nums1, int[] nums2)
        {
            if (nums1.Length == 0 || nums2.Length == 0)
                return new int[] { };

            var dict1 = new Dictionary<int, int>(nums1.Length);
            var dict2 = new Dictionary<int, int>(nums2.Length);

            foreach (var num in nums1)
                dict1[num] = dict1.TryGetValue(num, out var occ) ? occ + 1 : 1;

            foreach (var num in nums2)
                dict2[num] = dict2.TryGetValue(num, out var occ) ? occ + 1 : 1;

            var result = new List<int>();
            foreach (var kvp in dict1)
            {
                if (dict2.TryGetValue(kvp.Key, out var dict2Occ))
                {
                    for (int i = 0; i < Math.Min(kvp.Value, dict2Occ); i++)
                        result.Add(kvp.Key);
                }
            }

            return result.ToArray();
        }

        [Theory]
        [InlineData(new[] { 1, 2, 2, 1 }, new [] { 2, 2 }, new[] { 2, 2 })]
        [InlineData(new[] { 4, 9, 5 }, new [] { 9, 4, 9, 8, 4 }, new[] { 4, 9 })]
        public void TestSolution(int[] nums1, int[] nums2, int[] expected)
        {
            // Given

            // When
            var answer = Solution(nums1, nums2);

            // Then
            Assert.Equal(expected, answer);
        }
    }
}