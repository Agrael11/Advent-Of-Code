using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code.Year2016.Day24
{
    internal class Node(int id, int x, int y)
    {
        public int Id { get; private set; } = id;
        public Dictionary<int, int> Targets { get; } = new Dictionary<int, int>();
        public (int x, int y) Position { get; } = (x, y);

        public void AddConnection(int id, int distance)
        {
            Targets.Add(id, distance);
        }
    }
}
