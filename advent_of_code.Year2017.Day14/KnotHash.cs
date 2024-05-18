using advent_of_code.Year2017.Day10;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code.Year2017.Day14
{
    public static class KnotHash
    {
        public enum HashFormat { Binary, Hex};

        public static byte[] GetDenseHash(string input)
        {
            return GetList(input).GetDenseHash();
        }

        public static string GetDenseHash(string input, HashFormat format)
        {
            var hash = "";

            foreach (var b in GetList(input).GetDenseHash())
            {
                hash += b.ToString((format == HashFormat.Binary)?"B8":"X2");
            }

            return hash.ToLower();
        }

        private static CircularList GetList(string input)
        {
            var newInput = "";

            foreach (var c in input)
            {
                newInput += (int)c + ",";
            }

            input = newInput + "17,31,73,47,23";

            var list = new CircularList(Enumerable.Range(0, 256).ToList());

            var skip = 0;
            var index = 0;

            for (var loops = 0; loops < 64; loops++)
            {
                foreach (var length in input.Split(','))
                {
                    var lng = int.Parse(length.Trim());
                    list.Reverse(index, lng);
                    index += lng + skip;
                    index = list.FixPointer(index);
                    skip++;
                }
            }

            return list;
        }
    }
}
