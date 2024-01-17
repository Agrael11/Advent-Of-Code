using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code.Year2015.Day15
{
    internal class Ingridient
    {
        public int Capacity { get; set; }
        public int Durability { get; set; }
        public int Flavor { get; set; }
        public int Texture { get; set; }
        public int Calories { get; set; }

        public Ingridient(string description)
        {
            string[] desc = description.Split(',');
            foreach (string part in desc)
            {
                string[] partSplit = part.Trim(' ').Split(" ");
                switch (partSplit[0])
                {
                    case "capacity": Capacity = int.Parse(partSplit[1]); break;
                    case "durability": Durability = int.Parse(partSplit[1]); break;
                    case "flavor": Flavor = int.Parse(partSplit[1]); break;
                    case "texture": Texture = int.Parse(partSplit[1]); break;
                    case "calories": Calories = int.Parse(partSplit[1]); break;
                }
            }
        }
    }
}
