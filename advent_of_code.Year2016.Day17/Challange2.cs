using advent_of_code.Helpers;
using System.Security.Cryptography;
using System.Text;

namespace advent_of_code.Year2016.Day17
{
    public static class Challange2
    {
        private static string secret = "";

        public static int DoChallange(string inputData)
        {
            secret = inputData.Replace("\r", "").Replace("\n", "");

            var startPos = new Position(0, 0, "");

            return FindLongest(startPos);
        }

        public static int FindLongest(Position startPoint)
        {
            var ToExplore = new Stack<Position>();
            ToExplore.Push(startPoint);
            var longest = int.MinValue;
            while (ToExplore.Count > 0)
            {
                foreach (var position in GetNext(ToExplore.Pop()))
                {
                    if (IsEnd(position))
                    {
                        longest = int.Max(longest, position.Path.Length);
                    }
                    else
                    {
                        ToExplore.Push(position);
                    }
                }
            }

            return longest;
        }

        public static bool IsEnd(Position current)
        {
            return current.X == 3 && current.Y == 3;
        }

        public static IEnumerable<Position> GetNext(Position current)
        {
            var paths = GetDoors(secret + current.Path);
            if (current.Y > 0 && paths[0]) yield return new Position(current.X, current.Y - 1, current.Path + "U");
            if (current.Y < 3 && paths[1]) yield return new Position(current.X, current.Y + 1, current.Path + "D");
            if (current.X > 0 && paths[2]) yield return new Position(current.X - 1, current.Y, current.Path + "L");
            if (current.X < 3 && paths[3]) yield return new Position(current.X + 1, current.Y, current.Path + "R");
            yield break;
        }

        public static bool[] GetDoors(string key)
        {
            var data = MD5.HashData(Encoding.Default.GetBytes(key));
            var result = new bool[4];
            result[0] = (data[0] >> 4) > 10;
            result[1] = (data[0] & 0xf) > 10;
            result[2] = (data[1] >> 4) > 10;
            result[3] = (data[1] & 0xf) > 10;

            return result;
        }
    }
}