using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Newtonsoft.Json;
using NUnit.Framework;

namespace AmazonQuestions
{
    public abstract class TestQuestion<TArg1, TArg2, TResult>
    {
        public abstract (TArg1 arg1, TArg2 arg2)[] TestCases { get; }

        public abstract TResult[] TestAnswers { get; }

        public abstract TResult Solution(TArg1 arg1, TArg2 arg2);

        public virtual bool CompareByReference { get; } = true;

        [Test]
        public void Solve()
        {
            if (TestCases.Length != TestAnswers.Length)
                Assert.Ignore($"Mismatched TestCases:TestAnswers ({TestCases.Length}:{TestAnswers.Length})");

            for (var i = 0; i < TestCases.Length; i++)
            {
                var (arg1, arg2) = TestCases[i];
                var solution = Solution(arg1, arg2);

                var serializedTestCase = JsonConvert.SerializeObject(TestCases[i]);
                var serializedAnswer = JsonConvert.SerializeObject(TestAnswers[i]);
                var serializedSolution = JsonConvert.SerializeObject(solution);
                var failedTestMessage = $"Failed test case #{i + 1} with args:\n{serializedTestCase}\n\n" +
                                        $"Expected: {serializedAnswer}\n\n" +
                                        $"Was: {serializedSolution}\n\n";

                if (CompareByReference)
                    Assert.AreEqual(TestAnswers[i], solution, failedTestMessage);
                else
                    Assert.AreEqual(serializedAnswer, serializedSolution, failedTestMessage);
            }
        }
    }
}