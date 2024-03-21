using System.Security.Cryptography;
using System.Text;

namespace advent_of_code.Year2016.Day05
{
    public static class Challange1
    {
        public static string DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n');
            var password = "";
            var hashSalt = 0;
            while (password.Length < 8)
            {
                var md5 = MD5.HashData(Encoding.ASCII.GetBytes(input + hashSalt));
                hashSalt++;
                if (md5[0] == 0 && md5[1] == 0 && md5[2] < 10)
                {
                    password += (char)('0' + md5[2]);
                }
                else if (md5[0] == 0 && md5[1] == 0 && md5[2] < 16)
                {
                    password += (char)('a' + (md5[2] - 10));
                }
            }
            return password;
        }
    }
}