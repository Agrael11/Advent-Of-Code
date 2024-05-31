using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code.Year2017.Day21
{
    internal class RulesKeeper
    {
        private readonly Dictionary<string, string> rules = new Dictionary<string, string>();

        public string this[string key] => rules[key];

        public void Clear()
        {
            rules.Clear();
        }

        public void ParseRule(string rule)
        {
            var ruleData = rule.Split(" => ");
            var rotRule = Rotate90(ruleData[0]);
            rules.TryAdd(ruleData[0], ruleData[1]);
            rules.TryAdd(FlipHorizontal(ruleData[0]), ruleData[1]);
            rules.TryAdd(FlipVertical(ruleData[0]), ruleData[1]);
            rules.TryAdd(FlipVertical(FlipHorizontal(ruleData[0])), ruleData[1]);
            rules.TryAdd(rotRule, ruleData[1]);
            rules.TryAdd(FlipHorizontal(rotRule), ruleData[1]);
            rules.TryAdd(FlipVertical(rotRule), ruleData[1]);
            rules.TryAdd(FlipVertical(FlipHorizontal(rotRule)), ruleData[1]);
        }

        private static string Rotate90(string input)
        {
            var split = input.Split('/');
            var output = "";

            for (var x = split[0].Length - 1; x >= 0; x--)
            {
                for (var y = 0; y < split.Length; y++)
                {
                    output += split[y][x];
                }
                output += "/";
            }

            return output.TrimEnd('/');
        }

        internal static string FlipHorizontal(string input)
        {
            var split = input.Split('/');
            var output = "";

            for (var y = 0; y < split.Length; y++)
            {
                for (var x = split[0].Length - 1; x >= 0; x--)
                {
                    output += split[y][x];
                }
                output += "/";
            }

            return output.TrimEnd('/');
        }

        internal static string FlipVertical(string input)
        {
            var split = input.Split('/');
            var output = "";

            for (var y = split.Length - 1; y >= 0; y--)
            {
                for (var x = 0; x < split[0].Length; x++)
                {
                    output += split[y][x];
                }
                output += "/";
            }

            return output.TrimEnd('/');
        }
    }
}
