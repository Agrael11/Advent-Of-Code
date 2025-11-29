using System.Numerics;

namespace advent_of_code.Year2019.Day22
{
    public static class Challange2
    {
        private static readonly long targetCard = 2020;
        private static readonly long deckSize = 119315717514047L;
        private static readonly long shuffleCount = 101741582076661L;

        public static string DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");

            var instructions = input.Select(t => new Instruction(t));


            return GetCardAtPositionBeforeShuffling(targetCard, deckSize, instructions, shuffleCount).ToString();
        }

        /// <summary>
        /// So umm... how the fuck should i have known these?
        /// </summary>
        /// <param name="n"></param>
        /// <param name="mod"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private static BigInteger ModInverse(BigInteger n, BigInteger mod)
        {
            BigInteger t = 0;
            BigInteger newT = 1;
            var r = mod;
            var newR = n;

            while (newR != 0)
            {
                var q = r / newR;

                var tempT = t;
                t = newT;
                newT = tempT - q * newT;

                var tempR = r;
                r = newR;
                newR = tempR - q * newR;
            }

            if (r != 1)
                throw new Exception("No modular inverse exists.");

            if (t < 0)
                t += mod;

            return t % mod;
        }

        private static BigInteger GetCardAtPositionBeforeShuffling(BigInteger card, BigInteger size, IEnumerable<Instruction> instructions, long shulffles)
        {
            BigInteger a = 1;
            BigInteger b = 0;

            foreach (var instruction in instructions.Reverse())
            {
                switch (instruction.Type)
                {
                    case InstructionType.Cut:
                        b = (b + instruction.Value) % size;
                        break;
                    case InstructionType.DealWithIncrement:
                        a = (a * ModInverse(instruction.Value, size)) % size;
                        b = (b * ModInverse(instruction.Value, size)) % size;
                        break;
                    case InstructionType.Deal:
                        a = -a;
                        b = ((size - 1) - b) % size;
                        break;
                }

                while (a < 0) a += size;
                while (b < 0) b += size;

            }

            var shuffler = shulffles;

            BigInteger finalA = 1;
            BigInteger finalB = 0;

            while (shuffler > 0)
            {
                if ((shuffler & 1) == 1)
                {
                    var an = (finalA * a) % size;
                    var bn = (finalA * b + finalB) % size;

                    finalA = an;
                    finalB = bn;
                }

                var a2 = (a * a) % size;
                var b2 = (a * b + b) % size;
                a = a2;
                b = b2;

                shuffler >>= 1;
            }

            var position = (finalA * card + finalB) % size;

            while (position < 0)
            {
                position += size;
            }

            return position;
        }
    }
}