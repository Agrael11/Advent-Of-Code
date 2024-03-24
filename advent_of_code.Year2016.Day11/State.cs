using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code.Year2016.Day11
{
    public class State
    {
        public FloorState[] Floors = new FloorState[4];
        private int _elevatorPosition = 0;
        public int ElevatorPosition
        {
            get => _elevatorPosition;
            set
            {
                _elevatorPosition = value;
                _pairs = null;
            }
        }

        public Dictionary<string, int> generators = new Dictionary<string, int>();
        public Dictionary<string, int> microchips = new Dictionary<string, int>();

        private List<int>? _pairs = null;
        public List<int> Pairs
        {
            get
            {
                if (_pairs is not null) return _pairs;

                _pairs = new List<int>();
                foreach (var generator in generators)
                {
                    _pairs.Add(generator.Value * 10 + microchips[generator.Key]);
                }
                _pairs = _pairs.OrderBy(t => t).ToList();

                return _pairs;
            }
        }

        public State()
        {
            Floors[0] = new FloorState();
            Floors[1] = new FloorState();
            Floors[2] = new FloorState();
            Floors[3] = new FloorState();
        }

        public IEnumerable<State> GeneratePossibleStates()
        {
            var canMoveUp = false;
            var canMoveDown = false;
            foreach (var item in Floors[ElevatorPosition].Items)
            {
                foreach (var item2 in Floors[ElevatorPosition].Items)
                {
                    if (Equals(item, item2)) continue;

                    var stateUp = MoveItems(item, item2, ElevatorPosition + 1);
                    if (stateUp != null)
                    {
                        yield return stateUp;
                        canMoveUp = true;
                    }
                }
            }
            foreach (var item in Floors[ElevatorPosition].Items)
            {
                var stateDown = MoveItems(item, null, ElevatorPosition - 1);
                if (stateDown != null)
                {
                    yield return stateDown;
                    canMoveDown = true;
                }
                
                
                if (!canMoveUp)
                {
                    var stateUp = MoveItems(item, null, ElevatorPosition + 1);
                    if (stateUp != null) yield return stateUp;
                }
            }

            if (!canMoveDown)
            {
                foreach (var item in Floors[ElevatorPosition].Items)
                {
                    foreach (var item2 in Floors[ElevatorPosition].Items)
                    {
                        if (Equals(item, item2)) continue;

                        var stateDown = MoveItems(item, item2, ElevatorPosition - 1);
                        if (stateDown != null) yield return stateDown;
                    }
                }
            }

            yield break;
        }

        public State? MoveItems(Item item1, Item? item2, int targetFloor)
        {
            if (targetFloor < 0 || targetFloor > 3) return null;
            if (targetFloor < ElevatorPosition)
            {
                var itemsLower = 0;
                for (var i= 0; i <= targetFloor; i++)
                {
                    itemsLower += Floors[i].Items.Count;
                }
                if (itemsLower == 0) return null;
            }

            var state = new State
            {
                ElevatorPosition = targetFloor
            };

            foreach (var generator in generators)
            {
                state.generators.Add(generator.Key, generator.Value);
            }
            foreach (var microchip in microchips)
            {
                state.microchips.Add(microchip.Key, microchip.Value);
            }
            
            if (item1.ItemType == Item.ItemTypes.Microchip)
            {
                state.microchips.Remove(item1.ItemSubtype);
            }
            else
            {
                state.generators.Remove(item1.ItemSubtype);
            }

            if (item2 is not null)
            {
                if (item2.ItemType == Item.ItemTypes.Microchip)
                {
                    state.microchips.Remove(item2.ItemSubtype);
                }
                else
                {
                    state.generators.Remove(item2.ItemSubtype);
                }
            }

            for (var j = 0; j < 4; j++)
            {
                if (j == ElevatorPosition) continue;

                state.Floors[j] = Floors[j].Clone();
            }

            foreach (var dupeitem in Floors[ElevatorPosition].Items)
            {
                if (dupeitem.Equals(item1) || dupeitem.Equals(item2))
                    continue;

                state.AddItem(ElevatorPosition, dupeitem);
            }

            state.AddItem(targetFloor, item1);
            if (item2 is not null) state.AddItem(targetFloor, item2);
            if (!state.Floors[targetFloor].FloorValid()) return null;
            return state;
        }

        public void AddItem(int floor, Item item)
        {
            if (floor < 0 || floor > 3) throw new IndexOutOfRangeException("Invalid floor");

            _pairs = null;

            if (item.ItemType == Item.ItemTypes.Generator)
            {
                generators.TryAdd(item.ItemSubtype, floor);
            }
            else
            {
                microchips.TryAdd(item.ItemSubtype, floor);
            }

            Floors[floor].AddItem(item);
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (obj is not State) return false; 
            var secondState = (State)obj;

            if (ElevatorPosition !=  secondState.ElevatorPosition) return false;

            if (Pairs.SequenceEqual(secondState.Pairs)) return true;
            return false;

            for (var i = 0; i < 4; i++)
            {
                if (!Floors[i].Equals(secondState.Floors[i])) return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            var hashCode = 17;

            foreach (var item in Pairs)
            {
                hashCode = hashCode * 31 + item.GetHashCode();
            }

            hashCode = hashCode * 31 + ElevatorPosition;

            return hashCode;
        }
    }
}
