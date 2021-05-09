using System;

namespace AmazonQuestions.Medium
{
    public class IsRobotBounded: TestQuestion<string, bool>
    {
        public override string[] TestCases => new[]
        {
            "GGLLGG",
            "GG",
            "GL",
            "L",
            "GLGL",
            "GLGLGL",
            "GLGLGLGL",
            "GLRRRR"
        };

        public override bool[] TestAnswers => new[]
        {
            true, false, true, true, true, true, true, true
        };

        public override bool Solution(string instructions)
        {
            var dir = 0;
            var (xpos, ypos) = (0, 0);

            (int, int) GetMove(int dir) => (Math.Abs(dir) % 4) switch
                                           {
                                               0 => (0, 1),
                                               1 => (1, 0),
                                               2 => (0, -1),
                                               3 => (-1, 0)
                                           };

            var (addX, addY) = GetMove(dir);
            foreach (var instr in instructions.AsSpan())
            {
                if (instr == 'G')
                {
                    (xpos, ypos) = (xpos + addX, ypos + addY);
                    continue;
                }

                dir += instr == 'R' ? 1 : -1;
                (addX, addY) = GetMove(dir);
            }

            return GetMove(dir) != (0, 1) || (xpos, ypos) == (0, 0);
        }
    }
}