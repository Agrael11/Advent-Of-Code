using System.Numerics;

namespace advent_of_code.Year2017.Day20
{
    internal class Particle
    {
        private readonly Vector3i position;
        private readonly Vector3i velocity;
        private readonly Vector3i acceleration;
        public int Index { get; private set; }

        public Particle(string definition, int index)
        {
            Index = index;

            var data = definition.Split('<');
            var posData = data[1][..data[1].LastIndexOf('>')].Split(',');
            var velData = data[2][..data[2].LastIndexOf('>')].Split(',');
            var accData = data[3][..data[3].LastIndexOf('>')].Split(',');
            position = new Vector3i(int.Parse(posData[0]), int.Parse(posData[1]), int.Parse(posData[2]));
            velocity = new Vector3i(int.Parse(velData[0]), int.Parse(velData[1]), int.Parse(velData[2]));
            acceleration = new Vector3i(int.Parse(accData[0]), int.Parse(accData[1]), int.Parse(accData[2]));
        }

        public long GetManhattanAcceleration()
        {
            return acceleration.GetDistanceFromZero();
        }

        public long GetManhattanDistance()
        {
            return position.GetDistanceFromZero();
        }

        public long GetManhattanVelocity()
        {
            return velocity.GetDistanceFromZero();
        }

        public long SimulateStep()
        {
            var prevDist = GetManhattanDistance();
            velocity.Add(acceleration);
            position.Add(velocity);
            return prevDist - GetManhattanDistance();
        }

        public bool Collides(Particle other)
        {
            return position.X == other.position.X && position.Y == other.position.Y && position.Z == other.position.Z;
        }

        public int CollideTime(Particle other)
        {
            (var tx1, var tx2) = Quadratic(position.X, other.position.X, velocity.X, other.velocity.X, acceleration.X, other.acceleration.X);
            if (tx1 >= 0)
            {
                if (QuadraCheck(position.X, other.position.X, velocity.X, other.velocity.X, acceleration.X, other.acceleration.X, tx1) &&
                    QuadraCheck(position.Y, other.position.Y, velocity.Y, other.velocity.Y, acceleration.Y, other.acceleration.Y, tx1) &&
                    QuadraCheck(position.Z, other.position.Z, velocity.Z, other.velocity.Z, acceleration.Z, other.acceleration.Z, tx1))
                {
                    return tx1;
                }
            }
            if (tx2 >= 0)
            {
                if (QuadraCheck(position.X, other.position.X, velocity.X, other.velocity.X, acceleration.X, other.acceleration.X, tx2) &&
                    QuadraCheck(position.Y, other.position.Y, velocity.Y, other.velocity.Y, acceleration.Y, other.acceleration.Y, tx2) &&
                    QuadraCheck(position.Z, other.position.Z, velocity.Z, other.velocity.Z, acceleration.Z, other.acceleration.Z, tx2))
                {
                    return tx2;
                }
            }

            return -1;
        }

        private static (int t1, int t2) Quadratic(int p1, int p2, int v1, int v2, int a1, int a2)
        {
            var a = a1 - a2;
            var b = 2 * (v1 - v2) + (a1 - a2);
            var c = 2 * (p1 - p2);

            if (a == 0)
            {
                if (b == 0)
                {
                    return (-1, -1);
                }

                var t = -c / b;
                if (t > 0 && QuadraCheck(p1, p2, v1, v2, a1, a2, t))
                {
                    return (t, -1);
                }

                return (-1, -1);
            }

            var discriminant = b * b - 4 * a * c;
            if (discriminant < 0)
            {
                return (-1,-1);
            }

            var sqrtDiscriminant = Math.Sqrt(discriminant);

            var t1 = (-b + sqrtDiscriminant) / (2*a);
            var t2 = (-b - sqrtDiscriminant) / (2*a);

            return ((int)t1, (int)t2);
        }

        private static bool QuadraCheck(int p1, int p2, int v1, int v2, int a1, int a2, int t)
        {
            return (p1 + v1 * t + a1 * t * (t + 1) / 2) == (p2 + v2 * t + a2 * t * (t + 1) / 2);
        }
    }
}