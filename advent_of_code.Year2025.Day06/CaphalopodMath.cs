namespace advent_of_code.Year2025.Day06
{
    internal class CaphalopodMath
    {
        private List<long> Numbers = new List<long>();
        private char? Operand;
        public long Result => GetResult();

        /// <summary>
        /// Adds number into list of numbers of math problme
        /// </summary>
        /// <param name="number"></param>
        public void AddNumber(long number)
        {
            Numbers.Add(number);
        }

        /// <summary>
        /// Sets operand of math problem
        /// </summary>
        /// <param name="operand"></param>
        public void SetOperand(char operand)
        {
            Operand = operand;
        }

        /// <summary>
        /// Calculates the result using operand
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public long GetResult()
        {
            return Operand switch
            {
                '+' => Numbers.Sum(),
                '*' => Numbers.Multiply(),
                _ => throw new ArgumentException($"Unexpected operand: {Operand}"),
            };
        }
    }
}
