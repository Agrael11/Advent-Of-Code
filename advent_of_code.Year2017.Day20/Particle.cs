namespace advent_of_code.Year2017.Day20
{
    internal class Particle
    {
        Vector3l position;
        Vector3l velocity;
        Vector3l acceleration;
        public int Index { get; private set; }

        public Particle(string definition, int index)
        {
            Index = index;

            var data = definition.Split('<');
            var posData = data[1].Substring(0, data[1].LastIndexOf('>')).Split(',');
            var velData = data[2].Substring(0, data[2].LastIndexOf('>')).Split(',');
            var accData = data[3].Substring(0, data[3].LastIndexOf('>')).Split(',');
            position = new Vector3l(int.Parse(posData[0]), int.Parse(posData[1]), int.Parse(posData[2]));
            velocity = new Vector3l(int.Parse(velData[0]), int.Parse(velData[1]), int.Parse(velData[2]));
            acceleration = new Vector3l(int.Parse(accData[0]), int.Parse(accData[1]), int.Parse(accData[2]));
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
    }
}