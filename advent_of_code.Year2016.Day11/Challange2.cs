namespace advent_of_code.Year2016.Day11
{
    public static class Challange2
    {
        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").Replace(",", "").Replace(".", "").TrimEnd('\n').Split("\n");

            var baseState = new State();

            for (var i = 0; i < 4; i++)
            {
                var data = input[i].Split(' ');
                for (var j = 0; j < data.Length; j++)
                {
                    if (data[j] == "generator")
                    {
                        var item = new Item(Item.ItemTypes.Generator, data[j - 1]);
                        baseState.AddItem(i, item);
                    }
                    if (data[j] == "microchip")
                    {
                        var item = new Item(Item.ItemTypes.Microchip, data[j - 1].Split('-')[0]);
                        baseState.AddItem(i, item);
                    }
                }
            }

            baseState.AddItem(0, new Item(Item.ItemTypes.Generator, "elerium"));
            baseState.AddItem(0, new Item(Item.ItemTypes.Microchip, "elerium"));
            baseState.AddItem(0, new Item(Item.ItemTypes.Generator, "dilithium"));
            baseState.AddItem(0, new Item(Item.ItemTypes.Microchip, "dilithium"));

            return Helpers.PathFinding.DoAStar(baseState, IsEnd, GetNextStates, Heuristic, 1).cost;
        }

        public static int Heuristic(State state)
        {
            var score = 0;
            for (var i = 0; i < 4; i++)
            {
                score += state.Floors[i].Items.Count * i;
            }
            return score;
        }

        public static bool IsEnd(State state)
        {
            return (state.Floors[0].Items.Count == 0 && state.Floors[1].Items.Count == 0 && state.Floors[2].Items.Count == 0);
        }

        public static IEnumerable<(State nextState, int nextPrice)> GetNextStates(State state)
        {
            foreach (var nextState in state.GeneratePossibleStates())
            {
                yield return (nextState, 1);
            }
        }
    }
}