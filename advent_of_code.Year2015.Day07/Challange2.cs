namespace advent_of_code.Year2015.Day07
{
    public static class Challange2
    {
        private static readonly Dictionary<string, string> commands = new Dictionary<string, string>();
        private static readonly Dictionary<string, ushort> memory = new Dictionary<string, ushort>();

        public static ushort DoChallange(string inputData)
        {

            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            foreach (var inputLine in input)
            {
                var data = inputLine.Split("->");
                commands.Add(data[1].Trim(), data[0].Trim());
            }

            var b = GetCable("a");
            memory.Clear();
            memory.Add("b", b);

            var cableA = GetCable("a");

            commands.Clear();
            memory.Clear();

            return cableA;
        }

        private static ushort GetResult(string operation)
        {
            var data = operation.Split(' ');
            return data.Length == 1
                ? GetCable(data[0])
                : data.Length == 2
                ? Not(data[1])
                : data[1] switch
                {
                    "AND" => And(data[0], data[2]),
                    "OR" => Or(data[0], data[2]),
                    "LSHIFT" => LShift(data[0], data[2]),
                    "RSHIFT" => RShift(data[0], data[2]),
                    _ => throw new NotImplementedException(),
                };
        }

        private static ushort GetCable(string str)
        {
            if (memory.TryGetValue(str, out var memoryValue))
            {
                return memoryValue;
            }

            if (ushort.TryParse(str, out var ushortValue))
            {
                memory.Add(str, ushortValue);
                return ushortValue;
            }

            if (commands.TryGetValue(str, out var stringValue))
            {
                var result = GetResult(stringValue);
                memory.Add(str, result);
                return result;
            }

            return 0;
        }

        private static ushort RShift(string number1, string number2)
        {
            return (ushort)((GetCable(number1) >> GetCable(number2)) & 0xFFFF);
        }

        private static ushort LShift(string number1, string number2)
        {
            return (ushort)((GetCable(number1) << GetCable(number2)) & 0xFFFF);
        }

        private static ushort Or(string number1, string number2)
        {
            return (ushort)(GetCable(number1) | GetCable(number2));
        }

        private static ushort And(string number1, string number2)
        {
            return (ushort)(GetCable(number1) & GetCable(number2));
        }

        private static ushort Not(string number)
        {
            return (ushort)(~GetCable(number) & 0xFFFF);
        }
    }
}
