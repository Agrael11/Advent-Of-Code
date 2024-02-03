using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code.Helpers
{
    public static class ExtensionsIndexOf
    {
        public static IEnumerable<int> IndexesOf(this string source, string value, StringComparison comparison = StringComparison.Ordinal)
        {
            for (var i = source.IndexOf(value, 0, comparison); i > -1; i = source.IndexOf(value, i + 1, comparison))
            {
                yield return i;
            }
        }
        public static IEnumerable<int> IndexesOf(this string source, char value)
        {
            for (var i = 0; i < source.Length; i++)
            {
                if (source[i] == value)
                {
                    yield return i;
                }
            }
        }

        public static IEnumerable<int> LastIndexesOf(this string source, string value, StringComparison comparison = StringComparison.Ordinal)
        {
            for (var i = source.LastIndexOf(value, 0, comparison); i > -1; i = source.LastIndexOf(value, i - 1, comparison))
            {
                yield return i;
                if (i == 0) break;
            }
        }

        public static IEnumerable<int> LastIndexesOf(this string source, char value)
        {
            for (var i = source.Length - 1; i >= 0; i--)
            {
                if (source[i] == value)
                {
                    yield return i;
                }
            }
        }
    }
}
