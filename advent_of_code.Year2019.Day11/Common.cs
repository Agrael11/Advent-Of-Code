using IntMachine;

namespace advent_of_code.Year2019.Day11
{
    internal class Common
    {
        public static Robot RunNewRobot(long[] program, Robot.Color startingColor)
        {
            var machine = new Machine()
            {
                RAM = (Memory)program
            };

            List<long> instSet = [1, 2, 3, 4, 5, 6, 7, 8, 9, 99];
            var robot = new Robot(startingColor);

            var result = machine.Run(instSet);

            while (result == Machine.RunResult.WaitingForInput)
            {
                machine.PushInput((long)robot.GetCurrentPanelColor());
                result = machine.Run(instSet);

                var (color, movement) = GetCommand(machine) ?? throw new Exception("Machine did not provide enough output");

                robot.Paint(color);
                robot.Turn(movement);
                robot.MoveForward();
            }

            if (result != Machine.RunResult.Exit)
            {
                throw new Exception($"Machine stopped unexpectedly: {Enum.GetName(result)}");
            }

            return robot;
        }

        private static (Robot.Color color, Robot.Movement movement)? GetCommand(Machine machine)
        {
            machine.TryPopOutput(out var output1);
            machine.TryPopOutput(out var output2);
            if (output1 is null || output2 is null)
            {
                return null;
            }
            return ((Robot.Color)output1, (Robot.Movement)output2);
        }
    }
}
