namespace advent_of_code.Year2024.Day09
{
    internal class DataChunk (long id, int start, int length, bool file)
    {
        public bool File { get; } = file;
        public long ID { get; } = id;
        public int Start { get; set; } = start;
        public int Length { get; set; } = length;

        public override string ToString()
        {
            if (File)
            {
                return $"File {ID}, {Start}-{Start + Length - 1} ({Length} long)";
            }
            return $"Free space {Start}-{Start + Length - 1} ({Length} long)";
        }
    }
}
