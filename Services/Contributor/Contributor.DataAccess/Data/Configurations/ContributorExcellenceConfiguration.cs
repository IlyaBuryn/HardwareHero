using HardwareHero.Services.Shared.Models.Contributor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contributor.DataAccess.Data.Configurations
{
    internal class ContributorExcellenceConfiguration
        : IEntityTypeConfiguration<ContributorExcellence>
    {
        public void Configure(EntityTypeBuilder<ContributorExcellence> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.Name).IsRequired().HasMaxLength(128);
            builder.HasIndex(c => c.Name).IsUnique();

            builder.Property(c => c.Logo).IsRequired().HasMaxLength(512);

            builder.HasData
            (
                // [by]
                new ContributorExcellence()
                {
                    Id = new Guid("6a07d316-9c05-4feb-b93d-c3ed68e04b52"),
                    Name = "fk.by",
                    Logo = "fk.by_logo.png",
                },
                new ContributorExcellence()
                {
                    Id = new Guid("09202396-9388-47f2-9597-0afd53fce045"),
                    Name = "4pc.by",
                    Logo = "4pc.by_logo.png",
                },
                new ContributorExcellence()
                {
                    Id = new Guid("238dd562-0928-4791-bddc-f9bee6507eaf"),
                    Name = "RAM.by",
                    Logo = "RAM.by_logo.png",
                },
                new ContributorExcellence()
                {
                    Id = new Guid("95475ce0-2343-47bf-9774-b5786e6ae97d"),
                    Name = "7745.by",
                    Logo = "7745.by_logo.png",
                },
                new ContributorExcellence()
                {
                    Id = new Guid("c078962f-c9b1-4bb4-b726-ed37e4a71097"),
                    Name = "Technoby.by",
                    Logo = "Technoby.by_logo.png",
                },

                // [ru.msk]
                new ContributorExcellence()
                {
                    Id = new Guid("36142d8b-19e3-4a18-984a-459cf0c3d294"),
                    Name = "kns.ru",
                    Logo = "kns.ru_logo.png",
                },
                new ContributorExcellence()
                {
                    Id = new Guid("7ca46677-2a92-45d7-9e08-8866e17add0e"),
                    Name = "compday.ru",
                    Logo = "compday.ru_logo.png",
                },
                new ContributorExcellence()
                {
                    Id = new Guid("a3a10175-64b6-46e7-95bb-0481cdaa993a"),
                    Name = "xcom-shop.ru",
                    Logo = "xcom-shop.ru_logo.png",
                },
                new ContributorExcellence()
                {
                    Id = new Guid("35be6d3e-8570-4e64-8575-f158cbd2eb2e"),
                    Name = "pc-arena.ru",
                    Logo = "pc-arena.ru_logo.png",
                },

                // [pl]
                new ContributorExcellence()
                {
                    Id = new Guid("e3bffdf2-edfa-4b79-bf84-ebabfaad90b5"),
                    Name = "allegro.pl",
                    Logo = "allegro.pl_logo.png",
                },
                new ContributorExcellence()
                {
                    Id = new Guid("3d6e4c17-132d-4c2f-9a03-ece95c6903e6"),
                    Name = "avans.pl",
                    Logo = "avans.pl_logo.png",
                },
                new ContributorExcellence()
                {
                    Id = new Guid("6b885476-dd6d-4c62-9bca-277d68cf09e3"),
                    Name = "MediaMarkt.pl",
                    Logo = "MediaMarkt.pl_logo.png",
                },

                // [ru.spb]
                new ContributorExcellence()
                {
                    Id = new Guid("5e91bad7-6816-4f8d-a262-a7896207ca1f"),
                    Name = "planetacomp.com",
                    Logo = "planetacomp.com_logo.png",
                },
                new ContributorExcellence()
                {
                    Id = new Guid("5bafa535-64fa-4885-b4d0-72673183e311"),
                    Name = "royal-computers.ru",
                    Logo = "royal-computers.ru_logo.png",
                }
            );
        }
    }
}
