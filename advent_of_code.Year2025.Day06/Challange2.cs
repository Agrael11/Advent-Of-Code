namespace advent_of_code.Year2025.Day06
{
    public static class Challange2
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");

            //We split the inputs more carefully in this one (keeping trailing and ending spaces)
            var splitInput = SplitInputs(input);

            //And create list of math problems by ROTATING the input numbers
            var rotatedMaths = GetRotatedProblems(splitInput);

            return rotatedMaths.Sum(t=>t.Result);
        }

        /// <summary>
        /// We get list of math problems, but rotating the array of numbers
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        private static List<CaphalopodMath> GetRotatedProblems(List<List<string>> inputs)
        {
            var maths = new List<CaphalopodMath>();

            foreach (var currentProblem  in inputs)
            {
                //Here we prepere our new Math Problem object - also setting it's operand
                var math = new CaphalopodMath();
                maths.Add(math);
                math.SetOperand(currentProblem[^1][0]);

                var length = currentProblem[0].Length;

                //And this is just simple 2D array rotation, using array of same-sized strings.
                for (var j = length - 1; j >= 0; j--)
                {
                    var str = "";
                    for (var k = 0; k < currentProblem.Count-1; k++)
                    {
                        str += currentProblem[k][j];
                    }
                    math.AddNumber(long.Parse(str));
                }
            }

            return maths;
        }


        /// <summary>
        /// Splits the inputs by space, while keeping spaces adjusted for length of max element of split.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static List<List<string>> SplitInputs(string[] input)
        {
            var results = new List<List<string>>();

            while (input.Min(t => t.Length) > 0)
            {
                var current = new List<string>();

                //Finds appropriate end of column - making sure that it will work for ends of line
                var minIndex = input.Min(x => x.IndexOf(' '));
                var maxIndex = input.Max(x => x.IndexOf(' '));
                var index = (minIndex >= 0) ? maxIndex : input.Max(x => x.Length);

                for (var i = 0; i < input.Length; i++)
                {
                    var str = input[i][..index];
                    
                    //If it's last element - we trim it. It's operand - no need to keep whitespace.
                    if (i == input.Length - 1) str = str.Trim();
                    
                    current.Add(str);

                    //We also remove first element - if it was not last one, we also remove whitespace separating columns
                    if (input[i].Length > index)
                    {
                        input[i] = input[i][(index + 1)..];
                    }
                    else
                    {
                        input[i] = input[i][index..];
                    }
                }

                results.Add(current);
            }

            return results;
        }
    }
}