namespace advent_of_code.Year2016.Day19
{
    public static class Challange1
    {
        public static int DoChallange(string inputData)
        {
            var input = int.Parse(inputData.Replace("\r", "").Replace("\n",""));

            var queue = new Queue<int>(Enumerable.Range(1, input));

            while (queue.Count > 1)
            {
                queue.Enqueue(queue.Dequeue());
                queue.Dequeue();
            }

            return queue.Dequeue();
        }

#pragma warning disable IDE0051 // Remove unused private members
        private static int JustANiceSolutionIDidNotComeUpWith(int number)
        {
            return 2 * (number - (int)Math.Pow(2, Math.Floor(Math.Log2(number)))) + 1;
        }
#pragma warning restore IDE0051 // Remove unused private members
    }
}