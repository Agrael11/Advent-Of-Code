namespace advent_of_code.Year2018.Day03
{
    public static class Challange1
    {
        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');
            var rectangles = new List<Rectangle>();


            foreach (var line in input)
            {
                rectangles.Add(new Rectangle(line));
            }

            var overlaps = new HashSet<(int x, int y)>();

            for (var i = 0; i < rectangles.Count-1; i++)
            {
                var rectangle1 = rectangles[i];
                for (var j = i+1; j < rectangles.Count; j++)
                {
                    var rectangle2 = rectangles[j];
                    var overlap = rectangle1.Overlap(rectangle2);
                    if (overlap is not null)
                    {
                        for (var x = overlap.X1; x <= overlap.X2; x++)
                        {
                            for (var y = overlap.Y1; y <= overlap.Y2; y++)
                            {
                                overlaps.Add((x, y));
                            }
                        }
                    }
                }
            }

            return overlaps.Count;
        }
    }
}