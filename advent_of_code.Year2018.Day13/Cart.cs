using System.ComponentModel;
using System.Transactions;

namespace advent_of_code.Year2018.Day13
{
    internal class Cart
    {
        public (int X, int Y) Position;
        private Common.Direction Direction;

        private int MoveState = 0;

        public Cart(int X, int Y, Common.Direction direction)
        {
            Direction = direction;
            Position = (X, Y);
        }

        private void IncrementMove()
        {
            MoveState++;
            MoveState %= 3;
            ;
        }

        public void Move()
        {
            switch (Direction)
            {
                case Common.Direction.Up:
                    Position.Y--;
                    break;
                case Common.Direction.Down:
                    Position.Y++;
                    break;
                case Common.Direction.Left:
                    Position.X--;
                    break;
                case Common.Direction.Right:
                    Position.X++;
                    break;
            }
        }

        public void ChangeDirection(char currentTile)
        {
            switch (currentTile)
            {
                case '/':
                    TurnSlash();
                    break;
                case '\\':
                    TurnReverseSlash();
                    break;
                case '+':
                    Cross();
                    break;
            }
        }

        private void TurnSlash()
        {
            switch (Direction)
            {
                case Common.Direction.Up:
                    Direction = Common.Direction.Right;
                    break;
                case Common.Direction.Down:
                    Direction = Common.Direction.Left;
                    break;
                case Common.Direction.Left:
                    Direction = Common.Direction.Down;
                    break;
                case Common.Direction.Right:
                    Direction = Common.Direction.Up;
                    break;
            }
        }

        private void TurnReverseSlash()
        {
            switch (Direction)
            {
                case Common.Direction.Up:
                    Direction = Common.Direction.Left;
                    break;
                case Common.Direction.Down:
                    Direction = Common.Direction.Right;
                    break;
                case Common.Direction.Left:
                    Direction = Common.Direction.Up;
                    break;
                case Common.Direction.Right:
                    Direction = Common.Direction.Down;
                    break;
            }
        }

        private void Cross()
        {
            var next = (int)Direction + 3 + MoveState;
            Direction = (Common.Direction)(next % 4);
            IncrementMove();
        }

        public override string ToString()
        {
            var direction = '>';
            switch (Direction)
            {
                case Common.Direction.Up:
                    direction = '^';
                    break;
                case Common.Direction.Down:
                    direction = 'v';
                    break;
                case Common.Direction.Left:
                    direction = '<';
                    break;
            }
            return $"{direction} ({Position.X}-{Position.Y})";
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Cart otherCart) return false;
            return otherCart.Direction == Direction && otherCart.Position.X == Position.X && otherCart.Position.Y == Position.Y && otherCart.MoveState == MoveState;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Position, Direction, MoveState);
        }
    }
}
