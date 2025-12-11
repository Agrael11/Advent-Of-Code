namespace advent_of_code.Year2025.Day11
{
    public static class Challange2
    {
        public static long DoChallange(string inputData)
        {
            //Also parses the graph
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");
            var nodes = Common.ParseInput(input);

            //But now it searches the graph from one point to other
            var dactoout = Common.DFSAll(nodes["dac"], nodes["out"]);
            var ffttoout = Common.DFSAll(nodes["fft"], nodes["out"]);
            var dactofft = Common.DFSAll(nodes["dac"], nodes["fft"]);
            var ffttodac = Common.DFSAll(nodes["fft"], nodes["dac"]);
            var svrtofft = Common.DFSAll(nodes["svr"], nodes["fft"]);
            var svrtodac = Common.DFSAll(nodes["svr"], nodes["dac"]);

            //There are two checkpoint paths, which can be simplified into subpaths
            //Multiplying number of paths from from checkpoint to other gives us number of paths through the checkpointed subpath
            //Svr -> Dac -> Ftt -> Out (svrtodac -> dactoftt -> ftttoout)
            //Svr -> Ftt -> Dac -> Out (svrtodac -> dactoftt -> ftttoout)

            //Adding them together gives us total result
            return (svrtodac * dactofft * ffttoout) + (svrtofft * ffttodac * dactoout);
        }
    }
}