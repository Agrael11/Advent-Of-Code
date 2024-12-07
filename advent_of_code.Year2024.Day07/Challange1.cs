using advent_of_code.Helpers;
using System.Xml;

namespace advent_of_code.Year2024.Day07
{
    public static class Challange1
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");

            var total = 0L;

            foreach (var equation in input.Select(line => new Equation(line)))
            {
                total += Common.IsOkay(equation, GetNext);
            }

            return total;
        }

        public static IEnumerable<(Equation, int, long)> GetNext((Equation equation, int index, long intermediateResult) state)
        {
            if (state.index >= 0) //If we have next number
            {
                //We get next number and get result of subtraction
                var nextNumber = state.equation.Numbers[state.index];

                var resultSubtraction = state.intermediateResult - nextNumber;
                if (resultSubtraction >= 0) //If result is not negative, we return at possible subtraction
                {
                    yield return (state.equation, state.index - 1, resultSubtraction);
                }

                //We also get result of division, and it's remainder
                var resultDiv = state.intermediateResult / nextNumber;
                var remainderDiv = state.intermediateResult % nextNumber;

                if (remainderDiv == 0 && resultDiv >= 0) //If remainder is 0 and result is not negative we return as possible division
                {
                    yield return (state.equation, state.index - 1, resultDiv);
                }
            }
        }
    }
}