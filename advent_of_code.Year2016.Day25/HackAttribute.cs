namespace advent_of_code.Year2016.Day25
{
    [AttributeUsage(AttributeTargets.All)]
    public sealed class HackAttribute(string message) : Attribute
    {
        public string Message { get; } = message;
    }
}
