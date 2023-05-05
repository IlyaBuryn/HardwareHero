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
    [Migration("20230505125235_NewDefaultElements")]
    partial class NewDefaultElements
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
                        .HasDefaultValue(new DateTime(2023, 5, 5, 15, 52, 35, 510, DateTimeKind.Local).AddTicks(4601));

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
                            Id = new Guid("3f46062f-56d8-4897-a37f-ff4e920b2d73"),
                            Logo = "newegg.com.png",
                            Name = "NewEgg.com"
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

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<Guid?>("ReviewRefId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SubscriptionInfoId")
                        .HasColumnType("uniqueidentifier");

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
                            ContributorExcellenceId = new Guid("3f46062f-56d8-4897-a37f-ff4e920b2d73"),
                            Region = "Poland",
                            SubscriptionInfoId = new Guid("cf7a198c-c551-456f-a519-e8679f3d0662"),
                            UserId = new Guid("8fe35832-874a-447b-a076-6e030b87d7eb")
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
                        .HasDefaultValue(new DateTime(2023, 6, 4, 15, 52, 35, 512, DateTimeKind.Local).AddTicks(7810));

                    b.Property<Guid>("PlanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValue(new Guid("ca7f44ac-ec3c-4caa-9ee7-dc1c6550a681"));

                    b.Property<DateTime>("RenewalDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2023, 5, 5, 15, 52, 35, 512, DateTimeKind.Local).AddTicks(7437));

                    b.HasKey("Id");

                    b.HasIndex("ContributorId")
                        .IsUnique();

                    b.HasIndex("PlanId");

                    b.ToTable("SubscriptionInfo");

                    b.HasData(
                        new
                        {
                            Id = new Guid("cf7a198c-c551-456f-a519-e8679f3d0662"),
                            ContributorId = new Guid("ef12555d-c912-402d-a045-148091680d9a"),
                            ExpiryDate = new DateTime(2023, 6, 4, 15, 52, 35, 512, DateTimeKind.Local).AddTicks(8040),
                            PlanId = new Guid("ca7f44ac-ec3c-4caa-9ee7-dc1c6550a681"),
                            RenewalDate = new DateTime(2023, 5, 5, 15, 52, 35, 512, DateTimeKind.Local).AddTicks(8039)
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
