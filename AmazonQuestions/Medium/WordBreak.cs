using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AmazonQuestions.Medium
{
    public class WordBreak: TestQuestion<string, IList<string>, bool>
    {
        // https://leetcode.com/problems/word-break
        public override (string arg1, IList<string> arg2)[] TestCases => new (string arg1, IList<string> arg2)[]
        {
            (
                "leetcode",
                new List<string> { "leet", "code" }
            ),
            (
                "applepenapple",
                new List<string> { "apple", "pen" }
            ),
            (
                "catsandog",
                new List<string> { "cats", "dog", "sand", "and", "cat" }
            ),
            (
                "catsandogcat",
                new List<string> { "cats", "dog", "sand", "and", "cat", "an"}
            )
        };

        public override bool[] TestAnswers => new[]
        {
            true, true, false, true
        };

        public class Trie
        {
            public Node Root = new Node('$');

            public void AddWord(string s)
            {
                Node current = Root;
                foreach (var c in s)
                {
                    var idx = c - 'a';
                    if (current.Children[idx] == null)
                        current.Children[idx] = new Node(c);

                    current = current.Children[idx];
                }

                current.IsWord = true;
            }

            public bool ContainsWord(string s)
            {
                Console.WriteLine($"Checking {s}");
                var current = Root;
                for (var i = 0; i < s.Length; i++)
                {
                    var c = s[i];
                    var idx = c - 'a';
                    if (current.Children[idx] != null)
                        if (current.Children[idx].IsWord && i == s.Length - 1)
                            return true;
                        else
                            current = current.Children[idx];
                    else
                        return false;
                }

                return false;
            }

            public bool Break(string s, int start, bool?[] cache)
            {
                if (s.Length == start)
                    return true;

                if (cache[start] != null)
                    return cache[start].Value;

                for (int end = start + 1; end <= s.Length; end++)
                {
                    if (ContainsWord(s[start..end]))
                    {
                        if (Break(s, end, cache))
                            return (bool) (cache[start] = true);
                    }
                }

                return (bool) (cache[start] = false);
            }

            public class Node
            {
                public char Value;
                public bool IsWord;

                public Node[] Children = new Node[26];

                public Node(char c) => Value = c;
            }
        }

        /*
         * Arrived at a solution on my own that *worked* but was slow..had to seek out the solution
         * to get to the recursive/memoization answer. What a pain. Need to get better at breaking
         * things down into subproblems.
         */
        public override bool Solution(string s, IList<string> wordDict)
        {
            var trie = new Trie();
            foreach (var word in wordDict)
                trie.AddWord(word);

            var memo = new bool?[s.Length];
            Array.Fill(memo, null);

            return trie.Break(s, 0, memo);
        }
    }
}