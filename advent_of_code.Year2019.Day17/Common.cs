using IntMachine;

namespace advent_of_code.Year2019.Day17
{
    internal class Common
    {
        /// <summary>
        /// Parses the data from existing machine
        /// </summary>
        /// <param name="machine">IntMachine - expect to already have output ready</param>
        /// <returns>HashSet of Scaffoldings and Robot</returns>
        /// <exception cref="Exception"></exception>
        public static (HashSet<(int X, int Y)> map, Robot robot) ParseData(Machine machine)
        {
            var map = new HashSet<(int x, int y)>();
            var robot = new Robot(0,0,Robot.Direction.Error);

            var x = 0;
            var y = 0;

            //Parses data frm machines output.
            //. - empty space
            //# - scaffolding
            //^ v < > - directions for bot
            //X bat position for bot
            //\n - new line :D
            //We use PeekOutput and only pop on valid output.
            //Invalid output is end of stream.
            while (machine.OutputAvailable())
            {
                var output = machine.PeekOutput() ?? throw new Exception("Unexpected EoO");

                switch ((char)(output))
                {
                    case '.':
                        x++;
                        break;
                    case '#':
                        map.Add((x, y));
                        x++;
                        break;
                    case '^':
                    case 'v':
                    case '<':
                    case '>':
                    case 'X':
                        robot = new Robot(x, y, Robot.DirectionFromCharacter((char)output));
                        map.Add((x, y));
                        x++;
                        break;
                    case '\n':
                        y++;
                        x = 0;
                        break;
                    default:
                        return (map, robot);
                }

                machine.TryPopOutput(out _);
            }

            return (map, robot);
        }

        /// <summary>
        /// Parses the map while using new machine
        /// </summary>
        /// <param name="input"></param>
        /// <returns>HashSet of Scaffoldings and Robot</returns>
        /// <exception cref="Exception"></exception>
        public static (HashSet<(int X, int Y)> map, Robot robot) ParseData(long[] input)
        {
            var machine = new Machine()
            {
                RAM = (Memory)input
            };

            var result = machine.Run([1, 2, 3, 4, 5, 6, 7, 8, 9, 99]);
            if (result != Machine.RunResult.Exit)
            {
                throw new Exception($"Unexpected achine return code - {Enum.GetName(result)}. Expected {Enum.GetName(Machine.RunResult.Exit)}");
            }

            return ParseData(machine);
        }
    }
}
