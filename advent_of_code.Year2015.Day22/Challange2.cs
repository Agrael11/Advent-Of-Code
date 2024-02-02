using advent_of_code.Helpers;

namespace advent_of_code.Year2015.Day22
{
    public static class Challange2
    {
        private static short BossStartHP = 0;
        private static ushort BossStartDamage = 0;
        private static readonly short MyStartHP = 50;
        private static readonly ushort MyStartMana = 500;

        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');
            foreach (var line in input)
            {
                var definition = line.Split(':');
                if (definition[0] == "Hit Points") BossStartHP = short.Parse(definition[1].Trim());
                else if (definition[0] == "Damage") BossStartDamage = ushort.Parse(definition[1].Trim());
            }

            var startstate = new Gamestate(MyStartHP, MyStartMana, BossStartHP, BossStartDamage, true);
            var (endstate, cost) = PathFinding.DoDijkstra(startstate, IsEnd, GetNextStates);

            return cost;
        }

        private static bool IsEnd(Gamestate state)
        {
            return state.Wins() == true;
        }

        private static IEnumerable<(Gamestate, int)> GetNextStates(Gamestate state)
        {
            var states = new HashSet<(Gamestate, int)>();
            foreach (var magicName in MagicAvailable.Magics)
            {
                var magic = MagicAvailable.MagicStore[magicName];
                if (state.CanDoMagic(magicName))
                {
                    var newstate = new Gamestate(state, magic);
                    if (newstate.Wins() != false)
                    {
                        newstate = new Gamestate(newstate);
                        if (newstate.Wins() != false)
                        {
                            states.Add((newstate, magic.ManaCost));
                        }
                    }
                }
            }
            return states;
        }
    }
}