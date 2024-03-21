namespace advent_of_code.Year2016.Day09
{
    public static class Challange1
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n');
                
            var length = 0L;

            for (var i = 0; i < input.Length;)
            {
                if (input[i] == '(')
                {
                    var marker = input[(i+1)..input.IndexOf(')', i + 1)];
                    var markerData = marker.Split('x');
                    i = i + 2 + marker.Length + int.Parse(markerData[0]);
                    length += int.Parse(markerData[0]) * int.Parse(markerData[1]);
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