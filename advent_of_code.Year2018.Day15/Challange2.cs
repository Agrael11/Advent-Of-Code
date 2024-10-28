namespace advent_of_code.Year2018.Day15
{
    public static class Challange2
    {
        private static readonly int AttackStart = 3;
        private static int ElfAttack = AttackStart;
        private static readonly int HealthStart = 200;

        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            var entities = new Dictionary<(int X, int Y), Entity>();
            var walls = new List<(int X, int Y)>();
            ElfAttack = AttackStart;

            for (var y = 0; y < input.Length; y++)
            {
                for (var x = 0; x < input[y].Length; x++)
                {
                    var eType = input[y][x];

                    if (eType == '#')
                    {
                        walls.Add((x, y));
                        continue;
                    }

                    if (eType != 'G' && eType != 'E')
                    {
                        continue;
                    }

                    entities.Add((x, y), new Entity(eType, (x, y), ElfAttack, HealthStart));
                }
            }

            var lastWin = -1L;

            for (;;)
            {
                var game = new Game(entities, walls, ElfAttack, AttackStart);
                var (win, score) = game.PlayForElves();
                if (win)
                {
                    lastWin = score;
                    ElfAttack--;
                    continue;
                }
                if (lastWin == -1)
                {
                    ElfAttack += 5;
                    continue;
                }
                return lastWin;
            }
        }
    }
}