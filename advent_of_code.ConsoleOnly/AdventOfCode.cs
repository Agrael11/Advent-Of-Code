using Visualizers;

namespace advent_of_code.ConsoleOnly
{
    public static class AdventOfCode
    {
        public static void Register()
        {
            AOConsole.RegClear(Console.Clear);
            AOConsole.RegWrite(Console.Write);
            AOConsole.RegWriteLine(Console.WriteLine);
            AOConsole.RegWriteDebug((str) => { Console.Write($"\x1b[3m{str}\x1b[0m"); });
            AOConsole.RegWriteDebugLine((str) => { Console.WriteLine($"\x1b[3m{str}\x1b[0m"); });
            AOConsole.RegForeground((ConsoleColor color) => Console.ForegroundColor = color);
            AOConsole.RegForeground(() => Console.ForegroundColor);
            AOConsole.RegBackground((color) => Console.BackgroundColor = color);
            AOConsole.RegBackground(() => Console.BackgroundColor);
            AOConsole.RegCursorLeft((x) => Console.CursorLeft = x);
            AOConsole.RegCursorLeft(() => Console.CursorLeft);
            AOConsole.RegCursorTop((y) => Console.CursorTop = y);
            AOConsole.RegCursorTop(() => Console.CursorTop);
        }

        public static void Main()
        {
            DrawATree();
            Console.ForegroundColor = ConsoleColor.White;
            var result = "";
            var firstStart = true;
            if (!Directory.Exists("Settings")) Directory.CreateDirectory("Settings");
            if (!File.Exists(Path.Combine("Settings", "firstStart"))) File.Create(Path.Combine("Settings", "firstStart"));
            else firstStart = false;
            Console.WriteLine($"Advent of Code {DateTime.Now.Year}!");
            while (!result.Equals("Q", StringComparison.CurrentCultureIgnoreCase))
            {
                if (firstStart)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"You can select the challenge using year and days number [2013-{DateTime.Now.Year}] [1-25]");
                    Console.WriteLine("You can also select to run all available challenges by using letter \"A\"");
                    Console.WriteLine("You can delete your cached inputs by writing \"D\"");
                    Console.WriteLine("To quit write letter \"Q\"");
                    Console.WriteLine("To show this info, write letter \"I\"");
                    Console.WriteLine();
                    Console.WriteLine("This program is able to automatically download your inputs for Advent of Code");
                    Console.WriteLine("To do that you need to provide \"cookie.txt\" file in \"Settings\" folder of program.");
                    Console.WriteLine("To get detailed info about how to get \"cookie.txt\", use [C] option.");
                    Console.WriteLine("Your inputs are cached in \"Inputs\" folder.");
                    Console.WriteLine("You can also put them in manually in format \"inputData_{year}_{day}.txt\".");
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("What is your choice? ");
                    firstStart = false;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"Select the year and challenge [2013-{DateTime.Now.Year} 1-25] or all challenges [A], write [I] for more info or [Q] to quit: ");
                }
                result = Console.ReadLine() ?? "";

                var resultTemp = result.Split(' ');
                if (int.TryParse(resultTemp[0], out var year) && int.TryParse(resultTemp[1], out var day) && day >= 1 && day <= 25)
                {
                    if (ChallangeHandling.ClassExists(year, day))
                    {
                        DoChallenge(year, day);
                        Console.WriteLine();
                    }
                    else
                    {
                        DrawATree();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Challenge for Day {day} of {year} is not finished yet.");
                        Console.WriteLine();
                    }
                }
                else if (result.Equals("A", StringComparison.CurrentCultureIgnoreCase))
                {
                    DrawATree();
                    DoAllChallenges();
                }
                else if (result.Equals("I", StringComparison.CurrentCultureIgnoreCase))
                {
                    DrawATree();
                    firstStart = true;
                }
                else if (result.Equals("C", StringComparison.CurrentCultureIgnoreCase))
                {
                    DrawATree();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("To get your Advent of Code login cookie:");
                    Console.WriteLine("1) Open input in your browser when logged in.");
                    Console.WriteLine("2) Use \"F12\" to open developer tools, and select \"Network\" tab");
                    Console.WriteLine("3) Refresh the page");
                    Console.WriteLine("4) Select \"input\"");
                    Console.WriteLine("5) On right side, in  Headers tab, find \"Cookie\"");
                    Console.WriteLine("6) Copy the text of a Cookie");
                    Console.WriteLine("7) Paste the text into \"cookie.txt\" file in \"Settings\" folder of program");
                    Console.WriteLine();
                }
                else if (result.Equals("D", StringComparison.CurrentCultureIgnoreCase))
                {
                    DrawATree();
                    try
                    {
                        FileHandling.DeleteDírectory("Inputs");
                    }
                    catch
                    {

                    }
                }
                else if (!result.Equals("Q", StringComparison.CurrentCultureIgnoreCase))
                {
                    DrawATree();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Wrong command.");
                    Console.WriteLine();
                }
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private static ulong DoChallenge(int year, int day, bool nice = true)
        {
            if (nice)
            {
                DrawATree();
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"Results of Day {day} of {year} are: ");
            _ = advent_of_code.ChallangeHandling.GetInputAsync(year, day);
            var task1 = advent_of_code.ChallangeHandling.RunTaskAsync(year, day, 1);
            task1.Wait();
            (var watch1, var result1) = task1.Result;
            Console.ForegroundColor = ConsoleColor.Cyan;
            if (result1.Contains('\n') || result1.Length > 20)
            {
                Console.WriteLine();
                Console.WriteLine(result1);
            }
            else
            {
                Console.Write(result1);
            }
            Console.ForegroundColor = ConsoleColor.White;
            var task2 = advent_of_code.ChallangeHandling.RunTaskAsync(year, day, 2);
            task2.Wait();
            (var watch2, var result2) = task2.Result;
            Console.Write(" and ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            if (result2.Contains('\n') || result2.Length > 20)
            {
                Console.WriteLine();
                Console.WriteLine(result2);
            }
            else
            {
                Console.Write(result2);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("! ");
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("It took ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(FormatTime((ulong)watch1.Elapsed.TotalMilliseconds));
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" and ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(FormatTime((ulong)watch2.Elapsed.TotalMilliseconds));
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(".");
            return (ulong)watch1.ElapsedMilliseconds + (ulong)watch2.ElapsedMilliseconds;
        }

        private static void DoAllChallenges()
        {
            ulong totalTime = 0;
            for (var year = 2015; year <= DateTime.Now.Year; year++)
            {
                for (var i = 1; i <= 25; i++)
                {
                    if (advent_of_code.ChallangeHandling.ClassExists(year, i))
                    {
                        totalTime += DoChallenge(year, i, false);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Total time is: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(FormatTime(totalTime));
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(".");
            Console.WriteLine();
        }

        static private string FormatTime(ulong milliseconds)
        {
            var milli = (uint)(milliseconds % 1000);
            var seconds = (uint)(milliseconds / 1000);
            var minutes = seconds / 60;
            seconds %= 60;
            var hours = minutes / 60;
            minutes %= 60;
            var time = "";
            if (hours > 0) time = hours.ToString().PadLeft(2, '0') + ":";
            if (minutes > 0 || hours > 0) time += minutes.ToString().PadLeft(2, '0') + ":";
            if (seconds > 0 && (minutes > 0 || hours > 0)) time += seconds.ToString().PadLeft(2, '0') + "." + milli.ToString().PadLeft(3, '0');
            else if (seconds > 0) time += seconds.ToString().PadLeft(2, '0') + "." + milli.ToString().PadLeft(3, '0') + "s";
            else time += milli.ToString().PadLeft(3, '0') + "ms";
            return time;
        }

        private static void DrawATree()
        {
            Console.Clear();
            for (var i = 0; i < Console.WindowWidth / 40 * Console.WindowHeight; i++)
            {
                Console.ForegroundColor = (ConsoleColor)new Random().Next(1, 16);
                var x = new Random().Next(0, Console.WindowWidth);
                var y = new Random().Next(0, Console.WindowHeight);
                Console.CursorLeft = x;
                Console.CursorTop = y;
                Console.Write('*');

            }
            Console.CursorLeft = 0;
            Console.CursorTop = 0;

            DrawLine(@"()", 11, 25);
            DrawLine(@"/\", 11, 23);
            DrawLine(@"/i\\", 10, 21);
            DrawLine(@"o/\\", 10, 19);
            DrawLine(@"///\i\", 9, 17);
            DrawLine(@"/*/o\\", 9, 15);
            DrawLine(@"/i//\*\", 8, 13);
            DrawLine(@"/ o/*\\i\", 8, 11);
            DrawLine(@"//i//o\\\\", 7, 9);
            DrawLine(@"/*////\\\\i\", 6, 7);
            DrawLine(@"//o//i\\*\\\", 6, 5);
            DrawLine(@"/i///*/\\\\\o\", 5, 3);
            DrawLine(@"||", 11, 1);
        }

        private static void DrawLine(string line, int left, int day)
        {
            Console.CursorLeft += left;
            foreach (var c in line)
            {
                Console.ForegroundColor = c == '\\' || c == '/'
                    ? ConsoleColor.Green
                    : c == '(' || c == ')' ? ConsoleColor.Yellow : (ConsoleColor)new Random().Next(1, 16);
                if (!(DateTime.Now.Month == 12 && DateTime.Now.Day >= day))
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }
                Console.Write(c);
            }
            Console.WriteLine();
        }
    }
}
