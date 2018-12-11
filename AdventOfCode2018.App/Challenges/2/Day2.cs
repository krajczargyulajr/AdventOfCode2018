using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018.App.Challenges
{
    public class Day2 : ChallengeBase
    {
        public Day2() : base("Challenges/2/Day2_Input1.txt")
        {
        }

        public override void Run()
        {
            var splitInput = InputString.Split("\n").ToList();

            int twos = 0;
            int threes = 0;

            foreach (var word in splitInput)
            {
                var (twice, thrice) = FindTwosAndThrees(word);

                if (twice) twos++;
                if (thrice) threes++;
            }

            Console.WriteLine($"Checksum: {twos * threes}");

            for (var i = 0; i < splitInput.Count; ++i)
            {
                var word = splitInput[i];
                for (var j = i + 1; j < splitInput.Count; ++j)
                {
                    var secondWord = splitInput[j];

                    var mismatches = 0;
                    var mismatchIndex = -1;
                    for (var c = 0; c < word.Length; ++c)
                    {
                        var wordChar = word[c];
                        var secondWordChar = secondWord[c];

                        if (mismatches > 1) break;

                        if (wordChar != secondWordChar)
                        {
                            mismatches++;
                            mismatchIndex = c;
                        }
                    }

                    if (mismatches == 1)
                    {
                        Console.WriteLine($"Remaining chars: {word.Substring(0, mismatchIndex)}{word.Substring(mismatchIndex + 1)}");
                        goto complete;
                    }
                }
            }

        complete:
            Console.WriteLine();
        }

        public (bool, bool) FindTwosAndThrees(string word)
        {
            var letters = CountLetters(word);

            return (letters.Values.Any(x => x == 2), letters.Values.Any(x => x == 3));
        }

        public Dictionary<char, int> CountLetters(string word)
        {
            var letters = new Dictionary<char, int>();

            foreach (var letter in word)
            {
                if (!letters.ContainsKey(letter))
                {
                    letters[letter] = 0;
                }

                letters[letter]++;
            }

            return letters;
        }
    }
}
