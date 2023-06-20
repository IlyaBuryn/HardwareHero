﻿// <auto-generated />
using System;
using Contributor.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Contributor.DataAccess.Data.Migrations
{
    [DbContext(typeof(ContributorDbContext))]
    [Migration("20230612094618_ContributorInitMigration")]
    partial class ContributorInitMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ChatRoomContributorModel", b =>
                {
                    b.Property<Guid>("ChatRoomsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ContributorsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ChatRoomsId", "ContributorsId");

                    b.HasIndex("ContributorsId");

                    b.ToTable("ChatRoomContributorModel");
                });

            modelBuilder.Entity("HardwareHero.Services.Shared.Models.Contributor.ChatMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ChatRoomId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SenderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.Property<DateTime>("Timestamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2023, 6, 12, 12, 46, 18, 394, DateTimeKind.Local).AddTicks(2135));

                    b.HasKey("Id");

                    b.HasIndex("ChatRoomId");

                    b.HasIndex("SenderId");

                    b.ToTable("ChatMessages");
                });

            modelBuilder.Entity("HardwareHero.Services.Shared.Models.Contributor.ChatRoom", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("ChatRooms");
                });

            modelBuilder.Entity("HardwareHero.Services.Shared.Models.Contributor.ContributorExcellence", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Logo")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("ContributorExcellences");

                    b.HasData(
                        new
                        {
                            Id = new Guid("6a07d316-9c05-4feb-b93d-c3ed68e04b52"),
                            Logo = "fk.by_logo.png",
                            Name = "fk.by"
                        },
                        new
                        {
                            Id = new Guid("09202396-9388-47f2-9597-0afd53fce045"),
                            Logo = "4pc.by_logo.png",
                            Name = "4pc.by"
                        },
                        new
                        {
                            Id = new Guid("238dd562-0928-4791-bddc-f9bee6507eaf"),
                            Logo = "RAM.by_logo.png",
                            Name = "RAM.by"
                        },
                        new
                        {
                            Id = new Guid("95475ce0-2343-47bf-9774-b5786e6ae97d"),
                            Logo = "7745.by_logo.png",
                            Name = "7745.by"
                        },
                        new
                        {
                            Id = new Guid("c078962f-c9b1-4bb4-b726-ed37e4a71097"),
                            Logo = "Technoby.by_logo.png",
                            Name = "Technoby.by"
                        },
                        new
                        {
                            Id = new Guid("36142d8b-19e3-4a18-984a-459cf0c3d294"),
                            Logo = "kns.ru_logo.png",
                            Name = "kns.ru"
                        },
                        new
                        {
                            Id = new Guid("7ca46677-2a92-45d7-9e08-8866e17add0e"),
                            Logo = "compday.ru_logo.png",
                            Name = "compday.ru"
                        },
                        new
                        {
                            Id = new Guid("a3a10175-64b6-46e7-95bb-0481cdaa993a"),
                            Logo = "xcom-shop.ru_logo.png",
                            Name = "xcom-shop.ru"
                        },
                        new
                        {
                            Id = new Guid("35be6d3e-8570-4e64-8575-f158cbd2eb2e"),
                            Logo = "pc-arena.ru_logo.png",
                            Name = "pc-arena.ru"
                        },
                        new
                        {
                            Id = new Guid("e3bffdf2-edfa-4b79-bf84-ebabfaad90b5"),
                            Logo = "allegro.pl_logo.png",
                            Name = "allegro.pl"
                        },
                        new
                        {
                            Id = new Guid("3d6e4c17-132d-4c2f-9a03-ece95c6903e6"),
                            Logo = "avans.pl_logo.png",
                            Name = "avans.pl"
                        },
                        new
                        {
                            Id = new Guid("6b885476-dd6d-4c62-9bca-277d68cf09e3"),
                            Logo = "MediaMarkt.pl_logo.png",
                            Name = "MediaMarkt.pl"
                        },
                        new
                        {
                            Id = new Guid("5e91bad7-6816-4f8d-a262-a7896207ca1f"),
                            Logo = "planetacomp.com_logo.png",
                            Name = "planetacomp.com"
                        },
                        new
                        {
                            Id = new Guid("5bafa535-64fa-4885-b4d0-72673183e311"),
                            Logo = "royal-computers.ru_logo.png",
                            Name = "royal-computers.ru"
                        });
                });

            modelBuilder.Entity("HardwareHero.Services.Shared.Models.Contributor.ContributorModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ComponentRefId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ContributorExcellenceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool?>("IsConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<Guid?>("ReviewRefId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SubscriptionInfoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("TimeStamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2023, 6, 12, 12, 46, 18, 395, DateTimeKind.Local).AddTicks(8234));

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ComponentRefId");

                    b.HasIndex("ContributorExcellenceId");

                    b.HasIndex("ReviewRefId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Contributors", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("ef12555d-c912-402d-a045-148091680d9a"),
                            ContributorExcellenceId = new Guid("6a07d316-9c05-4feb-b93d-c3ed68e04b52"),
                            IsConfirmed = true,
                            Region = "Belarus",
                            SubscriptionInfoId = new Guid("00000000-0000-0000-0000-000000000000"),
                            UserId = new Guid("a9caa7b2-109b-4c21-bc24-749ff87b9b18")
                        },
                        new
                        {
                            Id = new Guid("203d006a-bdff-494e-94ad-8159c060f6bb"),
                            ContributorExcellenceId = new Guid("09202396-9388-47f2-9597-0afd53fce045"),
                            IsConfirmed = true,
                            Region = "Belarus",
                            SubscriptionInfoId = new Guid("00000000-0000-0000-0000-000000000000"),
                            UserId = new Guid("274f801d-2117-48ff-96f7-ecb9b193bc7f")
                        },
                        new
                        {
                            Id = new Guid("fd88dbea-599f-43d5-858e-513cfcd80235"),
                            ContributorExcellenceId = new Guid("238dd562-0928-4791-bddc-f9bee6507eaf"),
                            IsConfirmed = true,
                            Region = "Belarus",
                            SubscriptionInfoId = new Guid("00000000-0000-0000-0000-000000000000"),
                            UserId = new Guid("46f5064b-a6fe-4b58-b303-9ed344700195")
                        },
                        new
                        {
                            Id = new Guid("e61701f7-2c95-4986-bde9-0c18975e6cd6"),
                            ContributorExcellenceId = new Guid("95475ce0-2343-47bf-9774-b5786e6ae97d"),
                            IsConfirmed = true,
                            Region = "Belarus",
                            SubscriptionInfoId = new Guid("cf7a198c-c551-456f-a519-e8679f3d0662"),
                            UserId = new Guid("7c5086ea-4faf-4db2-91a4-c1217a2f3029")
                        },
                        new
                        {
                            Id = new Guid("74bf3426-0a22-4c7a-ba2a-478a95363d46"),
                            ContributorExcellenceId = new Guid("c078962f-c9b1-4bb4-b726-ed37e4a71097"),
                            IsConfirmed = true,
                            Region = "Belarus",
                            SubscriptionInfoId = new Guid("751d41e0-ebfa-435a-867c-8f00de10465f"),
                            UserId = new Guid("b0a3bda2-525d-42c0-b7ff-8b0f68b4ca84")
                        },
                        new
                        {
                            Id = new Guid("7736fd1d-4967-4d00-9249-3a384c0d4686"),
                            ContributorExcellenceId = new Guid("36142d8b-19e3-4a18-984a-459cf0c3d294"),
                            IsConfirmed = true,
                            Region = "Russia (Moscow)",
                            SubscriptionInfoId = new Guid("00000000-0000-0000-0000-000000000000"),
                            UserId = new Guid("17f87d98-17f0-4708-a7ff-0cb4ec09b58a")
                        },
                        new
                        {
                            Id = new Guid("27abb845-e5a9-4b87-a253-843463b3a2cd"),
                            ContributorExcellenceId = new Guid("7ca46677-2a92-45d7-9e08-8866e17add0e"),
                            IsConfirmed = true,
                            Region = "Russia (Moscow)",
                            SubscriptionInfoId = new Guid("00000000-0000-0000-0000-000000000000"),
                            UserId = new Guid("8bc0e747-443a-4f62-a05a-6e7d8cb1516f")
                        },
                        new
                        {
                            Id = new Guid("cd5673e3-3614-4e5f-96fb-3052a6548e50"),
                            ContributorExcellenceId = new Guid("a3a10175-64b6-46e7-95bb-0481cdaa993a"),
                            IsConfirmed = true,
                            Region = "Russia (Moscow)",
                            SubscriptionInfoId = new Guid("00000000-0000-0000-0000-000000000000"),
                            UserId = new Guid("02e91bcd-c2f5-4025-8f2f-5bac70b6924c")
                        },
                        new
                        {
                            Id = new Guid("9c3436f0-0d5e-4295-9da8-9d61674bc14b"),
                            ContributorExcellenceId = new Guid("35be6d3e-8570-4e64-8575-f158cbd2eb2e"),
                            IsConfirmed = true,
                            Region = "Russia (Moscow)",
                            SubscriptionInfoId = new Guid("751d41e0-ebfa-435a-867c-8f00de10465f"),
                            UserId = new Guid("ffcd6b86-9327-4b7a-b2ad-ec13cf531d3f")
                        },
                        new
                        {
                            Id = new Guid("943e86a4-9322-4050-81a0-705eeaf9aee3"),
                            ContributorExcellenceId = new Guid("e3bffdf2-edfa-4b79-bf84-ebabfaad90b5"),
                            IsConfirmed = true,
                            Region = "Poland",
                            SubscriptionInfoId = new Guid("00000000-0000-0000-0000-000000000000"),
                            UserId = new Guid("67bfe5a9-28e2-4c55-8549-888556d2a670")
                        },
                        new
                        {
                            Id = new Guid("e5846ece-f28d-4fa1-82bb-390f3275b868"),
                            ContributorExcellenceId = new Guid("3d6e4c17-132d-4c2f-9a03-ece95c6903e6"),
                            IsConfirmed = true,
                            Region = "Poland",
                            SubscriptionInfoId = new Guid("00000000-0000-0000-0000-000000000000"),
                            UserId = new Guid("34302079-5037-499e-8703-9920be62adf7")
                        },
                        new
                        {
                            Id = new Guid("16584510-b499-4176-9f30-ba2b57b7106c"),
                            ContributorExcellenceId = new Guid("6b885476-dd6d-4c62-9bca-277d68cf09e3"),
                            IsConfirmed = true,
                            Region = "Poland",
                            SubscriptionInfoId = new Guid("00000000-0000-0000-0000-000000000000"),
                            UserId = new Guid("0a3a8a9f-9bb3-4e06-a050-20c953855795")
                        },
                        new
                        {
                            Id = new Guid("1ff4cd39-1db6-4494-84d7-268e131430c5"),
                            ContributorExcellenceId = new Guid("5e91bad7-6816-4f8d-a262-a7896207ca1f"),
                            IsConfirmed = true,
                            Region = "Russia (Saint Petersburg)",
                            SubscriptionInfoId = new Guid("00000000-0000-0000-0000-000000000000"),
                            UserId = new Guid("bad8a170-deb8-44e7-965a-2f660079d5ed")
                        },
                        new
                        {
                            Id = new Guid("a9963c4e-353d-4f47-82c0-39f1c9f90385"),
                            ContributorExcellenceId = new Guid("5bafa535-64fa-4885-b4d0-72673183e311"),
                            IsConfirmed = true,
                            Region = "Russia (Saint Petersburg)",
                            SubscriptionInfoId = new Guid("00000000-0000-0000-0000-000000000000"),
                            UserId = new Guid("373ec651-0d88-45f4-90dd-4b2a98500ecb")
                        });
                });

            modelBuilder.Entity("HardwareHero.Services.Shared.Models.Contributor.Reference", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MainApiKey")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MainApiLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MainWebLink")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("References");
                });

            modelBuilder.Entity("HardwareHero.Services.Shared.Models.Contributor.SubscriptionInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ContributorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ExpiryDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2023, 7, 12, 12, 46, 18, 396, DateTimeKind.Local).AddTicks(6135));

                    b.Property<Guid>("PlanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValue(new Guid("ca7f44ac-ec3c-4caa-9ee7-dc1c6550a681"));

                    b.Property<DateTime>("RenewalDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2023, 6, 12, 12, 46, 18, 396, DateTimeKind.Local).AddTicks(5738));

                    b.HasKey("Id");

                    b.HasIndex("ContributorId")
                        .IsUnique();

                    b.HasIndex("PlanId");

                    b.ToTable("SubscriptionInfo");

                    b.HasData(
                        new
                        {
                            Id = new Guid("cf7a198c-c551-456f-a519-e8679f3d0662"),
                            ContributorId = new Guid("e61701f7-2c95-4986-bde9-0c18975e6cd6"),
                            ExpiryDate = new DateTime(2023, 7, 12, 12, 46, 18, 396, DateTimeKind.Local).AddTicks(6379),
                            PlanId = new Guid("d4c656b7-5a3d-4318-bbf9-37438750e542"),
                            RenewalDate = new DateTime(2023, 6, 12, 12, 46, 18, 396, DateTimeKind.Local).AddTicks(6377)
                        },
                        new
                        {
                            Id = new Guid("751d41e0-ebfa-435a-867c-8f00de10465f"),
                            ContributorId = new Guid("74bf3426-0a22-4c7a-ba2a-478a95363d46"),
                            ExpiryDate = new DateTime(2023, 7, 12, 12, 46, 18, 396, DateTimeKind.Local).AddTicks(6384),
                            PlanId = new Guid("d4c656b7-5a3d-4318-bbf9-37438750e542"),
                            RenewalDate = new DateTime(2023, 6, 12, 12, 46, 18, 396, DateTimeKind.Local).AddTicks(6383)
                        },
                        new
                        {
                            Id = new Guid("9fb5a64e-8211-4a6a-835d-ece23056ff31"),
                            ContributorId = new Guid("9c3436f0-0d5e-4295-9da8-9d61674bc14b"),
                            ExpiryDate = new DateTime(2023, 7, 12, 12, 46, 18, 396, DateTimeKind.Local).AddTicks(6388),
                            PlanId = new Guid("d4c656b7-5a3d-4318-bbf9-37438750e542"),
                            RenewalDate = new DateTime(2023, 6, 12, 12, 46, 18, 396, DateTimeKind.Local).AddTicks(6388)
                        });
                });

            modelBuilder.Entity("HardwareHero.Services.Shared.Models.Contributor.SubscriptionPlan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("DaysCount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(30);

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("PriorityLevel")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("SubscriptionPlans");

                    b.HasData(
                        new
                        {
                            Id = new Guid("ca7f44ac-ec3c-4caa-9ee7-dc1c6550a681"),
                            DaysCount = 30,
                            Price = 0m,
                            PriorityLevel = 0
                        },
                        new
                        {
                            Id = new Guid("d4c656b7-5a3d-4318-bbf9-37438750e542"),
                            DaysCount = 30,
                            Price = 100.0m,
                            PriorityLevel = 1
                        },
                        new
                        {
                            Id = new Guid("39bc7661-8341-40e6-9065-a77d31926484"),
                            DaysCount = 30,
                            Price = 500.0m,
                            PriorityLevel = 2
                        });
                });

            modelBuilder.Entity("ChatRoomContributorModel", b =>
                {
                    b.HasOne("HardwareHero.Services.Shared.Models.Contributor.ChatRoom", null)
                        .WithMany()
                        .HasForeignKey("ChatRoomsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HardwareHero.Services.Shared.Models.Contributor.ContributorModel", null)
                        .WithMany()
                        .HasForeignKey("ContributorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HardwareHero.Services.Shared.Models.Contributor.ChatMessage", b =>
                {
                    b.HasOne("HardwareHero.Services.Shared.Models.Contributor.ChatRoom", "ChatRoom")
                        .WithMany("ChatMessages")
                        .HasForeignKey("ChatRoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HardwareHero.Services.Shared.Models.Contributor.ContributorModel", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChatRoom");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("HardwareHero.Services.Shared.Models.Contributor.ContributorModel", b =>
                {
                    b.HasOne("HardwareHero.Services.Shared.Models.Contributor.Reference", "ComponentRef")
                        .WithMany()
                        .HasForeignKey("ComponentRefId");

                    b.HasOne("HardwareHero.Services.Shared.Models.Contributor.ContributorExcellence", "ContributorExcellence")
                        .WithMany()
                        .HasForeignKey("ContributorExcellenceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HardwareHero.Services.Shared.Models.Contributor.Reference", "ReviewRef")
                        .WithMany()
                        .HasForeignKey("ReviewRefId");

                    b.Navigation("ComponentRef");

                    b.Navigation("ContributorExcellence");

                    b.Navigation("ReviewRef");
                });

            modelBuilder.Entity("HardwareHero.Services.Shared.Models.Contributor.SubscriptionInfo", b =>
                {
                    b.HasOne("HardwareHero.Services.Shared.Models.Contributor.ContributorModel", "Contributor")
                        .WithOne("SubscriptionInfo")
                        .HasForeignKey("HardwareHero.Services.Shared.Models.Contributor.SubscriptionInfo", "ContributorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HardwareHero.Services.Shared.Models.Contributor.SubscriptionPlan", "Plan")
                        .WithMany()
                        .HasForeignKey("PlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contributor");

                    b.Navigation("Plan");
                });

            modelBuilder.Entity("HardwareHero.Services.Shared.Models.Contributor.ChatRoom", b =>
                {
                    b.Navigation("ChatMessages");
                });

            modelBuilder.Entity("HardwareHero.Services.Shared.Models.Contributor.ContributorModel", b =>
                {
                    b.Navigation("SubscriptionInfo")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
