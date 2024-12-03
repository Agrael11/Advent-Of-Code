using System.Text.RegularExpressions;

namespace advent_of_code.Year2024.Day03
{
    internal static partial class Regexes
    {
        /// <summary>
        /// Only focuses on matching the "mul(number,number)"
        /// Finds string mul(
        /// Then it looks for any number of digits, character ',', again any number of digits and closing parenthesis - )
        /// \d - digit, {1,3} = 1 to 3 times (so 1 to 3 digits one after another)
        /// \( and \) are just escaped parenthesis
        /// </summary>
        [GeneratedRegex(@"mul\(\d{1,3},\d{1,3}\)")]
        internal static partial Regex RegexMuls();

        /// <summary>
        /// Starts same, but puts expression from previous one into group by putting () around it.
        /// Then it has two more literal groups - one for do() and one for don't()
        /// | is "OR" - allowing multiple groupss
        /// </summary>
        [GeneratedRegex(@"(mul\(\d{1,3},\d{1,3}\))|(do\(\))|(don't\(\))")]
        internal static partial Regex RegexMulsDoDonts();
    }
}