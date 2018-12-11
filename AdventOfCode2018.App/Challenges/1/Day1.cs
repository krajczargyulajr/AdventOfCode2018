using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AdventOfCode2018.App.Challenges
{
    public class Day1 : IChallenge
    {
        public void Run()
        {
            var file = File.OpenText("Challenges/1/Day1_Input1.txt");
            var inputTxt = file.ReadToEnd();

            var inputs = inputTxt.Split("\n").Select(x => int.Parse(x)).ToList();

            // 1
            var inputsSum = inputs.Sum();

            Console.WriteLine($"Sum of inputs: {inputsSum}");

            // 2
            var frequencies = new List<int>() { 0 };
            var sum = 0;
            int? result = null;

        restart:
            for (var i = 0; i < inputs.Count; ++i)
            {
                var current = inputs[i];
                sum += current;

                if (frequencies.Contains(sum))
                {
                    result = sum;
                    goto complete;
                }
                else
                {
                    frequencies.Add(sum);
                }
            }
            goto restart;
        complete:

            Console.WriteLine($"First frequency reached twice is: {result}");
        }
    }
}
