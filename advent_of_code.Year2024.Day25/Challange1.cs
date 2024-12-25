namespace advent_of_code.Year2024.Day25
{
    public static class Challange1
    {
        private static readonly List<int[]> keys = new List<int[]>();
        private static readonly List<int[]> locks = new List<int[]>();
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");
            keys.Clear();
            locks.Clear();

            for (var i = 0; i < input.Length; i+=8)
            {
                if (input[i][0] == '.') keys.Add(Parse(input[(i + 1)..(i + 6)], true));
                else locks.Add(Parse(input[(i+1)..(i+6)], false));
            }

            var fits = 0;

            //Tests every key with every lock - counts up if it fits.
            foreach (var _lock in locks)
            {
                foreach (var _key in keys)
                {
                    fits += KeyFitsLock(_lock, _key) ? 1 : 0;
                }
            }

            return fits;
        }

        //Checks if key fits lock - if pin + tooth of key are larger than keyhole (5) it doesn't fit
        public static bool KeyFitsLock(int[] _lock, int[] key)
        {
            for (var pin = 0; pin < 5; pin++)
            {
                if (_lock[pin] + key[pin] > 5) return false;
            }
            return true;
        }

        //Just parses the input of single key or lock
        public static int[] Parse(string[] input, bool key)
        {
            int[] pins = [0, 0, 0, 0, 0];
            for (var pin = 0; pin < 5; pin++)
            {
                for (var height = 4; height >= 0; height--)
                {
                    var heightIndex = key ? (4 - height) : height; //If it's key the input is upside down
                    if (input[heightIndex][pin] == '#')
                    {
                        pins[pin] = height + 1;
                        break;
                    }
                }
            }

            return pins;
        }
    }
}