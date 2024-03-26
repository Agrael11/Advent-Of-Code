using advent_of_code.Helpers;

namespace advent_of_code.Year2015.Day13
{
    public static class Challange2
    {
        private readonly static Dictionary<(string person1, string person2), int> moods = 
            new Dictionary<(string person1, string person2), int>();

        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            var people = new HashSet<string>();
            people.Add("me");

            foreach (var line in input)
            {
                var lineInfo = line.TrimEnd('.').Split(' ');
                var person1 = lineInfo[0];
                var person2 = lineInfo[^1];
                var moodChange = int.Parse(lineInfo[3]) * ((lineInfo[2] == "gain") ? 1 : -1);
                people.Add(person1);
                moods.Add((person1, person2), moodChange);
                moods.TryAdd((person1, "me"), 0);
                moods.TryAdd(("me", person1), 0);
            }

            var bestMood = int.MinValue;
            foreach (var arrangement in people.Permutate())
            {
                var mood = CalculateTotalMood(arrangement);
                bestMood = int.Max(mood, bestMood);
            }

            moods.Clear();

            return bestMood;
        }

        private static int CalculateTotalMood(List<string> arrangement)
        {
            var mood = 0;

            mood += moods[(arrangement[0], arrangement[^1])] + moods[(arrangement[0], arrangement[1])];

            for (var i = 1; i < arrangement.Count - 1; i++)
            {
                mood += moods[(arrangement[i], arrangement[i - 1])] + moods[(arrangement[i], arrangement[i + 1])];
            }

            mood += moods[(arrangement[^1], arrangement[^2])] + moods[(arrangement[^1], arrangement[0])];

            return mood;
        }
    }
}
