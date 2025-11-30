using IntMachine;
using System.Reflection.Metadata.Ecma335;

namespace advent_of_code.Year2019.Day23
{
    public static partial class Challange2
    {

        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").Replace("\n", "").
               Split(',').Select(long.Parse).ToArray();

            var machines = new List<Machine>();
            
            for (var i = 0; i < 50; i++)
            {
                var machine = new Machine([1, 2, 3, 4, 5, 6, 7, 8, 9, 99]);
                machine.RAM = (Memory)input;
                machine.PushInput(i);
                machines.Add(machine);
            }

            while (true)
            {
                ReadOutputs(machines);

                var inputEmpty = !machines.Any(t => t.InputAvailable());

                if (inputEmpty && NAT.Retry())
                {
                    if (NAT.Y == NAT.LastSent)
                    {
                        return NAT.Y;
                    }
                    machines[0].PushInput(NAT.X);
                    machines[0].PushInput(NAT.Y);
                    NAT.LastSent = NAT.Y;
                    NAT.ResetRetries();
                }

                if (!inputEmpty)
                {
                    NAT.ResetRetries();
                }

                foreach (var machine in machines)
                {
                    if (!machine.InputAvailable()) machine.PushInput(-1);
                    machine.Run();
                }
            }
        }

        private static void ReadOutputs(List<Machine> machines)
        {
            foreach (var machine in machines)
            {
                while (Common.TryGetMachineOutputs(machine, out var target, out var x, out var y))
                {
                    if (target == 255)
                    {
                        (NAT.X, NAT.Y) = (x, y);
                    }
                    else
                    {
                        machines[target].PushInput(x);
                        machines[target].PushInput(y);
                    }
                }
            }
        }
    }
}