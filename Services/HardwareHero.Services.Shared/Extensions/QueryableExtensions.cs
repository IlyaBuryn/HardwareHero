using HardwareHero.Services.Shared.Exceptions;
using HardwareHero.Services.Shared.Filters;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HardwareHero.Services.Shared.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T?>? ApplyFilter<T>(this IQueryable<T?>? query, Filter<T> filter) where T : class
        {
            if (filter == null || query == null || query.Count() == 0)
            {
                return query;
            }

            var expression = FilterExpressionManager.BuildFilterExpression(filter);
            
            if (expression != null)
            { 
                query = query.Where(expression!);
            }

            return query;
        }

        public static IQueryable<T?>? ApplyOrderBy<T>(this IQueryable<T?>? query, Filter<T> filter) where T : class
        {
            if (filter == null || query == null || 
                string.IsNullOrEmpty(filter.SortBy) || string.IsNullOrEmpty(filter.SortOrder))
            {
                return query;
            }

            var expression = FilterExpressionManager.BuildArrangementExpression(filter);

            if (expression != null)
            {
                return filter.SortOrder.ToLower() == "asc"
                    ? query.OrderBy(expression!)
                    : query.OrderByDescending(expression!);
            }

            return query;
        }

        public static IQueryable<T?>? ApplyGroupBy<T>(this IQueryable<T?>? query, Filter<T> filter) where T: class
        {
            if (filter == null || query == null || string.IsNullOrEmpty(filter.GroupBy))
            {
                return query;
            }

            var expression = FilterExpressionManager.BuildArrangementExpression(filter);

            if (expression != null)
            {
                var groupedQuery = query.GroupBy(expression!);
                return filter.GroupedPattern(groupedQuery);
            }

            return query;
        }

        public static IQueryable<T?>? ApplySelection<T>(this IQueryable<T?>? query, Filter<T>? filter) where T : class
        {
            if (filter == null || query == null)
            {
                return query;
            }

            query = query.Select(item => item != null ? filter.SelectionPattern(item) : item);

            return query;
        }

        public static IQueryable<T?> CheckIfObjectNotFound<T>(this IQueryable<T?>? query) where T : class
        {
            if (query == null || query.Count() == 0)
            {
                throw new NotFoundException(nameof(query));
            }

            return query;
        }
    }
}
