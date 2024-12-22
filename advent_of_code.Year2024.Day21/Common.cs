using advent_of_code.Helpers;
using Visualizers;

namespace advent_of_code.Year2024.Day21
{
    internal class Common
    {
        private enum DButton { A, Left, Right, Up, Down }
        private static readonly Dictionary<(DButton a, DButton b), List<List<DButton>>> DButtonMap = new Dictionary<(DButton a, DButton b), List<List<DButton>>>();

        private static readonly Dictionary<int, (int x, int y)> NumpadMap = new Dictionary<int, (int x, int y)>();

        private static readonly Cache<(DButton, DButton, int), long> Cache = new Cache<(DButton, DButton, int), long>();
        private static readonly (int offsetX, int offsetY, DButton button)[] OFFSETS = [(-1, 0, DButton.Left), (+1, 0, DButton.Right), (0, -1, DButton.Up), (0, +1, DButton.Down)];

        public static long CrackTheCode(string code, int startLevel)
        {
            AOConsole.ForegroundColor = AOConsoleColor.Green;
            AOConsole.WriteLine($"{code}");

            var totalLength = 0L;
            var current = -1;
            foreach (var digit in code)
            {
                //Selects next digit
                var next = -1;
                if (digit != 'A')
                {
                    next = digit - '0';
                }

                AOConsole.ForegroundColor = AOConsoleColor.White;
                AOConsole.Write($"{next}: ");
                //Finds fastest "path" to next button from the one we are at now
                var result = MoveFromButtonToButton(current, next, startLevel);
                AOConsole.ForegroundColor = AOConsoleColor.Yellow;
                AOConsole.WriteLine($"    ({result})");

                totalLength += result;
                //Sets the current button to one we just pressed
                current = next;
            }


            AOConsole.ForegroundColor = AOConsoleColor.Green;
            AOConsole.WriteLine($"{totalLength}*{code[..3]}={totalLength * int.Parse(code[..3])}");
            AOConsole.WriteLine("\n");
            return totalLength * int.Parse(code[..3]);
        }

        private static long MoveFromButtonToButton(int button1, int button2, int level)
        {
            (var x1, var y1) = NumpadMap[button1];
            (var x2, var y2) = NumpadMap[button2];

            //I ended up using Dijkstra for this so i don't have to hardcode every path from every button.
            var pqueue = new PriorityQueue<(int x, int y, DButton last), long>();
            var visited = new HashSet<(int, int, DButton)>();
            pqueue.Enqueue((x1, y1, DButton.A), 0);
            while (pqueue.Count > 0)
            {
                pqueue.TryDequeue(out var current, out var price);


                if (!visited.Add(current))
                    continue;

                long result;

                //IF we are at end
                if (current.x == x2 && current.y == y2)
                {
                    //And have pressed A - we are already at best result
                    if (current.last == DButton.A)
                    {
                        return price;
                    }
                    else
                    {
                        //Otherwise we try to press A (not necessarily next best result)

                        result = PressDir(current.last, DButton.A, level - 1);
                        pqueue.Enqueue((current.x, current.y, DButton.A), price + result);
                        continue;
                    }
                }

                //We try to move in every other direction on numpad. could have been optimizied, but this is just... easier
                foreach (var (offsetX, offsetY, button) in OFFSETS)
                {
                    var nextX = current.x + offsetX;
                    var nextY = current.y + offsetY;
                    if (nextX < 0 || nextX > 2 || nextY < 0 || nextY > 3)
                        continue;
                    if (nextX == 0 && nextY == 3)
                        continue;
                    result = PressDir(current.last, button, level - 1);
                    pqueue.Enqueue((nextX, nextY, button), price + result);
                }
            }

            return -1;
        }


        private static long PressDir(DButton start, DButton target, int level)
        {
            if (level == 0)
            {
                return 1;
            }

            if (target == start) //If we want to stay, all other robots would just push A - that is one push total
            {
                return 1;
            }

            return Cache.TryGetResult((start, target, level), ((DButton start, DButton target, int level) state) =>
            {
                //Just search for lowest path out of those predefined ones
                var lowestTotal = long.MaxValue;

                var paths = DButtonMap[(state.start, state.target)];
                var current = DButton.A;

                foreach (var path in paths)
                {
                    var total = 0L;
                    foreach (var move in path)
                    {
                        total += PressDir(current, move, state.level - 1);
                        current = move;
                    }
                    lowestTotal = long.Min(lowestTotal, total);
                }

                return lowestTotal;
            });
        }

        public static void Prepare()
        {
            RegisterNumpad();
            RegisterDPad();

            Cache.Clear();
        }

        private static void RegisterDPad()
        {
            DButtonMap.Clear();
            //This are all paths from one button to another
            //Most buttons have only one way to get to another
            //But some have two

            //A
            DButtonMap.Add((DButton.A, DButton.Up), [[DButton.Left, DButton.A]]);
            DButtonMap.Add((DButton.A, DButton.Right), [[DButton.Down, DButton.A]]);
            //A => Down can be <v or v<
            DButtonMap.Add((DButton.A, DButton.Down), [[DButton.Left, DButton.Down, DButton.A],
            [DButton.Down, DButton.Left, DButton.A]]);
            //A => Left  can be <v< or v<< (never <<v as it would hit empty spot above Left)
            DButtonMap.Add((DButton.A, DButton.Left), [[DButton.Left, DButton.Down, DButton.Left, DButton.A],
            [DButton.Down, DButton.Left, DButton.Left, DButton.A]]);

            //Up
            DButtonMap.Add((DButton.Up, DButton.A), [[DButton.Right, DButton.A]]);
            //Up => Right can be v> or >v
            DButtonMap.Add((DButton.Up, DButton.Right), [[DButton.Down, DButton.Right, DButton.A],
                [DButton.Right, DButton.Down, DButton.A]]);
            DButtonMap.Add((DButton.Up, DButton.Down), [[DButton.Down, DButton.A]]);
            DButtonMap.Add((DButton.Up, DButton.Left), [[DButton.Down, DButton.Left, DButton.A]]);

            //Right
            DButtonMap.Add((DButton.Right, DButton.A), [[DButton.Up, DButton.A]]);
            //Right => Up can be <^ or ^<
            DButtonMap.Add((DButton.Right, DButton.Up), [[DButton.Left, DButton.Up, DButton.A],
            [DButton.Up, DButton.Left, DButton.A]]);
            DButtonMap.Add((DButton.Right, DButton.Down), [[DButton.Left, DButton.A]]);
            DButtonMap.Add((DButton.Right, DButton.Left), [[DButton.Left, DButton.Left, DButton.A]]);

            //Down
            //Down => A can be ^> or >^
            DButtonMap.Add((DButton.Down, DButton.A), [[DButton.Up, DButton.Right, DButton.A],
            [DButton.Right, DButton.Up, DButton.A]]);
            DButtonMap.Add((DButton.Down, DButton.Up), [[DButton.Up, DButton.A]]);
            DButtonMap.Add((DButton.Down, DButton.Right), [[DButton.Right, DButton.A]]);
            DButtonMap.Add((DButton.Down, DButton.Left), [[DButton.Left, DButton.A]]);

            //Left
            //Left => A can be >^> or >>^ (never ^>> as it would hit empty spot above Left)
            DButtonMap.Add((DButton.Left, DButton.A), [[DButton.Right, DButton.Up, DButton.Right, DButton.A],
                [DButton.Right, DButton.Right, DButton.Up, DButton.A]]);
            DButtonMap.Add((DButton.Left, DButton.Up), [[DButton.Right, DButton.Up, DButton.A]]);
            DButtonMap.Add((DButton.Left, DButton.Right), [[DButton.Right, DButton.Right, DButton.A]]);
            DButtonMap.Add((DButton.Left, DButton.Down), [[DButton.Right, DButton.A]]);
        }

        private static void RegisterNumpad()
        {
            //Registers Numerical Key Positions

            NumpadMap.Clear();
            NumpadMap.Add(0, (1, 3));
            NumpadMap.Add(-1, (2, 3));
            NumpadMap.Add(1, (0, 2));
            NumpadMap.Add(2, (1, 2));
            NumpadMap.Add(3, (2, 2));
            NumpadMap.Add(4, (0, 1));
            NumpadMap.Add(5, (1, 1));
            NumpadMap.Add(6, (2, 1));
            NumpadMap.Add(7, (0, 0));
            NumpadMap.Add(8, (1, 0));
            NumpadMap.Add(9, (2, 0));
        }
    }
}
