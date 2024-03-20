namespace advent_of_code.Year2016.Day04
{
    public static class Challange1
    {
        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');
            var realRooms = 0;
            foreach (var room in input)
            {
                realRooms += IsRoomReal(room);
            }
            return realRooms;
        }

        public static int IsRoomReal(string room)
        {
            var roomname = room[..room.LastIndexOf('-')].Replace("-","");
            var roomId = int.Parse(room[(room.LastIndexOf('-') + 1)..room.LastIndexOf('[')]);
            var checksum = room[(room.LastIndexOf('[') + 1)..^1];
            var occurances = new Dictionary<char, int>();
            foreach (var c in roomname)
            {
                occurances.TryGetValue(c, out var value);
                occurances[c] = value + 1;
            }

            occurances = occurances.OrderByDescending(t => (t.Value * 100 + ('z' - t.Key))).ToDictionary();
            for (var i = 0; i < checksum.Length; i++)
            {
                if (occurances.Keys.ElementAt(i) != checksum[i]) return 0;
            }

            return roomId;
        }
    }
}