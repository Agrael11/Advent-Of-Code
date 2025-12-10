namespace advent_of_code.Year2025.Day10
{
    internal class Machine
    {
        public readonly List<int[]> Buttons = new List<int[]>();
        public int ButtonCount => Buttons.Count;
        private readonly int Target = 0;
        public readonly List<int> Joltages = new List<int>();
        public int JoltagesCount => Joltages.Count;


        /// <summary>
        /// Parses the defintion from string into Machine
        /// </summary>
        /// <param name="definition"></param>
        /// <exception cref="Exception"></exception>
        public Machine(string definition)
        {
            //We split by spaces (simple yay)
            var subDefintiions = definition.Split(' ');
            foreach (var sub in subDefintiions)
            {
                var cleanedSub = sub[1..^1];
                //If we start with [ it is definition of final LEDs
                if (sub.StartsWith('['))
                {
                    for (var i = 0; i < cleanedSub.Length; i++)
                    {
                        if (cleanedSub[i] == '#')
                        {
                            Target |= 1 << i;
                        }
                    }
                }
                //If we start with ( it is definition of buttons
                else if (sub.StartsWith('('))
                {
                    Buttons.Add(cleanedSub.Split(',').Select(int.Parse).ToArray());
                }
                //And if we start with { it is defintion of joltages
                else if (sub.StartsWith('{'))
                {
                    Joltages = cleanedSub.Split(',').Select(int.Parse).ToList();
                }
                else
                {
                    throw new Exception($"Unexpected symbol {sub[0]}");
                }
            }
        }

        /// <summary>
        /// Checks if light state matches
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public bool IsLightTarget(int state)
        {
            return state == Target;
        }

        /// <summary>
        /// Gets all next possible light states
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public IEnumerable<LightState> GetNextLightStates(LightState state)
        {
            for (var i = 0; i < Buttons.Count; i++)
            {
                if (TryPushLightButton(i, state, out var nextState))
                {
                    yield return nextState;
                }
            }
        }

        /// <summary>
        /// Tries to "push button" - aka checks if button is not already bushed and gives new state.
        /// </summary>
        /// <param name="buttonID"></param>
        /// <param name="currentState"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private bool TryPushLightButton(int buttonID, LightState currentState, out LightState result)
        {
            result = new LightState(currentState.Lights, currentState.Buttons, currentState.Steps + 1);
            var buttonMask = 1 << buttonID;
            if ((currentState.Buttons & buttonMask) != 0)
            {
                return false;
            }
            
            result.Buttons |= buttonMask;
            foreach (var light in Buttons[buttonID])
            {
                var lightMask = 1 << light;
                result.Lights ^= lightMask;
            }

            return true;
        }
    }
}
