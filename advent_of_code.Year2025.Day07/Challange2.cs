namespace advent_of_code.Year2025.Day07
{
    public static class Challange2
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");

            //Same as part 1
            (var  manifoldMap, var entryPoint) = Common.ParseMap(input);

            Common.GenerateConnections(manifoldMap);

            //Except we use Value property - calculates total amount of visits of each manifold.
            return entryPoint.Value;
        }
    }
}