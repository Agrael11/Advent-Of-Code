using System.Security.Cryptography;

namespace advent_of_code.Year2016.Day14
{
    public static class Challange2
    {
        private static readonly Dictionary<string, byte[]> _memoryGenerateMD5_2016 = new Dictionary<string, byte[]>();
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
                foreach (var run in GetRunsOfFive(input + index))
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

                if (GetFirstRunOfThree(input + index, out var character))
                {
                    if (!_runsOf3.TryGetValue(character, out var threes)) threes = new List<int>();
                    threes.Add(index);
                    _runsOf3[character] = threes;
                }

                index++;
            }

            _runsOf3.Clear();
            _memoryGenerateMD5_2016.Clear();

            return indicies.Order().ToList()[63];
        }

        private static List<int> GetRunsOfFive(string key)
        {
            var data = GenerateMD5_2016(key);
            var runsOfFive = new List<int>();

            for (var i = 0; i < data.Length - 2; i++)
            {
                var b1 = (data[i] >> 4) & 0xF;
                var b2 = data[i] & 0xF;
                var b3 = (data[i + 1] >> 4) & 0xF;
                var b4 = data[i + 1] & 0xF;
                var b5 = (data[i + 2] >> 4) & 0xF;
                var b6 = data[i + 2] & 0xF;
                if (b2 == b3 && b3 == b4 && b4 == b5 && (b1 == b2 || b5 == b6))
                {
                    runsOfFive.Add(b2);
                }
            }
            return runsOfFive;
        }

        private static bool GetFirstRunOfThree(string key, out byte character)
        {
            var data = GenerateMD5_2016(key);

            for (var i = 0; i < data.Length - 1; i++)
            {
                var b1 = (data[i] >> 4) & 0xF;
                var b2 = data[i] & 0xF;
                var b3 = (data[i + 1] >> 4) & 0xF;
                var b4 = data[i + 1] & 0xF;
                if (b2 == b3 && (b1 == b2 || b3 == b4))
                {
                    character = (byte)b2;
                    return true;
                }
            }

            character = 0;
            return false;
        }

        private static byte[] GenerateMD5_2016(string key)
        {
            if (_memoryGenerateMD5_2016.TryGetValue(key, out var md5)) { return md5; }

            var resultHash = GenerateMD5(key);
            for (var i = 0; i < 2016; i++)
            {
                resultHash = GenerateMD5(Convert.ToHexString(resultHash).ToLower());
            }

            _memoryGenerateMD5_2016[key] = resultHash;
            return resultHash;
        }

        private static byte[] GenerateMD5(string key)
        {
            var input = System.Text.Encoding.ASCII.GetBytes(key);
            return MD5.HashData(input);
        }
    }
}