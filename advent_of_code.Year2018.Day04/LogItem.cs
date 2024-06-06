using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code.Year2018.Day04
{
    internal class LogItem
    {
        public enum ActionType { WakeUp, FallsAsleep, GuardChange}

        public DateTime Time { get; }
        public ActionType Action { get; }
        public int GuardID { get; } = -1;

        public LogItem (string definition)
        {
            var datas = definition.TrimStart('[').Split(']');
            Time = DateTime.Parse(datas[0]);
            var actionData = datas[1].TrimStart(' ').Split(' ');
            switch (actionData[0].ToLower())
            {
                case "falls":
                    Action = ActionType.FallsAsleep;
                    break;
                case "wakes":
                    Action = ActionType.WakeUp;
                    break;
                case "guard":
                    Action = ActionType.GuardChange;
                    GuardID = int.Parse(actionData[1][1..]);
                    break;
                default:
                    throw new Exception("Wrong Action");
            }
        }

        public override string ToString()
        {
            var time = Time.ToString("yyyy-MM-dd HH:mm");
            return Action switch
            {
                ActionType.WakeUp => $"[{time}] wakes up",
                ActionType.FallsAsleep => $"[{time}] falls asleep",
                ActionType.GuardChange => $"[{time}] Guard #{GuardID} begins shift",
                _ => "Error",
            };
        }
    }
}
