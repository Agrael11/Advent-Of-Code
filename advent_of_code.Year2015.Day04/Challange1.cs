using System.Security.Cryptography;
using System.Text;

namespace advent_of_code.Year2015.Day04
{
    public static class Challange1
    {
        public static int DoChallange(string inputData)
        {
            inputData = inputData.Replace("\r", "").TrimEnd('\n');

            int magicNumber = 0;

            while (!HexStartsWithFiveZeroes(GetMD5(inputData + magicNumber)))
            {
                magicNumber++;
            }

            return magicNumber;
        }

        private static bool HexStartsWithFiveZeroes(byte[] bytes)
        {
            return (bytes[0] == 0 && bytes[1] == 0 && bytes[2] < 0x10);
        }

        public static byte[] GetMD5(string input)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(input);
            return MD5.HashData(bytes);
        }
    }
}
