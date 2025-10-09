namespace advent_of_code.Year2019.Day16
{
    public static class Challange1
    {
        private static readonly int[] BasePattern = [0, 1, 0, -1];

        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").Replace("\n","").Select(t=>int.Parse(t.ToString())).ToArray();

            return long.Parse(string.Join("",DoPhases(input, 100).Take(8)));
        }

        public static int[] DoPhases(int[] input, int count)
        {
            for (var i = 0; i < count; i++)
            {
                input = DoThePhase(input);
            }

            return input;
        }

        private static int[] DoThePhase(int[] inputSignal)
        {
            var length = inputSignal.Length;
            var result = new int[length];
            for (var targetPosition = 0; targetPosition < length; targetPosition++)
            {
                var intResult = 0;
                for (var inputPosition = 0; inputPosition < length; inputPosition++)
                {
                    var patternPoint = GetItemInPattern(inputPosition, targetPosition);
                    if (patternPoint == 0) continue;
                    var item = inputSignal[inputPosition];
                    if (item == 0) continue;
                    intResult += item * patternPoint;
                }
                if (intResult < 0)
                {
                    result[targetPosition] = Math.Abs(intResult) % 10;
                }
                else
                {
                    result[targetPosition] = intResult % 10;
                }
            }
            return result;
        }

        private static int GetItemInPattern(int inputPosition, int outputPosition)
        {
            var realPos = (inputPosition + 1) / (outputPosition + 1);
            return BasePattern[realPos % BasePattern.Length];
        }
    }
}