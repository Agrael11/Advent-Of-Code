namespace advent_of_code.Year2015.Day05
{
    public static class Challange2
    {
        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            var niceWords = 0;

            foreach (var line in input)
            {
                var last = '\0';
                var lastFound = "";
                var doubles = new HashSet<string>();
                var foundDouble = false;
                var foundTripple = false;

                for (var i = 0; i < line.Length; i++)
                {
                    if (!foundDouble && last != '\0')
                    {
                        var found = last.ToString() + line[i];
                        if (lastFound != found)
                        {
                            foundDouble |= !doubles.Add(found);
                        }
                        lastFound = found;
                    }

                    if (!foundTripple && line.Length > i + 2)
                    {
                        foundTripple |= line[i] == line[i + 2];
                    }

                    if (foundDouble && foundTripple)
                    {
                        niceWords++;
                        break;
                    }

                    last = line[i];
                }
            }

            return niceWords;
        }
    }
}
