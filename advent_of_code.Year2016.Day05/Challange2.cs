using System.Security.Cryptography;
using System.Text;

namespace advent_of_code.Year2016.Day05
{
    public static class Challange2
    {
        public static string DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n');
            var positions = new HashSet<int>();
            var password = new char[8];
            var hashSalt = 0;
            while (positions.Count < 8)
            {
                var md5 = MD5.HashData(Encoding.ASCII.GetBytes(input + hashSalt));
                hashSalt++;
                if (md5[0] == 0 && md5[1] == 0 && md5[2] < 8 && !positions.Contains(md5[2]))
                {
                    positions.Add(md5[2]);
                    var c = md5[3] / 16;
                    if (c < 10)
                        password[md5[2]] = (char)('0' + c);
                    else
                        password[md5[2]] = (char)('a' + (c - 10));
                }
            }
            return new String(password);
        }
    }
}