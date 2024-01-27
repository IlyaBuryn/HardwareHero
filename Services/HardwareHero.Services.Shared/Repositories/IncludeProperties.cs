using System.Linq.Expressions;

namespace HardwareHero.Services.Shared.Repositories
{
    public class IncludeProperties<T>
    {
        public Expression<Func<T, object>>[]? IncludeExpressions { get; set; }
        public bool IsAllIncludes { get; set; } = false;

        public IncludeProperties()
        {
            IsAllIncludes = true;
        }

        public IncludeProperties(bool isAllInclude)
        {
            IsAllIncludes = isAllInclude;
        }

        public IncludeProperties(params Expression<Func<T, object>>[]? expressions)
        {
            IncludeExpressions = expressions;
            IsAllIncludes = false;
        }

        public IncludeProperties(bool isAllIncludes, params Expression<Func<T, object>>[] expressions)
        {
            IncludeExpressions = expressions;
            IsAllIncludes = isAllIncludes;
        }
    }
}
