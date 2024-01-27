using System.Linq.Expressions;
using System.Reflection;

namespace HardwareHero.Services.Shared.Filters
{
    public static class FilterExpressionManager
    {
        public static Expression<Func<T, bool>>? BuildFilterExpression<T>(Filter<T> filter)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            Expression? expression = null;

            foreach (var property in filter.GetType().GetProperties())
            {
                var filterValue = property.GetValue(filter);
                if (filterValue == null)
                {
                    continue;
                }

                var filterFieldAttribute = property.GetCustomAttribute<FilterPropertyAttribute>();
                if (filterFieldAttribute == null)
                {
                    continue;
                }

                try
                {
                    var filterExpression = GetFilterExpressionForAttribute(filterFieldAttribute, parameter, filterValue);
                    if (filterExpression != null)
                    {
                        expression = expression == null
                            ? filterExpression
                            : Expression.AndAlso(expression, filterExpression);
                    }

                    var orFilterAttributes = property.GetCustomAttributes<OrFilterPropertyAttribute>();
                    if (orFilterAttributes != null && orFilterAttributes.Any())
                    {
                        foreach (var orFilterAttribute in orFilterAttributes)
                        {
                            var orFilterExpression = GetFilterExpressionForAttribute(orFilterAttribute, parameter, filterValue);

                            expression = expression == null
                                ? orFilterExpression
                                : Expression.OrElse(expression, orFilterExpression);
                        }
                    }
                    else
                    {
                        expression = expression == null
                            ? filterExpression
                            : Expression.AndAlso(expression, filterExpression);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing filter: {ex.Message}");
                }
            }

            if (expression != null)
            {
                return Expression.Lambda<Func<T, bool>>(expression, parameter);
            }
            else
            {
                return null;
            }
        }

        public static Expression<Func<T, dynamic>>? BuildArrangementExpression<T>(Filter<T> filter)
        {
            Expression<Func<T, dynamic>>? result = null;
            try
            {
                var parameter = Expression.Parameter(typeof(T), "x");
                var property = Expression.Property(parameter, filter.SortBy);
                result = Expression.Lambda<Func<T, dynamic>>(property, parameter);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing filter: {ex.Message}");
            }
            return result;
        }

        public static Expression GetExpressionByFilterOperation(FilterOperation operation, Expression propertyExpression, object filterValue)
        {
            var options = new FilterOperations();
            Expression filterExpression;

            switch (operation)
            {
                case FilterOperation.Contains:
                    filterExpression = options.BuildContainsExpression(propertyExpression, filterValue);
                    break;
                case FilterOperation.IsNull:
                    filterExpression = options.BuildIsNullExpression(propertyExpression, filterValue);
                    break;
                case FilterOperation.Equal:
                    filterExpression = options.BuildEqualsExpression(propertyExpression, filterValue);
                    break;
                case FilterOperation.GreaterThanCurrentTime:
                    filterExpression = options.BuildCurrentTimeCompareExpression(propertyExpression, filterValue, operation);
                    break;
                case FilterOperation.LessThanCurrentTime:
                    filterExpression = options.BuildCurrentTimeCompareExpression(propertyExpression, filterValue, operation);
                    break;

                default:
                    throw new NotSupportedException($"Operation {operation} is not supported.");
            }

            return filterExpression;
        }

        public static Expression BuildPropertyExpression(ParameterExpression parameter, string propertyPath)
        {
            var parts = propertyPath.Split('.');
            Expression propertyExpression = parameter;

            foreach (var part in parts)
            {
                propertyExpression = Expression.PropertyOrField(propertyExpression, part);
            }

            return propertyExpression;
        }

        private static Expression GetFilterExpressionForAttribute(FilterAttribute attribute, ParameterExpression parameter, object filterValue)
        {
            var propertyExpression = GetPropertyExpression(attribute, parameter);
            var filterExpression = GetExpressionByFilterOperation(attribute.Operation, propertyExpression, filterValue);
            return filterExpression;
        }

        private static Expression GetPropertyExpression(FilterAttribute attribute, ParameterExpression parameter)
        {
            var entityProperty = attribute.EntityProperty;

            Expression propertyExpression = FilterExpressionManager
                .BuildPropertyExpression(parameter, entityProperty);

            return propertyExpression;
        }

        private static Expression GetFilterExpression(FilterAttribute attribute, Expression propertyExpression, object filterValue)
        {
            var operation = attribute.Operation;

            Expression filterExpression = FilterExpressionManager
                .GetExpressionByFilterOperation(operation, propertyExpression, filterValue);

            return filterExpression;
        }
    }
}
