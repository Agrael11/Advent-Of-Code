namespace advent_of_code.Year2018.Day22
{
    public class State (int x, int y, State.Item equip)
    {
        public enum Item { None, Torch, ClimbingGear}
        public (int X, int Y) Position = (x,y);
        public Item Equipment = equip;

        public override int GetHashCode()
        {
            return HashCode.Combine(Position, Equipment);
        }

        public override bool Equals(object? obj)
        {
            if (obj is not State state) return false;
            return state.Position.X == Position.X && state.Position.Y == Position.Y && state.Equipment == Equipment;
        }

        public static bool operator == (State state1, State state2)
        {
            return state1.Equals(state2);
        }

        public static bool operator !=(State state1, State state2)
        {
            return !state1.Equals(state2);
        }
    }
}