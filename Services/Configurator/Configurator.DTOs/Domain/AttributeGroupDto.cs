namespace Configurator.DTOs.Domain
{
    public enum GroupDataType
    {
        List,
        Range,
    }

    public class AttributeGroupDto
    {
        public string Key { get; set; }
        public List<string> Values { get; set; }
        public string DataType { get; set; }
    }
}
