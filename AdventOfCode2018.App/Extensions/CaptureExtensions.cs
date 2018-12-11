using System;
using System.Text.RegularExpressions;

namespace AdventOfCode2018.App.Extensions
{
    public static class CaptureExtensions
    {
        public static int IntValue(this Capture capture)
        {
            return int.Parse(capture.Value);
        }
    }
}
