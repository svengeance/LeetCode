using System;
using System.Linq;
using System.Net.NetworkInformation;

namespace AmazonQuestions.Medium
{
    // https://leetcode.com/problems/meeting-rooms-ii
    public class MinMeetingRooms: TestQuestion<int[][], int>
    {
        public override int[][][] TestCases => new[]
        {
            new[]
            {
                new[] { 0, 30 }, new[] { 5, 10 }, new[] { 15, 20 }
            },
            new[]
            {
                new[] { 7, 10 }, new[] { 2, 4 }
            },
            new[]
            {
                new[] { 1, 2 }, new[] { 2, 3 }
            },
            new []
            {
                new [] { 1, 30 }, new [] { 2, 30 }, new [] { 3, 30 }, new [] { 4, 30 }, new [] { 29, 30 }
            }
        };

        public override int[] TestAnswers => new[]
        {
            2, 1, 1, 5
        };

        public override int Solution(int[][] intervals)
        {
            var startTimes = new int[intervals.Length];
            var endTimes = new int[intervals.Length];
            for (int i = 0; i < intervals.Length; i++)
            {
                var interval = intervals[i];
                startTimes[i] = interval[0];
                endTimes[i] = interval[1];
            }

            Array.Sort(startTimes);
            Array.Sort(endTimes);
            
            var maxCollisions = 0;
            var collisions = 0;
            var numClosed = 0;
            foreach (var start in startTimes)
            {
                collisions++;

                if (endTimes[numClosed] <= start)
                {
                    collisions--;
                    numClosed++;
                }

                maxCollisions = Math.Max(collisions, maxCollisions);
            }

            return maxCollisions;
        }
    }
}