namespace advent_of_code.Year2025.Day05
{

    /// <summary>
    /// Standard Inclusive Range Class With Merging
    /// </summary>
    /// <param name="start">Start point of Class</param>
    /// <param name="end">End point of Class</param>
    internal class InclusiveRange(long start, long end)
    {
        public long Start { get; set; } = start;
        public long End { get; set; } = end;
        public long Length => End - Start + 1;

        /// <summary>
        /// Checks if Value is Included in Range
        /// </summary>
        /// <param name="value">Number to be checked</param>
        /// <returns></returns>
        public bool Includes(long value)
        {
            return Start <= value && End >= value;
        }

        /// <summary>
        /// Tries to merge range into this one.
        /// </summary>
        /// <param name="otherRange">Other Range to be merged into this one</param>
        /// <returns>Success result</returns>
        public bool TryMerge(InclusiveRange otherRange)
        {
            if (((otherRange.Start >= Start) && (otherRange.Start <= End)) || ((otherRange.Start < Start) && (otherRange.End >= Start)))
            {
                Start = long.Min(Start, otherRange.Start);
                End = long.Max(End, otherRange.End);
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return $"<{Start} - {End}>";
        }
    }
}
