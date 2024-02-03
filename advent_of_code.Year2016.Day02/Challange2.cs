namespace advent_of_code.Year2016.Day02
{
    public static class Challange2
    {
        public static string DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            var map = new string[] { "XXOXX", "XOOOX", "OOOOO", "XOOOX", "XXOXX"};
           
            var currentX = 0;
            var currentY = 2;
            var code = "";

            foreach (var line in input)
            {
                foreach (var c in line)
                {
                    var newX = currentX;
                    var newY = currentY;

                    switch (c)
                    {
                        case 'U': newY--; break;
                        case 'D': newY++; break;
                        case 'L': newX--; break;
                        case 'R': newX++; break;
                    }

                    newX = Math.Clamp(newX, 0, 4);
                    newY = Math.Clamp(newY, 0, 4);

                    if (map[newY][newX] == 'O')
                    {
                        currentX = newX;
                        currentY = newY;
                    }
                }

                switch (currentY * 5 + currentX)
                {
                    case 2: code += "1"; break;
                    case 6: code += "2"; break;
                    case 7: code += "3"; break;
                    case 8: code += "4"; break;
                    case 10: code += "5"; break;
                    case 11: code += "6"; break;
                    case 12: code += "7"; break;
                    case 13: code += "8"; break;
                    case 14: code += "9"; break;
                    case 16: code += "A"; break;
                    case 17: code += "B"; break;
                    case 18: code += "C"; break;
                    case 22: code += "D"; break;
                }
            }

            return code;
        }
    }
}