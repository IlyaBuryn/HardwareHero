namespace HardwareHero.Shared.Extensions
{
    public static class QueryableExtensions
    {
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
