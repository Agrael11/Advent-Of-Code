namespace advent_of_code.Year2018.Day15
{
    internal class Entity
    {
        public char EntityType { get; init; }
        public (int X, int Y) Position { get; set; }
        public int AttackStrength { get; init; }
        public int HealthPoints { get; set; }

        public Entity(Entity entity, int newAttackStrength)
        {
            EntityType = entity.EntityType;
            Position = (entity.Position.X, entity.Position.Y);
            AttackStrength = newAttackStrength;
            HealthPoints = entity.HealthPoints;
        }

        public Entity(char entityType, (int X, int Y) position, int attackStrength, int healthPoints)
        {
            EntityType = entityType;
            Position = position;
            AttackStrength = attackStrength;
            HealthPoints = healthPoints;
        }
    }
}
