﻿using System;

namespace advent_of_code.Year2016.Day10
{
    public static class Challange1
    {
        private static readonly int TargetLow = 17;
        private static readonly int TargetHigh = 61;

        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");

            var bots = new Dictionary<int, Bot>();
            var botsToParse = new List<int>();

            foreach (var line in input)
            {
                var linedata = line.Split(' ');
                if (linedata[0] == "value")
                {
                    var botId = int.Parse(linedata[5]);
                    var botValue = int.Parse(linedata[1]);
                    bots.TryGetValue(botId, out var bot);
                    bot ??= new Bot();
                    bot.AddValue(botValue);
                    bots[botId] = bot;

                    if (bot.ReadyToSend()) botsToParse.Add(botId);
                }
                else if (linedata[0] == "bot")
                {
                    var botId = int.Parse(linedata[1]);
                    var type1 = (linedata[5] == "bot") ? Bot.TargetType.Bot : Bot.TargetType.Output;
                    var target1 = int.Parse(linedata[6]);
                    var type2 = (linedata[10] == "bot") ? Bot.TargetType.Bot : Bot.TargetType.Output;
                    var target2 = int.Parse(linedata[11]);

                    bots.TryGetValue(botId, out var bot);
                    bot ??= new Bot();
                    bot.LowTarget = (type1, target1);
                    bot.HighTarget = (type2, target2);
                    bots[botId] = bot;

                    if (bot.ReadyToSend()) botsToParse.Add(botId);
                }
                else
                {
                    throw new Exception($"Invalid Insturction {line}");
                }
            }

            while (botsToParse.Count > 0)
            {
                var botKey = botsToParse[0];
                botsToParse.RemoveAt(0);

                var bot = bots[botKey];
                if (bot.HighValue == TargetHigh && bot.LowValue == TargetLow) return botKey;

                if (bot.HighTarget.targetType == Bot.TargetType.Bot)
                {
                    var bot2 = bots[bot.HighTarget.targetId];
                    bot2.AddValue(bot.HighValue);

                    if (bot2.ReadyToSend()) botsToParse.Add(bot.HighTarget.targetId);
                }
                if (bot.LowTarget.targetType == Bot.TargetType.Bot)
                {
                    var bot2 = bots[bot.LowTarget.targetId];
                    bot2.AddValue(bot.LowValue);

                    if (bot2.ReadyToSend()) botsToParse.Add(bot.LowTarget.targetId);
                }

            }

            throw new Exception("out of bots");
        }
    }
}