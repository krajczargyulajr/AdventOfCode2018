﻿using System;
using AdventOfCode2018.App.Challenges;

namespace AdventOfCode2018.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World! Advent Of Code 2018 Solutions - Gyula Krajczar");

            var challenge = new Day3();
            challenge.Run();

            Console.ReadLine();
        }
    }
}
