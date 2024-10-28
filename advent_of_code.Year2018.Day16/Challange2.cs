namespace advent_of_code.Year2018.Day16
{
    public static class Challange2
    {
        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            var cpu = new CPU(0, 0, 0, 0);

            while (!cpu.AllAssigned())
            {
                for (var i = 0; i < input.Length; i+=4)
                {
                    if (cpu.AllAssigned() || !input[i].StartsWith("Before"))
                    {
                        break;
                    }

                    var registersStartText = input[i].Replace(" ", "").Split(':')[1].TrimStart('[').TrimEnd(']').Split(',');
                    int[] registersStart = [int.Parse(registersStartText[0]), int.Parse(registersStartText[1]), int.Parse(registersStartText[2]),
                    int.Parse(registersStartText[3])];
                    var registersEndText = input[i + 2].Replace(" ", "").Split(':')[1].TrimStart('[').TrimEnd(']').Split(',');
                    int[] registersEnd = [int.Parse(registersEndText[0]), int.Parse(registersEndText[1]), int.Parse(registersEndText[2]),
                    int.Parse(registersEndText[3])];
                    var opcodeInfoText = input[i + 1].Split(' ');
                    int[] opcodeInfo = [int.Parse(opcodeInfoText[0]), int.Parse(opcodeInfoText[1]), int.Parse(opcodeInfoText[2]), int.Parse(opcodeInfoText[3])];
                    cpu.LimitOpcode(opcodeInfo[0], opcodeInfo[1], opcodeInfo[2], opcodeInfo[3],
                        (registersStart[0], registersEnd[0]), (registersStart[1], registersEnd[1]), (registersStart[2], registersEnd[2]),
                        (registersStart[3], registersEnd[3]));
                    while (cpu.CheckSingles()) ;
                }
            }

            cpu.SetRegisters(0, 0, 0, 0);

            var lastAfter = Array.FindLastIndex(input, line => line.StartsWith("After"));

            for (var i = lastAfter + 1; i < input.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(input[i]))
                {
                    continue;
                }
                var opcodeInfoText = input[i].Split(' ');
                int[] opcodeInfo = [int.Parse(opcodeInfoText[0]), int.Parse(opcodeInfoText[1]), int.Parse(opcodeInfoText[2]),
                    int.Parse(opcodeInfoText[3])];
                cpu.ExecuteOpcode(opcodeInfo[0], opcodeInfo[1], opcodeInfo[2], opcodeInfo[3]);
            }

            return cpu.GetRegister(0);
        }
    }
}