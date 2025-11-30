using IntMachine;

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
                var sending = false;
                foreach (var machine in machines)
                {
                    while (Common.TryGetMachineOutputs(machine, out var target, out var x, out var y))
                    {
                        sending = true;
                        if (target == 255)
                        {
                            (NAT.X, NAT.Y) = (x,y);
                        }
                        else
                        {
                            machines[target].PushInput(x);
                            machines[target].PushInput(y);
                        }
                    }
                }
                var empty = !machines.Any(t=>t.InputAvailable());
                if (!sending && empty)
                {
                    NAT.Retry();
                    if (NAT.IsRetryLimit())
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
                }
                else
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
    }
}