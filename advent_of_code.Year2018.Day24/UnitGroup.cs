using System.Text;

namespace advent_of_code.Year2018.Day24
{
    internal class UnitGroup
    {
        public static int Bonus { get; set; } = 0;

        public enum UnitTypes { Infection, Immunity};
        public UnitTypes UnitType { get; init; }
        public int HPPerUnit { get; init; }
        public int UnitsCount { get; set; }
        public string AttackType { get; init; }
        private readonly int _attackStrength;
        public int AttackStrength => _attackStrength + ((UnitType == UnitTypes.Immunity) ? Bonus : 0);
        public int Initiative { get; init; }
        public int EffectiveAttack => AttackStrength * UnitsCount;
        public readonly List<string> Weaknesses = new List<string>();
        public readonly List<string> Immunities = new List<string>();

        public UnitGroup(UnitGroup group)
        {
            UnitType = group.UnitType;
            HPPerUnit = group.HPPerUnit;
            UnitsCount = group.UnitsCount;
            AttackType = group.AttackType;
            _attackStrength = group._attackStrength;
            Initiative = group.Initiative;
            foreach (var s in group.Weaknesses) Weaknesses.Add(s);
            foreach (var s in group.Immunities) Immunities.Add(s);
        }

        public UnitGroup(string definition, UnitTypes unitType)
        {
            var lineSplit = definition.Split(' ');

            UnitType = unitType;
            UnitsCount = int.Parse(lineSplit[Array.IndexOf(lineSplit, "units") - 1]);
            HPPerUnit = int.Parse(lineSplit[Array.IndexOf(lineSplit, "hit") - 1]);
            AttackType = lineSplit[Array.IndexOf(lineSplit, "damage") - 1];
            _attackStrength = int.Parse(lineSplit[Array.IndexOf(lineSplit, "damage") - 2]);
            Initiative = int.Parse(lineSplit[Array.IndexOf(lineSplit, "initiative") + 1]);

            if (definition.Contains('('))
            {
                var parenthesisPart = definition[(definition.IndexOf('(') + 1)..definition.IndexOf(')')].Split(';');


                foreach (var part in parenthesisPart.Select(t => t.Trim()))
                {
                    var options = part[(part.Split(',')[0].LastIndexOf(' ') + 1)..].Split(',');
                    if (part.StartsWith("immune"))
                    {
                        foreach (var option in options)
                        {
                            Immunities.Add(option.Trim());
                        }
                    }
                    else if (part.StartsWith("weak"))
                    {
                        foreach (var option in options)
                        {
                            Weaknesses.Add(option.Trim());
                        }
                    }
                    else
                    {
                        throw new Exception($"What kind of modifier is {part}?");
                    }
                }
            }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append($"{UnitsCount} units; {HPPerUnit} HP per Unit; {AttackStrength} {AttackType} Attack, {EffectiveAttack} Effective; {Initiative} Initiative; ");
            if (Weaknesses.Count > 0) builder.Append($"Weaknesses: {string.Join(", ", Weaknesses)}; ");
            if (Immunities.Count > 0) builder.Append($"Immunities: {string.Join(", ", Immunities)}; ");
            builder.Remove(builder.Length - 2, 2);
            return builder.ToString();
        }

        public int CalculateDamageTo(UnitGroup defender)
        {
            return CalculateDamage(this, defender);
        }
        public static int CalculateDamage(UnitGroup attacker, UnitGroup defender)
        {
            return attacker.EffectiveAttack *
                (defender.Weaknesses.Contains(attacker.AttackType) ? 2 : 1) *
                (defender.Immunities.Contains(attacker.AttackType) ? 0 : 1);
        }
    }
}
