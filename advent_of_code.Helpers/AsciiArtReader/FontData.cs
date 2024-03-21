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

        public FontData(string fontFile)
        {
            definitions = new List<CharacterDefinition>();
            var fontData = FileHandling.ReadFile(fontFile).Replace("\r","").Split("\n\n");
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
