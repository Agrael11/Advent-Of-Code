using Visualizers;

namespace advent_of_code.Year2024.Day17
{
    public static class Challange2
    {
        private static List<int> Program = new List<int>();
        private static long Power = 0;
        private static long XOR1 = 0;
        private static long XOR2 = 0;

        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n").Select(t => t.Split(' ').Last()).ToArray();

            Program = input[4].Split(',').Select(int.Parse).ToList();
            var expectedResult = input[4].Replace(",", "");

            AOConsole.Write(Disassembler.Disasemble(Program));

            //Extract some variables that i expect to be different per input
            XOR1 = Program[3];
            XOR2 = Program[7];
            Power = (long)Math.Pow(2, Program[9]);

            return FindNext(0);
        }

        private static long FindNext(long regA)
        {
            //Now this one is hell :D ... We search for correct A, slowly moving towards end. It's basically BFS
            
            var queue = new Queue<(long, int)>();
            queue.Enqueue((regA, Program.Count-1));

            while (queue.Count > 0)
            {
                (var currentA, var currentIndex) = queue.Dequeue(); //We get last known "A", and index of address in program
                var target = Program[currentIndex]; //we get the value at address

                //We check ever possible "A" for this level.
                //That is current (A * "Power") + (0..Power-1) - every "x" for A=x/Power
                for (var aOff = 0; aOff < Power; aOff++)
                {
                    var a = currentA * Power + aOff;

                    if (a == 0) continue; //If it is A, and yet we haven't found the correct result, it is not correct "A"

                    //Math directly translated from program.
                    var b = (a % 8) ^ XOR1;
                    var c = a / (long)Math.Pow(2, b);
                    b = (b ^ XOR2) ^ c;
                    
                    if (b % 8 != target) continue; //If this is not correct target, we are not at correct "A"

                    if (currentIndex == 0) return a; //If we are at last point, this is THE Final Correct "A"

                    queue.Enqueue((a, currentIndex - 1)); //Otherwise this was possible correct "A" for this iteration,
                                                          //and we enqueue it to check on next target
                }
            }

            return -1;
        }
    }
}