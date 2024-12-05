namespace advent_of_code.Year2024.Day05
{
    internal class Common
    {
        /// <summary>
        /// Parses input data
        /// </summary>
        /// <param name="input">Input data, split by lines</param>
        /// <returns>
        /// rules = Dictionary of rules: int Key = page, List<int> value = rules applying to page
        /// startOfData = int index, where post-Rules data start in input - print jobs.
        /// </returns>
        internal static (Dictionary<int,List<int>> rules, int startOfData) Parse(string[] input)
        {
            var rules = new Dictionary<int, List<int>>();

            var dataStart = 0;

            for (var i = 0; i < input.Length; i++)
            {
                //If line is empty, it is end of rules. We mark start of print job list, and end the loop
                if (string.IsNullOrWhiteSpace(input[i]))
                {
                    dataStart = i + 1;
                    break;
                }

                var data = input[i].Split('|');
                var first = int.Parse(data[0]);
                var second = int.Parse(data[1]);

                //If we already don't have this page in rules, we create list for it
                if (!rules.TryGetValue(second, out var list))
                {
                    list = new List<int>();
                }

                //Then we add it's counterpart and assign the rules to page
                list.Add(first);
                rules[second] = list;
            }

            return (rules, dataStart);
        }

        /// <summary>
        /// Checks if Entire Page List adheres to Rules
        /// </summary>
        /// <param name="rules">All Rules</param>
        /// <param name="pages">All Pages in Print Job</param>
        /// <returns>Returns true if page list adheres to rules, false otherwise</returns>
        internal static bool PrintJobAdheresTorRules(Dictionary<int, List<int>> rules, int[] pages)
        {
            for (var pageIndex = 0; pageIndex < pages.Length; pageIndex++)
            {
                //If our page has rules associated to it, and doesn't adhere to them, entire list fails.
                var page = pages[pageIndex];
                if (rules.TryGetValue(page, out var pageRules) && !PageAdheresToRules(pageRules, pages, pageIndex))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Checkes if page at @pageIndex adgere to rules
        /// </summary>
        /// <param name="pageRules">Rules this applying to this page</param>
        /// <param name="pages">All pages list</param>
        /// <param name="pageIndex">Index of current page</param>
        /// <returns>True if page adheres to rules, false otherwise</returns>
        private static bool PageAdheresToRules(List<int> pageRules, int[] pages, int pageIndex)
        {
            foreach (var previousPage in pageRules)
            {
                //If index of every page that should be before current page according to rules is
                //larger than our index, it means they are out of order
                //If page is not included in our list, it is -1, (always in order), and therefore it is okay
                if (Array.IndexOf(pages, previousPage) > pageIndex)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
