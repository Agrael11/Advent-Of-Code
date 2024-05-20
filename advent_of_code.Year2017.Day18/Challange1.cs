namespace advent_of_code.Year2017.Day18
{
    public static class Challange1
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            var resultSound = 0L;

            var duetCPU = new DuetCPU();
            duetCPU.RecoveryEvent += (sound) => resultSound = sound;
            duetCPU.Run(input);

            return resultSound;
        }
    }
}