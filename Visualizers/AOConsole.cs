using System.Drawing;
using System.IO.Pipes;

namespace Visualizers
{
    public static class AOConsole
    {
        public delegate void WriteDelegate(string str);
        public delegate void ClearDelegate();
        public delegate void SetColorDelegate(AOConsoleColor color);
        public delegate AOConsoleColor GetColorDelegate();
        public delegate void SetNumberDelegate(int number);
        public delegate int GetNumberDelegate();


        private static AOConsoleColor fgColor = AOConsoleColor.White;
        private static AOConsoleColor bgColor = AOConsoleColor.Black;

        public static AOConsoleColor BackgroundColor
        {
            set => SetBackgroundColor(value);
            get => GetBackgroundColor();
        }
        public static AOConsoleColor ForegroundColor
        {
            set => SetForegroundColor(value);
            get => GetForegroundColor();
        }

        public static int CursorLeft
        {
            set => SetCursorLeft(value);
            get => GetCursorLeft();
        }
        public static int CursorTop
        {
            set => SetCursorTop(value);
            get => GetCursorTop();
        }

        public static bool Enabled { get; set; } = false;
        public static bool Debugging { get; set; } = false;
        private static SetColorDelegate? setForegroundColor;
        private static SetColorDelegate? setBackgroundColor;
        private static WriteDelegate? write;
        private static WriteDelegate? writeLine;
        private static WriteDelegate? writeDebug;
        private static WriteDelegate? writeDebugLine;
        private static ClearDelegate? clear;

        private static SetNumberDelegate? setCursorLeft;
        private static SetNumberDelegate? setCursorTop;
        private static GetNumberDelegate? getCursorLeft;
        private static GetNumberDelegate? getCursorTop;

        public static void Clear()
        {
            if (Enabled || Debugging)
            {
                clear?.Invoke();
            }
        }

        public static void Write(string text)
        {
            if (Enabled)
            {
                write?.Invoke(text);
            }
        }

        public static void WriteLine(string text)
        {
            if (Enabled)
            {
                writeLine?.Invoke(text);
            }
        }

        public static void WriteDebug(string text)
        {
            if (Debugging)
            {
                writeDebug?.Invoke(text);
            }
        }

        public static void WriteDebugLine(string text)
        {
            if (Debugging)
            {
                writeDebugLine?.Invoke(text);
            }
        }

        public static void SetForegroundColor(AOConsoleColor color)
        {
            if (Enabled)
            {
                fgColor = color; 
                setForegroundColor?.Invoke(color);
            }
        }

        public static void SetBackgroundColor(AOConsoleColor color)
        {
            if (Enabled)
            {
                fgColor = color;
                setBackgroundColor?.Invoke(color);
            }
        }

        public static AOConsoleColor GetBackgroundColor()
        {
            return bgColor;
        }

        public static AOConsoleColor GetForegroundColor()
        {
            return bgColor;
        }

        public static void SetCursorLeft(int x)
        {
            if (Enabled)
            {
                setCursorLeft?.Invoke(x);
            }
        }

        public static void SetCursorTop(int x)
        {
            if (Enabled)
            {
                setCursorTop?.Invoke(x);
            }
        }

        public static int GetCursorLeft()
        {
            return getCursorLeft?.Invoke()??0;
        }

        public static int GetCursorTop()
        {
            return getCursorTop?.Invoke()??0;
        }

        public static void RegWrite(WriteDelegate action)
        {
            write += action;
        }

        public static void RegWriteLine(WriteDelegate action)
        {
            writeLine += action;
        }

        public static void RegWriteDebug(WriteDelegate action)
        {
            writeDebug += action;
        }

        public static void RegWriteDebugLine(WriteDelegate action)
        {
            writeDebugLine += action;
        }

        public static void RegClear(ClearDelegate action)
        {
            clear += action;
        }

        public static void RegForeground(SetColorDelegate action)
        {
            setForegroundColor += action;
        }

        public static void RegBackground(SetColorDelegate action)
        {
            setBackgroundColor += action;
        }

        public static void RegCursorLeft(SetNumberDelegate action)
        {
            setCursorLeft += action;
        }
        public static void RegCursorTop(SetNumberDelegate action)
        {
            setCursorTop += action;
        }
        public static void RegCursorLeft(GetNumberDelegate action)
        {
            getCursorLeft += action;
        }
        public static void RegCursorTop(GetNumberDelegate action)
        {
            getCursorTop += action;
        }
    }
}

