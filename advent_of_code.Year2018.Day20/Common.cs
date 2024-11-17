using System.Text.RegularExpressions;

namespace advent_of_code.Year2018.Day20
{
    internal static partial class Common
    {
        [GeneratedRegex(@"([A-Z|]+)|(\((?>\((?<c>)|[^()]+|\)(?<-c>))*(?(c)(?!))\))")]
        private static partial Regex MatchRegex();
        private static readonly Dictionary<char, (int X, int Y)> DirectionsToOffsetMap = new Dictionary<char, (int X, int Y)>()
        { { 'N', (0,-1)}, { 'S', (0, +1) }, { 'W', (-1, 0)},{ 'E', (+1, 0)} };


        public static Dictionary<(int X, int Y), Node> nodes = new Dictionary<(int X, int Y), Node>();

        public static void Init()
        {
            nodes.Clear();
        }

        public static List<(string text, bool inside)> GetGroups(string input)
        {
            var groups = new List<(string text, bool inside)>();

            if (MatchRegex().IsMatch(input))
            {
                var matches = MatchRegex().Matches(input);

                for (var matchIndex = 0; matchIndex < matches.Count; matchIndex++)
                {
                    var inside = false;
                    var match = matches[matchIndex];
                    if (match.Groups[2].Value == match.Value)
                    {
                        inside = true;
                    }
                    groups.Add((match.Value, inside));
                }
            }
            else
            {
                groups.Add(("", false));
            }

            return groups;
        }

        public static SequencePart BuildNavMap(string input)
        {
            var startInfo = new SequencePart(input, null);

            var toParse = new Queue<SequencePart>();
            toParse.Enqueue(startInfo);

            while (toParse.Count > 0)
            {
                var current = toParse.Dequeue();
                var groups = GetGroups(current.MainString);
                var split = false;
                var path = new Sequence(current);
                if (groups.Count == 1)
                {
                    if (groups[0].text.Split('|').Length == 1)
                    {
                        continue;
                    }
                }
                foreach (var (group, inside) in groups)
                {
                    if (inside)
                    {
                        var newInfo = new SequencePart(group[1..^1], path);
                        path.AddSequencePart(newInfo);
                        toParse.Enqueue(newInfo);
                    }
                    else
                    {
                        foreach (var pathPart in group.Split('|'))
                        {
                            if (split)
                            {
                                current.AddOption(path);
                                path = new Sequence(current);
                            }
                            split = true;
                            var newInfo = new SequencePart(pathPart, path);
                            path.AddSequencePart(newInfo);
                            toParse.Enqueue(newInfo);
                        }
                        if (!group.EndsWith('|')) split = false;
                    }
                }
                current.AddOption(path);
            }

            return startInfo;
        }

        public static void TraverseNavMap(Sequence startSequence, (int X, int Y) startPosition)
        {
            var stack = new Stack<(Sequence currentSequence, int partIndex, (int X, int Y) position)>();
            stack.Push((startSequence, 0, startPosition));

            while (stack.Count > 0)
            {
                var (currentSequence, partIndex, position) = stack.Pop();

                var currentPart = currentSequence.GetSequencePart(partIndex);

                if (TryCrawl(ref position, currentPart))
                {
                    if (partIndex + 1 < currentSequence.SequenceLength)
                    {
                        stack.Push((currentSequence, partIndex + 1, position));
                    }
                }
                else
                {
                    if (partIndex + 1 < currentSequence.SequenceLength)
                    {
                        stack.Push((currentSequence, partIndex + 1, position));
                    }

                    for (var i = currentPart.OptionsCount - 1; i >= 0; i--)
                    {
                        var optionSequence = currentPart.GetOption(i);
                        stack.Push((optionSequence, 0, position));
                    }
                }
            }
        }

        public static bool TryCrawl(ref (int X, int Y) position, SequencePart part)
        {
            if (part.OptionsCount > 0) return false;

            foreach (var (offsetX, offsetY) in part.MainString.Select(t => DirectionsToOffsetMap.TryGetValue(t, out var value) ? value : (0, 0)))
            {
                var newX = position.X + offsetX;
                var newY = position.Y + offsetY;
                TryAddNodeConnection(position.X, newX, position.Y, newY);
                position.X = newX;
                position.Y = newY;
            }

            return true;
        }

        public static void TryAddNodeConnection(int X1, int X2, int Y1, int Y2, bool first = true)
        {
            if (!nodes.TryGetValue((X1, Y1), out var node))
            {
                node = new Node(X1, Y1);
            }

            if (!node.ConnectedNodes.Contains((X2, Y2)))
            {
                node.ConnectedNodes.Add((X2, Y2));
            }

            nodes[(X1, Y1)] = node;

            if (first)
            {
                TryAddNodeConnection(X2, X1, Y2, Y1, false);
            }
        }
    }
}