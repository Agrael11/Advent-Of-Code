﻿namespace advent_of_code.Year2024.Day07
{
    internal static class Common
    {
        public static bool IsOkay(Equation equation, Func<(Equation, int, long), IEnumerable<(Equation, int, long)>> getNext)
        {
            //Simple DFS
            var stack = new Stack<(Equation, int, long)>();
            stack.Push((equation, 1, equation.Numbers[0]));
            while (stack.Count > 0)
            {
                var current = stack.Pop();
                if (IsEnd(current)) return true;
                foreach (var next in getNext(current))
                {
                    stack.Push(next);
                }
            }
            return false;
        }

        public static bool IsEnd((Equation equation, int index, long intermidateResult) state)
        {
            //If we used all numbers and are equal to result we are at correct end
            return (state.index == state.equation.Numbers.Length && state.intermidateResult == state.equation.ExpectedResult);
        }
    }
}
