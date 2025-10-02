namespace advent_of_code.Year2019.Day11
{
    internal class Robot
    {
        public enum Color
        {
            Black = 0,
            White = 1
        }

        public enum Movement
        {
            TurnLeft = 0,
            TurnRight = 1
        }

        public enum Direction
        {
            Up = 0,
            Right = 1,
            Down = 2,
            Left = 3
        }

        private (int x, int y) position = (0, 0);
        private Direction direction = 0; // 0=up, 1=right, 2=down, 3=left
        public HashSet<(int x, int y)> PaintedPanels { get; private set; } = new();
        private readonly HashSet<(int x, int y)> visitedPanels = new();

        public Robot(Color startingColor = Color.Black)
        {
            if (startingColor == Color.White) PaintedPanels.Add((position.x, position.y));
        }
        
        public void Turn(Movement movement)
        {
            if (movement == Movement.TurnLeft)
            {
                TurnLeft();
            }
            else if (movement == Movement.TurnRight)
            {
                TurnRight();
            }
            else
            {
                throw new Exception($"Unknown movement command: {movement}");
            }
        }

        public void TurnLeft()
        {
            direction = (Direction)(((int)direction + 3) % 4);
        }

        public void TurnRight()
        {
            direction = (Direction)(((int)direction + 1) % 4);
        }

        public void MoveForward()
        {
            switch (direction)
            {
                case Direction.Up: position.y -= 1; break;
                case Direction.Right: position.x += 1; break;
                case Direction.Down: position.y += 1; break;
                case Direction.Left: position.x -= 1; break;
                default: throw new Exception($"Unknown direction: {direction}");
            }
        }

        public void Paint(Color color)
        {
            switch (color)
            {
                case Color.Black:
                    PaintedPanels.Remove(position);
                    break;
                case Color.White:
                    PaintedPanels.Add(position);
                    break;
                default:
                    throw new Exception($"Unknown color: {color}");
            }
            visitedPanels.Add(position);
        }

        public Color GetCurrentPanelColor()
        {
            return PaintedPanels.Contains(position) ? Color.White : Color.Black;
        }

        public int GetVisitedPanelCount()
        {
            return visitedPanels.Count;
        }
    }
}
