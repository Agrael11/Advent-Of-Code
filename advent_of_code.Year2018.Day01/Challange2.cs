namespace advent_of_code.Year2018.Day01
{
    public static class Challange2
    {
        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            var frequency = 0;
            var frequencies = new HashSet<int>();
            var instructions = new List<int>();
            frequencies.Add(0);


            foreach (var line in input)
            {
                var instruction = int.Parse(line);
                frequency += instruction;
                if (frequencies.Contains(frequency))
                {
                    return frequency;
                }
                instructions.Add(instruction);
                frequencies.Add(frequency);
            }

            for (; ;)
            {
                foreach (var instruction in instructions)
                {
                    frequency += instruction;
                    if (frequencies.Contains(frequency))
                    {
                        return frequency;
                    }
                    frequencies.Add(frequency);
                }
            }


            return -1;
        }
    }
}