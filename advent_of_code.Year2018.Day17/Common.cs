using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code.Year2018.Day17
{
    internal static class Common
    {
        private static readonly List<OLine> Walls = new List<OLine>();
        private static readonly List<OLine> StillWater = new List<OLine>();
        private static readonly Dictionary<(int X, int Y), OLine> Flows = new Dictionary<(int X, int Y), OLine>();

        private static readonly (int X, int Y) WaterSource = (500, 0);
        private static int Top;
        private static int Bottom;

        public static void Reset(string[] input)
        {
            Walls.Clear();
            StillWater.Clear();
            Flows.Clear();

            Top = int.MaxValue;
            Bottom = int.MinValue;

            foreach (var line in input)
            {
                var coords = line.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                var x1 = -1;
                var x2 = -1;
                var y1 = -1;
                var y2 = -1;
                ParseInfo(coords[0].Split('='), ref x1, ref x2, ref y1, ref y2);
                ParseInfo(coords[1].Split('='), ref x1, ref x2, ref y1, ref y2);
                Top = int.Min(Top, y1);
                Bottom = int.Max(Bottom, y2);
                Walls.Add(new OLine(x1, x2, y1, y2));
            }
        }

        public static (int still, int flowing, int sum) Run()
        {
            var first = VerticalScan(WaterSource.X, WaterSource.Y);

            var linesToDo = new Stack<OLine>();
            linesToDo.Push(new OLine(WaterSource.X, WaterSource.X, int.Max(WaterSource.Y, Top), first - 1));

            while (linesToDo.Count > 0)
            {
                var current = linesToDo.Pop();
                var nextLines = GetNextLines(current);
                foreach (var line in nextLines)
                {
                    linesToDo.Push(line);
                }
            }

            var stillWaterBlocks = StillWater.Sum(w => w.GetLength());
            var flowWaterBlocks = Flows.Sum(f => f.Value.GetLength());
            return (stillWaterBlocks, flowWaterBlocks, stillWaterBlocks + flowWaterBlocks);
        }

        private static List<OLine> GetNextLines(OLine current)
        {
            var lines = new List<OLine>();
            if (current.Orientation == OLine.Orientations.Vertical)
            {
                if (Flows.Any(f => f.Value.Intersects(current)))
                {
                    Flows.TryAdd((current.X1, current.Y1), new OLine(current.X1, current.X2, current.Y1, Flows.Where(t => t.Value.Intersects(current)).OrderBy(t => t.Value.Y1).First().Value.Y1 - 1));
                }
                else if (StillWater.Any(w => w.IsPointOnLine(current.X1, current.Y2)))
                {
                    if (current.Y2 > current.Y1)
                    {
                        lines.Add(new OLine(current.X1, current.X2, current.Y1, current.Y2 - 1));
                    }
                }
                else
                {
                    var (left, right) = HorizontalScan(current.X1, current.Y2);
                    if (left.hit && right.hit)
                    {
                        var waterLine = new OLine(left.pos + 1, right.pos - 1, current.Y2, current.Y2);
                        StillWater.Add(waterLine);
                        if (current.Y2 > current.Y1)
                        {
                            lines.Add(new OLine(current.X1, current.X2, current.Y1, current.Y2 - 1));
                        }
                    }
                    else
                    {
                        var waterLine = new OLine(left.pos + ((left.hit) ? 1 : 0), right.pos - ((right.hit) ? 1 : 0), current.Y2, current.Y2)
                        {
                            LeftHit = left.hit,
                            RightHit = right.hit
                        };
                        if (!Flows.ContainsValue(waterLine))
                        {
                            lines.Add(new OLine(current.X1, current.X2, current.Y1, current.Y2));
                            lines.Add(waterLine);
                        }
                        else if (current.Y2 > current.Y1)
                        {
                            Flows.TryAdd((current.X1, current.Y1), new OLine(current.X1, current.X2, current.Y1, current.Y2 - 1));
                        }
                    }
                }
            }
            else //Horizontal
            {
                var added = 0;
                if (!current.LeftHit)
                {
                    var vLineBottom = VerticalScan(current.X1, current.Y1 + 1);
                    if (vLineBottom == -1)
                    {
                        Flows.TryAdd((current.X1, current.Y1 + 1), new OLine(current.X1, current.X1, current.Y1 + 1, Bottom));
                    }
                    else if (!Flows.ContainsKey((current.X1, current.Y1 + 1)))
                    {
                        lines.Add(new OLine(current.X1, current.X1, current.Y1 + 1, vLineBottom - 1));
                        added++;
                    }
                }
                if (!current.RightHit)
                {
                    var vLineBottom = VerticalScan(current.X2, current.Y1 + 1);
                    if (vLineBottom == -1)
                    {
                        Flows.TryAdd((current.X2, current.Y1 + 1), new OLine(current.X2, current.X2, current.Y1 + 1, Bottom));
                    }
                    else if (!Flows.ContainsKey((current.X2, current.Y1 + 1)))
                    {
                        lines.Add(new OLine(current.X2, current.X2, current.Y1 + 1, vLineBottom - 1));
                        added++;
                    }
                }
                if (added == 0)
                {
                    Flows.TryAdd((current.X1, current.Y1), current);
                }
            }

            return lines;
        }

        private static int VerticalScan(int x, int y)
        {
            var possibleWalls = Walls.Where(w => w.X2 >= x && w.X1 <= x && w.Y1 > y);
            var possibleWaters = StillWater.Where(w => w.X2 >= x && w.X1 <= x && w.Y1 > y);
            var possibleCollisions = possibleWalls.Concat(possibleWaters);
            if (!possibleCollisions.Any()) return -1;
            return possibleCollisions.OrderBy(w => w.Y1).First().Y1;
        }

        private static ((int pos, bool hit) left, (int pos, bool hit) right) HorizontalScan(int x, int y)
        {
            var possibleWallsHorizontal = Walls.Where(w => w.Y2 >= y && w.Y1 <= y);
            var possibleWallsUnderneath = Walls.Where(w => w.Y1 == y + 1);
            var possibleWatersUnderneath = StillWater.Where(w => w.Y1 == y + 1);
            var possibleCollisionsUndernaeth = possibleWallsUnderneath.Concat(possibleWatersUnderneath);

            var left = -1;
            var LHit = false;
            var right = -1;
            var RHit = false;

            for (var newX = x - 1; ; newX--)
            {
                if (possibleWallsHorizontal.Any(w => w.IsPointOnLine(newX, y)))
                {
                    LHit = true;
                    left = newX;
                    break;
                }
                if (!possibleCollisionsUndernaeth.Any(w => w.IsPointOnLine(newX, y + 1)))
                {
                    LHit = false;
                    left = newX;
                    break;
                }
            }

            for (var newX = x + 1; ; newX++)
            {
                if (possibleWallsHorizontal.Any(w => w.IsPointOnLine(newX, y)))
                {
                    RHit = true;
                    right = newX;
                    break;
                }
                if (!possibleCollisionsUndernaeth.Any(w => w.IsPointOnLine(newX, y + 1)))
                {
                    RHit = false;
                    right = newX;
                    break;
                }
            }

            return ((left, LHit), (right, RHit));
        }

        private static void ParseInfo(string[] coordInfo, ref int x1, ref int x2, ref int y1, ref int y2)
        {
            var (start, end) = ParseCoords(coordInfo[1]);
            if (coordInfo[0] == "x")
            {
                x1 = start;
                x2 = end;
            }
            else
            {
                y1 = start;
                y2 = end;
            }
        }

        private static (int start, int end) ParseCoords(string coords)
        {
            if (coords.Contains(".."))
            {
                var coordsSplit = coords.Split("..");
                var c1 = int.Parse(coordsSplit[0]);
                var c2 = int.Parse(coordsSplit[1]);
                if (c1 < c2) return (c1, c2);
                else return (c2, c1);
            }
            var c = int.Parse(coords);
            return (c, c);
        }
    }
}
