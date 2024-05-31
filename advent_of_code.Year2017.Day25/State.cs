using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code.Year2017.Day25
{
    internal class State(string[] data)
    {
        public Instruction Instruction0 { get; private set; } = new Instruction(data[2..]);
        public Instruction Instruction1 { get; private set; } = new Instruction(data[6..]);
    }
}
