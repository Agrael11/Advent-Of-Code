﻿namespace advent_of_code.Helpers
{
    public static partial class PathFinding
    {
        private static List<T> ReconstructPath<T>(Dictionary<T, T> parents, T endNode) where T : notnull
        {
            List<T> path = [];
            T current = endNode;

            while (parents.ContainsKey(current))
            {
                path.Insert(0, current);
                current = parents[current];
            }

            path.Insert(0, current);
            return path;
        }
    }
}