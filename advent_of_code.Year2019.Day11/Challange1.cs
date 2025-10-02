using IntMachine;

namespace advent_of_code.Year2019.Day11
{
    public static class Challange1
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").Replace("\n","").Split(",").Select(long.Parse).ToArray();
            
            var robot = Common.RunNewRobot(input);

            return robot.GetVisitedPanelCount();
        }
    }
}