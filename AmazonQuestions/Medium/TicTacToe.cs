using System;
using NUnit.Framework;

namespace AmazonQuestions.Medium
{
    public class TicTacToe: TestQuestion<int[][], int?[]>
    {
        public override int[][][] TestCases => new[]
        {
            new[]
            {
                new[] { 3 },
                new[] { 0, 0, 1 },
                new[] { 0, 2, 2 },
                new[] { 2, 2, 1 },
                new[] { 1, 1, 2 },
                new[] { 2, 0, 1 },
                new[] { 1, 0, 2 },
                new[] { 2, 1, 1 }
            },
            new []
            {
                new [] { 2},
                new [] { 0, 0, 2},
                new [] { 0, 1, 1},
                new [] { 1, 1, 2}
            },
            new []
            {
                new [] { 2},
                new [] { 0, 1, 1},
                new [] { 1, 1, 2},
                new [] { 1, 0, 1}
            }
        };

        public override int?[][] TestAnswers => new[]
        {
            new[]
            {
                (int?) null, 0, 0, 0, 0, 0, 0, 1,
            },
            new[]
            {
                (int?) null, 0, 0, 2
            },
            new[]
            {
                (int?) null, 0, 0, 1
            }
        };

        public override int?[] Solution(int[][] arg1)
        {
            var ttt = new TicTacToeGame(arg1[0][0]);
            var moves = new int?[arg1.Length];

            for (int i = 1; i < arg1.Length; i++)
            {
                var move = arg1[i];
                moves[i] = ttt.Move(move[0], move[1], move[2]);
            }

            return moves;
        }

        public class TicTacToeGame
        {
            public int Count;

            public int[] Rows;
            public int[] Cols;

            public int[] RowOwner;
            public int[] ColOwner;

            public int[,] Board;

            public int PlayerOneTarget;
            public int PlayerTwoTarget;


            public TicTacToeGame(int n)
            {
                Count = n;
                Board = new int[n, n];

                Rows = new int[n];
                Cols = new int[n];
                
                RowOwner = new int[n];
                ColOwner = new int[n];

                PlayerOneTarget = n;
                PlayerTwoTarget = 2 * n;
            }

            public int Move(int row, int col, int player)
            {
                Board[row, col] = player;

                if (RowOwner[row] == 0)
                    RowOwner[row] = player;

                if (ColOwner[col] == 0)
                    ColOwner[col] = player;

                Rows[row] += player == RowOwner[row] ? player : 0;
                Cols[col] += player == ColOwner[col] ? player : 0;

                var target = player == 1 ? PlayerOneTarget : PlayerTwoTarget;

                if (ColOwner[col] == player && Cols[col] == target)
                    return player;

                if (RowOwner[row] == player && Rows[row] == target)
                    return player;

                var leftToRightOwner = Board[0, 0];
                var rightToLeftOwner = Board[0, Count - 1];
                for (int i = 0; i < Count; i++)
                {
                    if (leftToRightOwner == 0 && rightToLeftOwner == 0)
                        continue; // Unnecessary but decreases our runtime by almost 10% on leetcode :D

                    var opposite = Count - 1 - i;
                    if (leftToRightOwner != Board[i, i])
                        leftToRightOwner = 0;

                    if (rightToLeftOwner != Board[i, opposite])
                        rightToLeftOwner = 0;
                }

                if (leftToRightOwner > 0)
                    return leftToRightOwner;

                if (rightToLeftOwner > 0)
                    return rightToLeftOwner;

                return 0;
            }
        }
    }
}