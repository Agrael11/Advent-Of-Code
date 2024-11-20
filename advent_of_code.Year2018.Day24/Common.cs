using System.Text;

namespace advent_of_code.Year2018.Day24
{
    internal static class Common
    {
        internal static (Dictionary<int, UnitGroup> ImmuneSystem, Dictionary<int, UnitGroup> Infection) ParseInput(string[] input)
        {
            var immuneSystem = new Dictionary<int, UnitGroup>();
            var infection = new Dictionary<int, UnitGroup>();
            UnitGroup.UnitTypes? unitType = null;
            foreach (var line in input)
            {
                if (string.IsNullOrEmpty(line))
                {
                    unitType = null;
                    continue;
                }

                if (unitType is null)
                {
                    if (line.Contains("Immune")) unitType = UnitGroup.UnitTypes.Immunity;
                    else if (line.Contains("Infection")) unitType = UnitGroup.UnitTypes.Infection;
                    else throw new Exception($"What is {line}?");

                    continue;
                }

                var group = new UnitGroup(line, unitType.Value);

                if (unitType == UnitGroup.UnitTypes.Immunity) immuneSystem.Add(immuneSystem.Count, group);
                else if (unitType == UnitGroup.UnitTypes.Infection) infection.Add(infection.Count, group);
            }

            return (immuneSystem, infection);
        }

        internal static (UnitGroup.UnitTypes? winner, int ResultForce) Simulate(Dictionary<int, UnitGroup> immunityGroups, Dictionary<int, UnitGroup> infectionGroups, int BonusForce = 0)
        {
            var immunityGroups_Clone = new Dictionary<int, UnitGroup>();
            var infectionGroups_Clone = new Dictionary<int, UnitGroup>();
            
            foreach (var pair in immunityGroups)
            {
                immunityGroups_Clone.Add(pair.Key, new UnitGroup(pair.Value));
            }
            foreach (var pair in infectionGroups)
            {
                infectionGroups_Clone.Add(pair.Key, new UnitGroup(pair.Value));
            }
            
            var last = -1;
            UnitGroup.Bonus = BonusForce;
            
            while (immunityGroups_Clone.Count > 0 && infectionGroups_Clone.Count > 0)
            {
                Round(immunityGroups_Clone, infectionGroups_Clone);

                var result = HashCode.Combine(immunityGroups_Clone.Sum(g => g.Value.UnitsCount), infectionGroups_Clone.Sum(g => g.Value.UnitsCount));
                if (last == result) break;
                last = result;
            }

            if (infectionGroups_Clone.Count > 0 && immunityGroups_Clone.Count == 0) return (UnitGroup.UnitTypes.Infection, infectionGroups_Clone.Sum(g => g.Value.UnitsCount));
            if (immunityGroups_Clone.Count > 0 && infectionGroups_Clone.Count == 0) return (UnitGroup.UnitTypes.Immunity, immunityGroups_Clone.Sum(g => g.Value.UnitsCount));
            return (null, 0);
        }

        private static void Round(Dictionary<int, UnitGroup> ImmuneGroups, Dictionary<int, UnitGroup> InfectionGroups)
        {
            var immuneToInfectionTargets = GetTargets(ImmuneGroups, InfectionGroups);
            var infectionToImmuneTargets = GetTargets(InfectionGroups, ImmuneGroups);

            var allUnits = ImmuneGroups.Union(InfectionGroups);

            foreach (var attacker in allUnits.OrderByDescending(g => g.Value.Initiative))
            {
                if (attacker.Value.UnitsCount <= 0) continue;

                //Attacks opposite team
                switch (attacker.Value.UnitType)
                {
                    case UnitGroup.UnitTypes.Infection:
                        {
                            if (!infectionToImmuneTargets.TryGetValue(attacker.Key, out var targetIndex))
                                continue;
                            var targetUnit = ImmuneGroups[targetIndex];
                            var hit = attacker.Value.CalculateDamageTo(targetUnit);
                            var kills = hit / targetUnit.HPPerUnit;
                            targetUnit.UnitsCount -= kills;
                            break;
                        }
                    case UnitGroup.UnitTypes.Immunity:
                        {
                            if (!immuneToInfectionTargets.TryGetValue(attacker.Key, out var targetIndex))
                                continue;
                            var targetUnit = InfectionGroups[targetIndex];
                            var hit = attacker.Value.CalculateDamageTo(targetUnit);
                            var kills = hit / targetUnit.HPPerUnit;
                            targetUnit.UnitsCount -= kills;
                            break;
                        }
                }
            }

            //Removes "dead" groups
            foreach (var key in ImmuneGroups.Where(g => g.Value.UnitsCount <= 0).Select(g => g.Key))
            {
                ImmuneGroups.Remove(key);
            }
            foreach (var key in InfectionGroups.Where(g => g.Value.UnitsCount <= 0).Select(g => g.Key))
            {
                InfectionGroups.Remove(key);
            }
        }

        private static Dictionary<int, int> GetTargets(Dictionary<int, UnitGroup> Attackers, Dictionary<int, UnitGroup> Defenders)
        {
            var targetsByAttackers = new Dictionary<int, int>();

            //Get all attackers, in order - first strongest, if tie - higher initiative
            foreach (var group in Attackers.OrderByDescending(g => g.Value.EffectiveAttack).
                                            ThenByDescending(g => g.Value.Initiative))
            {

                //Find Possible Targets, if not selected already, first by highest possible damage, then strongest
                //if still tie - higher initiative
                var availableTargets = Defenders.Where(g => !targetsByAttackers.ContainsValue(g.Key)).
                                                 OrderByDescending(g => UnitGroup.CalculateDamage(group.Value, g.Value)).
                                                 ThenByDescending(g => g.Value.EffectiveAttack).
                                                 ThenByDescending(g => g.Value.Initiative);

                //If any target is, select the first one (top of list)
                if (availableTargets.Any())
                {
                    var first = availableTargets.First();
                    if (UnitGroup.CalculateDamage(group.Value, first.Value) > 0)
                    {
                        targetsByAttackers.Add(group.Key, first.Key);
                    }
                }
            }

            return targetsByAttackers;
        }
    }
}
