namespace advent_of_code.Year2025.Day09
{

    /// <summary>
    /// Polygon created from list of vertices
    /// </summary>
    internal class Polygon
    {
        public readonly List<Vertex> Vertices = new List<Vertex>();
        private readonly Dictionary<long, List<Edge>> _verticalEdges = new Dictionary<long, List<Edge>>();
        private readonly Dictionary<long, List<Edge>> _horizontalEdges = new Dictionary<long, List<Edge>>();

        public Polygon(List<Vertex> vertices)
        {
            Vertices.AddRange(vertices);
            GenerateEdges();
        }

        /// <summary>
        /// Generates Edges from vertices
        /// </summary>
        private void GenerateEdges()
        {
            for (var index = 0; index < Vertices.Count; index++)
            {
                var v1 = Vertices[index];
                var v2 = Vertices[(index + 1) % Vertices.Count];
                AddEdgeToList(new Edge(v1, v2));
            }
        }

        /// <summary>
        /// Adds single Edge to horizontal or vertical list, depending on it's orientation
        /// </summary>
        /// <param name="edge"></param>
        /// <exception cref="Exception"></exception>
        private void AddEdgeToList(Edge edge)
        {
            if (edge.Horizontal)
            {
                if (!_horizontalEdges.TryGetValue(edge.Y1, out var list))
                {
                    list = new List<Edge>();
                    _horizontalEdges.Add(edge.Y1, list);
                }
                list.Add(edge);
                return;
            }
            if (edge.Vertical)
            {
                if (!_verticalEdges.TryGetValue(edge.X1, out var list))
                {
                    list = new List<Edge>();
                    _verticalEdges.Add(edge.X1, list);
                }
                list.Add(edge);
                return;
            }
            throw new Exception("Edge is not Vertical nor Horizontal.");
        }

        /// <summary>
        /// Checks if Polygon fully contains rectangle
        /// </summary>
        /// <param name="rectangle"></param>
        /// <returns></returns>
        public bool Contains(Rectangle rectangle)
        {
            foreach (var (y, edgeList) in _horizontalEdges)
            {
                if (y <= rectangle.Y1 || y >= rectangle.Y2) continue;
                foreach (var edge in edgeList)
                {
                    if (edge.IsInsideRectangle(rectangle)) return false;
                }
            }
            foreach (var (x, edgeList) in _verticalEdges)
            {
                if (x <= rectangle.X1 || x >= rectangle.X2) continue;
                foreach (var edge in edgeList)
                {
                    if (edge.IsInsideRectangle(rectangle)) return false;
                }
            }
            return true;
        }
    }
}
