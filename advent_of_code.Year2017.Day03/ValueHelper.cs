namespace advent_of_code.Year2017.Day03
{
    internal static class ValueHelper
    {
        internal static (int X, int Y) GetXY(int target)
        {
            var swaps = (int)Math.Sqrt(target);
            var x = GetX(target, swaps);
            var y = GetY(target, swaps);
            return (x, y);
        }
        internal static (int X, int Y) GetAbsXY(int target)
        {
            var swaps = (int)Math.Sqrt(target);
            var x = GetX(target, swaps);
            var y = GetY(target, swaps);
            return (Math.Abs(x), Math.Abs(y));
        }

        private static int GetX(int target, int swaps)
        {
            var def = (swaps + 1) / 2;
            var dif = target - swaps * swaps;
            if (dif > (swaps + 1) * (swaps + 1) - target) return (def - dif + swaps) * ((swaps % 2 == 0) ? -1 : 1);
            return def * ((swaps % 2 == 0) ? -1 : 1);
        }

        private static int GetY(int target, int swaps)
        {
            var def = swaps / 2;
            var dif = target - swaps * swaps;
            dif = (swaps < dif) ? swaps : dif;
            return (def - dif) * ((swaps % 2 == 0) ? 1 : -1);
        }
    }
}
