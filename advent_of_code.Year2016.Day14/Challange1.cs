using System.Collections.Generic;
using System.Security.Cryptography;

namespace advent_of_code.Year2016.Day14
{
    public static class Challange1
    {
        private static readonly Dictionary<int, List<int>> _runsOf3 = new Dictionary<int, List<int>>();

        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n');

            var keys = 0;
            var indicies = new List<int>();
            var foundLast = 0;
            var index = 0;

            while (keys < 64 || index - foundLast < 1000)
            {
                var data = MD5.HashData(System.Text.Encoding.ASCII.GetBytes(input + index)); ;

                foreach (var run in GetRunsOfFive(data))
                {
                    if (_runsOf3.TryGetValue(run, out var value))
                    {
                        for (var i = value.Count - 1; i >= 0; i--)
                        {
                            if (index - value[i] < 1000)
                            {
                                keys++;
                                if (keys == 64) foundLast = index;
                                indicies.Add(value[i]);
                            }
                        }
                        _runsOf3[run].Clear();
                    }
                }

                if (GetFirstRunOfThree(data, out var character))
                {
                    if (!_runsOf3.TryGetValue(character, out var threes)) threes = new List<int>();
                    threes.Add(index);
                    _runsOf3[(int)character] = threes;
                }

                index++;
            }

            _runsOf3.Clear();

            return indicies.Order().ElementAt(63);
        }

        private static List<int> GetRunsOfFive(byte[] data)
        {
            var runsOfFive = new List<int>();

            for (var i = 0; i < data.Length - 2; i++)
            {
                var b1 = (data[i] >> 4) & 0xF;
                var b2 = data[i] & 0xF;
                var b3 = (data[i + 1] >> 4) & 0xF;
                var b4 = data[i + 1] & 0xF;
                var b5 = (data[i + 2] >> 4) & 0xF;
                var b6 = data[i + 2] & 0xF;
                if (b2 == b3 && b3 == b4 && b4 == b5 && (b1==b2 || b5==b6))
                {
                    runsOfFive.Add(b2);
                }
            }
            return runsOfFive;
        }

        private static bool GetFirstRunOfThree(byte[] data, out byte character)
        {
            for (var i = 0; i < data.Length - 1; i++)
            {
                var b1 = (data[i] >> 4) & 0xF;
                var b2 = data[i] & 0xF;
                var b3 = (data[i+1] >> 4 ) & 0xF;
                var b4 = data[i + 1] & 0xF;
                if (b2==b3 && (b1==b2 || b3==b4))
                {
                    character = (byte)b2;
                    return true;
                }
            }

            character = 0;
            return false;
        }
    }
}