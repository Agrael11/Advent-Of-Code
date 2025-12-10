using System.Collections.Generic;
using System.Text;

namespace advent_of_code
{
    internal class VirtualFiles
    {
        private Dictionary<string, byte[]> files = new Dictionary<string, byte[]>();

        public bool FileExists(string fileName)
        {
            return files.ContainsKey(fileName);
        }

        public void WriteToFile(string fileName, string content)
        {
            files[fileName] = Encoding.Unicode.GetBytes(content);
        }


        public void WriteToFile(string fileName, byte[] content)
        {
            files[fileName] = content;
        }

        public string ReadFromFile(string fileName)
        {
            return Encoding.Unicode.GetString(files[fileName]);
        }

        public byte[] ReadBytesFromFile(string fileName)
        {
            return files[fileName];
        }
    }
}
