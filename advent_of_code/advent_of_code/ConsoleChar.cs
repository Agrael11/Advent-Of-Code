using System;
using Visualizers;

namespace advent_of_code
{
    internal struct ConsoleChar (char character, AOConsoleColor background, AOConsoleColor foreground, bool oblique)
    {
        public char Character { get; set; } = character;
        public AOConsoleColor Background { get; set; } = background;
        public AOConsoleColor Foreground { get; set; } = foreground;
        public bool Oblique { get; set; } = oblique;
    }
}
