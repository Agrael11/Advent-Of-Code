using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code.Year2018.Day15
{
    internal class Game
    {
        private readonly (int X, int Y)[] ReadingOrder = [(0, -1), (-1, 0), (1, 0), (0, 1)];
        private readonly Dictionary<(int X, int Y), Entity> Entities;
        private readonly List<(int X, int Y)> Walls;
        private int ElfLosses = 0;

        public Game(Dictionary<(int X, int Y), Entity> entities, List<(int X, int Y)> walls, int ElfForce, int GoblinForce)
        {
            Entities = new Dictionary<(int X, int Y), Entity>();
            Walls = walls;

            foreach ((_, var entity) in entities)
            {
                var newEntity = new Entity(entity, (entity.EntityType == 'E' ? ElfForce : GoblinForce));
                Entities.Add(newEntity.Position, newEntity);
            }

        }

        public long Play()
        {
            var rounds = 0L;
            for (; ; rounds++)
            {
                if (!Round()) break;
            }

            return rounds * Entities.Sum(entity => entity.Value.HealthPoints);
        }

        public (bool win, long score) PlayForElves()
        {
            var rounds = 0L;
            for (; ; rounds++)
            {
                if (!Round()) break;
                if (ElfLosses > 0) return (false, -1);
            }

            var Score = (rounds) * Entities.Sum(entity => entity.Value.HealthPoints);
            return (true, Score);
        }

        private bool Round()
        {
            var order = Entities.Select(entityDef => entityDef.Key).
            OrderBy(position => position.Y).ThenBy(position => position.X).
            ToList<(int X, int Y)>();

            foreach (var turn in order)
            {
                //Check if still exists
                if (!Entities.ContainsKey(turn))
                {
                    continue;
                }

                var entity = Entities[turn];

                var attackResult = TryAttack(entity);
                if (attackResult == 1)
                {
                    continue;
                }
                if (attackResult == 2)
                {
                    return false;
                }

                if (!Move(entity.Position.X, entity.Position.Y))
                {
                    continue;
                }

                attackResult = TryAttack(entity);
                if (attackResult == 2)
                {
                    return false;
                }
            }

            return true;
        }

        private int TryAttack(Entity entity)
        {
            //Check if anyone to attack
            if (!Entities.Any(e => e.Value.EntityType != entity.EntityType)) return 2;
            
            //Find Target
            var (targetX, targetY) = FindEnemyAround(entity.Position.X, entity.Position.Y, (entity.EntityType == 'E') ? 'G' : 'E');

            //If target exists, attack target
            if ((targetY != -1) && (targetX != -1))
            {
                Attack(targetX, targetY, entity);
                return 1;
            }

            return 0;
        }

        private (int X, int Y) FindEnemyAround(int thisX, int thisY, char TargetType)
        {
            (int X, int Y) target = (-1, -1);
            var targetHealth = int.MaxValue;

            foreach (var (offsetX, offsetY) in ReadingOrder)
            {
                var offsetPosition = (thisX + offsetX, thisY + offsetY);
                if (Entities.TryGetValue(offsetPosition, out var entity) && entity.EntityType == TargetType)
                {
                    if (entity.HealthPoints < targetHealth)
                    {
                        targetHealth = entity.HealthPoints;
                        target = offsetPosition;
                    }
                }
            }

            return target;
        }

        private void Attack(int x, int y, Entity attacker)
        {
            var target = Entities[(x, y)];
            target.HealthPoints -= attacker.AttackStrength;
            if (target.HealthPoints <= 0)
            {
                if (target.EntityType == 'E')
                {
                    ElfLosses++;
                }
                Entities.Remove((x, y));
            }
        }

        private bool Move(int x, int y)
        {
            var entity = Entities[(x, y)];
            var targetType = (entity.EntityType == 'E') ? 'G' : 'E';

            var (resultPath, resultLength) = Helpers.PathFinding.DoBFSWithPath<((int X, int Y) position, char targetType)>(((x, y), targetType), IsEnd, GetNext);
            if (resultLength > 1 && resultPath is not null)
            {
                entity.Position = resultPath[1].position;
                Entities.Remove((x, y));
                Entities.Add((entity.Position.X, entity.Position.Y), entity);
                return true;
            }

            return false;
        }

        private bool IsEnd(((int X, int Y) position, char targetType) current)
        {
            return (Entities.ContainsKey(current.position) && (Entities[current.position].EntityType == current.targetType));
        }

        private IEnumerable<((int X, int Y) position, char targetType)> GetNext(((int X, int Y) position, char targetType) info)
        {
            foreach ((var offsetX, var offsetY) in ReadingOrder)
            {
                var newPosition = (info.position.X + offsetX, info.position.Y + offsetY);

                if (Walls.Contains(newPosition))
                {
                    continue;
                }

                if (Entities.TryGetValue(newPosition, out var collider) && collider.EntityType != info.targetType)
                {
                    continue;
                }

                yield return (newPosition, info.targetType);
            }
        }
    }
}