namespace advent_of_code.Year2017.Day02
{
    public static class Challange2
    {
        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');
            var result = 0;

            foreach (var row in input)
            {
                result += FindResult(row);
            }

            return result;
        }

        private static int FindResult(string row)
        {
            var numbers = row.Split('\t');
            for (var NumIndex1 = 0; NumIndex1 < numbers.Length - 1; NumIndex1++)
            {
                var num1 = int.Parse(numbers[NumIndex1]);
                for (var NumIndex2 = NumIndex1 + 1; NumIndex2 < numbers.Length; NumIndex2++)
                {
                    var num2 = int.Parse(numbers[NumIndex2]);
                    if (num1 % num2 == 0)
                    {
                        return num1 / num2;
                    }
                    if (num2 % num1 == 0)
                    {
                        return num2 / num1;
                    }
                }
            }
            return -1;
        }
    }
}