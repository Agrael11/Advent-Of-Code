namespace advent_of_code.Year2015.Day16
{
    public static class Challange1
    {
        private static readonly Dictionary<Aunt.Property, int> Prototype = new(){
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
            string[] input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');

            foreach (string inputLine in input)
            {
                Aunt aunt = new(inputLine[(inputLine.IndexOf(':')+1)..]);
                if (aunt.EqualsProperties(Prototype, false))
                {
                    return int.Parse(inputLine[(inputLine.IndexOf(' ') + 1)..inputLine.IndexOf(':')]);
                }
            }

            return -1;
        }
    }
}