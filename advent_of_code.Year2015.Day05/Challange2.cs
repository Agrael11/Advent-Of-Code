namespace advent_of_code.Year2015.Day05
{
    public static class Challange2
    {
        public static int DoChallange(string inputData)
        {
            string[] input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');
            
            int niceWords = 0;

            foreach (string line in input)
            {
                char last = '\0';
                string lastFound = "";
                HashSet<string> doubles = [];
                bool foundDouble = false;
                bool foundTripple = false;

                for (int i = 0; i < line.Length; i++)
                {
                    if (!foundDouble && last != '\0')
                    {
                        string found = last.ToString() + line[i];
                        if (lastFound != found)
                        {
                            foundDouble |= !doubles.Add(found);
                        }
                        lastFound = found;
                    }
                    
                    if (!foundTripple && line.Length > i+2)
                    {
                        foundTripple |= line[i] == line[i+2];
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
