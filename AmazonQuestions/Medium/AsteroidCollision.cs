using System;
using System.Collections.Generic;
using System.Linq;

namespace AmazonQuestions.Medium
{
    // https://leetcode.com/problems/asteroid-collision
    public class AsteroidCollision: TestQuestion<int[], int[]>
    {
        public override int[][] TestCases => new[]
        {
            new[] { 5, 10, -5 },
            new[] { 8, -8 },
            new[] { 10, 2, -5 },
            new[] { -2, -1, 1, 2 },
            new [] { 5, 5 },
            new [] { 5, 5, 5, 5, -6 },
            new [] { 5, 5, 5, 5, -6, -10 },
            new [] { 5, -10, 5, -10 }
        };

        public override int[][] TestAnswers => new[]
        {
            new[] { 5, 10 },
            Array.Empty<int>(),
            new[] { 10 },
            new[] { -2, -1, 1, 2 },
            new [] { 5, 5},
            new [] { -6 },
            new [] { -6, -10 },
            new [] { -10, -10 }
        };

        public override int[] Solution(int[] asteroids)
        {
            var battleGround = new Stack<int>(asteroids.Length);

            for (int i = 0; i < asteroids.Length; i++)
            {
                var next = asteroids[i];

                if (battleGround.Count == 0 || (battleGround.Peek() > 0 && next > 0) || battleGround.Peek() < 0 && next > 0)
                {
                    battleGround.Push(next);
                    continue;
                }

                while (battleGround.TryPeek(out var prior) && (prior > 0 && next < 0))
                {
                    prior = battleGround.Pop();

                    if (prior + next == 0)
                        (prior, next) = (0, 0);

                    if (prior > Math.Abs(next))
                    {
                        next = 0;
                        battleGround.Push(prior);
                    }
                }

                if (next < 0)
                    battleGround.Push(next);
            }

            return battleGround.Reverse().ToArray();
        }
    }
}