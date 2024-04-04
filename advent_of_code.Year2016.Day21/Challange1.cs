namespace advent_of_code.Year2016.Day21
{
    public static class Challange1
    {
        public static string DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');
            var mainString = new Passtring("abcdefgh");

            foreach (var instruction in input)
            {
                var instructionData = instruction.Split(' ');
                switch (instructionData[0])
                {
                    case "swap":
                        switch (instructionData[1])
                        {
                            case "position":
                                mainString.Swap(int.Parse(instructionData[2]), int.Parse(instructionData[5]));
                                break;
                            case "letter":
                                mainString.Swap(instructionData[2][0], instructionData[5][0]);
                                break;
                            default:
                                throw new Exception("Unknown instruction");
                        }
                        break;
                    case "rotate":
                        switch (instructionData[1])
                        {
                            case "left":
                                mainString.RotateLeft(int.Parse(instructionData[2]));
                                break;
                            case "right":
                                mainString.RotateRight(int.Parse(instructionData[2]));
                                break;
                            case "based":
                                mainString.RotateIndex(instructionData[6][0]);
                                break;
                            default:
                                throw new Exception("Unknown instruction");
                        }
                        break;
                    case "reverse":
                        mainString.Reverse(int.Parse(instructionData[2]), int.Parse(instructionData[4]));
                        break;
                    case "move":
                        mainString.Move(int.Parse(instructionData[2]), int.Parse(instructionData[5]));
                        break;
                    default:
                        throw new Exception("Unknown instruction");
                }
            }

            return mainString.ToString();
        }
    }
}