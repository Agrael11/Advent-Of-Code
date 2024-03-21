namespace advent_of_code.Year2016.Day07
{
    public static class Challange1
    {
        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            var count = 0;

            foreach (var address in input)
            {
                count += CheckTLS(address);
            }

            return count;
        }

        public static int CheckTLS(string address)
        {
            var hyperNetState = false;
            var foundAbba = 0;
            for (var i = 0; i < address.Length - 3; i++)
            {
                if (address[i] == '[')
                {
                    hyperNetState = true;
                    continue;
                }

                if (address[i] == ']')
                {
                    hyperNetState = false;
                    continue;
                }

                if (address[i] == address[i + 3] && address[i + 1] == address[i + 2] && address[i] != address[i + 1])
                {
                    if (hyperNetState)
                    {
                        return 0;
                    }
                    
                    foundAbba = 1;
                }
            }

            return foundAbba;
        }
    }
}