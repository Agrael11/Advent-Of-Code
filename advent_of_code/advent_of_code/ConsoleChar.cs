using System;

namespace advent_of_code
{
    internal struct ConsoleChar (char character, ConsoleColor background, ConsoleColor foreground, bool oblique)
    {
        public char Character { get; set; } = character;
        public ConsoleColor Background { get; set; } = background;
        public ConsoleColor Foreground { get; set; } = foreground;
        public bool Oblique { get; set; } = oblique;
    }
}
