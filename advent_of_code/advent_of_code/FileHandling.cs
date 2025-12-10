using Avalonia.Controls;
using Avalonia.Platform.Storage;
using DocumentFormat.OpenXml.Bibliography;
using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Threading.Tasks;

namespace advent_of_code
{
    public class FileHandling
    {
        private static VirtualFiles vf = new VirtualFiles();
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
            if (OperatingSystem.IsBrowser())
            {
                return vf.FileExists(file);
            }
            var privateFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file);
            return File.Exists(privateFile);
        }

        public static string ReadFile(string file)
        {
            if (OperatingSystem.IsBrowser())
            {
                if (File.Exists(file))
                {
                    return vf.ReadFromFile(file);
                }
            }
            var privateFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file);
            return File.ReadAllText(privateFile);
        }

        public static async Task<string> ReadFile(Stream stream)
        {
            return await new StreamReader(stream).ReadToEndAsync();
        }

        public static void WriteToFile(string file, string content)
        {
            if (OperatingSystem.IsBrowser())
            {
                vf.WriteToFile(file, content);
            }
            var privateFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file);
            File.WriteAllText(privateFile, content);
        }

        public static void WriteToFile(string file, byte[] content)
        {
            if (OperatingSystem.IsBrowser())
            {
                vf.WriteToFile(file, content);
            }
            var privateFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file);
            File.WriteAllBytes(privateFile, content);
        }

        public static void WriteToFile(Stream stream, string content)
        {
            new StreamWriter(stream).Write(content);
        }

        public static void WriteToFile(Stream stream, byte[] content)
        {
            new StreamWriter(stream).Write(content);
        }
    }
}
