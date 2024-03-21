namespace advent_of_code.Year2016.Day07
{
    public static class Challange2
    {
        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            var count = 0;

            foreach (var address in input)
            {
                count += CheckSSL(address);
            }

            return count;
        }

        public static int CheckSSL(string address)
        {
            var hyperNetState = false;
            var abas = new HashSet<string>();
            var babs = new HashSet<string>();
            for (var i = 0; i < address.Length - 2; i++)
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

                if (address[i] == address[i + 2] && address[i] != address[i + 1] && (address[i + 1] != '[' || address[i + 1] != ']'))
                {
                    if (hyperNetState)
                    {
                        if (abas.Contains(address[i + 1] + "" + address[i])) return 1;
                        babs.Add(address[i + 1] + "" + address[i]);
                    }
                    else
                    {
                        if (babs.Contains(address[i] + "" + address[i + 1])) return 1;
                        abas.Add(address[i] + "" + address[i + 1]);
                    }
                }
            }

            return 0;
        }
    }
}