namespace Visualizers
{
    public static class AOConsole
    {
        public delegate void WriteDelegate(string str);
        public delegate void ClearDelegate();
        public delegate void ColorDelegate(ConsoleColor color);

        public static ConsoleColor BackgroundColor
        {
            set => SetBackgroundColor(value);
        }
        public static ConsoleColor ForegroundColor
        {
            set => SetForegroundColor(value);
        }

        public static bool Enabled { get; set; } = false;
        private static ColorDelegate setForegroundColor = (ConsoleColor color) => Console.ForegroundColor = color;
        private static ColorDelegate setBackgroundColor = (ConsoleColor color) => Console.BackgroundColor = color;
        private static WriteDelegate write = Console.Write;
        private static WriteDelegate writeLine = Console.WriteLine;
        private static ClearDelegate clear = Console.Clear;

        public static void Clear()
        {
            if (Enabled)
            {
                clear.Invoke();
            }
        }

        public static void Write(string text)
        {
            if (Enabled)
            {
                write.Invoke(text);
            }
        }

        public static void WriteLine(string text)
        {
            if (Enabled)
            {
                writeLine.Invoke(text);
            }
        }
        public static void SetForegroundColor(ConsoleColor color)
        {
            if (Enabled)
            {
                setForegroundColor.Invoke(color);
            }
        }

        public static void SetBackgroundColor(ConsoleColor color)
        {
            if (Enabled)
            {
                setBackgroundColor.Invoke(color);
            }
        }
        public static void RegWrite(WriteDelegate action)
        {
            write += action;
        }

        public static void RegWriteLine(WriteDelegate action)
        {
            writeLine += action;
        }

        public static void RegClear(ClearDelegate action)
        {
            clear += action;
        }

        public static void RegSetForeground(ColorDelegate action)
        {
            setForegroundColor += action;
        }

        public static void RegSetBackground(ColorDelegate action)
        {
            setBackgroundColor += action;
        }
    }
}

