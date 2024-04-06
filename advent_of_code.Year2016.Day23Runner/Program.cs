using advent_of_code.Year2016.Day23;

namespace advent_of_code.Year2016.Day23Runner
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var arguments = new Arguments(args);
            if (arguments.HelpRequested)
            {
                ShowHelp();
                return;
            }
            if (arguments.MrasmPath is null && arguments.BinPath is null)
            {
                Console.WriteLine("You need to specify exactly one path");
                ShowHelp();
                return;
            }
            if (arguments.MrasmPath is not null && arguments.BinPath is not null && !arguments.CompilationRequested)
            {
                Console.WriteLine("You need to specificy exactly one path or compilation needs to be requested");
                ShowHelp();
                return;
            }
            if (arguments.MrasmPath is not null && arguments.BinPath is not null && arguments.CompilationRequested)
            {
                if (!File.Exists(arguments.MrasmPath))
                {
                    Console.WriteLine($"File {arguments.MrasmPath} doesn't exist.");
                    return;
                }


                if (!IsFilePathValid(arguments.BinPath))
                {
                    Console.WriteLine($"{arguments.BinPath} is not a valid path.");
                    return;
                }

                if (File.Exists(arguments.BinPath))
                {
                    Console.Write($"File {arguments.MrasmPath} already exists. Overwrite? [Y/N]: ");
                    while (true)
                    {
                        var answer = Console.ReadKey().KeyChar;

                        if (answer == 'y' || answer == 'Y')
                        {
                            Console.WriteLine();
                            Console.WriteLine();
                            break;
                        }

                        if (answer == 'n' || answer == 'N')
                        {
                            return;
                        }
                        Console.WriteLine();
                    }

                }

                var cpu = new CPU(0, 0, 0, 0);
                cpu.Compile(File.ReadAllText(arguments.MrasmPath).Replace("\r", "").TrimEnd('\n').Split("\n"));
                cpu.SaveBinary(arguments.BinPath);

                Console.WriteLine("Done!");

                return;
            }
            if (arguments.MrasmPath is not null)
            {
                if (!File.Exists(arguments.MrasmPath))
                {
                    Console.WriteLine($"File {arguments.MrasmPath} doesn't exist.");
                    return;
                }

                var cpu = new CPU(arguments.A, arguments.B, arguments.C, arguments.D);
                cpu.Compile(File.ReadAllText(arguments.MrasmPath).Replace("\r", "").TrimEnd('\n').Split("\n"));
                cpu.Run();

                Console.WriteLine();
                Console.WriteLine($"Done: A={cpu.Registers[0]}; B={cpu.Registers[1]}; C={cpu.Registers[2]}; D={cpu.Registers[3]};");

                return;
            }

            if (arguments.BinPath is not null)
            {
                if (!File.Exists(arguments.BinPath))
                {
                    Console.WriteLine($"File {arguments.BinPath} doesn't exist.");
                    return;
                }

                var cpu = new CPU(arguments.A, arguments.B, arguments.C, arguments.D);
                cpu.LoadBinary(arguments.BinPath);
                cpu.Run();

                Console.WriteLine();
                Console.WriteLine($"Done: A={cpu.Registers[0]}; B={cpu.Registers[1]}; C={cpu.Registers[2]}; D={cpu.Registers[3]};");
            }
        }

        private static void ShowHelp()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("program.exe -a <integer> -b <integer> -c <integer> -d <integer> -bin <file_path> -mrasm <file_path> -compile");
            Console.WriteLine("Options:");
            Console.WriteLine("-a <integer>        Specifies value for 'a'.");
            Console.WriteLine("-b <integer>        Specifies value for 'b'.");
            Console.WriteLine("-c <integer>        Specifies value for 'c'.");
            Console.WriteLine("-d <integer>        Specifies value for 'd'.");
            Console.WriteLine("-bin <file_path>    Specifies binary file path.");
            Console.WriteLine("-mrasm <file_path>  Specifies MRASM file path.");
            Console.WriteLine("-compile            Displays help information.");
            Console.WriteLine("-h                  Displays help information.");
        }

        public static bool IsFilePathValid(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return false;

            try
            {
                // Get the full absolute path
                var fullPath = Path.GetFullPath(filePath);

                // Get the directory part of the full path
                var directoryPath = Path.GetDirectoryName(fullPath);

                // Check if the directory exists
                if (!Directory.Exists(directoryPath))
                {
                    // Directory doesn't exist
                    return false;
                }

                // Get invalid characters in file names
                var invalidChars = Path.GetInvalidFileNameChars();

                // Check if the file name contains any invalid characters
                foreach (var invalidChar in invalidChars)
                {
                    if (filePath.Contains(invalidChar))
                    {
                        // File name contains invalid characters
                        return false;
                    }
                }

                // File path is valid
                return true;
            }
            catch (Exception ex)
            {
                // Error occurred while checking file path validity
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }
    }
}