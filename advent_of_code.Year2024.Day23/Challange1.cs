namespace advent_of_code.Year2024.Day23
{
    public static class Challange1
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");
            Common.Parse(input);
            return CountGroupsWith('t');
        }

        //I think doing this manually for part 1 is absolutely okay, and if I knew what is coming
        //for part 2, I doubt I would choose any other way
        private static int CountGroupsWith(char startChar)
        {
            var set = new HashSet<string>();
            foreach (var node in Common.Nodes.Where(pair => pair.Key[0] == startChar).Select(pair => pair.Value))
            {
                for (var i = 0; i < node.ConnectedIDS.Count - 1; i++)
                {
                    var id1 = node.ConnectedIDS[i];
                    for (var j = i + 1; j < node.ConnectedIDS.Count; j++)
                    {
                        var id2 = node.ConnectedIDS[j];
                        if (Common.Nodes[id2].ConnectedIDS.Contains(id1))
                        {
                            set.Add(string.Join(',', new string[] { node.Name, id1, id2 }.Order()));
                        }
                    }
                }
            }

            return set.Count;
        }
    }
}