namespace advent_of_code.Year2024.Day20
{
    public static class Challange1
    {
        
        public static long DoChallange(string inputData)
        {
            //inputData = "###############\r\n#...#...#.....#\r\n#.#.#.#.#.###.#\r\n#S#...#.#.#...#\r\n#######.#.#.###\r\n#######.#.#...#\r\n#######.#.###.#\r\n###..E#...#...#\r\n###.#######.###\r\n#...###...#...#\r\n#.#####.#.###.#\r\n#.#...#.#.#...#\r\n#.#.#.#.#.#.###\r\n#...#...#...###\r\n###############";
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");
            Common.Parse(input);

            return Common.CountSavesAboveTreshold(100, 2);
        }
    }
}