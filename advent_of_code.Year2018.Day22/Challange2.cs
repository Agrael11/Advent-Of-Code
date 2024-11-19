namespace advent_of_code.Year2018.Day22
{

    public static class Challange2
    {
        private static Map map = new Map(0,0,0);
        private static readonly (int X, int Y)[] Offsets = [(-1, 0), (+1, 0), (0, -1), (0, +1)];

        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n");

            var Depth = int.Parse(input[0].Split(' ')[1]);
            (var X, var Y) = (int.Parse(input[1].Split(' ')[1].Split(',')[0]), int.Parse(input[1].Split(' ')[1].Split(',')[1]));

            map = new Map(X, Y, Depth);

            var (_, cost) = Helpers.PathFinding.DoAStar(new State(0, 0, State.Item.Torch), IsEnd, GetNext, Heuristic, 1);

            return cost;
        }

        private static bool IsEnd(State state)
        {
            return (state.Position.X == map.Target.X && state.Position.Y == map.Target.Y && state.Equipment == State.Item.Torch);
        }

        private static IEnumerable<(State nextState, int Cost)> GetNext(State state)
        {
            foreach (var offset in Offsets)
            {
                var X = state.Position.X + offset.X;
                var Y = state.Position.Y + offset.Y;
                if (X < 0 || Y < 0) continue;

                if (Map.ValidEquipment(map.GetRegionTypeAt(X, Y), state.Equipment))
                {
                    yield return (new State(X, Y, state.Equipment), 1);
                }
            }
            var currentType = map.GetRegionTypeAt(state.Position.X, state.Position.Y);

            foreach (var item in Map.GetValidEquipments(currentType, state.Equipment))
            {
                yield return (new State(state.Position.X, state.Position.Y, item), 7);
            }
        }

        private static int Heuristic(State state)
        {
            return Math.Abs(map.Target.X - state.Position.X) + Math.Abs(map.Target.Y - state.Position.Y);
        }
    }
}