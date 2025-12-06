namespace advent_of_code.Year2025.Day06
{
    public static class Challange1
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n")
                .Select(t => t.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)).ToList();

            //Parses numbers in inputs into list of math problems

            var mathProblems = new List<CaphalopodMath>();

            for (var problemIndex = 0; problemIndex < input[0].Length; problemIndex++)
            {
                var mathProblem = new CaphalopodMath();
                mathProblems.Add(mathProblem);

                //We parse every line as number, except last, where the operand is
                for (var line = 0; line < input.Count - 1; line++)
                {
                    mathProblem.AddNumber(long.Parse(input[line][problemIndex]));
                }

                mathProblem.SetOperand(input[^1][problemIndex][0]);
            }

            //And calculate sum of results
            return mathProblems.Sum(t => t.Result);
        }
    }
}