using System.Security.Cryptography;
using System.Text;

namespace advent_of_code.Year2015.Day04
{
    public static class Challange2
    {
        public static int DoChallange(string inputData)
        {
            inputData = inputData.Replace("\r", "").TrimEnd('\n');

            var magicNumber = 0;

            while (!HexStartsWithSixZeroes(GetMD5(inputData + magicNumber)))
            {
                magicNumber++;
            }
            return magicNumber;
        }

        private static bool HexStartsWithSixZeroes(byte[] bytes)
        {
            return (bytes[0] == 0 && bytes[1] == 0 && bytes[2] == 0);
        }

        public static byte[] GetMD5(string input)
        {
            var bytes = Encoding.ASCII.GetBytes(input);
            return MD5.HashData(bytes);
        }
    }
}
