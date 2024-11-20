namespace advent_of_code.Year2018.Day24
{
    public static class Challange1
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");

            (var immuneGroups, var infectionGroups) = Common.ParseInput(input);

            return Common.Simulate(immuneGroups, infectionGroups).ResultForce;
        }
    }
}