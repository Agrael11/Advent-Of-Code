using Visualizers;

namespace advent_of_code.Year2024.Day21
{
    internal class Common
    {
        private enum DButton { A, Left, Right, Up, Down }
        private static readonly Dictionary<(DButton a, DButton b), List<List<DButton>>> DButtonMap = new Dictionary<(DButton a, DButton b), List<List<DButton>>>();

        private static readonly Dictionary<int, (int x, int y)> NumMap = new Dictionary<int, (int x, int y)>();

        private static readonly Dictionary<(DButton, DButton, int), long> Memo = new Dictionary<(DButton, DButton, int), long>();
        public static long CrackTheCode(string code, int startLevel)
        {
            AOConsole.WriteLine($"{code}");
            var totalLength = 0L;
            var current = -1;
            foreach (var digit in code)
            {
                var next = -1;
                if (digit != 'A')
                {
                    next = digit - '0';
                }
                AOConsole.Write($"{next}: ");
                var result = MoveFromButtonToButton(current, next, startLevel);
                totalLength += result;
                AOConsole.WriteLine($"    ({result})");
                current = next;
            }
            AOConsole.WriteLine($"{totalLength}*{code[..3]}={totalLength * int.Parse(code[..3])}");
            AOConsole.WriteLine("");
            AOConsole.WriteLine("");
            return totalLength * int.Parse(code[..3]);
        }

        private static long MoveFromButtonToButton(int button1, int button2, int level)
        {
            (var x1, var y1) = NumMap[button1];
            (var x2, var y2) = NumMap[button2];
            var cantLeft = (y1 == 3) && (x2 == 0); //If we are at bottom row and want to go left
            var cantDown = (y2 == 3) && (x1 == 0); //If we are on left and want to go down
            var moves = new List<DButton>();
            if (cantLeft) //We cannot go to left directly or we would hit empty spot
            {
                while (y2 < y1)
                {
                    moves.Add(DButton.Up);
                    y1--;
                }
                while (x2 < x1)
                {
                    moves.Add(DButton.Left);
                    x1--;
                }
            }
            else if (cantDown) //Same for going down
            {
                while (x2 > x1)
                {
                    moves.Add(DButton.Right);
                    x1++;
                }
                while (y2 > y1)
                {
                    moves.Add(DButton.Down);
                    y1++;
                }
            }
            else 
            {
                //Otherwise this sounds like nice order. Worked for part 1 and i tried switching them up.
                //Please don't tell me zigzagging would be better!
                while (x2 < x1)
                {
                    moves.Add(DButton.Left);
                    x1--;
                }
                while (y2 < y1)
                {
                    moves.Add(DButton.Up);
                    y1--;
                }
                while (x2 > x1)
                {
                    moves.Add(DButton.Right);
                    x1++;
                }
                while (y2 > y1)
                {
                    moves.Add(DButton.Down);
                    y1++;
                }
            }
            moves.Add(DButton.A);

            var total = 0L;
            var current = DButton.A;

            foreach (var move in moves)
            {
                total += PressDir(current, move, level - 1);
                current = move;
            }

            return total;
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

            if (Memo.TryGetValue((start, target, level), out var price))
            {
                return price;
            }

            //Just search for lowest path out of those predefined ones
            var lowestTotal = long.MaxValue;

            var paths = DButtonMap[(start, target)];
            var current = DButton.A;

            foreach (var path in paths)
            {
                var total = 0L;
                foreach (var move in path)
                {
                    total += PressDir(current, move, level - 1);
                    current = move;
                }
                lowestTotal = long.Min(lowestTotal, total);
            }

            Memo.Add((start, target, level), lowestTotal);
            return lowestTotal;
        }

        public static void RegisterButtons()
        {
            //This are all paths from one button to another

            DButtonMap.Clear();
            DButtonMap.Add((DButton.A, DButton.Up), [[DButton.Left, DButton.A]]);
            DButtonMap.Add((DButton.A, DButton.Right), [[DButton.Down, DButton.A]]);
            DButtonMap.Add((DButton.A, DButton.Down), [[DButton.Left, DButton.Down, DButton.A],
            [DButton.Down, DButton.Left, DButton.A]]);
            DButtonMap.Add((DButton.A, DButton.Left), [[DButton.Left, DButton.Down, DButton.Left, DButton.A],
            [DButton.Down, DButton.Left, DButton.Left, DButton.A]]);

            DButtonMap.Add((DButton.Up, DButton.A), [[DButton.Right, DButton.A]]);
            DButtonMap.Add((DButton.Up, DButton.Right), [[DButton.Down, DButton.Right, DButton.A],
                [DButton.Right, DButton.Down, DButton.A]]);
            DButtonMap.Add((DButton.Up, DButton.Down), [[DButton.Down, DButton.A]]);
            DButtonMap.Add((DButton.Up, DButton.Left), [[DButton.Down, DButton.Left, DButton.A]]);

            DButtonMap.Add((DButton.Right, DButton.A), [[DButton.Up, DButton.A]]);
            DButtonMap.Add((DButton.Right, DButton.Up), [[DButton.Left, DButton.Up, DButton.A],
            [DButton.Up, DButton.Left, DButton.A]]);
            DButtonMap.Add((DButton.Right, DButton.Down), [[DButton.Left, DButton.A]]);
            DButtonMap.Add((DButton.Right, DButton.Left), [[DButton.Left, DButton.Left, DButton.A]]);

            DButtonMap.Add((DButton.Down, DButton.A), [[DButton.Up, DButton.Right, DButton.A],
            [DButton.Right, DButton.Up, DButton.A]]);
            DButtonMap.Add((DButton.Down, DButton.Up), [[DButton.Up, DButton.A]]);
            DButtonMap.Add((DButton.Down, DButton.Right), [[DButton.Right, DButton.A]]);
            DButtonMap.Add((DButton.Down, DButton.Left), [[DButton.Left, DButton.A]]);

            DButtonMap.Add((DButton.Left, DButton.A), [[DButton.Right, DButton.Up, DButton.Right, DButton.A],
                [DButton.Right, DButton.Right, DButton.Up, DButton.A]]);
            DButtonMap.Add((DButton.Left, DButton.Up), [[DButton.Right, DButton.Up, DButton.A]]);
            DButtonMap.Add((DButton.Left, DButton.Right), [[DButton.Right, DButton.Right, DButton.A]]);
            DButtonMap.Add((DButton.Left, DButton.Down), [[DButton.Right, DButton.A]]);


            //I will clean this up later

            NumMap.Clear();
            NumMap.Add(0, (1, 3));
            NumMap.Add(-1, (2, 3));
            NumMap.Add(1, (0, 2));
            NumMap.Add(2, (1, 2));
            NumMap.Add(3, (2, 2));
            NumMap.Add(4, (0, 1));
            NumMap.Add(5, (1, 1));
            NumMap.Add(6, (2, 1));
            NumMap.Add(7, (0, 0));
            NumMap.Add(8, (1, 0));
            NumMap.Add(9, (2, 0));

            Memo.Clear();
        }
    }
}
