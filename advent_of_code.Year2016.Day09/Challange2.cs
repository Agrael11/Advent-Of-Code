namespace advent_of_code.Year2016.Day09
{
    public static class Challange2
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n');

            return Decompress(input);
        }

        public static long Decompress(string input)
        {
            var length = 0L;

            for (var i = 0; i < input.Length;)
            {
                if (input[i] == '(')
                {
                    var marker = input[(i + 1)..input.IndexOf(')', i + 1)];
                    var markerData = marker.Split('x');
                    i = i + 2 + marker.Length;
                    var markedInput = input[i..(i + int.Parse(markerData[0]))];
                    i += int.Parse(markerData[0]);
                    length += Decompress(markedInput) * int.Parse(markerData[1]);
                }
                else
                {
                    length++;
                    i++;
                }
            }

            return length;
        }
    }
}