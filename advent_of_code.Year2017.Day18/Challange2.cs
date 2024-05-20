namespace advent_of_code.Year2017.Day18
{
    public static class Challange2
    {
        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            var sentMessages = 0;

            var duetCPU1 = new CorrectDuetCPU(0);
            var duetCPU2 = new CorrectDuetCPU(1);

            duetCPU1.SendMessageEvent += duetCPU2.GetMessage;
            duetCPU2.SendMessageEvent += (message) => { duetCPU1.GetMessage(message); sentMessages++; }; 

            var running = true;
            while (running)
            {
                running = duetCPU1.DoStep(input);
                running |= duetCPU2.DoStep(input);
            }

            return sentMessages;
        }
    }
}