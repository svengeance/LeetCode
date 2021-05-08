using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using NUnit.Framework;

namespace AmazonQuestions.Medium
{
    // https://leetcode.com/problems/number-of-provinces/
    public class FindCircleNum: TestQuestion<int[][], int>
    {
        public override int[][][] TestCases => new[]
        {
            new[]
            {
                new[] { 1, 1, 0 },
                new[] { 1, 1, 0 },
                new[] { 0, 0, 1 }
            },
            new[]
            {
                new[] { 1, 0, 0 },
                new[] { 0, 1, 0 },
                new[] { 0, 0, 1 }
            },
            new[]
            {
                new[] { 1, 1, 0, 0 },
                new[] { 1, 1, 1, 0 },
                new[] { 0, 1, 1, 1 },
                new[] { 0, 0, 1, 1 }
            },
            new []
            {
                new [] { 1 }
            },
            Array.Empty<int[]>()
        };

        public override int[] TestAnswers => new[] { 2, 3, 1, 1, 0 };

        private class City
        {
            public readonly int Position;
            public int? Grouping;
            public List<City> ConnectedCities = new();

            public City(int position)
            {
                Position = position;
            }
        }

        public override int Solution(int[][] isConnected)
        {
            if (isConnected.Length == 0)
                return 0;

            var cities = new List<City>(isConnected[0].Length);
            
            for (int i = 0; i < isConnected[0].Length; i++)
                cities.Add(new City(i));

            for (int i = 0; i < isConnected.Length; i++)
            {
                var connections = isConnected[i];
                var city = cities[i];
                for (int j = 0; j < connections.Length; j++)
                {
                    if (connections[j] == 1)
                        city.ConnectedCities.Add(cities[j]);
                }
            }

            void MarkConnected(City c, int group)
            {
                if (c.Grouping != null)
                    return;

                c.Grouping = group;
                foreach (var city in c.ConnectedCities)
                    MarkConnected(city, group);
            }

            var grouping = 0;
            foreach (var city in cities)
            {
                if (city.Grouping is null)
                {
                    MarkConnected(city, grouping);
                    grouping++;
                }
            }

            return grouping;
        }
    }
}