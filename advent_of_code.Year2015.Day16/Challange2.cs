namespace advent_of_code.Year2015.Day16
{
    public static class Challange2
    {
        private static readonly Dictionary<Aunt.Property, int> Prototype = new Dictionary<Aunt.Property, int>(){
                { Aunt.Property.Children, 3 } ,
                { Aunt.Property.Cats, 7 } ,
                { Aunt.Property.Samoyeds, 2 } ,
                { Aunt.Property.Pomeranians, 3 } ,
                { Aunt.Property.Akitas, 0 } ,
                { Aunt.Property.Vizslas, 0 } ,
                { Aunt.Property.Goldfish, 5 } ,
                { Aunt.Property.Trees, 3 } ,
                { Aunt.Property.Cars, 2 } ,
                { Aunt.Property.Perfumes, 1 }
            };

        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            foreach (var inputLine in input)
            {
                var aunt = new Aunt(inputLine[(inputLine.IndexOf(':') + 1)..]);
                if (aunt.EqualsProperties(Prototype, true))
                {
                    return int.Parse(inputLine[(inputLine.IndexOf(' ') + 1)..inputLine.IndexOf(':')]);
                }
            }

            return -1;
        }
    }
}
