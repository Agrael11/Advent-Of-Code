using IntMachine;

namespace advent_of_code.Year2019.Day13
{
    public static class Challange1
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").Replace("\n","").Split(",").Select(long.Parse).ToArray();

            var machine = new Machine()
            {
                RAM = (Memory)input,
            };

            machine.Run([1,2,3,4,5,6,7,8,9,99]);

            var blockPositions = new HashSet<(long x, long y)>();

            while (machine.OutputAvailable())
            {
                var result = Common.ReadMachineOutputs(machine);
                if (result.TileID == 2)
                {
                    blockPositions.Add((result.X, result.Y));
                }
            }

            return blockPositions.Count;
        }
    }
}