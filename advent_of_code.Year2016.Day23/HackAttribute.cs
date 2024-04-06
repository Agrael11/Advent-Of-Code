namespace advent_of_code.Year2016.Day23
{
    [AttributeUsage(AttributeTargets.All)]
    public sealed class HackAttribute(string message) : Attribute
    {
        public string Message { get; } = message;
    }
}
