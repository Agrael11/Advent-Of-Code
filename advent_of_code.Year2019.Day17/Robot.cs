namespace advent_of_code.Year2019.Day17
{
    internal struct Robot (int x,int y, Robot.Direction direction)
    {
        public enum Direction { Left, Up, Right, Down, Error };

        public int X = x;
        public int Y = y;
        public Direction CurrentDirection = direction;

        public static char CharacterFromDirection(Direction direction)
        {
            return direction switch
            {
                Robot.Direction.Up => '^',
                Robot.Direction.Down => 'v',
                Robot.Direction.Left => '<',
                Robot.Direction.Right => '>',
                Robot.Direction.Error => 'X',
                _ => throw new Exception($"Unexpected direction {Enum.GetName(direction)}"),
            };
        }

        public static Direction DirectionFromCharacter(char character)
        {
            return character switch
            {
                '^' => Robot.Direction.Up,
                'v' => Robot.Direction.Down,
                '<' => Robot.Direction.Left,
                '>' => Robot.Direction.Right,
                'X' => Robot.Direction.Error,
                _ => throw new Exception($"Unexpected character {character}"),
            };
        }
    }
}
