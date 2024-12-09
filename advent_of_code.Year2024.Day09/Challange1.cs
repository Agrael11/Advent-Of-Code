using System.ComponentModel.DataAnnotations;
using System.Xml;

namespace advent_of_code.Year2024.Day09
{
    public static class Challange1
    {
        public static long DoChallange(string inputData)
        {
            //inputData = "2333133121414131402";
            var input = inputData.Replace("\r", "").TrimEnd('\n');
            var chunks = ParseInput(input);
            
            //We go through empty spaces in our file list
            for (var spaceIndex = 0; spaceIndex < chunks.Count; spaceIndex++)
            {
                if (chunks[spaceIndex].File)
                {
                    continue;
                }

                //We try to fill the empty space - if we fail, there is nothing more to fill spaces with
                if (!PushToSpace(chunks, spaceIndex))
                    break;
            }

            return chunks.Sum(c => c.File ? (Sum(c.Start, c.Length) * c.ID) : 0);
        }

        /// <summary>
        /// Calculates sum of consecutive numbers.
        /// </summary>
        /// <param name="start">Start</param>
        /// <param name="length">Length</param>
        /// <returns></returns>
        internal static long Sum(int start, int length)
        {
            return (start * 2 + length - 1) * length / 2;
        }

        private static bool PushToSpace(List<DataChunk> chunks, int spaceAddress)
        {
            //For every File available
            for (var fileAddres = chunks.Count - 1; fileAddres >= spaceAddress; fileAddres--)
            {
                if (!chunks[fileAddres].File)
                {
                    continue;
                }

                //We try to fill space with it. If it is fully filled, we end this function
                if (TryFilleSpaceWithFile(chunks, spaceAddress, fileAddres))
                {
                    return true;
                }
                else
                {
                    //Otherwise we increment our positions (because they were pushed by partially filling the space) and continue
                    fileAddres++;
                    spaceAddress++;
                }
            }

            //We failed to fill the space
            return false;
        }

        private static bool TryFilleSpaceWithFile(List<DataChunk> chunks, int spaceAddress, int fileAddress)
        {
            var file = chunks[fileAddress];
            var space = chunks[spaceAddress];
            
            //If file is smaller, we fully move it at spaces position
            if (file.Length < space.Length)
            {
                chunks.RemoveAt(fileAddress);
                chunks.Insert(spaceAddress, file);
                file.Start = space.Start;
                space.Start += file.Length;
                space.Length -= file.Length;
                return false;
            }
            
            //If it is same we also remove the empty space
            if (file.Length == space.Length)
            {
                chunks.RemoveAt(fileAddress);
                chunks.RemoveAt(spaceAddress);
                file.Start = space.Start;
                chunks.Insert(spaceAddress, file);
                return true;
            }
            
            //If the file is larger, we only shorten the original file and create new part of it on empty space. (and remove the space)
            chunks.RemoveAt(spaceAddress);
            file.Length -= space.Length;
            chunks.Insert(spaceAddress, new DataChunk(file.ID, space.Start, space.Length, true));

            return true;
        }

        private static List<DataChunk> ParseInput(string input)
        {
            var chunks = new List<DataChunk>();
            var space = false;
            var id = 0;
            var position = 0;
            foreach (var lengthInfo in input.Select(c=>int.Parse(c.ToString())))
            {
                var start = position;
                var length = lengthInfo;
                position += lengthInfo;
                if (space)
                {
                    chunks.Add(new DataChunk(-1, start, length, false));
                }
                else
                {
                    chunks.Add(new DataChunk(id, start, length, true));
                    id++;
                }
                space = !space;
            }
            return chunks;
        }
    }
}