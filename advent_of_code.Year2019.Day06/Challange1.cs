namespace advent_of_code.Year2019.Day06
{
    public static class Challange1
    {
        public static long DoChallange(string inputData)
        {
            //inputData = "COM)B\r\nB)C\r\nC)D\r\nD)E\r\nE)F\r\nB)G\r\nG)H\r\nD)I\r\nE)J\r\nJ)K\r\nK)L";
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n").Select(t=>t.Split(')'));

            var nodes = new Dictionary<string, Node>();
            foreach (var connection in input)
            {
                if (!nodes.TryGetValue(connection[0], out var source))
                {
                    source = new Node(connection[0]);
                    nodes.Add(connection[0], source);
                }
                if (!nodes.TryGetValue(connection[1], out var target))
                {
                    target = new Node(connection[1]);
                    nodes.Add(connection[1], target);
                }
                source.AddChild(target);
            }

            return nodes.Sum(t=>t.Value.Distance);
        }
    }
}