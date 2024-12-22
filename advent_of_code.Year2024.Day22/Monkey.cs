using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace advent_of_code.Year2024.Day22
{
    internal class Monkey(long secret)
    {
        public long Secret { get; set; } = secret;
        private readonly HashSet<uint> sequences = new HashSet<uint>(2000);
        public readonly (uint sequence, uint price)[] SequencePrices = new (uint, uint)[2001];
        private uint sequence = 0;
        private byte sequenceLength = 0;
        private byte currentPrice = 0;
        private int lastSequence = 0;

        public void CalculateNextSecret()
        {
            Secret ^= Secret * 64;
            Secret %= 16777216;
            Secret ^= Secret / 32;
            Secret %= 16777216;
            Secret ^= Secret * 2048;
            Secret %= 16777216;
        }

        //Gets new sequence based on curent price and new price (secret number)
        public void UpdateSequence()
        {
            var newPrice = (byte)(Secret % 10);
            var difference = (byte)(currentPrice - newPrice);
         
            if (sequenceLength < 4) sequenceLength++;
            
            //Can't be better than this i suppose
            sequence <<= 8;
            sequence |= difference;
            
            currentPrice = newPrice;
            
            //HashSet slow!
            if (sequenceLength == 4 && sequences.Add(sequence))
            {
                SequencePrices[lastSequence] = (sequence, currentPrice);
                lastSequence++;
            }
        }
    }
}
