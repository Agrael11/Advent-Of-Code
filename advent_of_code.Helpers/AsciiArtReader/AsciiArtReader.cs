using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code.Helpers.AsciiArtReader
{
    public static class Reader
    {
        public enum FontTypes { Type_A, Type_B };
        public static (bool FoundAll, string Text) ReadText(string input, int spacing, FontTypes fontType, char enabled = '#', char lineSplit = '\n')
        {
            var lines = input.Split(lineSplit);
            var data = new Grid<bool>(lines[0].Length,lines.Length);

            for (var y = 0; y < lines.Length; y++)
            {
                var line = lines[y];
                for (var x = 0; x < line.Length; x++) 
                {
                    var c = line[x];
                    data[x, y] = c == enabled;
                }
            }

            return ReadText(data, spacing, fontType);
        }

        public static (bool FoundAll, string Text) ReadText(Grid<bool> input, int spacing, FontTypes fontType = FontTypes.Type_A)
        {
            var font = fontType switch
            {
                FontTypes.Type_A => new FontData(FontData.TypeA, false),
                FontTypes.Type_B => new FontData(FontData.TypeB, false),
                _ => throw new Exception("Wrong font Type"),
            };

            var result = "";
            var foundAll = true;

            var pos = 0;
            while (pos < input.Width)
            {
                var found = false;
                foreach (var charDef in font.definitions)
                {
                    if (charDef.IsCharacter(input, pos, 0))
                    {
                        found = true;
                        result += charDef.Character;
                        break;
                    }
                }
                if (!found)
                {
                    result += "?";
                }
                foundAll |= found;
                pos += spacing + font.Width;
            }
            return (foundAll, result);
        }
    }
}
