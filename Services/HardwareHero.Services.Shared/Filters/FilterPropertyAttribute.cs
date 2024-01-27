namespace HardwareHero.Services.Shared.Filters
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class FilterAttribute : Attribute
    {
        public string EntityProperty { get; }
        public FilterOperation Operation { get; }

        public FilterAttribute(string entityProperty, FilterOperation operation)
        {
            EntityProperty = entityProperty;
            Operation = operation;
        }
    }

    public class FilterPropertyAttribute : FilterAttribute
    {
        public FilterPropertyAttribute(string entityProperty, FilterOperation operation)
            : base(entityProperty, operation)
        { }
    }


    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public class OrFilterPropertyAttribute : FilterAttribute
    {
        public OrFilterPropertyAttribute(string entityProperty, FilterOperation operation)
            : base(entityProperty, operation)
        { }
    }
}
