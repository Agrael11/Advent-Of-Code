using IntMachine;

namespace advent_of_code.Year2019.Day15
{
    internal class Common
    {

        public static (int x, int y) WalkCoordinate((int x, int y) coord, int direction)
        {
            var x = coord.x;
            var y = coord.y;
            switch (direction)
            {
                case 1:
                    y++;
                    break;
                case 2:
                    y--;
                    break;
                case 3:
                    x--;
                    break;
                case 4:
                    x++;
                    break;
            }
            return (x, y);
        }

        public static long Navigate(Machine machine, int direction)
        {
            machine.PushInput(direction);

            return GetLastOutput(machine);
        }

        private static long GetLastOutput(Machine machine)
        {
            var expectResult = 0L;
            machine.Run([1, 2, 3, 4, 5, 6, 7, 8, 9, 99]);
            while (machine.OutputAvailable())
            {
                machine.TryPopOutput(out var result);
                if (result is not null) expectResult = result.Value;
            }
            return expectResult;
        }
    }
}
