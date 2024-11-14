using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aggregator.DataAccess.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitAggregator : Migration
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
                    FullName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    AccessibleForConfigurator = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpecificationCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecificationCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpecificationFilterTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecificationFilterTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpecificationAttributes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    ComponentTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SpecificationFilterTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SpecificationCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecificationAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpecificationAttributes_ComponentTypes_ComponentTypeId",
                        column: x => x.ComponentTypeId,
                        principalTable: "ComponentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpecificationAttributes_SpecificationCategories_SpecificationCategoryId",
                        column: x => x.SpecificationCategoryId,
                        principalTable: "SpecificationCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpecificationAttributes_SpecificationFilterTypes_SpecificationFilterTypeId",
                        column: x => x.SpecificationFilterTypeId,
                        principalTable: "SpecificationFilterTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ComponentAttributes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    ComponentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SpecificationAttributeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComponentAttributes_SpecificationAttributes_SpecificationAttributeId",
                        column: x => x.SpecificationAttributeId,
                        principalTable: "SpecificationAttributes",
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
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValue: new DateTime(2024, 11, 14, 15, 3, 27, 324, DateTimeKind.Utc).AddTicks(6359)),
                    IsRecommended = table.Column<bool>(type: "bit", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentGlobalReviews", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComponentImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    ComponentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RevokedId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Index = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentImages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComponentLocalReviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    ComponentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValue: new DateTime(2024, 11, 14, 15, 3, 27, 325, DateTimeKind.Utc).AddTicks(619)),
                    IsRecommended = table.Column<bool>(type: "bit", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentLocalReviews", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComponentMetrics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    ComponentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ViewsCount = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    ReviewCount = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Rating = table.Column<double>(type: "float", nullable: false, defaultValue: 0.0),
                    MinPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 11, 14, 15, 3, 27, 325, DateTimeKind.Utc).AddTicks(5053)),
                    ComponentId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentMetrics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Components",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    ComponentTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComponentMetricId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Components", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Components_ComponentMetrics_ComponentMetricId",
                        column: x => x.ComponentMetricId,
                        principalTable: "ComponentMetrics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Components_ComponentTypes_ComponentTypeId",
                        column: x => x.ComponentTypeId,
                        principalTable: "ComponentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComponentAttributes_ComponentId",
                table: "ComponentAttributes",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentAttributes_SpecificationAttributeId",
                table: "ComponentAttributes",
                column: "SpecificationAttributeId");

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
                name: "IX_ComponentMetrics_ComponentId1",
                table: "ComponentMetrics",
                column: "ComponentId1");

            migrationBuilder.CreateIndex(
                name: "IX_Components_ComponentMetricId",
                table: "Components",
                column: "ComponentMetricId");

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
                name: "IX_SpecificationAttributes_ComponentTypeId",
                table: "SpecificationAttributes",
                column: "ComponentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecificationAttributes_SpecificationCategoryId",
                table: "SpecificationAttributes",
                column: "SpecificationCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecificationAttributes_SpecificationFilterTypeId",
                table: "SpecificationAttributes",
                column: "SpecificationFilterTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecificationCategories_Name",
                table: "SpecificationCategories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SpecificationFilterTypes_Name",
                table: "SpecificationFilterTypes",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentAttributes_Components_ComponentId",
                table: "ComponentAttributes",
                column: "ComponentId",
                principalTable: "Components",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentGlobalReviews_Components_ComponentId",
                table: "ComponentGlobalReviews",
                column: "ComponentId",
                principalTable: "Components",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentImages_Components_ComponentId",
                table: "ComponentImages",
                column: "ComponentId",
                principalTable: "Components",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentLocalReviews_Components_ComponentId",
                table: "ComponentLocalReviews",
                column: "ComponentId",
                principalTable: "Components",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentMetrics_Components_ComponentId1",
                table: "ComponentMetrics",
                column: "ComponentId1",
                principalTable: "Components",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComponentMetrics_Components_ComponentId1",
                table: "ComponentMetrics");

            migrationBuilder.DropTable(
                name: "ComponentAttributes");

            migrationBuilder.DropTable(
                name: "ComponentGlobalReviews");

            migrationBuilder.DropTable(
                name: "ComponentImages");

            migrationBuilder.DropTable(
                name: "ComponentLocalReviews");

            migrationBuilder.DropTable(
                name: "SpecificationAttributes");

            migrationBuilder.DropTable(
                name: "SpecificationCategories");

            migrationBuilder.DropTable(
                name: "SpecificationFilterTypes");

            migrationBuilder.DropTable(
                name: "Components");

            migrationBuilder.DropTable(
                name: "ComponentMetrics");

            migrationBuilder.DropTable(
                name: "ComponentTypes");
        }
    }
}
