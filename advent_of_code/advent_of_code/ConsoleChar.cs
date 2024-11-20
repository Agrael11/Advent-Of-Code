using System;

namespace advent_of_code
{
    internal struct ConsoleChar (char character, ConsoleColor background, ConsoleColor foreground)
    {
        public char Character { get; set; } = character;
        public ConsoleColor Background { get; set; } = background;
        public ConsoleColor Foreground { get; set; } = foreground;
    }
}
