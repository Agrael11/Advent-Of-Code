using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code.Year2018.Day13
{
    internal class Common
    {
        internal enum Direction { Left, Up, Right, Down };

        internal static bool MoveCarts(ref List<Cart> carts, ref string[] lines, bool remove = false)
        {
            carts = carts.OrderBy(c => c.Position.Y).ThenBy(c => c.Position.X).ToList();

            for (var i = 0; i < carts.Count; i++)
            {
                var cart = carts[i];
                cart.Move();
                cart.ChangeDirection(lines[cart.Position.Y][cart.Position.X]);
                var anyColliding = carts.GroupBy(g => g.Position).Any(g => g.Count() > 1);
                if (anyColliding && !remove)
                {
                    return true;
                }
                else if (anyColliding && remove)
                {
                    i -= RemoveCarts(ref carts, i);
                }
            }

            return (remove && carts.Count == 1);
        }

        private static int RemoveCarts(ref List<Cart> carts, int currentIndex)
        {
            var Position = carts[currentIndex].Position;
            var removedBefore = 0;
            
            for (var i = carts.Count - 1; i >= 0; i--)
            {
                if (carts[i].Position == Position)
                {
                    if (i <= currentIndex) 
                    {
                        removedBefore++; 
                    }
                    carts.RemoveAt(i);
                }
            }

            return removedBefore;
        }

        internal static bool TryAddCart(ref List<Cart> carts, int X, int Y, string[] lines)
        {
            if (Y < 0 || Y >= lines.Length || X < 0 || X >= lines[Y].Length)
                return false;

            switch (lines[Y][X])
            {
                case '>':
                    carts.Add(new Cart(X, Y, Common.Direction.Right));
                    break;
                case '<':
                    carts.Add(new Cart(X, Y, Common.Direction.Left));
                    break;
                case 'v':
                    carts.Add(new Cart(X, Y, Common.Direction.Down));
                    break;
                case '^':
                    carts.Add(new Cart(X, Y, Common.Direction.Up));
                    break;
                default:
                    return false;
            }

            return true;
        }
    }
}
