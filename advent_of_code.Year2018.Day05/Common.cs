namespace advent_of_code.Year2018.Day05
{
    public static class Common
    {
        public static int Compact(List<char> elements)
        {
            var stack = new Stack<char>();
            foreach (var element in elements)
            {
                if (stack.Count > 0 && (Math.Abs(element - stack.Peek()) == 32))
                {
                    stack.Pop();
                }
                else
                {
                    stack.Push(element);
                }
            }
            return stack.Count;
        }
    }
}