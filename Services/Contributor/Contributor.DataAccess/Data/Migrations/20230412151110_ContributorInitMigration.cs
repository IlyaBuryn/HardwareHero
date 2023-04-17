using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Contributor.DataAccess.Data.Migrations
{
    /// <inheritdoc />
    public partial class ContributorInitMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatRooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatRooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContributorExcellences",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContributorExcellences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "References",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MainWebLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MainApiLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MainApiKey = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_References", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionPlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DaysCount = table.Column<int>(type: "int", nullable: false, defaultValue: 30),
                    PriorityLevel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contributors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Region = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    SubscriptionInfoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComponentRefId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReviewRefId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ContributorExcellenceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contributors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contributors_ContributorExcellences_ContributorExcellenceId",
                        column: x => x.ContributorExcellenceId,
                        principalTable: "ContributorExcellences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contributors_References_ComponentRefId",
                        column: x => x.ComponentRefId,
                        principalTable: "References",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Contributors_References_ReviewRefId",
                        column: x => x.ReviewRefId,
                        principalTable: "References",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 4, 12, 18, 11, 10, 250, DateTimeKind.Local).AddTicks(4552)),
                    SenderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChatRoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatMessages_ChatRooms_ChatRoomId",
                        column: x => x.ChatRoomId,
                        principalTable: "ChatRooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatMessages_Contributors_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Contributors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChatRoomContributorModel",
                columns: table => new
                {
                    ChatRoomsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContributorsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatRoomContributorModel", x => new { x.ChatRoomsId, x.ContributorsId });
                    table.ForeignKey(
                        name: "FK_ChatRoomContributorModel_ChatRooms_ChatRoomsId",
                        column: x => x.ChatRoomsId,
                        principalTable: "ChatRooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatRoomContributorModel_Contributors_ContributorsId",
                        column: x => x.ContributorsId,
                        principalTable: "Contributors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RenewalDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 4, 12, 18, 11, 10, 251, DateTimeKind.Local).AddTicks(9664)),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 5, 12, 18, 11, 10, 251, DateTimeKind.Local).AddTicks(9894)),
                    PlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValue: new Guid("ca7f44ac-ec3c-4caa-9ee7-dc1c6550a681")),
                    ContributorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionInfo_Contributors_ContributorId",
                        column: x => x.ContributorId,
                        principalTable: "Contributors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubscriptionInfo_SubscriptionPlans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "SubscriptionPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ContributorExcellences",
                columns: new[] { "Id", "Logo", "Name" },
                values: new object[] { new Guid("3f46062f-56d8-4897-a37f-ff4e920b2d73"), "", "Test Name" });

            migrationBuilder.InsertData(
                table: "SubscriptionPlans",
                columns: new[] { "Id", "DaysCount", "Price", "PriorityLevel" },
                values: new object[,]
                {
                    { new Guid("39bc7661-8341-40e6-9065-a77d31926484"), 30, 500.0m, 2 },
                    { new Guid("ca7f44ac-ec3c-4caa-9ee7-dc1c6550a681"), 30, 0m, 0 },
                    { new Guid("d4c656b7-5a3d-4318-bbf9-37438750e542"), 30, 100.0m, 1 }
                });

            migrationBuilder.InsertData(
                table: "Contributors",
                columns: new[] { "Id", "ComponentRefId", "ContributorExcellenceId", "Region", "ReviewRefId", "SubscriptionInfoId", "UserId" },
                values: new object[] { new Guid("ef12555d-c912-402d-a045-148091680d9a"), null, new Guid("3f46062f-56d8-4897-a37f-ff4e920b2d73"), "Poland", null, new Guid("cf7a198c-c551-456f-a519-e8679f3d0662"), new Guid("8fe35832-874a-447b-a076-6e030b87d7eb") });

            migrationBuilder.InsertData(
                table: "SubscriptionInfo",
                columns: new[] { "Id", "ContributorId", "ExpiryDate", "PlanId", "RenewalDate" },
                values: new object[] { new Guid("cf7a198c-c551-456f-a519-e8679f3d0662"), new Guid("ef12555d-c912-402d-a045-148091680d9a"), new DateTime(2023, 5, 12, 18, 11, 10, 252, DateTimeKind.Local).AddTicks(144), new Guid("ca7f44ac-ec3c-4caa-9ee7-dc1c6550a681"), new DateTime(2023, 4, 12, 18, 11, 10, 252, DateTimeKind.Local).AddTicks(143) });

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_ChatRoomId",
                table: "ChatMessages",
                column: "ChatRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_SenderId",
                table: "ChatMessages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatRoomContributorModel_ContributorsId",
                table: "ChatRoomContributorModel",
                column: "ContributorsId");

            migrationBuilder.CreateIndex(
                name: "IX_ContributorExcellences_Name",
                table: "ContributorExcellences",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contributors_ComponentRefId",
                table: "Contributors",
                column: "ComponentRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Contributors_ContributorExcellenceId",
                table: "Contributors",
                column: "ContributorExcellenceId");

            migrationBuilder.CreateIndex(
                name: "IX_Contributors_ReviewRefId",
                table: "Contributors",
                column: "ReviewRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Contributors_UserId",
                table: "Contributors",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionInfo_ContributorId",
                table: "SubscriptionInfo",
                column: "ContributorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionInfo_PlanId",
                table: "SubscriptionInfo",
                column: "PlanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatMessages");

            migrationBuilder.DropTable(
                name: "ChatRoomContributorModel");

            migrationBuilder.DropTable(
                name: "SubscriptionInfo");

            migrationBuilder.DropTable(
                name: "ChatRooms");

            migrationBuilder.DropTable(
                name: "Contributors");

            migrationBuilder.DropTable(
                name: "SubscriptionPlans");

            migrationBuilder.DropTable(
                name: "ContributorExcellences");

            migrationBuilder.DropTable(
                name: "References");
        }
    }
}
