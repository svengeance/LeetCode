using System;
using Newtonsoft.Json;

namespace AmazonQuestions.Medium
{
    // https://leetcode.com/problems/break-a-palindrome
    public class BreakPalindrome: TestQuestion<string, string>
    {
        public override string[] TestCases => new[]
        {
            "abccba", "a", "aa", "aba", "zaz", "aaaadddaaaa"
        };

        public override string[] TestAnswers => new[]
        {
            "aaccba", "", "ab", "abb", "aaz", "aaaaaddaaaa"
        };

        public override string Solution(string palindrome)
        {
            var i = 0;
            var span = palindrome.AsSpan();
            foreach (var ch in span[..(palindrome.Length/2)])
            {
                if (ch > 'a')
                    return new string(span[..i]) + 'a' + new string(span[(i + 1)..]);

                i++;
            }

            for (int j = 1; j < span.Length; j++)
            {
                var charToIncrease = span[^j];
                if (charToIncrease < 'z')
                    return new string(span[..^j]) + (char) (charToIncrease + 1) + new string(span[^(j - 1)..]);
            }

            return string.Empty;
        }
    }
}