using HardwareHero.Services.Shared.Models.Contributor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contributor.DataAccess.Data.Configurations
{
    internal class ContributorConfiguration : IEntityTypeConfiguration<ContributorModel>
    {
        public void Configure(EntityTypeBuilder<ContributorModel> builder)
        {
            builder.ToTable("Contributors");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.UserId).IsRequired();
            builder.HasIndex(c => c.UserId).IsUnique();

            builder.Property(c => c.Region).IsRequired().HasMaxLength(128);

            builder.Property(c => c.IsConfirmed).HasDefaultValue(null);

            builder.HasOne(a => a.SubscriptionInfo)
                .WithOne(b => b.Contributor)
                .HasForeignKey<SubscriptionInfo>(b => b.ContributorId);

            builder.Property(c => c.TimeStamp).HasDefaultValue(DateTime.Now);

            builder.HasData
            (
                // [by]
                new ContributorModel()
                {
                    Id = new Guid("ef12555d-c912-402d-a045-148091680d9a"),
                    UserId = new Guid("a9caa7b2-109b-4c21-bc24-749ff87b9b18"),
                    IsConfirmed = true,
                    Region = "Belarus",
                    ContributorExcellenceId = new Guid("6a07d316-9c05-4feb-b93d-c3ed68e04b52"),
                },
                new ContributorModel()
                {
                    Id = new Guid("203d006a-bdff-494e-94ad-8159c060f6bb"),
                    UserId = new Guid("274f801d-2117-48ff-96f7-ecb9b193bc7f"),
                    IsConfirmed = true,
                    Region = "Belarus",
                    ContributorExcellenceId = new Guid("09202396-9388-47f2-9597-0afd53fce045"),
                },
                new ContributorModel()
                {
                    Id = new Guid("fd88dbea-599f-43d5-858e-513cfcd80235"),
                    UserId = new Guid("46f5064b-a6fe-4b58-b303-9ed344700195"),
                    IsConfirmed = true,
                    Region = "Belarus",
                    ContributorExcellenceId = new Guid("238dd562-0928-4791-bddc-f9bee6507eaf"),
                },
                new ContributorModel()
                {
                    Id = new Guid("e61701f7-2c95-4986-bde9-0c18975e6cd6"),
                    UserId = new Guid("7c5086ea-4faf-4db2-91a4-c1217a2f3029"),
                    IsConfirmed = true,
                    Region = "Belarus",
                    SubscriptionInfoId = new Guid("cf7a198c-c551-456f-a519-e8679f3d0662"),
                    ContributorExcellenceId = new Guid("95475ce0-2343-47bf-9774-b5786e6ae97d"),
                },
                new ContributorModel()
                {
                    Id = new Guid("74bf3426-0a22-4c7a-ba2a-478a95363d46"),
                    UserId = new Guid("b0a3bda2-525d-42c0-b7ff-8b0f68b4ca84"),
                    IsConfirmed = true,
                    Region = "Belarus",
                    SubscriptionInfoId = new Guid("751d41e0-ebfa-435a-867c-8f00de10465f"),
                    ContributorExcellenceId = new Guid("c078962f-c9b1-4bb4-b726-ed37e4a71097"),
                },

                // [ru.msk]
                new ContributorModel()
                {
                    Id = new Guid("7736fd1d-4967-4d00-9249-3a384c0d4686"),
                    UserId = new Guid("17f87d98-17f0-4708-a7ff-0cb4ec09b58a"),
                    IsConfirmed = true,
                    Region = "Russia (Moscow)",
                    ContributorExcellenceId = new Guid("36142d8b-19e3-4a18-984a-459cf0c3d294"),
                },
                new ContributorModel()
                {
                    Id = new Guid("27abb845-e5a9-4b87-a253-843463b3a2cd"),
                    UserId = new Guid("8bc0e747-443a-4f62-a05a-6e7d8cb1516f"),
                    IsConfirmed = true,
                    Region = "Russia (Moscow)",
                    ContributorExcellenceId = new Guid("7ca46677-2a92-45d7-9e08-8866e17add0e"),
                },
                new ContributorModel()
                {
                    Id = new Guid("cd5673e3-3614-4e5f-96fb-3052a6548e50"),
                    UserId = new Guid("02e91bcd-c2f5-4025-8f2f-5bac70b6924c"),
                    IsConfirmed = true,
                    Region = "Russia (Moscow)",
                    ContributorExcellenceId = new Guid("a3a10175-64b6-46e7-95bb-0481cdaa993a"),
                },
                new ContributorModel()
                {
                    Id = new Guid("9c3436f0-0d5e-4295-9da8-9d61674bc14b"),
                    UserId = new Guid("ffcd6b86-9327-4b7a-b2ad-ec13cf531d3f"),
                    IsConfirmed = true,
                    Region = "Russia (Moscow)",
                    SubscriptionInfoId = new Guid("751d41e0-ebfa-435a-867c-8f00de10465f"),
                    ContributorExcellenceId = new Guid("35be6d3e-8570-4e64-8575-f158cbd2eb2e"),
                },

                // [pl]
                new ContributorModel()
                {
                    Id = new Guid("943e86a4-9322-4050-81a0-705eeaf9aee3"),
                    UserId = new Guid("67bfe5a9-28e2-4c55-8549-888556d2a670"),
                    IsConfirmed = true,
                    Region = "Poland",
                    ContributorExcellenceId = new Guid("e3bffdf2-edfa-4b79-bf84-ebabfaad90b5"),
                },
                new ContributorModel()
                {
                    Id = new Guid("e5846ece-f28d-4fa1-82bb-390f3275b868"),
                    UserId = new Guid("34302079-5037-499e-8703-9920be62adf7"),
                    IsConfirmed = true,
                    Region = "Poland",
                    ContributorExcellenceId = new Guid("3d6e4c17-132d-4c2f-9a03-ece95c6903e6"),
                },
                new ContributorModel()
                {
                    Id = new Guid("16584510-b499-4176-9f30-ba2b57b7106c"),
                    UserId = new Guid("0a3a8a9f-9bb3-4e06-a050-20c953855795"),
                    IsConfirmed = true,
                    Region = "Poland",
                    ContributorExcellenceId = new Guid("6b885476-dd6d-4c62-9bca-277d68cf09e3"),
                },

                // [ru.spb]
                new ContributorModel()
                {
                    Id = new Guid("1ff4cd39-1db6-4494-84d7-268e131430c5"),
                    UserId = new Guid("bad8a170-deb8-44e7-965a-2f660079d5ed"),
                    IsConfirmed = true,
                    Region = "Russia (Saint Petersburg)",
                    ContributorExcellenceId = new Guid("5e91bad7-6816-4f8d-a262-a7896207ca1f"),
                },
                new ContributorModel()
                {
                    Id = new Guid("a9963c4e-353d-4f47-82c0-39f1c9f90385"),
                    UserId = new Guid("373ec651-0d88-45f4-90dd-4b2a98500ecb"),
                    IsConfirmed = true,
                    Region = "Russia (Saint Petersburg)",
                    ContributorExcellenceId = new Guid("5bafa535-64fa-4885-b4d0-72673183e311"),
                }
            );
        }
    }
}
