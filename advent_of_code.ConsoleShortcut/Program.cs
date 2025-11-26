namespace advent_of_code.ConsoleShortcut
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsoleOnly.AdventOfCode.Register();
            Visualizers.AOConsole.Clear();
            ConsoleOnly.AdventOfCode.Main();
        }
    }
}
