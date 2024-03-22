using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code.Year2016.Day10
{
    internal class Bot
    {
        public enum TargetType { Bot, Output, Unassigned};


        readonly List<int> values = new List<int>();

        public int LowValue => (values[0] > values[1]) ? values[1] : values[0];
        public int HighValue => (values[0] > values[1]) ? values[0] : values[1];

        public (TargetType targetType, int targetId) LowTarget { get; set; } = (TargetType.Unassigned, 0);
        public (TargetType targetType, int targetId) HighTarget { get; set; } = (TargetType.Unassigned, 0);

        public bool ReadyToSend()
        {
            return values.Count == 2 && LowTarget.targetType != TargetType.Unassigned && HighTarget.targetType != TargetType.Unassigned;
        }

        public void AddValue(int value)
        {
            values.Add(value);
        }
    }
}
