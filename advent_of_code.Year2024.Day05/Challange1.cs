namespace advent_of_code.Year2024.Day05
{
    public static class Challange1
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");

            //Parse the input
            (var rules, var startOfData) = Common.Parse(input);

            var checkSum = 0;

            for (var i = startOfData; i < input.Length; i++)
            {
                var printJob = input[i].Split(',').Select(int.Parse).ToArray();

                //If PrintJob is okay, we add it's midpoint to total checksum;
                if (Common.PrintJobAdheresTorRules(rules, printJob))
                {
                    checkSum += printJob[printJob.Length / 2];
                }
            }

            return checkSum;
        }
    }
}