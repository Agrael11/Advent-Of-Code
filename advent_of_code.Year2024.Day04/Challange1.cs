namespace advent_of_code.Year2024.Day04
{
    public static class Challange1
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");
            
            Common.Letters = input;
            return SearchWordsearch("XMAS");
        }

        /// <summary>
        /// Searches for word in entire crossword
        /// </summary>
        /// <param name="word">Word to search</param>
        /// <param name="backwards">8-directional if false, 4-directional if true</param>
        /// <returns></returns>
        public static int SearchWordsearch(string word, bool backwards = false)
        {
            var count = 0;
            var startChar = word[0];

            for (var y = 0; y < Common.Height; y++)
            {
                for (var x = 0; x < Common.Width; x++)
                {
                    //If the letter is not the first letter of word we are searching for, there is no point to test it at all
                    if (Common.GetLetterAt(x, y) != startChar)
                        continue;

                    count += CountWordsearch(word, x, y);
                }
            }

            //Since we search for word in 4 directions only, we search for reversed version of word too
            //That effectively simulates remaining 4 directions.
            if (!backwards)
            {
                count += SearchWordsearch(new string(word.Reverse().ToArray()), true);
            }

            return count;
        }

        /// <summary>
        /// Searches for word - 4 directions only - right, down, right-down and left-down
        /// </summary>
        /// <param name="word">Word we search for</param>
        /// <param name="x">X position</param>
        /// <param name="y">Y position</param>
        /// <returns></returns>
        public static int CountWordsearch(string word, int x, int y)
        {
            //Instead of counting up as we find matching words, we count down as we find unmatching ones
            var count = 4;

            //This makes sure we already did not break the direction
            var horizontal = true;
            var vertical = true;
            var diagonal1 = true;
            var diagonal2 = true;

            //We iterate over each letter in word, AND use the offset of letter to get position it should be at
            for (var offset = 0; offset < word.Length; offset++)
            {
                if (horizontal && Common.GetLetterAt(x + offset, y) != word[offset]) //Right
                {
                    horizontal = false;
                    count--;
                }
                if (diagonal1 && Common.GetLetterAt(x + offset, y + offset) != word[offset]) //Right, Down
                {
                    diagonal1 = false;
                    count--;
                }
                if (vertical && Common.GetLetterAt(x, y + offset) != word[offset]) //Down
                {
                    vertical = false;
                    count--;
                }
                if (diagonal2 && Common.GetLetterAt(x - offset, y + offset) != word[offset]) //Left, Down
                {
                    diagonal2 = false;
                    count--;
                }
                
                //Early exit if we found nothing.
                if (!horizontal && !diagonal1 && !vertical && !diagonal2) break;
            }

            return count;
        }
    }
}