using Contributor.DataAccess.Models;

namespace Contributor.DataAccess.Data.Configurations
{
    public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.Code).IsRequired();
            builder.HasIndex(c => c.Code).IsUnique();
        }
    }
}
