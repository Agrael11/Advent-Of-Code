namespace advent_of_code.Year2016.Day04
{
    public static class Challange2
    {
        public static readonly int aValue = 'a';
        public static readonly int alphabetLength = 'z'-'a'+1;

        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            foreach (var room in input)
            {
                if (IsRoomReal(room, out string roomName, out int roomId))
                {
                    if (NameDecypher(roomName, roomId).Contains("north")) return roomId;
                }
            }

            return -1;
        }

        public static string NameDecypher(string roomName, int key)
        {
            var room = roomName.ToCharArray();
            for (var charId = 0; charId < room.Length; charId++)
            {
                if (room[charId] == '-')
                {
                    room[charId] = ' ';
                    continue;
                }

                room[charId] = (char)((room[charId] - aValue + key) % alphabetLength + aValue);
            }

            return new string(room);
        }

        public static bool IsRoomReal(string room, out string roomName, out int roomId)
        {
            roomName = room[..room.LastIndexOf('-')].Replace("-", "");
            roomId = int.Parse(room[(room.LastIndexOf('-') + 1)..room.LastIndexOf('[')]);
            var checksum = room[(room.LastIndexOf('[') + 1)..^1];
            var occurances = new Dictionary<char, int>();
            foreach (var c in roomName)
            {
                occurances.TryGetValue(c, out var value);
                occurances[c] = value + 1;
            }

            occurances = occurances.OrderByDescending(t => (t.Value * 100 + ('z' - t.Key))).ToDictionary();
            for (var i = 0; i < checksum.Length; i++)
            {
                if (occurances.Keys.ElementAt(i) != checksum[i]) return false;
            }

            return true;
        }
    }
}