using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AmazonQuestions.Medium
{
    // https://leetcode.com/problems/the-maze/
    public class TheMaze: TestQuestion<int[][], int[], int[], bool>
    {
        public override (int[][] arg1, int[] arg2, int[] arg3)[] TestCases => new[]
        {
            (
                new[]
                {
                    new[] { 0, 0, 1, 0, 0 },
                    new[] { 0, 0, 0, 0, 0 },
                    new[] { 0, 0, 0, 1, 0 },
                    new[] { 1, 1, 0, 1, 1 },
                    new[] { 0, 0, 0, 0, 0 }
                },
                new[] { 0, 4 },
                new[] { 4, 4 }
            ),
            (
                new[]
                {
                    new[] { 0, 0, 1, 0, 0 },
                    new[] { 0, 0, 0, 0, 0 },
                    new[] { 0, 0, 0, 1, 0 },
                    new[] { 1, 1, 0, 1, 1 },
                    new[] { 0, 0, 0, 0, 0 }
                },
                new[] { 0, 4 },
                new[] { 3, 2 }
            ),
            (
                new[]
                {
                    new[] { 0, 0, 0, 0, 0 },
                    new[] { 1, 1, 0, 0, 1 },
                    new[] { 0, 0, 0, 0, 0 },
                    new[] { 0, 1, 0, 0, 1 },
                    new[] { 0, 1, 0, 0, 0 }
                },
                new[] { 4, 3 },
                new[] { 0, 1 }
            ),
            (
                new[]
                {
                    new[] { 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 1 },
                    new[] { 0, 1, 0, 1, 1, 1, 0, 1, 1, 0, 0 },
                    new[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                    new[] { 0, 0, 0, 1, 1, 1, 0, 1, 0, 0, 1 },
                    new[] { 1, 1, 0, 0, 0, 1, 0, 0, 0, 1, 1 },
                    new[] { 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0 },
                    new[] { 0, 0, 0, 0, 1, 0, 0, 1, 1, 1, 0 }
                },
                new[] { 2, 1 },
                new[] { 1, 0 }
            )
        };

        public override bool[] TestAnswers => new[] { true, false, false, true };

        public override bool Solution(int[][] maze, int[] start, int[] destination)
        {
            var game = new Maze(maze);
            game.Explore(start[0], start[1]);

            return game.CanReachDestination(destination[0], destination[1]);
        }

        public class Maze
        {
            public readonly int[][] Board;

            public readonly int Rows;
            public readonly int Cols;

            private HashSet<(int row, int col)> VisitedNodes = new HashSet<(int row, int col)>();

            public Maze(int[][] board)
            {
                Board = board;
                Rows = board.Length;
                Cols = board[0].Length;
            }

            public bool CanReachDestination(int row, int col) => VisitedNodes.Contains((row, col));

            public void Explore(int rowStart, int colStart)
            {
                var nodesToExplore = new Queue<(int row, int col)>();
                nodesToExplore.Enqueue((rowStart, colStart));
                var potentialNodes = new (int row, int col)[4];
                while (nodesToExplore.TryDequeue(out var nodeToExplore))
                {
                    VisitedNodes.Add(nodeToExplore);
                    potentialNodes[0] = MoveDown(nodeToExplore.row, nodeToExplore.col);
                    potentialNodes[1] = MoveLeft(nodeToExplore.row, nodeToExplore.col);
                    potentialNodes[2] = MoveRight(nodeToExplore.row, nodeToExplore.col);
                    potentialNodes[3] = MoveUp(nodeToExplore.row, nodeToExplore.col);

                    foreach (var node in potentialNodes)
                        if (node != nodeToExplore && !VisitedNodes.Contains(node))
                            nodesToExplore.Enqueue(node);
                }
            }

            public (int row, int col) MoveDown(int rowStart, int colStart)
            {
                for (int i = rowStart; i < Rows; i++)
                {
                    if (Board[i][colStart] == 1)
                        return (i - 1, colStart);

                    if (Rows - 1 == i)
                        return (Rows - 1, colStart);
                }

                return (rowStart, colStart);
            }

            public (int row, int col) MoveUp(int rowStart, int colStart)
            {
                for (int i = rowStart; i >= 0; i--)
                {
                    if (Board[i][colStart] == 1)
                        return (i + 1, colStart);

                    if (i == 0)
                        return (0, colStart);
                }

                return (rowStart, colStart);
            }

            public (int row, int col) MoveLeft(int rowStart, int colStart)
            {
                for (int i = colStart; i >= 0; i--)
                {
                    if (Board[rowStart][i] == 1)
                        return (rowStart, i + 1);

                    if (i == 0)
                        return (rowStart, 0);
                }

                return (rowStart, colStart);
            }

            public (int row, int col) MoveRight(int rowStart, int colStart)
            {
                for (int i = colStart; i < Cols; i++)
                {
                    if (Board[rowStart][i] == 1)
                        return (rowStart, i - 1);

                    if (Cols - 1 == i)
                        return (rowStart, Cols - 1);
                }

                return (rowStart, colStart);
            }
        }
    }
}