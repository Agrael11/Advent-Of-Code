using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code.Helpers
{
    public static class MathHelpers
    {
        #region public static double/float/long/int Sum(... start, ... end)
        public static double Sum(double start, double end)
        {
            if (end < start) return 0;
            return ((start + end) / 2.0) * (end - start + 1);
        }
        public static float Sum(float start, float end)
        {
            if (end < start) return 0;
            return ((start + end) / 2.0f) * (end - start + 1);
        }
        public static long Sum(long start, long end)
        {
            if (end < start) return 0;
            return (long)(((start + end) / 2.0f) * (end - start + 1));
        }
        public static int Sum(int start, int end)
        {
            if (end < start) return 0;
            return (int)(((start + end) / 2.0f) * (end - start + 1));
        }
        #endregion

        #region BitCount
        public static int BitCount(int n)
        {
            var count = 0;
            while (n != 0)
            {
                n &= (n - 1);
                count++;
            }
            return count;
        }
        public static int BitCount(long n)
        {
            var count = 0;
            while (n != 0)
            {
                n &= (n - 1);
                count++;
            }
            return count;
        }
        #endregion
    }
}
