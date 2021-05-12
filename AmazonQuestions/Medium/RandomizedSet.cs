using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace AmazonQuestions.Medium
{
    // https://leetcode.com/problems/insert-delete-getrandom-o1
    public class RandomizedSet
    {
        private Random _random = new Random();

        private HashSet<int> _backingSet = new HashSet<int>();

        public RandomizedSet() { }

        public bool Insert(int val) => _backingSet.Add(val);

        public bool Remove(int val) => _backingSet.Remove(val);

        public int GetRandom() => _backingSet.Skip(_random.Next(_backingSet.Count)).Take(1).First();

        [Test]
        public void Can_init_set() => _ = new RandomizedSet();

        [Test]
        public void Can_insert_and_remove_item_in_set()
        {
            var set = new RandomizedSet();
            set.Insert(1);

            Assert.True(set.Remove(1));
        }

        [Test]
        public void Can_get_random_item_in_uniform_way()
        {
            var setOfTenThousandItems = new RandomizedSet();
            foreach (var i in Enumerable.Range(0, 10_000))
                _ = setOfTenThousandItems.Insert(i);

            var averageOfFiveThousandRemovals = Enumerable.Range(0, 5_000)
                                                          .Select(s => setOfTenThousandItems.GetRandom())
                                                          .Average();

            Assert.AreEqual(5_000d, averageOfFiveThousandRemovals, 100);
        }
    }
}