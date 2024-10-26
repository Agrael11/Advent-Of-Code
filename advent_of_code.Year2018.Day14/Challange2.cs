namespace advent_of_code.Year2018.Day14
{
    public static class Challange2
    {
        public static long DoChallange(string inputData)
        {
            var target = inputData.Replace("\r", "").TrimEnd('\n');
            var byteTarget = new byte[target.Length];
            for (var i = 0; i < target.Length; i++)
            {
                byteTarget[i] = (byte)int.Parse(target[i].ToString());
            }

            Common.Reset();

            var circularArray = new byte[target.Length];
            circularArray[0] = 3;
            circularArray[1] = 7;
            var circular = 2;
            
            var index = Common.RecipesCount - target.Length;

            while (!CompareArray(circular, circularArray, byteTarget, target.Length))
            {
                index++;
                var newDigits = Common.AddRound();
                if (newDigits >= 10)
                {
                    AddToCircularArray(ref circular, ref circularArray, target.Length, (byte)(newDigits / 10));
                    if (CompareArray(circular, circularArray, byteTarget, target.Length)) break;
                    index++;
                    newDigits %= 10;
                }
                AddToCircularArray(ref circular, ref circularArray, target.Length, (byte)(newDigits));
            }
            
            return index;
        }

        private static void AddToCircularArray(ref int index, ref byte[] circularArray, int length, byte digit)
        {
            circularArray[index] = digit;
            index = (index + 1) % length;
        }

        private static bool CompareArray(int index, byte[] circularArray, byte[] normalArray, int length)
        {
            for (var i = 0; i < length; i++)
            {
                var cIndex = (index + i) % length;
                if (normalArray[i] != circularArray[cIndex]) 
                    return false;
            }
            return true;
        }
    }
}