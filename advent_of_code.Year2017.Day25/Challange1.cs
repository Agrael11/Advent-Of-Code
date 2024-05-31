using System.Security.Cryptography.X509Certificates;

namespace advent_of_code.Year2017.Day25
{
    public static class Challange1
    {
        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');
            var states = new Dictionary<string, State>();
            var currentState = input[0].Split(' ')[^1].TrimEnd('.');
            var targetSteps = int.Parse(input[1].Split(' ')[^2].TrimEnd('.'));

            for (var i = 3; i < input.Length; i += 10)
            {
                states.Add(input[i].Split(' ')[^1].TrimEnd(':'), new State(input[i..]));
            }

            var cursor = 0;
            var tape = new Dictionary<int, bool>();
            var checksum = 0;

            for (var i = 0; i < targetSteps; i++)
            {
                var state = false;
                if (tape.TryGetValue(cursor, out var value)) state = value;
                var instruction = state ? states[currentState].Instruction1 : states[currentState].Instruction0;
                tape[cursor] = instruction.NewValue;
                if (tape[cursor] && !state) checksum++;
                if (!tape[cursor] && state) checksum--;
                cursor += instruction.Direction;
                currentState = instruction.TargetState;
            }

            return checksum;
        }
    }
}