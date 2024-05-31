namespace advent_of_code.Year2017.Day24
{
    public static class Challange1
    {
        private static readonly List<Domino> dominoes = new List<Domino>(); 

        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            dominoes.Clear();

            foreach (var line in input)
            {
                dominoes.Add(new Domino(line));
            }

            return Search(new HashSet<Domino>(), 0);
        }

        public static int Search(HashSet<Domino> used, int currentEnd)
        {
            var strongest = 0;
            foreach (var domino in GetNext(currentEnd, used))
            {
                used.Add(domino);
                var stregth = domino.Strength + Search(used, domino.OtherEnd(currentEnd));
                if (stregth > strongest) strongest = stregth;
                used.Remove(domino);
            }
            return strongest;
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