using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aggregator.DataAccess.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitAggregatorMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComponentTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaintenanceTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Components",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    ComponentTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Components", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Components_ComponentTypes_ComponentTypeId",
                        column: x => x.ComponentTypeId,
                        principalTable: "ComponentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Maintenances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    MaintenanceTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContributorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maintenances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Maintenances_MaintenanceTypes_MaintenanceTypeId",
                        column: x => x.MaintenanceTypeId,
                        principalTable: "MaintenanceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ComponentAttributes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    ComponentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttributeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AttributeValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComponentAttributes_Components_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "Components",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComponentGlobalReviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    ContributorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComponentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 11, 1, 21, 41, 44, 677, DateTimeKind.Local).AddTicks(2287)),
                    Rating = table.Column<int>(type: "int", nullable: true),
                    IsRecommended = table.Column<bool>(type: "bit", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentGlobalReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComponentGlobalReviews_Components_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "Components",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComponentImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    ComponentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComponentImages_Components_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "Components",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComponentLocalReviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    ComponentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 11, 1, 21, 41, 44, 678, DateTimeKind.Local).AddTicks(2327)),
                    Rating = table.Column<int>(type: "int", maxLength: 5, nullable: true),
                    IsRecommended = table.Column<bool>(type: "bit", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentLocalReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComponentLocalReviews_Components_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "Components",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComponentViews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComponentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ViewsCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentViews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComponentViews_Components_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "Components",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaintenanceGlobalReviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    ContributorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaintenanceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 11, 1, 21, 41, 44, 679, DateTimeKind.Local).AddTicks(9334)),
                    Rating = table.Column<int>(type: "int", nullable: true),
                    IsRecommended = table.Column<bool>(type: "bit", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceGlobalReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaintenanceGlobalReviews_Maintenances_MaintenanceId",
                        column: x => x.MaintenanceId,
                        principalTable: "Maintenances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaintenanceLocalReviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    MaintenanceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 11, 1, 21, 41, 44, 680, DateTimeKind.Local).AddTicks(3808)),
                    Rating = table.Column<int>(type: "int", nullable: true),
                    IsRecommended = table.Column<bool>(type: "bit", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceLocalReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaintenanceLocalReviews_Maintenances_MaintenanceId",
                        column: x => x.MaintenanceId,
                        principalTable: "Maintenances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComponentAttributes_ComponentId",
                table: "ComponentAttributes",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentGlobalReviews_ComponentId",
                table: "ComponentGlobalReviews",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentImages_ComponentId",
                table: "ComponentImages",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentLocalReviews_ComponentId",
                table: "ComponentLocalReviews",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_Components_ComponentTypeId",
                table: "Components",
                column: "ComponentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Components_Name",
                table: "Components",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ComponentTypes_Name",
                table: "ComponentTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ComponentViews_ComponentId",
                table: "ComponentViews",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceGlobalReviews_MaintenanceId",
                table: "MaintenanceGlobalReviews",
                column: "MaintenanceId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceLocalReviews_MaintenanceId",
                table: "MaintenanceLocalReviews",
                column: "MaintenanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenances_MaintenanceTypeId",
                table: "Maintenances",
                column: "MaintenanceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceTypes_Name",
                table: "MaintenanceTypes",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComponentAttributes");

            migrationBuilder.DropTable(
                name: "ComponentGlobalReviews");

            migrationBuilder.DropTable(
                name: "ComponentImages");

            migrationBuilder.DropTable(
                name: "ComponentLocalReviews");

            migrationBuilder.DropTable(
                name: "ComponentViews");

            migrationBuilder.DropTable(
                name: "MaintenanceGlobalReviews");

            migrationBuilder.DropTable(
                name: "MaintenanceLocalReviews");

            migrationBuilder.DropTable(
                name: "Components");

            migrationBuilder.DropTable(
                name: "Maintenances");

            migrationBuilder.DropTable(
                name: "ComponentTypes");

            migrationBuilder.DropTable(
                name: "MaintenanceTypes");
        }
    }
}
