using AdventOfCode2018.App.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2018.App.Challenges
{
    public class Day5 : ChallengeBase
    {
        public Day5() : base("Challenges/5/Day5_Input1.txt") { }

        public override void Run()
        {
            var allProcessed = Process(InputString);

            Console.WriteLine($"Answer1: {allProcessed.Length}");

            var abc = "abcdefghijklmnopqrstuv";
            var reducedLengths = new Dictionary<char, int>();
            foreach (var ch in abc)
            {
                var input = RemoveChars(InputString, ch);
                var processed = Process(input);

                reducedLengths[ch] = processed.Length;
            }

            Console.WriteLine($"Answer2: {reducedLengths.Values.Min()}");
        }

        public string Process(string input)
        {
            // Console.Write(".");
            StringBuilder nextString = new StringBuilder(input.Length / 2);
            int found = 0;

            for (var i = 0; i < input.Length; ++i)
            {
                var char1 = input[i];
                var char2 = i == input.Length - 1 ? '0' : input[i + 1];

                if (Char.ToLowerInvariant(char1) == Char.ToLowerInvariant(char2) && (char1.IsUpper() ^ char2.IsUpper()))
                {
                    ++found;
                    ++i;
                }
                else
                {
                    nextString.Append(char1);
                }
            }

            if (found == 0)
            {
                return nextString.ToString();
            }
            else
            {
                // Console.Write(found);
                return Process(nextString.ToString());
            }
        }

        public string RemoveChars(string input, char ch)
        {
            var lowerChar = Char.ToLowerInvariant(ch);
            var upperChar = Char.ToUpperInvariant(ch);

            return string.Join("", input.Where(x => x != lowerChar && x != upperChar));
        }
    }
}
