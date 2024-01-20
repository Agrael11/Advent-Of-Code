using advent_of_code.Helpers;

namespace advent_of_code.Year2015.Day13
{
    public static class Challange2
    {
        private static Dictionary<(string person1, string person2), int> moods = [];

        public static int DoChallange(string inputData)
        {
            string[] input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            HashSet<string> people = [];
            moods = [];
            people.Add("me");

            foreach (string line in input)
            {
                string[] lineInfo = line.TrimEnd('.').Split(' ');
                string person1 = lineInfo[0];
                string person2 = lineInfo[^1];
                int moodChange = int.Parse(lineInfo[3]) * ((lineInfo[2] == "gain") ? 1 : -1);
                people.Add(person1);
                moods.Add((person1, person2), moodChange);
                moods.TryAdd((person1, "me"), 0);
                moods.TryAdd(("me", person1), 0);
            }

            int bestMood = int.MinValue;
            foreach (List<string> arrangement in people.Permutate())
            {
                int mood = CalculateTotalMood(arrangement);
                bestMood = int.Max(mood, bestMood);
            }

            return bestMood;
        }

        static int CalculateTotalMood(List<string> arrangement)
        {
            int mood = 0;

            mood += moods[(arrangement[0], arrangement[^1])] + moods[(arrangement[0], arrangement[1])];

            for (int i = 1; i < arrangement.Count - 1; i++)
            {
                mood += moods[(arrangement[i], arrangement[i - 1])] + moods[(arrangement[i], arrangement[i + 1])];
            }

            mood += moods[(arrangement[^1], arrangement[^2])] + moods[(arrangement[^1], arrangement[0])];

            return mood;
        }
    }
}
