using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Aggregator.DataAccess.Data.Migrations
{
    /// <inheritdoc />
    public partial class AggregatorInitMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Components",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Images = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specifications = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InitialPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Components", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComponentReviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ComponentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Recommended = table.Column<bool>(type: "bit", nullable: false),
                    ContributorName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    ContributorLogo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComponentReviews_Components_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "Components",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Components",
                columns: new[] { "Id", "Description", "Images", "InitialPrice", "Name", "Specifications" },
                values: new object[,]
                {
                    { new Guid("0712d311-71e5-4c5b-8f80-1b1b08180851"), "Configuration items just for test", "nothing,nothing,nothing", 8.90m, "Graphics Card #1", "{\"ch1\":\"Great\",\"ch2\":\"Bad\",\"ch3\":\"Normal\"}" },
                    { new Guid("17bb6742-6611-4865-99f4-222610fb1b88"), "Configuration items just for test", "nothing,nothing,nothing,aaaand//nothing", 30000m, "Processor #2", "{\"ch1\":\"Wo\",\"ch2\":\"Ah\",\"ch3\":\"Oi\"}" }
                });

            migrationBuilder.InsertData(
                table: "ComponentReviews",
                columns: new[] { "Id", "ComponentId", "ContributorLogo", "ContributorName", "Date", "Name", "Recommended", "Text" },
                values: new object[] { new Guid("665cba20-d56b-47ba-be57-4672014a91f6"), new Guid("0712d311-71e5-4c5b-8f80-1b1b08180851"), null, "Amazon", new DateTime(2023, 4, 23, 17, 42, 21, 662, DateTimeKind.Local).AddTicks(9625), "Bob", true, "Very good, very nice!" });

            migrationBuilder.CreateIndex(
                name: "IX_ComponentReviews_ComponentId",
                table: "ComponentReviews",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_Components_Name",
                table: "Components",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComponentReviews");

            migrationBuilder.DropTable(
                name: "Components");
        }
    }
}
