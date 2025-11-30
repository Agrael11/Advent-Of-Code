using IntMachine;

namespace Cryostasis_Runner
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var inputData = "input.txt";
            if (args.Length > 0)
            {
                inputData = string.Join("", args);
            }
            if (!File.Exists(inputData))
            {
                Console.WriteLine($"Input file {inputData} not found.");
                return;
            }
            inputData = File.ReadAllText(inputData);

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
            var text = (Console.ReadLine() ?? "") + "\n";
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
            Console.Write(text);
        }
    }
}