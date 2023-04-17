using HardwareHero.Services.Shared.Models.Contributor;
using System.Diagnostics.CodeAnalysis;

namespace HardwareHero.Services.Shared.Comparers
{
    public class ContributorModelComparer : IEqualityComparer<ContributorModel>
    {
        public bool Equals(ContributorModel? x, ContributorModel? y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode([DisallowNull] ContributorModel obj)
        {
            return obj.GetHashCode();
        }
    }
}
