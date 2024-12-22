namespace advent_of_code.Year2024.Day22
{
    public static class Challange2
    {
        private static readonly Dictionary<uint, uint> PricesPerSequence = new Dictionary<uint, uint>(2000);
        
        public static long DoChallange(string inputData)
        {
            PricesPerSequence.Clear();

            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n").Select(int.Parse).ToArray();

            foreach (var number in input)
            {
                var monkey = CalculateSecretManyTimes(number, 2001); //Off by one screw you! Hmm.. I wonder why though
                foreach (var sequence in monkey.SequencePrices)
                {
                    //We are accumulating prices of each sequence. faster than testing monkeys later
                    if (!PricesPerSequence.TryAdd(sequence.sequence, sequence.price))
                    {
                        //And yet this is still pretty slow
                        PricesPerSequence[sequence.sequence] += sequence.price;
                    }
                }
            }

            return PricesPerSequence.MaxBy((pair) => pair.Value).Value;
        }

        //Repeat 2001 times (still wonder why that is needed for me...
        private static Monkey CalculateSecretManyTimes(long secret, int times)
        {
            var monkey = new Monkey(secret);

            for (var i = 0; i < times; i++)
            {
                monkey.CalculateNextSecret();
                //But this time we also update the sequence
                monkey.UpdateSequence();
            }

            return monkey;
        }
    }
}