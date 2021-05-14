using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace AmazonQuestions.Medium
{
    public class MostVisitedPattern: TestQuestion<string[], int[], string[], IList<string>>
    {
        public override (string[] arg1, int[] arg2, string[] arg3)[] TestCases => new[]
        {
            (
                new[] { "joe", "joe", "joe", "james", "james", "james", "james", "mary", "mary", "mary" },
                new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 },
                new[] { "home", "about", "career", "home", "cart", "maps", "home", "home", "about", "career" }
            ),
            (
                new[] { "bob", "bob", "bob", "alice", "alice", "alice" },
                new[] { 1, 2, 3, 4, 5, 6 },
                new[] { "b", "c", "d", "a", "b", "c" }
            ),
            (
                new[] { "bob", "bob", "bob", "alice", "alice", "alice" },
                new[] { 1, 2, 3, 4, 5, 6 },
                new[] { "a", "b", "c", "b", "c", "d" }
            ),
            (
                new[] { "zkiikgv", "zkiikgv", "zkiikgv", "zkiikgv" },
                new[] { 436363475, 710406388, 386655081, 797150921 },
                new[] { "wnaaxbfhxp", "mryxsjc", "oz", "wlarkzzqht" }
            ),
            (
                new[] { "h", "eiy", "cq", "h", "cq", "txldsscx", "cq", "txldsscx", "h", "cq", "cq" },
                new[] { 527896567, 334462937, 517687281, 134127993, 859112386, 159548699, 51100299, 444082139, 926837079, 317455832, 411747930 },
                new[]
                {
                    "hibympufi", "hibympufi", "hibympufi", "hibympufi", "hibympufi", "hibympufi", "hibympufi", "hibympufi", "yljmntrclw", "hibympufi",
                    "yljmntrclw"
                }
            )
        };


        public override IList<string>[] TestAnswers => new[]
        {
            new List<string>
            {
                "home", "about", "career"
            },
            new List<string>
            {
                "a", "b", "c",
            },
            new List<string>
            {
                "a", "b", "c"
            },
            new List<string>
            {
                "oz","mryxsjc","wlarkzzqht"
            },
            new List<string>
            {
                "hibympufi","hibympufi","yljmntrclw"
            }
        };
        
        public class IntBox
        {
            public int Value = 0;
        }
        public class WebsiteVisit
        {
            public string Username { get; set; }
            public int Timestamp { get; set; }
            public string Website { get; set; }
        }

        public override IList<string> Solution(string[] username, int[] timestamp, string[] website)
        {
            var orderedWebsites = Enumerable.Range(0, username.Length)
                                            .Select(s => new WebsiteVisit { Username = username[s], Timestamp = timestamp[s], Website = website[s] })
                                            .GroupBy(g => g.Username)
                                            .Select(s => s.OrderBy(o => o.Timestamp).ToList())
                                            .ToArray();

            var sequenceMap = new Dictionary<(string, string, string), HashSet<string>>();

            var permutations = new List<Queue<WebsiteVisit>>();

            foreach (var web in orderedWebsites)
            {
                for (int i = 0; i < web.Count; i++)
                for (int j = i + 1; j < web.Count; j++)
                for (int k = j + 1; k < web.Count; k++)
                {
                    var q = new Queue<WebsiteVisit>();
                    q.Enqueue(web[i]);
                    q.Enqueue(web[j]);
                    q.Enqueue(web[k]);
                    permutations.Add(q);
                }
            }

            foreach (var visits in permutations)
            {
                if (visits.Count < 3)
                    continue;

                var web1 = visits.Dequeue();
                var web2 = visits.Dequeue();
                var web3 = visits.Dequeue();

                while (true)
                {
                    var sequence = (web1.Website, web2.Website, web3.Website);

                    var foundSequence = sequenceMap.TryGetValue(sequence, out var count);
                    if (!foundSequence)
                        sequenceMap[sequence] = new HashSet<string> { web3.Username };
                    else
                        count.Add(web3.Username);

                    web1 = web2;
                    web2 = web3;
                    if (!visits.TryDequeue(out web3))
                        break;
                }
            }

            var highestMap = sequenceMap.OrderByDescending(o => o.Value.Count)
                                        .ThenBy(o => o.Key.Item1)
                                        .ThenBy(o => o.Key.Item2)
                                        .ThenBy(o => o.Key.Item3)
                                        .Select(s => s.Key)
                                        .First();

            return new List<string> { highestMap.Item1, highestMap.Item2, highestMap.Item3 };
        }
    }
}