using advent_of_code.Helpers;

namespace advent_of_code.Year2024.Day07
{
    public static class Challange2
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");

            //Same as part 1
            var jobs = input.Select(line =>
                new SingleJob<long>(new Func<long>(() =>
                {
                    var equation = new Equation(line);
                    var result = Common.IsOkay(equation, GetNext);
                    return result ? equation.ExpectedResult : 0;
                }))).ToList();

            //But batch size is not 5 to accomodate higher complexity
            var batches = SingleJob<long>.RunParallelized<long>(jobs, 5, (results) => results.Sum());

            return batches.Sum(batch => batch.Results);
        }

        public static IEnumerable<(Equation, int, long)> GetNext((Equation equation, int index, long intermediateResult) state)
        {
            if (state.index < state.equation.Numbers.Length) //If there is next number
            {
                //We get next number and get two results - one for addition and one for multiplication
                var nextNumber = state.equation.Numbers[state.index];
                var i1 = state.intermediateResult * nextNumber;
                var i2 = state.intermediateResult + nextNumber;
                
                if (i1 <= state.equation.ExpectedResult) //If we did not exceeded expected result by addition
                {
                    yield return (state.equation, state.index + 1, i1);
                }
                if (i2 <= state.equation.ExpectedResult) //If we did not exceeded expected result by multiplication
                {
                    yield return (state.equation, state.index + 1, i2);
                }
                if (long.TryParse(state.intermediateResult.ToString() + nextNumber, out var i3) && i3 <= state.equation.ExpectedResult) //If we did not exceeded expected result by concatenation
                {
                    yield return (state.equation, state.index + 1, i3);
                }
            }
        }
    }
}