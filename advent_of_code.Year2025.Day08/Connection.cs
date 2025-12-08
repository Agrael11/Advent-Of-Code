namespace advent_of_code.Year2025.Day08
{

    /// <summary>
    /// Contains two boxes and a distance between them.
    /// Just so i don't have to write tuples everywhere
    /// </summary>
    /// <param name="firstBox">First Junction Box of Connection</param>
    /// <param name="secondBox">Second Junction Box of Connection</param>
    internal class Connection(JunctionBox firstBox, JunctionBox secondBox)
    {
        public JunctionBox FirstBox { get; } = firstBox;
        public JunctionBox SecondBox { get; } = secondBox;
        public double Distance { get; } = firstBox.GetDistanceTo(secondBox);

        //Following is actually never used

        public override int GetHashCode()
        {
            return HashCode.Combine(FirstBox, SecondBox, Distance);
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Connection other) return false;
            return (FirstBox == other.FirstBox && SecondBox == other.SecondBox && Distance == other.Distance);
        }

        public override string ToString()
        {
            return $"{FirstBox} <---> {SecondBox} = {Distance}";
        }

        public static bool operator ==(Connection left, Connection right)
        {
            return left.Equals(right);
        }

        public static bool operator!=(Connection left, Connection right)
        {
            return !left.Equals(right);
        }
    }
}
