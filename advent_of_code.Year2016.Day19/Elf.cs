using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code.Year2016.Day19
{
    internal class Elf
    {
        public Elf PreviousElf { get; set; }
        public Elf NextElf { get; set; }
        public int ID { get; set; }

        public Elf()
        {
            PreviousElf = this;
            NextElf = this;
            ID = -1;
        }
        public Elf(int id)
        {
            PreviousElf = this;
            NextElf = this;
            ID = id;
        }

        public override string ToString()
        {
            return $"Elf {ID} {{{PreviousElf.ID}-{NextElf.ID}}}";
        }
    }
}
