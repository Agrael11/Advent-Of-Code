namespace advent_of_code.Year2015.Day01
{
    public static class Challange1
    {
        public static int DoChallange(string inputData)
        {
            inputData = inputData.Replace("\r", "").TrimEnd('\n');
            return inputData.Count(s => s == '(') - (inputData.Count(s => s == ')'));
        }
    }
}
