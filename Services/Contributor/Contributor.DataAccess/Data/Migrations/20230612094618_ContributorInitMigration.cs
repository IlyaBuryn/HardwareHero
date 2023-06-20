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
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: true),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValue: new DateTime(2023, 6, 12, 12, 46, 18, 395, DateTimeKind.Local).AddTicks(8234)),
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
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 6, 12, 12, 46, 18, 394, DateTimeKind.Local).AddTicks(2135)),
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
                    RenewalDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 6, 12, 12, 46, 18, 396, DateTimeKind.Local).AddTicks(5738)),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 7, 12, 12, 46, 18, 396, DateTimeKind.Local).AddTicks(6135)),
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
                values: new object[,]
                {
                    { new Guid("09202396-9388-47f2-9597-0afd53fce045"), "4pc.by_logo.png", "4pc.by" },
                    { new Guid("238dd562-0928-4791-bddc-f9bee6507eaf"), "RAM.by_logo.png", "RAM.by" },
                    { new Guid("35be6d3e-8570-4e64-8575-f158cbd2eb2e"), "pc-arena.ru_logo.png", "pc-arena.ru" },
                    { new Guid("36142d8b-19e3-4a18-984a-459cf0c3d294"), "kns.ru_logo.png", "kns.ru" },
                    { new Guid("3d6e4c17-132d-4c2f-9a03-ece95c6903e6"), "avans.pl_logo.png", "avans.pl" },
                    { new Guid("5bafa535-64fa-4885-b4d0-72673183e311"), "royal-computers.ru_logo.png", "royal-computers.ru" },
                    { new Guid("5e91bad7-6816-4f8d-a262-a7896207ca1f"), "planetacomp.com_logo.png", "planetacomp.com" },
                    { new Guid("6a07d316-9c05-4feb-b93d-c3ed68e04b52"), "fk.by_logo.png", "fk.by" },
                    { new Guid("6b885476-dd6d-4c62-9bca-277d68cf09e3"), "MediaMarkt.pl_logo.png", "MediaMarkt.pl" },
                    { new Guid("7ca46677-2a92-45d7-9e08-8866e17add0e"), "compday.ru_logo.png", "compday.ru" },
                    { new Guid("95475ce0-2343-47bf-9774-b5786e6ae97d"), "7745.by_logo.png", "7745.by" },
                    { new Guid("a3a10175-64b6-46e7-95bb-0481cdaa993a"), "xcom-shop.ru_logo.png", "xcom-shop.ru" },
                    { new Guid("c078962f-c9b1-4bb4-b726-ed37e4a71097"), "Technoby.by_logo.png", "Technoby.by" },
                    { new Guid("e3bffdf2-edfa-4b79-bf84-ebabfaad90b5"), "allegro.pl_logo.png", "allegro.pl" }
                });

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
                columns: new[] { "Id", "ComponentRefId", "ContributorExcellenceId", "IsConfirmed", "Region", "ReviewRefId", "SubscriptionInfoId", "UserId" },
                values: new object[,]
                {
                    { new Guid("16584510-b499-4176-9f30-ba2b57b7106c"), null, new Guid("6b885476-dd6d-4c62-9bca-277d68cf09e3"), true, "Poland", null, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("0a3a8a9f-9bb3-4e06-a050-20c953855795") },
                    { new Guid("1ff4cd39-1db6-4494-84d7-268e131430c5"), null, new Guid("5e91bad7-6816-4f8d-a262-a7896207ca1f"), true, "Russia (Saint Petersburg)", null, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("bad8a170-deb8-44e7-965a-2f660079d5ed") },
                    { new Guid("203d006a-bdff-494e-94ad-8159c060f6bb"), null, new Guid("09202396-9388-47f2-9597-0afd53fce045"), true, "Belarus", null, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("274f801d-2117-48ff-96f7-ecb9b193bc7f") },
                    { new Guid("27abb845-e5a9-4b87-a253-843463b3a2cd"), null, new Guid("7ca46677-2a92-45d7-9e08-8866e17add0e"), true, "Russia (Moscow)", null, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("8bc0e747-443a-4f62-a05a-6e7d8cb1516f") },
                    { new Guid("74bf3426-0a22-4c7a-ba2a-478a95363d46"), null, new Guid("c078962f-c9b1-4bb4-b726-ed37e4a71097"), true, "Belarus", null, new Guid("751d41e0-ebfa-435a-867c-8f00de10465f"), new Guid("b0a3bda2-525d-42c0-b7ff-8b0f68b4ca84") },
                    { new Guid("7736fd1d-4967-4d00-9249-3a384c0d4686"), null, new Guid("36142d8b-19e3-4a18-984a-459cf0c3d294"), true, "Russia (Moscow)", null, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("17f87d98-17f0-4708-a7ff-0cb4ec09b58a") },
                    { new Guid("943e86a4-9322-4050-81a0-705eeaf9aee3"), null, new Guid("e3bffdf2-edfa-4b79-bf84-ebabfaad90b5"), true, "Poland", null, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("67bfe5a9-28e2-4c55-8549-888556d2a670") },
                    { new Guid("9c3436f0-0d5e-4295-9da8-9d61674bc14b"), null, new Guid("35be6d3e-8570-4e64-8575-f158cbd2eb2e"), true, "Russia (Moscow)", null, new Guid("751d41e0-ebfa-435a-867c-8f00de10465f"), new Guid("ffcd6b86-9327-4b7a-b2ad-ec13cf531d3f") },
                    { new Guid("a9963c4e-353d-4f47-82c0-39f1c9f90385"), null, new Guid("5bafa535-64fa-4885-b4d0-72673183e311"), true, "Russia (Saint Petersburg)", null, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("373ec651-0d88-45f4-90dd-4b2a98500ecb") },
                    { new Guid("cd5673e3-3614-4e5f-96fb-3052a6548e50"), null, new Guid("a3a10175-64b6-46e7-95bb-0481cdaa993a"), true, "Russia (Moscow)", null, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("02e91bcd-c2f5-4025-8f2f-5bac70b6924c") },
                    { new Guid("e5846ece-f28d-4fa1-82bb-390f3275b868"), null, new Guid("3d6e4c17-132d-4c2f-9a03-ece95c6903e6"), true, "Poland", null, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("34302079-5037-499e-8703-9920be62adf7") },
                    { new Guid("e61701f7-2c95-4986-bde9-0c18975e6cd6"), null, new Guid("95475ce0-2343-47bf-9774-b5786e6ae97d"), true, "Belarus", null, new Guid("cf7a198c-c551-456f-a519-e8679f3d0662"), new Guid("7c5086ea-4faf-4db2-91a4-c1217a2f3029") },
                    { new Guid("ef12555d-c912-402d-a045-148091680d9a"), null, new Guid("6a07d316-9c05-4feb-b93d-c3ed68e04b52"), true, "Belarus", null, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("a9caa7b2-109b-4c21-bc24-749ff87b9b18") },
                    { new Guid("fd88dbea-599f-43d5-858e-513cfcd80235"), null, new Guid("238dd562-0928-4791-bddc-f9bee6507eaf"), true, "Belarus", null, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("46f5064b-a6fe-4b58-b303-9ed344700195") }
                });

            migrationBuilder.InsertData(
                table: "SubscriptionInfo",
                columns: new[] { "Id", "ContributorId", "ExpiryDate", "PlanId", "RenewalDate" },
                values: new object[,]
                {
                    { new Guid("751d41e0-ebfa-435a-867c-8f00de10465f"), new Guid("74bf3426-0a22-4c7a-ba2a-478a95363d46"), new DateTime(2023, 7, 12, 12, 46, 18, 396, DateTimeKind.Local).AddTicks(6384), new Guid("d4c656b7-5a3d-4318-bbf9-37438750e542"), new DateTime(2023, 6, 12, 12, 46, 18, 396, DateTimeKind.Local).AddTicks(6383) },
                    { new Guid("9fb5a64e-8211-4a6a-835d-ece23056ff31"), new Guid("9c3436f0-0d5e-4295-9da8-9d61674bc14b"), new DateTime(2023, 7, 12, 12, 46, 18, 396, DateTimeKind.Local).AddTicks(6388), new Guid("d4c656b7-5a3d-4318-bbf9-37438750e542"), new DateTime(2023, 6, 12, 12, 46, 18, 396, DateTimeKind.Local).AddTicks(6388) },
                    { new Guid("cf7a198c-c551-456f-a519-e8679f3d0662"), new Guid("e61701f7-2c95-4986-bde9-0c18975e6cd6"), new DateTime(2023, 7, 12, 12, 46, 18, 396, DateTimeKind.Local).AddTicks(6379), new Guid("d4c656b7-5a3d-4318-bbf9-37438750e542"), new DateTime(2023, 6, 12, 12, 46, 18, 396, DateTimeKind.Local).AddTicks(6377) }
                });

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
