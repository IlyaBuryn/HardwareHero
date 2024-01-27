using System.Linq.Expressions;

namespace HardwareHero.Services.Shared.Filters
{
    public enum FilterOperation
    {
        Equal,
        Contains,
        GreaterThanCurrentTime,
        LessThanCurrentTime,
        IsNull,
        KeyValueOperation,
    }

    public class FilterOperations
    {
        // TODO: Remove warnings
        public Expression BuildContainsExpression(Expression propertyExpression, object value)
        {
            var stringValue = value.ToString().ToLower();

            var toLowerMethod = typeof(string).GetMethod("ToLower", Type.EmptyTypes);
            var toLowerExpression = Expression.Call(propertyExpression, toLowerMethod);

            var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            var valueExpression = Expression.Constant(stringValue);
            var containsExpression = Expression.Call(toLowerExpression, containsMethod, valueExpression);

            return containsExpression;
        }

        public Expression BuildIsNullExpression(Expression propertyExpression, object value)
        {
            var valueExpression = Expression.Constant(null, propertyExpression.Type);
            var isNullExpression = Expression.Equal(propertyExpression, valueExpression);

            return isNullExpression;
        }

        public Expression BuildEqualsExpression(Expression propertyExpression, object value)
        {
            var valueExpression = Expression.Constant(value);
            var equalsExpression = Expression.Equal(propertyExpression, valueExpression);

            return equalsExpression;
        }

        public Expression BuildCurrentTimeCompareExpression(Expression propertyExpression, object value, FilterOperation specificOperation)
        {
            if (value is not DateTime dateTimeValue)
            {
                throw new ArgumentException("Value should be of type DateTime for greater than current time operation.");
            }

            var nowExpression = Expression.Constant(DateTime.Now);

            BinaryExpression? specificExpression = null;
            if (specificOperation == FilterOperation.GreaterThanCurrentTime)
            {
                specificExpression = Expression.GreaterThan(propertyExpression, nowExpression);
            }
            else
            {
                specificExpression = Expression.LessThan(propertyExpression, nowExpression);
            }

            return specificExpression;
        }
    }
}
