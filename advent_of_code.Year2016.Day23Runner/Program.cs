using advent_of_code.Year2016.Day25;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;

namespace advent_of_code.Year2016.Day23Runner
{
    public static class Program
    {
        private static int? MaxOutput = null;
        private static StreamWriter? LogFile = null;
        private static int Outputs = 0;
        private static bool Running = true;

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

                var cpu = new CPU(0, 0, 0, 0, OutputInterrupt);
                cpu.Compile(File.ReadAllText(arguments.MrasmPath).Replace("\r", "").TrimEnd('\n').Split("\n"));
                cpu.SaveBinary(arguments.BinPath);

                Console.WriteLine("Done!");

                return;
            }

            var truecpu = new CPU(arguments.A, arguments.B, arguments.C, arguments.D, OutputInterrupt);
            
            if (arguments.MrasmPath is not null)
            {
                if (!File.Exists(arguments.MrasmPath))
                {
                    Console.WriteLine($"File {arguments.MrasmPath} doesn't exist.");
                    return;
                }

                truecpu.Compile(File.ReadAllText(arguments.MrasmPath).Replace("\r", "").TrimEnd('\n').Split("\n"));
            }
            else if (arguments.BinPath is not null)
            {
                if (!File.Exists(arguments.BinPath))
                {
                    Console.WriteLine($"File {arguments.BinPath} doesn't exist.");
                    return;
                }
                Console.WriteLine();

                truecpu.LoadBinary(arguments.BinPath);
            }

            if (arguments.OutputPath is not null)
            {
                if (!IsFilePathValid(arguments.OutputPath))
                {
                    Console.WriteLine("Log path not valid, ignoring.");
                }
                if (File.Exists(arguments.OutputPath))
                {
                    Console.Write($"File {arguments.OutputPath} already exists. Overwrite? [Y/N]: ");
                    while (true)
                    {
                        var answer = Console.ReadKey().KeyChar;

                        if (answer == 'y' || answer == 'Y')
                        {
                            File.Delete(arguments.OutputPath);
                            LogFile = new StreamWriter(arguments.OutputPath, false);
                            break;
                        }

                        if (answer == 'n' || answer == 'N')
                        {
                            Console.WriteLine();
                            Console.WriteLine();
                            break;
                        }
                        Console.WriteLine();
                    }
                }
                else
                {
                    LogFile = new StreamWriter(arguments.OutputPath, false);
                }
            }

            if (arguments.MaxOutput != -1) MaxOutput = arguments.MaxOutput;
            else MaxOutput = null;

            truecpu.Run(ref Running);

            LogFile?.Close();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"Done: A={truecpu.Registers[0]}; B={truecpu.Registers[1]}; C={truecpu.Registers[2]}; D={truecpu.Registers[3]};");
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
            Console.WriteLine("-log                Saves output as log.");
            Console.WriteLine("-maxoutput          Selects maximum output.");
            Console.WriteLine("-h                  Displays help information.");
        }

        public static void OutputInterrupt(int value)
        {
            var toWrite = (Outputs == 0 ? "" : "; ") + value;
            Outputs++;
            if (Outputs > MaxOutput) Running = false;


            if (LogFile is not null)
            {
                LogFile.Write(toWrite);
            }
            else
            {
                Console.Write(toWrite);
            }
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