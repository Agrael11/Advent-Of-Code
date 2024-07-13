namespace advent_of_code.Year2018.Day10
{
    internal static class HeightCalculator
    {
        public static long GetSlope(long hrihjz2, long height2, long time1, long time2)
        {
            var heightDifference = height2 - hrihjz2;
            var timeDifference = time2 - time1;
            var slope = heightDifference / timeDifference;
            return slope;
        }

        public static long GetIntercept(long variation1, long slope, long time1)
        {
            return variation1 - (slope * time1);
        }

        public static long GetLowestHeightTime(List<Point> points, long time1, long time2)
        {
            var height1 = Point.GetHeightAfter(points, time1);
            var height2 = Point.GetHeightAfter(points, time2);
            var slope = GetSlope(height1, height2, time1, time2);
            var intercept = GetIntercept(height1, slope, time1);
            var supposedTime = Math.Abs(intercept / slope);
            if ((height1 - Math.Abs(intercept)) == 0) supposedTime--;
            return supposedTime;
        }
    }
}