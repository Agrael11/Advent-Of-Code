using System.Text.Json;

namespace advent_of_code.Year2015.Day12
{
    public static class Challange1
    {
        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n');

            using var document = JsonDocument.Parse(input);
            var root = document.RootElement;

            return CalculateProperties([.. root.EnumerateObject()]);
        }

        public static int CalculateProperties(List<JsonProperty> properties)
        {
            var total = 0;

            foreach (var property in properties)
            {
                total += CalculateElement(property.Value);
            }

            return total;
        }

        public static int CalculateElements(List<JsonElement> elements)
        {
            var total = 0;

            foreach (var element in elements)
            {
                total += CalculateElement(element);
            }

            return total;
        }

        public static int CalculateElement(JsonElement element)
        {
            var total = 0;
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