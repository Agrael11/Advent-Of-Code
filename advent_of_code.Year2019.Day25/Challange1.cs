using IntMachine;
using Visualizers;

namespace advent_of_code.Year2019.Day25
{
    public static class Challange1
    {
        public static string DoChallange(string inputData)
        {
            if (AOConsole.Enabled)
            {
                Run(inputData);
                return "Answer is in Console";
            }
            return "Use Cryostasis_Runner.exe";
        }

        private static void Run(string inputData)
        {
            var input = inputData.Replace("\r", "").Replace("\n", "").
               Split(',').Select(long.Parse).ToArray();

            var machine = new Machine([1, 2, 3, 4, 5, 6, 7, 8, 9, 99])
            {
                RAM = (Memory)input
            };
            while (machine.Run() != Machine.RunResult.Exit)
            {
                ReadAsciiOutput(machine);
                SendAsciiInput(machine);
            }
            ReadAsciiOutput(machine);
        }

        private static void SendAsciiInput(Machine machine)
        {
            var text = ReadLine() + "\n";
            foreach (var character in text)
            {
                machine.PushInput((long)character);
            }
        }

        private static void ReadAsciiOutput(Machine machine)
        {
            var text = "";
            while (machine.TryPopOutput(out var data) && data is not null)
            {
                text += (char)data;
            }
            AOConsole.Write(text);
        }

        private static string ReadLine()
        {
            string? read = null;
            while (read is null)
            {
                read = AOConsole.ReadLine();
            }
            AOConsole.WriteLine(read);
            return read.ToString();
        }
    }
}