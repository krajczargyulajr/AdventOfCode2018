using AdventOfCode2018.App.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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
            Timeline currentTimeline = null;

            foreach (var record in records)
            {
                if (currentTimeline == null && record.GetType() != typeof(ShiftStartRecord))
                {
                    Console.WriteLine("List doesn't start with shift start");
                    break;
                }

                switch (record)
                {
                    case ShiftStartRecord shiftStart:
                        if (!guardTimeline.ContainsKey(shiftStart.GuardId))
                        {
                            guardTimeline.Add(shiftStart.GuardId, new Timeline());
                        }

                        currentTimeline?.Close(shiftStart.Minute);
                        currentTimeline = guardTimeline[shiftStart.GuardId];
                        break;
                    case AsleepRecord sleepStart:
                        currentTimeline.FallAsleep(sleepStart.Minute);
                        break;
                    case WakeUpRecord sleepEnd:
                        currentTimeline.Awake(sleepEnd.Minute);
                        break;
                }
            }

            foreach (var (guardId, timeline) in guardTimeline)
            {
                Console.WriteLine($"#{guardId.ToString().PadLeft(5)} ({timeline.TotalMinutes.ToString().PadLeft(5)}): {timeline.ToString()}");
            }

            var max = guardTimeline.OrderByDescending(x => x.Value.TotalMinutes).First();
            var maxMinute = max.Value.Breakdown.OrderByDescending(x => x.Value).First().Key;
            Console.WriteLine($"Answer1: {max.Key * maxMinute}");

            var max2 = guardTimeline.OrderByDescending(x => x.Value.MaxValue).First();

            Console.WriteLine($"Answer2: {max2.Key * max2.Value.MaxMinute}");
        }
    }

    public class Timeline
    {
        public Dictionary<int, int> Breakdown = new Dictionary<int, int>();

        public int TotalMinutes => Breakdown.Values.Sum();

        private KeyValuePair<int, int> max => Breakdown.OrderByDescending(x => x.Value).First();
        public int MaxValue => max.Value;
        public int MaxMinute => max.Key;


        public Timeline()
        {
            for (var i = 0; i < 59; ++i)
            {
                Breakdown[i] = 0;
            }
        }

        // 0 - awake, 1 - sleeping
        private int state = 0;
        private int sleepingSince = -1;

        public void Close(int minute)
        {
            if (state == 1) // sleeping
            {
                Awake(minute);
            }
        }

        public void FallAsleep(int minute)
        {
            state = 1;
            sleepingSince = minute;
        }

        public void Awake(int minute)
        {
            for (var i = sleepingSince; i < minute; ++i)
            {
                Breakdown[i]++;
            }

            state = 0;
        }

        public override string ToString()
        {
            return string.Join(' ', Breakdown.Values.Select(x => x.ToString().PadLeft(2)));
        }
    }

    public abstract class Record
    {
        public DateTime DateTime { get; set; }

        public int Minute => DateTime.Minute;
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
