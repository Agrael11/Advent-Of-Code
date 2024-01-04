namespace advent_of_code.Year2015.Day07
{
    public static class Challange2
    {
        private static Dictionary<string, string> commands = [];
        private static Dictionary<string, ushort> memory = [];

        public static ushort DoChallange(string inputData)
        {
            commands.Clear();
            memory.Clear();

            string[] input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            foreach (string inputLine in input)
            {
                string[] data = inputLine.Split("->");
                commands.Add(data[1].Trim(), data[0].Trim());
            }

            ushort b = GetCable("a");
            memory.Clear();
            memory.Add("b", b);
            return GetCable("a");
        }

        private static ushort GetResult(string operation)
        {
            string[] data = operation.Split(' ');
            if (data.Length == 1)
            {
                return GetCable(data[0]);
            }

            if (data.Length == 2)
            {
                return Not(data[1]);
            }

            return data[1] switch
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
            if (memory.TryGetValue(str, out ushort memoryValue))
            {
                return memoryValue;
            }

            if (ushort.TryParse(str, out ushort ushortValue))
            {
                memory.Add(str, ushortValue);
                return ushortValue;
            }

            if (commands.TryGetValue(str, out string? stringValue))
            {
                ushort result = GetResult(stringValue);
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
