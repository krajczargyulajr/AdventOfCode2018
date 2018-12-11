using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode2018.App.Extensions;

namespace AdventOfCode2018.App.Challenges
{
    public class Day4 : ChallengeBase
    {
        public Day4() : base("Challenges/4/Day4_Input1.txt")
        {
        }

        public Regex DateTimeRegex = new Regex(@"\[([\d]{4})-([\d]{2})-([\d]{2}) ([\d]{2}):([\d]{2})\]");
        public Regex GuardIdRegex = new Regex(@"#([\d]+)");

        public override void Run()
        {
            var inputLines = InputString.Split("\n");

            var records = new List<Record>();
            foreach (var line in inputLines)
            {
                var dateTimeMatch = DateTimeRegex.Match(line);
                var dateTime = new DateTime(
                    year: dateTimeMatch.Groups[1].IntValue(),
                    month: dateTimeMatch.Groups[2].IntValue(),
                    day: dateTimeMatch.Groups[3].IntValue(),
                    hour: dateTimeMatch.Groups[4].IntValue(),
                    minute: dateTimeMatch.Groups[5].IntValue(),
                    second: 0
                );

                if (line.Contains("Guard"))
                {
                    var guardIdMatch = GuardIdRegex.Match(line);
                    records.Add(new ShiftStartRecord
                    {
                        DateTime = dateTime,
                        GuardId = guardIdMatch.Groups[1].IntValue()
                    });
                }
                else if (line.Contains("falls"))
                {
                    records.Add(new AsleepRecord
                    {
                        DateTime = dateTime
                    });
                }
                else
                {
                    records.Add(new WakeUpRecord
                    {
                        DateTime = dateTime
                    });
                }
            }

            records = records.OrderBy(x => x.DateTime).ToList();

            var guardTimeline = new Dictionary<int, Timeline>();
            var currentGuardId = -1;

            foreach (var record in records)
            {
                if (currentGuardId == -1 && record.GetType() != typeof(ShiftStartRecord))
                {
                    Console.WriteLine("List doesn't start with shift start");
                    break;
                }

                switch (record)
                {
                    case ShiftStartRecord shiftStart:
                        break;
                    case AsleepRecord sleepStart:
                        break;
                    case WakeUpRecord sleepEnd:
                        break;

                }
            }
        }
    }

    public class Timeline
    {
        public int TotalMinutes { get; set; } = 0;

        public Dictionary<int, int> Breakdown = new Dictionary<int, int>();
    }

    public abstract class Record
    {
        public DateTime DateTime { get; set; }
    }

    public class ShiftStartRecord : Record
    {
        public int GuardId { get; set; }
    }

    public class AsleepRecord : Record
    {

    }

    public class WakeUpRecord : Record
    {

    }
}
