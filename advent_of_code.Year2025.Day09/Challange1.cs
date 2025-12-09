namespace advent_of_code.Year2025.Day09
{
    public static class Challange1
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n")
                .Select(t=>new Vertex(t)).ToList();

            //We generate a polygona and default rectangle
            var polygon = new Polygon(input);
                        
            var rectangle = new Rectangle(new Vertex(0, 0), new Vertex(0, 0));

            //And then we check all combinations of rectangles to find biggest one
            var largestSquare = long.MinValue;
            for (var i = 0; i < polygon.Vertices.Count - 1; i++)
            {
                rectangle.Corner1 = polygon.Vertices[i];
                for (var j = i + 1; j < polygon.Vertices.Count; j++)
                {
                    rectangle.Corner2 = polygon.Vertices[j];
                    var size = rectangle.GetSize();
                    largestSquare = Math.Max(largestSquare, size);
                }
            }

            return largestSquare;
        }
    }
}