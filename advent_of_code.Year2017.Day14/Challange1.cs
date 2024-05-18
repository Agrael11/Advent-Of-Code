namespace advent_of_code.Year2017.Day14
{
    public static class Challange1
    {
        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n');

            var UsedSquares = 0;

            for (var i = 0; i < 128; i++)
            {
                UsedSquares += KnotHash.GetDenseHash(input + "-" + i, KnotHash.HashFormat.Binary).Count( c=> c == '1');
            }

            return UsedSquares;
        }
    }
}