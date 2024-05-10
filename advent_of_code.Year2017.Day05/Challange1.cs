namespace advent_of_code.Year2017.Day05
{
    public static class Challange1
    {
        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');
            var instructions = new List<int>();

            foreach (var line in input)
            {
                instructions.Add(int.Parse(line));
            }

            var PC = 0;
            var steps = 0;

            while (PC < instructions.Count)
            {
                instructions[PC]++;
                PC += instructions[PC] - 1;
                steps++;
            }

            return steps;
        }
    }
}