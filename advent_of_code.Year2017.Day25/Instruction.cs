using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code.Year2017.Day25
{
    internal class Instruction(string[] data)
    {
        public bool NewValue { get; private set; } = data[0].Split(' ')[^1].TrimEnd('.') == "1";
        public int Direction { get; private set; } = (data[1].Split(' ')[^1].TrimEnd('.') == "right") ? 1 : -1;
        public string TargetState { get; private set; } = data[2].Split(' ')[^1].TrimEnd('.');
    }
}
