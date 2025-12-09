namespace advent_of_code.Year2025.Day09
{
    public static class Challange2
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n")
                .Select(t => new Vertex(t)).ToList();

            var polygon = new Polygon(input);

            //We check for largest rectangle that is fully contained in polygon
            //We do this parallelly to speed it up a bit
            var largestSquare = long.MinValue;
            Parallel.For<long>(0, polygon.Vertices.Count - 1, () => long.MinValue,
              (i,_ ,local) =>
              {
                  var rectangle = new Rectangle(polygon.Vertices[i], polygon.Vertices[i]);
                  for (var j = i + 1; j < polygon.Vertices.Count; j++)
                  {
                      rectangle.Corner2 = polygon.Vertices[j];
                      var size = rectangle.GetSize();
                      if ((size > local) && polygon.Contains(rectangle))
                      {
                          local = size;
                      }
                  }
                  return local;
              },
              (local) => largestSquare = Math.Max(largestSquare, local));

            return largestSquare;
        }
    }
}