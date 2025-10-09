using IntMachine;
using Visualizers;

namespace advent_of_code.Year2019.Day17
{
    /// <summary>
    /// Okay this one will require some explaining
    /// </summary>
    public static class Challange2
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").Replace("\n", "").Split(',')
                .Select(long.Parse).ToArray();

            var machine = new Machine([1, 2, 3, 4, 5, 6, 7, 8, 9, 99])
            {
                RAM = (Memory)input
            };

            //We set the machine to contain "2" flag at 0th point.
            //Canonically that means force wake-up
            machine.RAM.TryWrite(0, 2);
            machine.Run();

            //Here we just get map, figure out moves to get through it and
            //Compress the resulting string into 4 smaller instruction-strings
            (var scaffoldMap, var robot) = Common.ParseData(machine);
            var moves = GetMoves(scaffoldMap, robot);
            var (Main, A, B, C) = CompressString(moves);

            //Technically possible to send all at once and let it run,
            //but this makes it more explicit
            SendString(machine, Main);
            SendString(machine, A);
            SendString(machine, B);
            SendString(machine, C);
            SendString(machine, "n");

            var (_, result) = ReadOutput(machine);

            if (result is null) throw new Exception("Unexpected null result");

            return result.Value;
        }

        /// <summary>
        /// Compresses String
        /// </summary>
        /// <param name="moves"></param>
        /// <returns></returns>
        private static (string Main, string A, string B, string C) CompressString(List<string> moves)
        {
            //Finds three patterns and replaces them with their function name.
            var A = FindRepeatedPattern(moves);
            ReplacePattern(ref moves, A, "A");
            var B = FindRepeatedPattern(moves);
            ReplacePattern(ref moves, B, "B");
            var C = FindRepeatedPattern(moves);
            ReplacePattern(ref moves, C, "C");

            return
                (string.Join(',', moves),
                string.Join(',', A),
                string.Join(',', B),
                string.Join(',', C));
        }

        /// <summary>
        /// Replaces Pattern with "replacement"
        /// </summary>
        /// <param name="moves"></param>
        /// <param name="pattern"></param>
        /// <param name="replacement"></param>
        private static void ReplacePattern(ref List<string> moves, List<string> pattern, string replacement)
        {
            //We search through moveset to find out pattern.
            var patternStart = 0;
            for (var i = 0; i < moves.Count; i++)
            {
                var patternLength = i - patternStart;

                //If we found pattern (length of correct moves is equal to length of pattern
                //We remove the area from the moveset and insert replacement letter in it's place
                if (patternLength == pattern.Count)
                {
                    i = patternStart;
                    moves.RemoveRange(i, patternLength);
                    moves.Insert(i, replacement);
                    patternStart++;
                    continue;
                }

                //If we find non-match - we check if this is start of pattern and if not we
                //continue from next letter
                if (moves[i] != pattern[patternLength])
                {
                    patternStart = i;
                    if (moves[i] != pattern[0])
                    {
                        patternStart++;
                    }
                }
            }
            
            //Let's not forget the one extra that  can be found at end of set.
            //Yes - I had off by one
            if (moves.Count - patternStart == pattern.Count)
            {
                moves.RemoveRange(patternStart, moves.Count - patternStart);
                moves.Add(replacement);
            }
        }

        /// <summary>
        /// Finds longest repeatign pattern form start of string
        /// </summary>
        /// <param name="moves"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private static List<string> FindRepeatedPattern(List<string> moves)
        {
            var foundStart = false;
            var patternMoves = new List<string>();
            var offset = 0;

            for (var i = offset; i < moves.Count; i++)
            {
                //If we find other pattern's name, it either means we found out of this pattern
                //OR that we did not even found the start of it, if other pattern was discovered
                //before
                if (moves[i] == "A" || moves[i] == "B" || moves[i] == "C")
                {
                    if (foundStart)
                    {
                        return patternMoves;
                    }
                    else
                    {
                        offset++;
                        continue;
                    }
                }
                
                foundStart = true; //First non "A","B" or "C" is our start

                patternMoves.Add(moves[i]); //We add current to our moveset

                //First fail condition - if we are longer thann 20, we remove last move
                //And return the known pattern
                if (string.Join(',',patternMoves).Length > 20) 
                {
                    patternMoves.RemoveAt(patternMoves.Count - 1);
                    return patternMoves;
                }

                //Job of this mess is to find if this patterns appears again in moveset
                //We search from next point, until we find at least one repeat of it ...
                var patternLength = 0;
                for (var j = i+1; j < moves.Count; j++)
                {
                    if (moves[j] != moves[patternLength+offset])
                    {
                        patternLength = 0;
                    }
                    else
                    {
                        patternLength++;
                        if (patternLength > (i-offset))
                        {
                            break;
                        }
                    }
                }

                //...And if we did not - means we just made non-repeating pattern
                //Remove the latest addition and return it.
                if (patternLength < (i-offset))
                {
                    patternMoves.RemoveAt(patternMoves.Count - 1);
                    return patternMoves;
                }
            }
            throw new Exception("Pattern not found");
        }

        /// <summary>
        /// Gets moves required to explore the path.
        /// Assumes the best path is always "forward" on junctions
        /// </summary>
        /// <param name="scaffolds">Scaffolding hashset</param>
        /// <param name="bot">Robot (starting position)</param>
        /// <returns></returns>
        private static List<string> GetMoves(HashSet<(int x, int y)> scaffolds, Robot bot)
        {
            var moves = new List<string>();
            while (true)
            {
                //We try to move forward first
                var fwd = MoveForward(bot.X, bot.Y, bot.CurrentDirection);

                if (scaffolds.Contains(fwd))
                {
                    bot.X = fwd.x;
                    bot.Y = fwd.y;

                    //This adds "1" if we did not have number after move before,
                    //otherwise it increments it. Not efficient, but works.
                    if ((moves.Count == 0) || !(int.TryParse(moves[^1], out var num)))
                    {
                        moves.Add("1");
                        continue;
                    }
                    else
                    {
                        moves[^1] = (num + 1).ToString();
                        continue;
                    }
                }

                //Next we check if we can tur left
                var nextDir = (Robot.Direction)((int)(bot.CurrentDirection + 3) % 4);
                if (scaffolds.Contains(MoveForward(bot.X, bot.Y, nextDir)))
                {
                    bot.CurrentDirection = nextDir;
                    moves.Add("L");

                    continue;
                }

                //And last if we can turn right!
                nextDir = (Robot.Direction)((int)(bot.CurrentDirection + 1) % 4);
                if (scaffolds.Contains(MoveForward(bot.X, bot.Y, nextDir)))
                {
                    bot.CurrentDirection = nextDir;
                    moves.Add("R");
                    continue;
                }

                //If neither is possible - we are at end of path (only remaining way is backwards)
                break;
            }

            return moves;
        }

        /// <summary>
        /// Moves forward in direction
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private static (int x, int y) MoveForward(int x, int y, Robot.Direction direction)
        {
            return direction switch
            {
                Robot.Direction.Left => (x - 1, y),
                Robot.Direction.Right => (x + 1, y),
                Robot.Direction.Up => (x, y - 1),
                Robot.Direction.Down => (x, y + 1),
                _ => throw new NotImplementedException(),
            };
        }

        /// <summary>
        /// Reads Output of machine
        /// </summary>
        /// <param name="machine">Expects machine with available output</param>
        /// <returns>Text version of output and number that is larger than 255</returns>
        private static (string text, long? result) ReadOutput(Machine machine)
        {
            var queue = new List<char>();
            long? result = null;
            while (machine.OutputAvailable())
            {
                if (!machine.TryPopOutput(out var outp) || outp == null)
                {
                    break;
                }
                if (outp.Value <= 255)
                {
                    queue.Add((char)outp.Value);
                }
                else
                {
                    result = outp.Value;
                    queue.AddRange(outp.Value.ToString());
                }
            }

            var text = new string(queue.ToArray());
            if (AOConsole.Enabled) { AOConsole.Write("> " + text); }

            return (text, result);
        }

        /// <summary>
        /// Sends the string to machine
        /// </summary>
        /// <param name="machine">Expects machine that already ran</param>
        /// <param name="data">String to send</param>
        private static void SendString(Machine machine, string data)
        {
            _ = ReadOutput(machine);
            foreach (var instruction in data)
            {
                machine.PushInput(instruction);
            }
            machine.PushInput('\n');
            if (AOConsole.Enabled) { AOConsole.WriteLine("< " + data); };
            machine.Run();
        }
    }
}