using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code.Helpers.AsciiArtReader
{
    internal struct FontData
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public readonly List<CharacterDefinition> definitions;

        public static string TypeA = "4,6\r\n\r\nC\r\n*##*\r\n#**#\r\n#***\r\n#***\r\n#**#\r\n*##*\r\n\r\nF\r\n####\r\n#***\r\n###*\r\n#***\r\n#***\r\n#***\r\n\r\nG\r\n*##*\r\n#**#\r\n#***\r\n#*##\r\n#**#\r\n*###\r\n\r\nH\r\n#**#\r\n#**#\r\n####\r\n#**#\r\n#**#\r\n#**#\r\n\r\nJ\r\n**##\r\n***#\r\n***#\r\n***#\r\n#**#\r\n*##*\r\n\r\nL\r\n#***\r\n#***\r\n#***\r\n#***\r\n#***\r\n####\r\n\r\nO\r\n*##*\r\n#**#\r\n#**#\r\n#**#\r\n#**#\r\n*##*\r\n\r\nP\r\n###*\r\n#**#\r\n#**#\r\n###*\r\n#***\r\n#***\r\n\r\nS\r\n*###\r\n#***\r\n#***\r\n*##*\r\n***#\r\n###*\r\n\r\nZ\r\n####\r\n***#\r\n**#*\r\n*#**\r\n#***\r\n####";
        public static string TypeB = "6,10\r\n\r\nC\r\n*####*\r\n#****#\r\n#*****\r\n#*****\r\n#*****\r\n#*****\r\n#*****\r\n#*****\r\n#****#\r\n*####*\r\n\r\nE\r\n######\r\n#*****\r\n#*****\r\n#*****\r\n#####*\r\n#*****\r\n#*****\r\n#*****\r\n#*****\r\n######\r\n\r\nG\r\n*####*\r\n#****#\r\n#*****\r\n#*****\r\n#*****\r\n#**###\r\n#****#\r\n#****#\r\n#***##\r\n*###*#\r\n\r\nH\r\n#****#\r\n#****#\r\n#****#\r\n#****#\r\n######\r\n#****#\r\n#****#\r\n#****#\r\n#****#\r\n#****#\r\n\r\nL\r\n#*****\r\n#*****\r\n#*****\r\n#*****\r\n#*****\r\n#*****\r\n#*****\r\n#*****\r\n#*****\r\n######\r\n\r\nZ\r\n######\r\n*****#\r\n*****#\r\n****#*\r\n***#**\r\n**#***\r\n*#****\r\n#*****\r\n#*****\r\n######";

        public FontData(string fontFile, bool file)
        {
            definitions = new List<CharacterDefinition>();
            string[] fontData;
            if (file)
            {
                fontData = FileHandling.ReadFile(fontFile).Replace("\r", "").Split("\n\n");
            }
            else
            {
                fontData = fontFile.Replace("\r", "").Split("\n\n");
            }
            var header = fontData[0].Split(',');
            Width = int.Parse(header[0]);
            Height = int.Parse(header[1]);

            for (var i = 1 ; i < fontData.Length; i++)
            {
                var characterData = fontData[i].Split('\n');
                var character = characterData[0][0];
                var characterAscii = new bool[Width, Height];
                for (var y= 0; y < Height; y++)
                {
                    for (var x = 0; x < Width; x++)
                    {
                        characterAscii[x, y] = characterData[y + 1][x] == '#';
                    }
                }
                definitions.Add(new CharacterDefinition(characterAscii, character));
            }
        }
    }
}
