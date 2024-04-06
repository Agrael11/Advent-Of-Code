namespace advent_of_code.Year2016.Day25
{
    public static class Challange1
    {
        private static byte[] compiledProgram = Array.Empty<byte>();
        private static bool running = true;
        private static int timer = 0;
        private static int currentTest = 0;
        private static int correctInput = -1;
        private static readonly int SafetyThreshold = 10;

        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");

            var cpu = new CPU(0, 0, 0, 0, OutputInterrupt);
            cpu.Compile(input);
            compiledProgram = cpu.SaveBinary();

            currentTest = 0;
            while (correctInput == -1)
            {
                RunTest(currentTest);
                currentTest++;
            }

            return correctInput;
        }

        public static void RunTest(int number)
        {
            var cpu = new CPU(number, 0, 0, 0, OutputInterrupt);
            cpu.LoadBinary(compiledProgram);
            timer = 0;
            running = true;
            cpu.Run(ref running);
        }

        public static void OutputInterrupt(int value)
        {
            if (timer%2 != value)
            {
                running = false;
                return;
            }

            if (timer > SafetyThreshold)
            {
                correctInput = currentTest;
            }
            else
            {
                timer++;
            }
        }
    }
}