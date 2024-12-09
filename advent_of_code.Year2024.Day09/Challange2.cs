namespace advent_of_code.Year2024.Day09
{
    public static class Challange2
    {
        public static long DoChallange(string inputData)
        {
            //inputData = "2333133121414131402";
            var input = inputData.Replace("\r", "").TrimEnd('\n').Select(c => (int)(c - '0'));
            (var files, var spaces) = ParseInput(input);

            //We go through files from end, as problem asks.
            for (var i = files.Count-1; i>=1; i--)
            {
                if (TryMoveFile(files, spaces, i))
                {
                    i++;
                }
            }

            //We return som of file parts.
            return files.Sum(c => Challange1.Sum(c.Start, c.Length) * c.ID);
        }

        private static bool TryMoveFile(List<DataChunk> files, List<DataChunk> spaces, int fileAddress)
        {
            var file = files[fileAddress];

            //We search through all spaces
            for (var i = 0; i < spaces.Count; i++)
            {
                var space = spaces[i];

                //If space is beyond our file we did failed to move it
                if (space.Start > file.Start)
                {
                    return false;
                }

                //If file is bigger than space, it is not possible to move it there
                if (space.Length < file.Length) continue;

                //We move the file
                MoveFileIntoSpace(spaces, file, space, i);
                return true;
            }

            return false;
        }

        private static void MoveFileIntoSpace(List<DataChunk> spaces, DataChunk file, DataChunk space, int spaceAddress)
        {
            //We'll set files start position to be same as of empty space
            file.Start = space.Start;
            
            //If file is smaller, we just shorten the empty space
            if (file.Length < space.Length)
            {
                space.Start += file.Length;
                space.Length -= file.Length;
                return;
            }
            
            //At this point file is equal sized to empty space so we remove space
            spaces.RemoveAt(spaceAddress);
        }

        private static (List<DataChunk> files, List<DataChunk> spaces) ParseInput(IEnumerable<int> input)
        {
            var files = new List<DataChunk>();
            var spaces = new List<DataChunk>();
            var space = false;
            var id = 0;
            var position = 0;
            foreach (var length in input)
            {
                var start = position;
                position += length;
                if (space)
                {
                    spaces.Add(new DataChunk(-1, start, length, false));
                }
                else
                {
                    files.Add(new DataChunk(id, start, length, true));
                    id++;
                }
                space = !space;
            }
            return (files, spaces);
        }
    }
}