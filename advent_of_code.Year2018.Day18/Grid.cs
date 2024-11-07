using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code.Year2018.Day18
{
    internal class Grid(int width, int height)
    {
        internal enum State { Ground = '.', Trees = '|', Lumberyard = '#'};

        private State[] states = new State[width * height];
        private readonly State[] statesNew = new State[width * height];

        public int Width { get; private set; } = width;
        public int Height { get; private set; } = height;
        public int Length { get; private set; } = width * height;

        public State this[int index]
        {
            get => states[index];
            set => statesNew[index] = value;
        }

        public State this[int x, int y]
        {
            get => states[y * Width + x];
            set => statesNew[y * Width + x] = value;
        }

        public State Get(int x, int y)
        {
            return states[(y * Width) + x];
        }

        public void Set(int x, int y, State state)
        {
            statesNew[(y * Width) + x] = state;
        }

        public void Apply()
        {
            states = [..statesNew];
        }

        public int Count(State state)
        {
            return states.Count(s => s == state);
        }

        public int CountAround(State state, int x, int y)
        {
            var total = 0;

            for (var nY = y - 1; nY <= y + 1; nY++)
            {
                for (var nX = x - 1; nX <= x + 1; nX++)
                {
                    if (nY == y && nX == x) continue;
                    if (nX < 0 || nX >= Width || nY < 0 || nY >= Height) continue;
                    
                    total += (states[nY*Width+nX] == state) ? 1 : 0;
                }
            }

            return total;
        }

        public int GetGridHash()
        {
            var sb = new StringBuilder();
            for (var i = 0; i < states.Length; i++)
            {
                sb.Append((char)states[i]);
            }
            return sb.ToString().GetHashCode();
        }
    }
}
