using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code.Helpers.AsciiArtReader
{
    internal class CharacterDefinition(bool[,] asciiArt, char character)
    {
        readonly bool[,] _asciiArt = asciiArt;
        readonly int _width = asciiArt.GetLength(0);
        readonly int _height = asciiArt.GetLength(1);

        public char Character { get; private set; } = character;

        public bool IsCharacter(Grid<bool> grid, int x, int y)
        {
            for (var tx = 0; tx < _width; tx++)
            {
                var rx = tx + x;
                if (rx >= grid.Width) return false;
                for (var ty = 0; ty < _height; ty++)
                {
                    var ry = ty + y;
                    if (ry >= grid.Height) return false;

                    if (grid[rx, ry] != _asciiArt[tx, ty]) return false;
                }
            }

            return true;
        }
    }
}
