using System;
using System.IO;

namespace AdventOfCode2018.App.Challenges
{
    public abstract class ChallengeBase : IChallenge
    {
        public string InputString { get; }

        protected ChallengeBase(string inputFilePath)
        {
            InputString = File.OpenText(inputFilePath).ReadToEnd();
        }

        public abstract void Run();
    }
}
