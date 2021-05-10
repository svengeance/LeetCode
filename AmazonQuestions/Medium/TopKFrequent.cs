using System.Collections.Generic;
using System.Linq;

namespace AmazonQuestions.Medium
{
    // https://leetcode.com/problems/top-k-frequent-words/
    public class TopKFrequent: TestQuestion<string[], int, IList<string>>
    {
        public override (string[] arg1, int arg2)[] TestCases => new[]
        {
            (
                new[] { "i", "love", "leetcode", "i", "love", "coding" },
                2
            ),
            (
                new [] { "the", "day", "is", "sunny", "the", "the", "the", "sunny", "is", "is" },
                4
            ),
            (
                new[] { "i", "love", "leetcode", "i", "love", "coding" },
                3
            ),
        };

        public override IList<string>[] TestAnswers => new[]
        {
            new List<string> { "i", "love" },
            new List<string> { "the", "is", "sunny", "day" },
            new List<string> { "i", "love", "coding" },
        };

        public override IList<string> Solution(string[] words, int k)
        {
            var lookup = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (lookup.TryGetValue(word, out var val))
                    lookup[word] = val + 1;
                else
                    lookup[word] = 1;
            }

            return lookup.OrderByDescending(o => o.Value).ThenBy(o => o.Key).Select(s => s.Key).Take(k).ToList();
        }
    }
}