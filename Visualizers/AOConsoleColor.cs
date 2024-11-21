namespace Visualizers
{
    public class AOConsoleColor
    {
        public static readonly AOConsoleColor Black = new AOConsoleColor(255, 0, 0, 0);
        public static readonly AOConsoleColor DarkBlue = new AOConsoleColor(255, 0, 0, 139);
        public static readonly AOConsoleColor DarkGreen = new AOConsoleColor(255, 0, 100, 0);
        public static readonly AOConsoleColor DarkCyan = new AOConsoleColor(255, 0, 139, 139);
        public static readonly AOConsoleColor DarkRed = new AOConsoleColor(255, 139, 0, 0);
        public static readonly AOConsoleColor DarkMagenta = new AOConsoleColor(255, 139, 0, 139);
        public static readonly AOConsoleColor DarkYellow = new AOConsoleColor(255, 184, 134, 11);
        public static readonly AOConsoleColor Gray = new AOConsoleColor(255, 192, 192, 192);
        public static readonly AOConsoleColor DarkGray = new AOConsoleColor(255, 128, 128, 128);
        public static readonly AOConsoleColor Blue = new AOConsoleColor(255, 0, 0, 255);
        public static readonly AOConsoleColor Green = new AOConsoleColor(255, 0, 255, 0);
        public static readonly AOConsoleColor Cyan = new AOConsoleColor(255, 0, 255, 255);
        public static readonly AOConsoleColor Red = new AOConsoleColor(255, 255, 0, 0);
        public static readonly AOConsoleColor Magenta = new AOConsoleColor(255, 255, 0, 255);
        public static readonly AOConsoleColor Yellow = new AOConsoleColor(255, 255, 255, 0);
        public static readonly AOConsoleColor White = new AOConsoleColor(255, 255, 255, 255);

        public byte R {  get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
        public byte A { get; set; }

        public AOConsoleColor(byte a, byte r, byte g, byte b)
        {
            R = r; G = g; B = b; A = a;
        }
    }
}
