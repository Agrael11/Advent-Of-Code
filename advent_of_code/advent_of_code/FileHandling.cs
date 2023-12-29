using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code
{
    internal class FileHandling
    {
        public static bool DirectoryExists(string directory)
        {
            string privateDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, directory);
            return Directory.Exists(privateDirectory);
        }

        public static void CreateDirectory(string directory)
        {
            string privateDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, directory);
            Directory.CreateDirectory(privateDirectory);
        }

        public static bool FileExists(string file)
        {
            string privateFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file);
            return File.Exists(privateFile);
        }

        public static string ReadFile(string file)
        {
            string privateFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file);
            return File.ReadAllText(privateFile);
        }

        public static string ReadFile(Stream stream)
        {
            return new StreamReader(stream).ReadToEnd();
        }

        public static void WriteToFile(string file, string content)
        {
            string privateFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file);
            File.WriteAllText(privateFile, content);
        }

        public static void WriteToFile(string file, byte[] content)
        {
            string privateFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file);
            File.WriteAllBytes(privateFile, content);
        }
    }
}
