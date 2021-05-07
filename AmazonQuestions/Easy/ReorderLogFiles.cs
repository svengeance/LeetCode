using System;
using System.Collections.Generic;

namespace AmazonQuestions.Easy
{
    // https://leetcode.com/problems/reorder-data-in-log-files/
    public class ReorderLogFiles: TestQuestion<string[], string[]>
    {
        public override string[][] TestCases => new[]
        {
            new[] { "dig1 8 1 5 1", "let1 art can", "dig2 3 6", "let2 own kit dig", "let3 art zero" },
            new[] { "a1 9 2 3 1", "g1 act car", "zo4 4 7", "ab1 off key dog", "a8 act zoo" },
            new [] { "j 7 0", "i 23", "w 346", "g q w", "o krb" },
        };

        public override string[][] TestAnswers => new[]
        {
            new[] { "let1 art can", "let3 art zero", "let2 own kit dig", "dig1 8 1 5 1", "dig2 3 6" },
            new[] { "g1 act car", "a8 act zoo", "ab1 off key dog", "a1 9 2 3 1", "zo4 4 7" },
            new[] { "o krb", "g q w", "j 7 0", "i 23", "w 346" }
        };

        public override string[] Solution(string[] logs)
        {
            var letterLogs = new List<string>(logs.Length);
            var digitLogs = new List<string>(logs.Length);

            foreach (var log in logs)
            {
                var logspan = log.AsSpan();
                var firstSpace = logspan.IndexOf(' ');
                if (logspan[firstSpace + 1] >= 'a')
                    letterLogs.Add(log);
                else
                    digitLogs.Add(log);
            }

            letterLogs.Sort((l, r) =>
            {
                var leftSpan = l.AsSpan();
                var rightSpan = r.AsSpan();

                var leftSpace = l.IndexOf(' ');
                var rightSpace = r.IndexOf(' ');

                var contentComparison = leftSpan[(leftSpace + 1)..].CompareTo(rightSpan[(rightSpace + 1)..], StringComparison.Ordinal);

                return contentComparison.CompareTo(0) switch
                       {
                           1  => 1,
                           -1 => -1,
                           0  => leftSpan[..leftSpace].CompareTo(rightSpan[..rightSpace], StringComparison.Ordinal)
                       };
            });

            foreach (var log in digitLogs)
                letterLogs.Add(log);

            return letterLogs.ToArray();
        }
    }
}