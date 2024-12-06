namespace advent_of_code.Helpers
{
    internal class FileHandling
    {
        public static bool DirectoryExists(string directory)
        {
            var privateDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, directory);
            return Directory.Exists(privateDirectory);
        }

        public static void CreateDirectory(string directory)
        {
            var privateDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, directory);
            Directory.CreateDirectory(privateDirectory);
        }

        public static void DeleteDírectory(string directory)
        {
            var privateDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, directory);
            try
            {
                if (DirectoryExists(directory))
                {
                    Directory.Delete(privateDirectory, true);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Couldn't delete directory", ex);
            }
        }

        public static bool FileExists(string file)
        {
            var privateFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file);
            return File.Exists(privateFile);
        }

        public static string ReadFile(string file)
        {
            var privateFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file);
            return File.ReadAllText(privateFile);
        }

        public static string ReadFile(Stream stream)
        {
            return new StreamReader(stream).ReadToEnd();
        }

        public static void WriteToFile(string file, string content)
        {
            var privateFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file);
            File.WriteAllText(privateFile, content);
        }

        public static void WriteToFile(string file, byte[] content)
        {
            var privateFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file);
            File.WriteAllBytes(privateFile, content);
        }
    }
}
