using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2018.App.Challenges
{
    public class Day3 : ChallengeBase
    {
        public Day3() : base("Challenges/3/Day3_Input1.txt")
        {
        }

        public Regex InputRegex = new Regex("#(\\d+) @ (\\d+),(\\d+): (\\d+)x(\\d+)");

        public override void Run()
        {
            var inputLines = InputString.Split("\n");
            var claims = new List<Claim>();

            foreach (var line in inputLines)
            {
                var groups = InputRegex.Match(line).Groups;

                claims.Add(new Claim
                {
                    Id = int.Parse(groups[1].Value),
                    Left = int.Parse(groups[2].Value),
                    Top = int.Parse(groups[3].Value),
                    Width = int.Parse(groups[4].Value),
                    Height = int.Parse(groups[5].Value)
                });
            }

            var maxWidth = claims.Max(x => x.Left + x.Width);
            var maxHeight = claims.Max(x => x.Top + x.Height);

            var fabric = new int[maxWidth, maxHeight];
            for (var i = 0; i < maxWidth; ++i)
                for (var j = 0; j < maxHeight; ++j)
                    fabric[i, j] = 0;

            foreach (var claim in claims)
            {
                for (var i = claim.Left; i < claim.Left + claim.Width; ++i)
                    for (var j = claim.Top; j < claim.Top + claim.Height; ++j)
                        fabric[i, j]++;
            }

            Console.WriteLine(fabric.Cast<int>().Count(x => x > 1));

            Claim nonOverlappingClaim = null;
            foreach (var claim in claims)
            {
                for (var i = claim.Left; i < claim.Left + claim.Width; ++i)
                    for (var j = claim.Top; j < claim.Top + claim.Height; ++j)
                        if (fabric[i, j] != 1) goto overlaps;

                nonOverlappingClaim = claim;
                goto success;
            overlaps:
                ;
            }

        success:
            Console.WriteLine(nonOverlappingClaim?.Id);
        }
    }

    public class Claim
    {
        public int Id { get; set; }

        public int Left { get; set; }

        public int Top { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }
    }
}
