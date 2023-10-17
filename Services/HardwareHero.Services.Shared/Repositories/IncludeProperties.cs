using System.Linq.Expressions;

namespace HardwareHero.Services.Shared.Repositories
{
    public class IncludeProperties<T>
    {
        public Expression<Func<T, object>>[]? IncludeExpressions { get; set; }
        public bool IsAllIncludes { get; set; } = false;
    }
}
