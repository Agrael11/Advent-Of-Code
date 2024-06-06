using System.Text;

namespace advent_of_code.Year2018.Day02
{
    public static class Challange2
    {
        public static string DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            for (var index1 = 0; index1 < input.Length; index1++)
            {
                var box1 = input[index1];
                for (var index2 = index1+1; index2 < input.Length; index2++)
                {
                    var box2 = input[index2];
                    if (box1 == box2) continue;

                    var result = FindDifferent(box1, box2);
                    if (result != -1) return box1.Remove(result, 1);
                }
            }

            return "";
        }

        public static int FindDifferent(string str1, string str2)
        {
            var diff = -1;
            for (var indexC = 0; indexC < str1.Length; indexC++)
            {
                if (str1[indexC] != str2[indexC])
                {
                    if (diff != -1) return -1;
                    diff = indexC;
                }
            }
            if (diff != -1) return diff;
            return -1;
        }
    }
}