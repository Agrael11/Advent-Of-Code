using advent_of_code.Helpers;

namespace advent_of_code.Year2024.Day07
{
    public static class Challange2
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");

            //Same as part 1, but we instead use Parallelized job system. Batch size of 50 worked well :)
            var jobs = input.Select(line =>
            new SingleJob<long>(new Func<long>(() => Common.IsOkay(new Equation(line), GetNext)))).ToList();

            var batches = SingleJob<long>.RunParallelized<long>(jobs, 50, (results) => results.Sum());

            return batches.Sum(batch => batch.Results);
        }

        public static IEnumerable<(Equation, int, long)> GetNext((Equation equation, int index, long intermediateResult) state)
        {
            if (state.index >= 0) //If there is next number
            {
                //We get next number and get two results - one for addition and one for multiplication
                var nextNumber = state.equation.Numbers[state.index];

                //This is same as in part 1
                var resultSub = state.intermediateResult - nextNumber;
                if (resultSub >= 0)
                {
                    yield return (state.equation, state.index - 1, resultSub);
                }

                var resultDiv = state.intermediateResult / nextNumber;
                var remainderDiv = state.intermediateResult % nextNumber;

                if (remainderDiv == 0 && resultDiv >= 0)
                {
                    yield return (state.equation, state.index - 1, resultDiv);
                }

                //We get temporary string version for later reuse
                var intermidiateString = state.intermediateResult.ToString();
                var nextString = nextNumber.ToString();

                //If current result ends with the next number
                if (intermidiateString.ToString().EndsWith(nextString))
                {
                    //We remove this number from it and parse result as long. We add 0 to start so it is able to parse it. Dirty but simple solution
                    var result = long.Parse("0" + intermidiateString[..^nextString.Length]);
                    yield return (state.equation, state.index - 1, result);
                }
            }
        }
    }
}