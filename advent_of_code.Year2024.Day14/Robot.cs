namespace advent_of_code.Year2024.Day14
{
    internal class Robot
    {
        public int LocationX { get; set; }
        public int LocationY { get; set; }
        public int VectorX { get; private init; }
        public int VectorY { get; private init; }

        public Robot(string definition)
        {
            var definitionSplit = definition.Split(' ').Select(part => part[2..].Split(',').Select(int.Parse)).ToArray();
            LocationX = definitionSplit[0].First();
            LocationY = definitionSplit[0].Last();
            VectorX = definitionSplit[1].First();
            VectorY = definitionSplit[1].Last();
        }

        public void Move(int width, int height, int steps)
        {
            //This moves robot *steps* times by his vector. it also keeps him withing bounds by wrapping
            var x = (LocationX + VectorX * steps) % width;
            var y = (LocationY + VectorY * steps) % height;
            if (x < 0) x += width;
            if (y < 0) y += height;
            (LocationX, LocationY) = (x, y); 
        }
    }
}