using System.Security.Cryptography;

namespace advent_of_code.Year2019.Day18
{
    internal struct MapTile (MapTileType tileType, ulong keyID)
    {
        public MapTileType TileType { get; set; } = tileType;
        public ulong KeyID{ get; set; } = keyID;
    }

    internal enum MapTileType
    {
        Wall,
        Open,
        Key,
        Door,
    }

    internal class Map(int width, int height)
    {
        public static bool IsKey(char c)
        {
            return c is >= 'a' and <= 'z';
        }

        public static bool IsDoor(char c)
        {
            return c is >= 'A' and <= 'Z';
        }

        private static char KeyForDoor(char door)
        {
            return (char)(door - 'A' + 'a');
        }

        private static ulong KeyIDForKey(char key)
        {
            return 1UL << (key - 'a');
        }

        public readonly int Width = width;
        public readonly int Height = height;
        public int KeyCount => Keys.Count;

        private readonly MapTile[,] _tiles = new MapTile[width, height];
        public readonly List<ulong> Keys = new List<ulong>();

        public List<Common.Position> StartPoints {get; private set;} = [];

        public MapTile this[int x, int y]
        {
            get => _tiles[x, y];
            set => _tiles[x, y] = value;
        }

        public void FixStartPoint()
        {
            var point = StartPoints[0];
            StartPoints.Clear();
            for (var yOff = -1; yOff <= 1; yOff++)
            {

                for (var xOff = -1; xOff <= 1; xOff++)
                {
                    var x = point.X + xOff;
                    var y = point.Y + yOff;
                    if (xOff != 0 && yOff != 0)
                    {
                        StartPoints.Add(new(x, y));
                        this[x,y] = new MapTile(MapTileType.Open, 0);
                    }
                    else
                    {
                        this[x, y] = new MapTile(MapTileType.Wall, 0);
                    }
                }
            }
        }

        public void LoadFromInput(string[] input)
        {
            StartPoints.Clear();
            Keys.Clear();
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    var symbol = input[y][x];
                    ulong keyID = 0L;
                    var tileType = symbol switch
                    {
                        '#' => MapTileType.Wall,
                        '.' or '@' => MapTileType.Open,
                        _ when IsKey(symbol) => MapTileType.Key,
                        _ when IsDoor(symbol) => MapTileType.Door,
                        _ => throw new ArgumentOutOfRangeException($"Unknown map symbol: {symbol}")
                    };
                    if (symbol == '@')
                    {
                        StartPoints.Add(new (x, y));
                    }
                    if (tileType == MapTileType.Door)
                    {
                        symbol = KeyForDoor(symbol);
                        keyID = KeyIDForKey(symbol);
                    }
                    if (tileType == MapTileType.Key)
                    {
                        keyID = KeyIDForKey(symbol);
                        Keys.Add(keyID);
                    }
                    _tiles[x, y] = new MapTile(tileType, keyID);
                }
            }
        }
    }
}
