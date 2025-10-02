using advent_of_code.Helpers;

namespace advent_of_code.Year2019.Day10
{
    internal static class Vector2lExtension
    {
        public static double GetAngleCustom(this Vector2l vector)
        {
            var angle = Math.Atan2(vector.X, -vector.Y);

            if (angle < 0)
                angle += 2 * Math.PI;

            return angle;
        }

        public static void NormalizeGCD(this Vector2l vector)
        {
            var gcd = MathHelpers.GCD(Math.Abs(vector.X), Math.Abs(vector.Y));
            if (gcd != 0)
            {
                vector.X /= gcd;
                vector.Y /= gcd;
            }
        }
    }
}
