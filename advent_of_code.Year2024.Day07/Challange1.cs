using advent_of_code.Helpers;
using System.Xml;

namespace advent_of_code.Year2024.Day07
{
    public static class Challange1
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");

            //We create jobs - convert lines to equations and check them if they are okay. If they are - we take their result.
            var jobs = input.Select(line =>
                new SingleJob<long>(new Func<long>(() =>
                {
                    var equation = new Equation(line);
                    var result = Common.IsOkay(equation, GetNext);
                    return result ? equation.ExpectedResult : 0;
                }))).ToList();

            //We let jubs run, in batches of 100, which is arbitrary number that worked for me best. As result of them - we just sum them
            var batches = SingleJob<long>.RunParallelized<long>(jobs, 100, (results) => results.Sum());

            return batches.Sum(batch => batch.Results);
        }

        public static IEnumerable<(Equation, int, long)> GetNext((Equation equation, int index, long intermediateResult) state)
        {
            if (state.index < state.equation.Numbers.Length) //If we have next number
            {
                //We get next number and get two results - one for addition and one for multiplication
                var i1 = state.intermediateResult * state.equation.Numbers[state.index];
                var i2 = state.intermediateResult + state.equation.Numbers[state.index];
                if (i1 <= state.equation.ExpectedResult) //If we did not exceeded expected result by addition
                {
                    yield return (state.equation, state.index + 1, i1);
                }
                if (i2 <= state.equation.ExpectedResult) //If we did not exceeded expected result by multiplication
                {
                    yield return (state.equation, state.index + 1, i2);
                }
            }
        }
    }
}