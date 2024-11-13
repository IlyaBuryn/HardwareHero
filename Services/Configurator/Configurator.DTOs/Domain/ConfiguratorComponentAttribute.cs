namespace Configurator.DTOs.Domain
{
    public class ConfiguratorComponentAttribute
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Operation { get; set; } = "value";

        public bool Compare(ConfiguratorComponentAttribute other)
        {
            if (Operation == "value" && other.Operation == "value")
            {
                return CompareValues(other.Value);
            }
            if (Operation == "max" && other.Operation == "value")
            {
                return CompareMaxWithValue(other.Value);
            }
            if (Operation == "list" && other.Operation == "value")
            {
                return CompareListWithValue(other.Value);
            }
            if (Operation == "bool" && other.Operation == "max")
            {
                return CompareBoolWithMax(other.Value);
            }

            throw new InvalidOperationException($"Unsupported comparison between {Operation} and {other.Operation}");
        }

        private bool CompareValues(string otherValue)
        {
            return string.Equals(Value, otherValue, StringComparison.OrdinalIgnoreCase);
        }

        private bool CompareMaxWithValue(string otherValue)
        {
            if (int.TryParse(Value, out int maxValue) && int.TryParse(otherValue, out int value))
            {
                return maxValue <= value;
            }
            return false;
        }

        private bool CompareListWithValue(string otherValue)
        {
            var thisValues = Value.Split("/").Select(v => v.Trim()).ToHashSet(StringComparer.OrdinalIgnoreCase);
            return thisValues.Contains(otherValue.Trim());
        }

        private bool CompareBoolWithMax(string maxValue)
        {
            if (bool.TryParse(Value, out bool boolValue) && int.TryParse(maxValue, out int maxParsedValue))
            {
                int valueToCompare = boolValue ? 1 : 0;
                return maxParsedValue >= valueToCompare;
            }
            return false;
        }
    }
}
