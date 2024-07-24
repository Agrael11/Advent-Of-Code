using System.Text;

namespace advent_of_code.Year2018.Day12
{
    internal class Common
    {
        private static string Plants = "";
        private static readonly Dictionary<int, bool> Rules = new Dictionary<int, bool>();
        private static readonly int[] PowerMap = new int[5];
        private static long Offset = 0;

        internal static void Initialize(string[] definition)
        {
            Rules.Clear();

            Offset = 0;

            for (var exponent = 0; exponent < 5; exponent++)
            {
                PowerMap[exponent] = (int)Math.Pow(2, 4 - exponent);
            }

            Plants = definition[0][(definition[0].IndexOf(':') + 2)..];

            for (var lineIndex = 2; lineIndex < definition.Length; lineIndex++)
            {
                var splitRule = definition[lineIndex].Split(" => ");
                var rule = splitRule[0];
                var result = splitRule[1] == "#";
                var ruleCode = 0;
                for (var j = 0; j < 5; j++)
                {
                    ruleCode += (rule[j] == '#') ? PowerMap[j] : 0;
                }
                Rules[ruleCode] = result;
            }
        }

        internal static long CountPlants()
        {
            return Plants.Select((s,i) => new { i, s }).
                Sum(t => (t.i + Offset)*((t.s == '#')?1:0));
        }

        internal static void DoForNumberOfGens(long numberOfGens)
        {
            var loopData = new Dictionary<string, (int generation, long offset)>();
            var lastGen = -1L;
            for (var i = 0L; i < numberOfGens; i++)
            {
                NextGen();
                if (loopData.ContainsKey(Plants))
                {
                    lastGen = i;
                    break;
                }
                loopData.Add(Plants, ((int)i, Offset));
            }
            if (lastGen == -1) return;

            var (loopPoint, loopStartOffset) = loopData[Plants];

            var remainingGens = numberOfGens - lastGen - 1;
            var loopLength = lastGen - loopPoint;
            
            var loops = remainingGens / loopLength;
            var loopPart = (int)(remainingGens % loopLength);

            var midpointKey = loopData.Keys.ElementAt(loopPoint + loopPart);
            var (_, loopMidpointOffset) = loopData[midpointKey];

            var offsetChange = Offset - loopStartOffset;
            var inLoopOffsetChange = loopMidpointOffset - loopStartOffset;

            Offset += (offsetChange * loops) + (inLoopOffsetChange * loopPart);
            Plants = midpointKey;
        }

        internal static void NextGen()
        {
            var newPlantsBuilder = new StringBuilder();
            for (var i = -2; i < Plants.Length + 2; i++)
            {
                newPlantsBuilder.Append(GetResult(i)?'#':'.');
            }

            var newPlants = newPlantsBuilder.ToString();
            var realStart = newPlants.IndexOf('#');
            var realEnd = (newPlants.LastIndexOf('#') + 1);
            Offset += realStart - 2;
            Plants = newPlants[realStart..realEnd];
        }

        internal static bool GetResult(int position)
        {
            var rule = 0;
            for (var i = 0; i < 5; i++)
            {
                var pos = position + i - 2;
                if (pos >= 0 && pos < Plants.Length)
                {
                    rule += (Plants[pos] == '#') ? PowerMap[i] : 0;
                }
            }
            if (Rules.TryGetValue(rule, out var value)) return value;
            return false;
        }
    }
}
