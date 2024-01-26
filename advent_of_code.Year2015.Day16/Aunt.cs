namespace advent_of_code.Year2015.Day16
{
    internal class Aunt
    {
        public enum Property { Children, Cats, Samoyeds, Pomeranians, Akitas, Vizslas, Goldfish, Trees, Cars, Perfumes };
        private readonly Dictionary<Property, int?> properties = new Dictionary<Property, int?>();

        public Aunt(string infoString)
        {
            properties.Clear();
            foreach (Property property in Enum.GetValues(typeof(Property)))
            {
                properties.Add(property, null);
            }
            var infos = infoString.Split(',');
            foreach (var info in infos)
            {
                var data = info.Split(':');
                switch (data[0].Trim())
                {
                    case "children":
                        properties[Property.Children] = int.Parse(data[1].Trim());
                        break;
                    case "cats":
                        properties[Property.Cats] = int.Parse(data[1].Trim());
                        break;
                    case "samoyeds":
                        properties[Property.Samoyeds] = int.Parse(data[1].Trim());
                        break;
                    case "pomeranians":
                        properties[Property.Pomeranians] = int.Parse(data[1].Trim());
                        break;
                    case "akitas":
                        properties[Property.Akitas] = int.Parse(data[1].Trim());
                        break;
                    case "vizslas":
                        properties[Property.Vizslas] = int.Parse(data[1].Trim());
                        break;
                    case "goldfish":
                        properties[Property.Goldfish] = int.Parse(data[1].Trim());
                        break;
                    case "trees":
                        properties[Property.Trees] = int.Parse(data[1].Trim());
                        break;
                    case "cars":
                        properties[Property.Cars] = int.Parse(data[1].Trim());
                        break;
                    case "perfumes":
                        properties[Property.Perfumes] = int.Parse(data[1].Trim());
                        break;
                }
            }
        }

        public bool EqualsProperty(Property property, int value, bool part2)
        {
            if (part2 && (property == Property.Cats || property == Property.Trees))
            {
                return properties[property] == null || properties[property] > value;
            }
            else if (part2 && (property == Property.Pomeranians || property == Property.Goldfish))
            {
                return properties[property] == null || properties[property] < value;
            }
            return properties[property] == null || properties[property] == value;
        }
        public bool EqualsProperties(Dictionary<Property, int> properties, bool part2)
        {
            foreach (var key in properties.Keys)
            {
                if (!EqualsProperty(key, properties[key], part2)) return false;
            }
            return true;
        }
    }
}
