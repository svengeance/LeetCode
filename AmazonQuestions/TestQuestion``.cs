using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace AmazonQuestions
{
    public abstract class TestQuestion<TArg1, TResult>
    {
        public abstract TArg1[] TestCases { get; }

        public abstract TResult[] TestAnswers { get; }

        public abstract TResult Solution(TArg1 arg1);

        [Test]
        public void Solve()
        {
            if (TestCases.Length != TestAnswers.Length)
                Assert.Ignore($"Mismatched TestCases:TestAnswers ({TestCases.Length}:{TestAnswers.Length})");

            for (var i = 0; i < TestCases.Length; i++)
            {
                var arg1 = TestCases[i];
                var solution = Solution(arg1);

                Assert.AreEqual(TestAnswers[i], solution);
            }
        }
    }
}