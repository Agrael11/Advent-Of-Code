namespace advent_of_code.Year2025.Day10
{
    public static class Challange1
    {
        public static long DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split("\n")
                .Select(t=>new Machine(t)).ToList();

            var result = 0;

            //Does BFS to find minimum steps of each machine. We then add it to total steps count;
            foreach (var machine in input)
            {
                result += BFS(machine);
            }

            return result;
        }

        /// <summary>
        /// You saw BFS a million times. no need to explain again.
        /// </summary>
        /// <param name="machine"></param>
        /// <returns></returns>
        private static int BFS(Machine machine)
        {
            var queue = new Queue<LightState>();
            var visited = new HashSet<int>(); //state can be retrieved from buttonState, so only that is required to store
            
            queue.Enqueue(new LightState(0,0,0));

            while (queue.Count > 0)
            {
                var lightState = queue.Dequeue();
                
                if (!visited.Add(lightState.Buttons))
                {
                    continue;
                }

                if (machine.IsLightTarget(lightState.Lights))
                {
                    return lightState.Steps;
                }

                foreach (var nextState in machine.GetNextLightStates(lightState))
                {
                    queue.Enqueue(nextState);
                }
            }

            return -1;
        }
    }
}