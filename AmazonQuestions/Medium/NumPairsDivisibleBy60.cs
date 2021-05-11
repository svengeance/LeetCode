using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace AmazonQuestions.Medium
{
    // https://leetcode.com/problems/pairs-of-songs-with-total-durations-divisible-by-60
    public class NumPairsDivisibleBy60: TestQuestion<int[], int>
    {
        /*
         * Here lies a lesson in READING THE PROBLEM
         * because the problem constrains each element to be between 1 and 60... I didn't have to account for zero
         */
        public override int[][] TestCases => new[]
        {
            new[] { 30, 20, 150, 100, 40 },
            new[] { 60, 60, 60 },
            new[] { 60, 0 },
            new[] { 0, 0, 0, 0 },
            new[] { 60, 60, 120, 240 },
            new [] { 51, 9 },
            new [] { 1, 59, 2, 58, 3, 57},
            new[] { 60, 0, 0, 0, 0, 0, 60}
        };

        public override int[] TestAnswers => new[] { 3, 3, 1, 0, 6, 1, 3, 11 };

        public class Box<T>
        {
            public T Value;

            public Box(T value)
            {
                Value = value;
            }
        }

        public override int Solution(int[] time)
        {
            var timeMap = new Dictionary<int, Box<int>>(time.Length);
            int count = 0;

            for (int i = 0; i < time.Length; i++)
            {
                var observedTime = time[i];
                var key = (observedTime != 0 && observedTime % 60 == 0) ? 60 : observedTime % 60;
                var neededTime = 60 - key;

                if (key == 60)
                    count += (timeMap.TryGetValue(60, out var sixtyCount) ? sixtyCount.Value : 0) +
                             (timeMap.TryGetValue(0, out var zeroCount) ? zeroCount.Value : 0);
                else if (key == 0)
                    count += (timeMap.TryGetValue(60, out var sixtyCount) ? sixtyCount.Value : 0);
                else if (timeMap.TryGetValue(neededTime, out var matchingTime))
                    count += matchingTime.Value;

                if (timeMap.TryGetValue(key, out var entry))
                    entry.Value += 1;
                else
                    timeMap[key] = new Box<int>(1);
            }

            return count;
        }
    }
}