using System.Text;

namespace AmazonQuestions.Medium
{
    // https://leetcode.com/problems/integer-to-roman/
    public class IntToRoman: TestQuestion<int, string>
    {
        public override int[] TestCases => new[] { 3, 4, 9, 58, 1994 };

        public override string[] TestAnswers => new[]
        {
            "III", "IV", "IX", "LVIII", "MCMXCIV"
        };

        public override string Solution(int num)
        {
            var sb = new StringBuilder(10);
            var thousands = num / 1000;
            var hundreds = (num % 1000) / 100;
            var tens = (num % 100) / 10;
            var ones = num % 10;

            return sb.Append(GetCorrespondingChar(1000, thousands))
                     .Append(GetCorrespondingChar(100, hundreds))
                     .Append(GetCorrespondingChar(10, tens))
                     .Append(GetCorrespondingChar(1, ones))
                     .ToString();
        }

        /*
         * Looking at this it's easy to see there's _some_ pattern where each digit's place (ones, tens, etc)
         * has a corresponding value and half-value, so the end result can be computed for any length of places if you wanted.
         *
         * Is it faster? Maybe.
         */
        string GetCorrespondingChar(int places, int value)
            => places switch
               {
                   _ when value == 0 => string.Empty,
                   1 => value switch
                        {
                            4                 => "IV", 9 => "IX",
                            _ when value >= 5 => "V" + new string('I', value - 5),
                            _ => new string('I', value)
                        },
                   10 => value switch
                         {
                             4                 => "XL",
                             9                 => "XC",
                             _ when value >= 5 => "L" + new string('X', value - 5),
                             _  => new string('X', value)
                         },
                   100 => value switch
                          {
                              4                 => "CD",
                              9                 => "CM",
                              _ when value >= 5 => "D" + new string('C', value - 5),
                              _ => new string('C', value)
                          },
                   1000 => new string('M', value)
               };
    }
}