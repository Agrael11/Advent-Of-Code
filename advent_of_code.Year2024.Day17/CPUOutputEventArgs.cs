namespace advent_of_code.Year2024.Day17
{
    internal class CPUOutputEventArgs(int output) : EventArgs
    {
        public int Output { get; set; } = output;
        public bool Cancel { get; set; } = false;
    }
}
