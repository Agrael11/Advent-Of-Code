namespace advent_of_code.Year2017.Day24
{
    public static class Challange2
    {
        private readonly static List<Domino> dominoes = new List<Domino>();

        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            dominoes.Clear();

            foreach (var line in input)
            {
                dominoes.Add(new Domino(line));
            }

            return Search(new HashSet<Domino>(), 0).strength;
        }

        public static (int length, int strength) Search(HashSet<Domino> used, int currentEnd)
        {
            var longest = 0;
            var strongest = 0;
            foreach (var domino in GetNext(currentEnd, used))
            {
                used.Add(domino);
                var result = Search(used, domino.OtherEnd(currentEnd));
                var strength = domino.Strength + result.strength;
                var length = result.length + 1;
                if (length > longest)
                {
                    longest = length;
                    strongest = strength;
                }
                else if (length == longest)
                {
                    if (strength > strongest)
                    {
                        strongest = strength;
                    }
                }
                used.Remove(domino);
            }
            return (longest, strongest);
        }

        public static IEnumerable<Domino> GetNext(int currentEnd, HashSet<Domino> used)
        {
            foreach (var domino in dominoes)
            {
                if (used.Contains(domino)) continue;
                if (domino.Connects(currentEnd)) yield return domino;
            }
        }
    }
}