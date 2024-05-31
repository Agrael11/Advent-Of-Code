using System.Diagnostics.CodeAnalysis;

namespace advent_of_code.Year2017.Day24
{
    public class Domino
    {
        public int A;
        public int B;

        public int Strength => B + A;

        public Domino(int a, int b)
        {
            A = a;
            B = b;
        }

        public Domino(string source)
        {
            var split = source.Split('/');
            A = int.Parse(split[0]);
            B = int.Parse(split[1]);
        }

        public int OtherEnd(int end)
        {
            if (A == end) return B;
            return A;
        }

        public bool Connects(int end)
        {
            return (A == end || B == end);
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (obj is not Domino) return false;
            var other = (Domino)obj;
            return other.A == A && other.B == B;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(A, B);
        }
    }
}