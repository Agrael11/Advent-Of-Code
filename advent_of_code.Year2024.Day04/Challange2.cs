namespace advent_of_code.Year2024.Day04
{
    public static class Challange2
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");

            Common.Letters = input;
            return SearchX("MAS");
        }

        /// <summary>
        /// Searches for word in entire "X" crossword
        /// </summary>
        /// <param name="word">Word to search</param>
        /// <returns></returns>
        public static int SearchX(string word)
        {
            var count = 0;
            //We get reversed copy of word
            var reverseWord = new string(word.Reverse().ToArray());
            //Easier to check
            var startChar = word[0];
            var startChar2 = reverseWord[0];

            for (var y = 0; y < Common.Height; y++)
            {
                for (var x = 0; x < Common.Width; x++)
                {
                    var crosses = 0;
                    var thisChar = Common.GetLetterAt(x, y);
                    
                    if (thisChar != startChar && thisChar != startChar2)
                        continue;

                    //We search twice - for word and it's reverse copy
                    crosses += CountX(word, x, y);
                    crosses += CountX(reverseWord, x, y);

                    //If we found exactly two instances - it is X. (2 is maximum we can find)
                    if (crosses == 2) count++;
                }
            }

            return count;
        }

        /// <summary>
        /// Searches for word - 2 directions only - right-down and left-down (moved to right so it overlaps in "X")
        /// </summary>
        /// <param name="word">Word we search for</param>
        /// <param name="x">X position</param>
        /// <param name="y">Y position</param>
        /// <returns></returns>
        public static int CountX(string word, int x, int y)
        {
            //Very similar to CountWordsearch in Challange 1
            var count = 2;
            var diagonal1 = true;
            var diagonal2 = true;

            for (var offset = 0; offset < word.Length; offset++)
            {
                if (diagonal1 && Common.GetLetterAt(x + offset, y + offset) != word[offset]) //Right, Down
                {
                    diagonal1 = false;
                    count--;
                }
                //We start at "x + word.Length - 1" to make "X" shape.
                if (diagonal2 && Common.GetLetterAt(x + word.Length - 1 - offset, y + offset) != word[offset]) //Left, Down
                {
                    diagonal2 = false;
                    count--;
                }

                if (!diagonal1 && !diagonal2) break;
            }

            return count;
        }
    }
}