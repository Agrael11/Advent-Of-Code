namespace advent_of_code.Year2018.Day04
{
    internal class Common
    {
        internal static List<Guard> GetGuards(string[] input)
        {
            var actionLog = new List<LogItem>();

            foreach (var item in input)
            {
                actionLog.Add(new LogItem(item));
            }

            actionLog = actionLog.OrderBy(t => t.Time).ToList();

            var guards = new Dictionary<int, Guard>();
            var currentGuard = -1;
            var startTime = -1;
            foreach (var log in actionLog)
            {
                switch (log.Action)
                {
                    case LogItem.ActionType.GuardChange:
                        currentGuard = log.GuardID;
                        break;
                    case LogItem.ActionType.FallsAsleep:
                        startTime = log.Time.Minute;
                        break;
                    case LogItem.ActionType.WakeUp:
                        if (!guards.TryGetValue(currentGuard, out var guard))
                        {
                            guard = new Guard(currentGuard);
                        }
                        guard.AddSleep(startTime, log.Time.Minute);
                        guards[currentGuard] = guard;
                        break;
                }
            }

            return guards.Values.ToList();
        }
    }
}