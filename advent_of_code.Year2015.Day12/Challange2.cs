using System.Text.Json;

namespace advent_of_code.Year2015.Day12
{
    public static class Challange2
    {
        public static int DoChallange(string inputData)
        {
            string input = inputData.Replace("\r", "").TrimEnd('\n');

            using JsonDocument document = JsonDocument.Parse(input);
            JsonElement root = document.RootElement;

            return CalculateProperties([.. root.EnumerateObject()]);
        }

        public static int CalculateProperties(List<JsonProperty> properties)
        {
            int total = 0;

            if (properties.Any(t => { return t.Value.ValueKind == JsonValueKind.String && t.Value.GetString() == "red"; })) 
                return 0;

            foreach (JsonProperty property in properties)
            {
                total += CalculateElement(property.Value);
            }

            return total;
        }

        public static int CalculateElements(List<JsonElement> elements)
        {
            int total = 0;

            foreach (JsonElement element in elements)
            {
                total += CalculateElement(element);
            }

            return total;
        }

        public static int CalculateElement(JsonElement element)
        {
            int total = 0;
            if (element.ValueKind == JsonValueKind.Number)
            {
                total += element.GetInt32();
            }
            else if (element.ValueKind == JsonValueKind.Array)
            {
                total += CalculateElements([.. element.EnumerateArray()]);
            }
            else if (element.ValueKind == JsonValueKind.Object)
            {
                total += CalculateProperties([.. element.EnumerateObject()]);
            }
            return total;
        }
    }
}
