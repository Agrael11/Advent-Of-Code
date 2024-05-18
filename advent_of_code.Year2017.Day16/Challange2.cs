namespace advent_of_code.Year2017.Day16
{
    public static class Challange2
    {
        private static readonly int Count = 1_000_000_000;

        public static string DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split(',');

            var state = "abcdefghijklmnop".ToCharArray();
            var states = new List<string>();


            for (var r = 0; r < Count; r++)
            {
                foreach (var line in input)
                {
                    var data = line[1..].Split('/');
                    switch (line[0])
                    {
                        case 's':
                            var len = int.Parse(data[0]);
                            Spin(ref state, len);
                            break;
                        case 'x':
                            var pos1 = int.Parse(data[0]);
                            var pos2 = int.Parse(data[1]);
                            (state[pos2], state[pos1]) = (state[pos1], state[pos2]);
                            break;
                        case 'p':
                            var found = false;
                            for (var i = 0; i < state.Length; i++)
                            {
                                if (state[i] == data[0][0])
                                {
                                    state[i] = data[1][0];
                                    if (found) break;
                                    found = true;
                                }
                                else if (state[i] == data[1][0])
                                {
                                    state[i] = data[0][0];
                                    if (found) break;
                                    found = true;
                                }
                            }
                            break;
                    }
                }
                var newState = new string(state);
                if (!states.Contains(newState))
                {
                    states.Add(newState);
                    continue;
                }

                var loopStart = states.IndexOf(newState);
                var loopLen = states.Count - loopStart;
                var remainder = (Count - loopStart) % (loopLen);
                var position = loopStart + remainder - 1;
                if (position < 0) position = states.Count - 1;
                return states[position];
            }

            return new string(state);
        }

        private static void Spin(ref char[] array, int length)
        {
            var n = array.Length;
            length %= n;
            Reverse(ref array, 0, n - 1);
            Reverse(ref array, 0, length - 1);
            Reverse(ref array, length, n - 1);
        }

        private static void Reverse(ref char[] array, int start, int end)
        {
            while (start < end)
            {
                (array[start], array[end]) = (array[end], array[start]);
                start++;
                end--;
            }
        }
    }
}