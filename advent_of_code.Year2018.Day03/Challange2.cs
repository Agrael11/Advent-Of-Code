using System.Security.Principal;

namespace advent_of_code.Year2018.Day03
{
    public static class Challange2
    {
        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');
            var rectangles = new List<Rectangle>();


            foreach (var line in input)
            {
                rectangles.Add(new Rectangle(line));
            }

            foreach (var rectangle1 in rectangles)
            {
                if (rectangle1.Collided) continue;
                var overlap = false;
                foreach (var rectangle2 in rectangles)
                {
                    if (rectangle1.ID == rectangle2.ID) continue;
                    if (rectangle1.Intersects(rectangle2))
                    {
                        overlap = true;
                        rectangle2.Collided = true;
                    }
                }
                if (!overlap) return rectangle1.ID;
            }

            return -1;
        }
    }
}