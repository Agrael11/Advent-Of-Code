using IntMachine;
using System.Threading.Tasks.Sources;

namespace advent_of_code.Year2019.Day13
{
    public static class Challange2
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").Replace("\n", "").Split(",").Select(long.Parse).ToArray();

            var machine = new Machine()
            {
                RAM = (Memory)input,
            };

            machine.RAM.TryWrite(0, 2);

            var score = 0L;
            (long X, long Y) paddle = (0L, 0L);
            (long X, long Y) ball = (0L, 0L);
            var screen = new Dictionary<(long, long), long>();

            var oldResult = Machine.RunResult.WaitingForInput;
            var result = RunMachine(machine);

            while (oldResult == Machine.RunResult.WaitingForInput)
            {
                GetOutput(machine, ref screen, ref score, ref ball, ref paddle);

                var control = 0L;
                if (ball.X != paddle.X) control = ball.X < paddle.X ? -1 : 1;
                machine.PushInput(control);
                
                oldResult = result;
                result = RunMachine(machine);

                if (Visualizers.AOConsole.Enabled)
                {
                    Draw(screen);
                }
            }
            
            return score;
        }

        private static void GetOutput(Machine machine, ref Dictionary<(long, long), long> screen,
            ref long score, ref (long, long) ball, ref (long, long) paddle)
        {            
            while (machine.OutputAvailable())
            {
                var output = Common.ReadMachineOutputs(machine);
                if (output.IsScore())
                {
                    score = output.TileID;
                    continue;
                }
                if (output.TileID == 4) ball = (output.X, output.Y);
                if (output.TileID == 3) paddle = (output.X, output.Y);

                if (screen.ContainsKey((output.X, output.Y)))
                {
                    screen[(output.X, output.Y)] = output.TileID;
                }
                else
                {
                    screen.Add((output.X, output.Y), output.TileID);
                }
            }
        }

        private static Machine.RunResult RunMachine(Machine machine, long? input = null)
        {
            if (input is not null)
            {
                machine.PushInput(input.Value);
            }
            return machine.Run([1, 2, 3, 4, 5, 6, 7, 8, 9, 99]);
        }


        private static void Draw(Dictionary<(long x, long y), long> screen)
        {
            var builder = new System.Text.StringBuilder();

            var maxX = screen.Keys.Max(k => k.x) + 1;
            var maxY = screen.Keys.Max(k => k.y) + 1;

            for (var y = 0; y < maxY; y++)
            {
                for (var x = 0; x < maxX; x++)
                {
                    if (screen.TryGetValue((x, y), out var tileID))
                    {
                        switch (tileID)
                        {
                            case 0:
                                builder.Append(' ');
                                break;
                            case 1:
                                builder.Append('█');
                                break;
                            case 2:
                                builder.Append('□');
                                break;
                            case 3:
                                builder.Append('▬');
                                break;
                            case 4:
                                builder.Append('●');
                                break;
                        }
                    }
                    else
                    {
                        builder.Append(' ');
                    }
                }
                builder.AppendLine();
            }

            Visualizers.AOConsole.CursorLeft = 0;
            Visualizers.AOConsole.CursorTop = 0;
            Visualizers.AOConsole.Write(builder.ToString());
            Thread.Sleep(40);
        }
    }
}