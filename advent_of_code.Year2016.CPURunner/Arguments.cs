namespace advent_of_code.Year2016.CPURunner
{
    public struct Arguments
    {
        public int A { get; private set; }
        public int B { get; private set; }
        public int C { get; private set; }
        public int D { get; private set; }
        public string? BinPath { get; private set; }
        public string? MrasmPath { get; private set; }
        public string? OutputPath { get; private set; }
        public bool HelpRequested { get; private set; }
        public bool CompilationRequested { get; private set; }
        public int MaxOutput { get; private set; }

        public Arguments(string[] args)
        {
            A = B = C = D = 0;
            BinPath = MrasmPath = null;
            HelpRequested = false;
            CompilationRequested = false;

            for (var i = 0; i < args.Length; i++)
            {
                if (args[i].StartsWith('-'))
                {
                    switch (args[i])
                    {
                        case "-a":
                            A = int.TryParse(args[i + 1], out var a) ? a : 0;
                            break;
                        case "-b":
                            B = int.TryParse(args[i + 1], out var b) ? b : 0;
                            break;
                        case "-c":
                            C = int.TryParse(args[i + 1], out var c) ? c : 0;
                            break;
                        case "-d":
                            D = int.TryParse(args[i + 1], out var d) ? d : 0;
                            break;
                        case "-bin":
                            BinPath = i + 1 < args.Length && !args[i + 1].StartsWith('-') ? args[i + 1] : null;
                            break;
                        case "-mrasm":
                            MrasmPath = i + 1 < args.Length && !args[i + 1].StartsWith('-') ? args[i + 1] : null;
                            break;
                        case "-log":
                            OutputPath = i + 1 < args.Length && !args[i + 1].StartsWith('-') ? args[i + 1] : null;
                            break;
                        case "-h":
                            HelpRequested = true;
                            break;
                        case "-compile":
                            CompilationRequested = true;
                            break;
                        case "-maxoutput":
                            MaxOutput = int.TryParse(args[i + 1], out var maxout) ? maxout : 0;
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}