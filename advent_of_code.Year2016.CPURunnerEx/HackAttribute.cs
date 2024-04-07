namespace advent_of_code.Year2016.CPURunnerEx
{
    [AttributeUsage(AttributeTargets.All)]
    public sealed class HackAttribute(string message) : Attribute
    {
        public string Message { get; } = message;
    }
}
