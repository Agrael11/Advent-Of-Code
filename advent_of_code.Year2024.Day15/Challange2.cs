using System.Text;
using Visualizers;

namespace advent_of_code.Year2024.Day15
{
    public static class Challange2
    {
        private enum EntityType { Empty, BoxLeft, BoxRight, Wall };

        private static int Width = 0;
        private static int Height = 0;
        private static EntityType[,] Map = new EntityType[0, 0];
        private static (int X, int Y) Player = (0, 0);

        private static readonly (int OffsetX, int OffsetY)[] Offsets = [(-1, 0), (+1, 0), (0, -1), (0, +1)];

        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");
            var instructions = Parse(input);

            for (var i = 0; i < instructions.Length; i++)
            {
                var instruction = instructions[i];
                TryMove(instruction);
                DrawMap(true); //I need to add "Enable Visualisation" button :P
            }

            DrawMap(true);

            var total = 0L;

            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    if (Map[x, y] == EntityType.BoxLeft)
                    {
                        total += y * 100 + x;
                    }

                }
            }

            return total;
        }

        private static void DrawMap(bool colored)
        {
            if (!AOConsole.Enabled) return;

            AOConsole.CursorLeft = 0;
            AOConsole.CursorTop = 0;
            var builder = new StringBuilder();
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    if (Player.X == x && Player.Y == y)
                    {
                        if (colored)
                        {
                            AOConsole.ForegroundColor = AOConsoleColor.Gray;
                            AOConsole.Write(builder.ToString());
                            AOConsole.ForegroundColor = AOConsoleColor.Green;
                            AOConsole.Write("@");
                            AOConsole.ForegroundColor = AOConsoleColor.Gray;
                            builder.Clear();
                            continue;
                        }
                        builder.Append('☻');
                        continue;
                    }
                    switch (Map[x, y])
                    {
                        case EntityType.BoxLeft:
                            builder.Append('╠');
                            break;
                        case EntityType.BoxRight:
                            builder.Append('╣');
                            break;
                        case EntityType.Wall:
                            builder.Append('█');
                            break;
                        default:
                        case EntityType.Empty:
                            builder.Append(' ');
                            break;
                    }

                }
                builder.AppendLine();
            }
            AOConsole.Write(builder.ToString());
        }

        private static bool TryMove(int direction)
        {
            var nextX = Player.X + Offsets[direction].OffsetX;
            var nextY = Player.Y + Offsets[direction].OffsetY;
            var itemAt = Map[nextX, nextY];
            if (itemAt == EntityType.Wall) return false;

            if (!(itemAt == EntityType.Empty) && !TryPush(direction)) return false;

            Player.X = nextX;
            Player.Y = nextY;
            return true;
        }

        private static bool TryPush(int direction)
        {
            if (direction < 2) return TryPushHorizontal(direction);
            return TryPushVertical(direction);
        }

        //Similar to part 1s TryPush, but only horizontal and
        private static bool TryPushHorizontal(int direction)
        {
            (var offsetX, _) = Offsets[direction];
            var startX = Player.X + offsetX;
            var startY = Player.Y;
            
            //We are dealing with box parts
            if (Map[startX, startY] == EntityType.BoxRight)
            {
                startX--;
            }
            
            //We also need to keep track of boxes to push now - in reverse order
            var boxesToPush = new Stack<int>();
            boxesToPush.Push(startX);
            
            var currentX = startX;
            
            for (; ; )
            {
                currentX += offsetX;
                
                //If we're going to right, we need to move one over to find a box
                if (offsetX == 1)
                {
                    currentX++;
                }
                
                var currentBox = Map[currentX, startY];
                
                if (currentBox == EntityType.Wall)
                {
                    return false;
                }
                if (currentBox == EntityType.Empty)
                {
                    break;
                }

                //Should happen only if we're moving left - we move to left part of it
                if (currentBox == EntityType.BoxRight)
                {
                    currentX -= 1;
                }
                boxesToPush.Push(currentX);
            }

            //And now we push all boxes by 1
            while (boxesToPush.Count > 0)
            {
                var x = boxesToPush.Pop();
                var nextX = x + offsetX;
                Map[x, startY] = EntityType.Empty;
                Map[x + 1, startY] = EntityType.Empty;
                Map[nextX, startY] = EntityType.BoxLeft;
                Map[nextX + 1, startY] = EntityType.BoxRight;
            }

            return true;
        }
        
        //This handles vertical pushing
        private static bool TryPushVertical(int direction)
        {
            (var offsetX, var offsetY) = Offsets[direction];
            var startX = Player.X + offsetX;
            var startY = Player.Y + offsetY;
            var boxesToPush = new List<(int x, int y)>();

            //I decided to use DFS for it - will find wall sooner than BFS if there is any
            //it's easy to implement and works well for this problem
            var stack = new Stack<(int, int)>();
            stack.Push((startX, startY));

            while (stack.Count > 0)
            {
                (var currentX, var currentY) = stack.Pop();
                var currentBox = Map[currentX, currentY];
                if (currentBox == EntityType.BoxRight)
                {
                    currentX--;
                    currentBox = Map[currentX, currentY];
                }
                boxesToPush.Add((currentX, currentY));
                var offY = currentY + offsetY;

                //Finds boxes on left and right from our position
                var leftBox = Map[currentX, offY];
                var rightBox = Map[currentX + 1, offY];

                //If any of them is a wall... this is not possible to push
                if (leftBox == EntityType.Wall || rightBox == EntityType.Wall)
                {
                    return false;
                }

                //This just adds left and right boxes (if they exist) to the queue for check.
                if (leftBox == EntityType.BoxLeft)
                {
                    stack.Push((currentX, offY));
                }
                else if (leftBox == EntityType.BoxRight) //This automatically makes rightBox == EntityType.BoxLeft true
                {
                    stack.Push((currentX - 1, offY));
                }
                if (rightBox == EntityType.BoxLeft)
                {
                    stack.Push((currentX + 1, offY));
                }
            }

            //I would rather sort the list than think about how to do it properly :D
            switch (direction)
            {
                case 2:
                    boxesToPush = boxesToPush.OrderBy(box => box.y).ToList();
                    break;
                case 3:
                    boxesToPush = boxesToPush.OrderByDescending(box => box.y).ToList();
                    break;
            }
            
            foreach (var (x, y) in boxesToPush)
            {
                var nextX = x + offsetX;
                var nextY = y + offsetY;
                Map[x, y] = EntityType.Empty;
                Map[x+1, y] = EntityType.Empty;
                Map[nextX, nextY] = EntityType.BoxLeft;
                Map[nextX+1, nextY] = EntityType.BoxRight;
            }
            return true;
        }

        //Similar to part 1
        private static int[] Parse(string[] input)
        {
            for (var y = 0; y < input.Length; y++)
            {
                var line = input[y];
                if (string.IsNullOrWhiteSpace(line))
                {
                    Height = y;
                    break;
                }
            }
            Width = input[0].Length * 2; //But width is double
            Map = new EntityType[Width, Height];

            //And basically everything on X is double
            for (var y = 0; y < Height; y++)
            {
                var line = input[y];
                for (var x = 0; x < Width / 2; x++)
                {
                    switch (line[x])
                    {
                        case 'O':
                            Map[x * 2, y] = EntityType.BoxLeft;
                            Map[x * 2 + 1, y] = EntityType.BoxRight;
                            break;
                        case '#':
                            Map[x * 2, y] = EntityType.Wall;
                            Map[x * 2 + 1, y] = EntityType.Wall;
                            break;
                        case '@':
                            Player = (x*2, y);
                            break;
                    }
                }
            }

            return string.Join("", input[(Height + 1)..]).Select(c =>
            {
                return c switch
                {
                    '<' => 0,
                    '>' => 1,
                    '^' => 2,
                    'v' => 3,
                    _ => throw new Exception($"Unknown symbol {c}"),
                };
            }).ToArray();
        }
    }
}