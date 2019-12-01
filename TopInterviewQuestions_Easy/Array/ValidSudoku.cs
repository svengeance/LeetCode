using System;
using System.Collections.Generic;
using Xunit;

namespace TopInterviewQuestions_Easy.Array
{
    public class ValidSudoku
    {
        // Not submitted online due to a false-flag test case
        public bool Solution(char[][] board)
        {
            var rowSet = new HashSet<int>(9);
            var colSet = new HashSet<int>(9);
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    if (board[y][x] != '.' && !rowSet.Add(board[y][x]))
                        return false;

                    if (board[x][y] != '.' && !colSet.Add(board[x][y]))
                        return false;
                }

                rowSet.Clear();
                colSet.Clear();
                
                int x0 = y / 3;
                int y0 = y % 3;

                var unique = new HashSet<int>(9);
                for (int i = x0; i < 3; i++)
                    for (int j = y0; j < 3; j++)
                        if (board[i*3 + i][j*3 + j] != '.' && !unique.Add(board[i*3 + i][j*3 + j]))
                            return false;
            }

            return true;
        }

        [Fact]
        public void TestSolution_ShouldPass()
        {
            // Given
            var board = new char[][]
            {
                new[] { '5', '3', '.', '.', '7', '.', '.', '.', '.' },
                new[] { '6', '.', '.', '1', '9', '5', '.', '.', '.' },
                new[] { '.', '9', '8', '.', '.', '.', '.', '6', '.' },
                new[] { '8', '.', '.', '.', '6', '.', '.', '.', '3' },
                new[] { '4', '.', '.', '8', '.', '3', '.', '.', '1' },
                new[] { '7', '.', '.', '.', '2', '.', '.', '.', '6' },
                new[] { '.', '6', '.', '.', '.', '.', '2', '8', '.' },
                new[] { '.', '.', '.', '4', '1', '9', '.', '.', '5' },
                new[] { '.', '.', '.', '.', '8', '.', '.', '7', '9' }
            };
            // When

            var valid = Solution(board);
            // Then

            Assert.True(valid);
        }

        [Fact]
        public void TestSolution_ShouldFail_DuplicateInColumn()
        {
            // Given
            var board = new char[][]
            {
                new[] { '5', '3', '.', '.', '7', '.', '.', '.', '9' },
                new[] { '6', '.', '.', '1', '9', '5', '.', '.', '.' },
                new[] { '.', '9', '8', '.', '.', '.', '.', '6', '.' },
                new[] { '8', '.', '.', '.', '6', '.', '.', '.', '3' },
                new[] { '4', '.', '.', '8', '.', '3', '.', '.', '1' },
                new[] { '7', '.', '.', '.', '2', '.', '.', '.', '6' },
                new[] { '.', '6', '.', '.', '.', '.', '2', '8', '.' },
                new[] { '.', '.', '.', '4', '1', '9', '.', '.', '5' },
                new[] { '.', '.', '.', '.', '8', '.', '.', '7', '9' }
            };
            // When

            var valid = Solution(board);

            // Then
            Assert.False(valid);
        }

        [Fact]
        public void TestSolution_ShouldFail_DuplicateInRow()
        {
            // Given
            var board = new char[][]
            {
                new[] { '5', '3', '.', '.', '7', '.', '.', '.', '.' },
                new[] { '6', '.', '.', '1', '9', '5', '.', '.', '.' },
                new[] { '.', '9', '8', '.', '.', '.', '.', '6', '.' },
                new[] { '8', '.', '.', '.', '6', '.', '.', '.', '3' },
                new[] { '4', '.', '.', '8', '.', '3', '.', '.', '1' },
                new[] { '7', '.', '.', '.', '2', '.', '.', '.', '6' },
                new[] { '.', '6', '.', '.', '.', '.', '2', '8', '.' },
                new[] { '.', '.', '4', '4', '1', '9', '.', '.', '5' },
                new[] { '.', '.', '.', '.', '8', '.', '.', '7', '9' }
            };
            // When

            var valid = Solution(board);

            // Then
            Assert.False(valid);
        }

        [Fact]
        public void TestSolution_ShouldFail_DuplicateInSquare()
        {
            // Given
            var board = new char[][]
            {
                new[] { '7', '3', '.', '.', '4', '.', '.', '.', '.' },
                new[] { '6', '.', '.', '1', '9', '5', '.', '.', '.' },
                new[] { '.', '9', '8', '.', '.', '.', '.', '6', '.' },
                new[] { '8', '.', '.', '.', '6', '.', '.', '.', '3' },
                new[] { '4', '.', '.', '8', '.', '3', '.', '.', '1' },
                new[] { '1', '.', '.', '.', '2', '.', '.', '.', '6' },
                new[] { '.', '6', '.', '.', '.', '.', '2', '8', '.' },
                new[] { '.', '.', '.', '4', '1', '9', '.', '.', '5' },
                new[] { '3', '.', '6', '.', '8', '.', '.', '7', '9' }
            };
            // When

            var valid = Solution(board);
            // Then

            Assert.False(valid);
        }
    }
}