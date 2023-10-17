﻿// <auto-generated />
using System;
using Aggregator.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Aggregator.DataAccess.Data.Migrations
{
    [DbContext(typeof(AggregatorDbContext))]
    [Migration("20230907150225_InitAggregatorMigration")]
    partial class InitAggregatorMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HardwareHero.Services.Shared.Models.Aggregator.Component", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ComponentTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("ComponentTypeId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Components");
                });

            modelBuilder.Entity("HardwareHero.Services.Shared.Models.Aggregator.ComponentAttributes", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AttributeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AttributeValue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ComponentId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ComponentId");

                    b.ToTable("ComponentAttributes");
                });

            modelBuilder.Entity("HardwareHero.Services.Shared.Models.Aggregator.ComponentGlobalReview", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AuthorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ComponentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ContributorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool?>("IsRecommended")
                        .HasColumnType("bit");

                    b.Property<int?>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Timestamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2023, 9, 7, 18, 2, 25, 351, DateTimeKind.Local).AddTicks(7447));

                    b.HasKey("Id");

                    b.HasIndex("ComponentId");

                    b.ToTable("ComponentGlobalReviews");
                });

            modelBuilder.Entity("HardwareHero.Services.Shared.Models.Aggregator.ComponentImages", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ComponentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ComponentId");

                    b.ToTable("ComponentImages");
                });

            modelBuilder.Entity("HardwareHero.Services.Shared.Models.Aggregator.ComponentLocalReview", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ComponentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool?>("IsRecommended")
                        .HasColumnType("bit");

                    b.Property<int?>("Rating")
                        .HasMaxLength(5)
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Timestamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2023, 9, 7, 18, 2, 25, 352, DateTimeKind.Local).AddTicks(2379));

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ComponentId");

                    b.ToTable("ComponentLocalReviews");
                });

            modelBuilder.Entity("HardwareHero.Services.Shared.Models.Aggregator.ComponentType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("ComponentTypes");
                });

            modelBuilder.Entity("HardwareHero.Services.Shared.Models.Aggregator.Maintenance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ContributorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<Guid>("MaintenanceTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.HasIndex("MaintenanceTypeId");

                    b.ToTable("Maintenances");
                });

            modelBuilder.Entity("HardwareHero.Services.Shared.Models.Aggregator.MaintenanceGlobalReview", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AuthorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ContributorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool?>("IsRecommended")
                        .HasColumnType("bit");

                    b.Property<Guid>("MaintenanceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Timestamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2023, 9, 7, 18, 2, 25, 353, DateTimeKind.Local).AddTicks(831));

                    b.HasKey("Id");

                    b.HasIndex("MaintenanceId");

                    b.ToTable("MaintenanceGlobalReviews");
                });

            modelBuilder.Entity("HardwareHero.Services.Shared.Models.Aggregator.MaintenanceLocalReview", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool?>("IsRecommended")
                        .HasColumnType("bit");

                    b.Property<Guid>("MaintenanceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Timestamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2023, 9, 7, 18, 2, 25, 353, DateTimeKind.Local).AddTicks(2936));

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("MaintenanceId");

                    b.ToTable("MaintenanceLocalReviews");
                });

            modelBuilder.Entity("HardwareHero.Services.Shared.Models.Aggregator.MaintenanceType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("MaintenanceTypes");
                });

            modelBuilder.Entity("HardwareHero.Services.Shared.Models.Aggregator.Component", b =>
                {
                    b.HasOne("HardwareHero.Services.Shared.Models.Aggregator.ComponentType", "ComponentType")
                        .WithMany()
                        .HasForeignKey("ComponentTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ComponentType");
                });

            modelBuilder.Entity("HardwareHero.Services.Shared.Models.Aggregator.ComponentAttributes", b =>
                {
                    b.HasOne("HardwareHero.Services.Shared.Models.Aggregator.Component", "Component")
                        .WithMany("ComponentAttributes")
                        .HasForeignKey("ComponentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Component");
                });

            modelBuilder.Entity("HardwareHero.Services.Shared.Models.Aggregator.ComponentGlobalReview", b =>
                {
                    b.HasOne("HardwareHero.Services.Shared.Models.Aggregator.Component", "Component")
                        .WithMany()
                        .HasForeignKey("ComponentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Component");
                });

            modelBuilder.Entity("HardwareHero.Services.Shared.Models.Aggregator.ComponentImages", b =>
                {
                    b.HasOne("HardwareHero.Services.Shared.Models.Aggregator.Component", "Component")
                        .WithMany("ComponentImages")
                        .HasForeignKey("ComponentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Component");
                });

            modelBuilder.Entity("HardwareHero.Services.Shared.Models.Aggregator.ComponentLocalReview", b =>
                {
                    b.HasOne("HardwareHero.Services.Shared.Models.Aggregator.Component", "Component")
                        .WithMany()
                        .HasForeignKey("ComponentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Component");
                });

            modelBuilder.Entity("HardwareHero.Services.Shared.Models.Aggregator.Maintenance", b =>
                {
                    b.HasOne("HardwareHero.Services.Shared.Models.Aggregator.MaintenanceType", "MaintenanceType")
                        .WithMany()
                        .HasForeignKey("MaintenanceTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("MaintenanceType");
                });

            modelBuilder.Entity("HardwareHero.Services.Shared.Models.Aggregator.MaintenanceGlobalReview", b =>
                {
                    b.HasOne("HardwareHero.Services.Shared.Models.Aggregator.Maintenance", "Maintenance")
                        .WithMany()
                        .HasForeignKey("MaintenanceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Maintenance");
                });

            modelBuilder.Entity("HardwareHero.Services.Shared.Models.Aggregator.MaintenanceLocalReview", b =>
                {
                    b.HasOne("HardwareHero.Services.Shared.Models.Aggregator.Maintenance", "Maintenance")
                        .WithMany()
                        .HasForeignKey("MaintenanceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Maintenance");
                });

            modelBuilder.Entity("HardwareHero.Services.Shared.Models.Aggregator.Component", b =>
                {
                    b.Navigation("ComponentAttributes");

                    b.Navigation("ComponentImages");
                });
#pragma warning restore 612, 618
        }
    }
}
