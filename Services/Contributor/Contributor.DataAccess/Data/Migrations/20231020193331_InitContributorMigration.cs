using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contributor.DataAccess.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitContributorMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatRooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    TimeStamp = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "10/20/2023 22:33:31")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatRooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContributorConfirmInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 10, 20, 22, 33, 31, 631, DateTimeKind.Local).AddTicks(8213))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContributorConfirmInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionPlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    DaysCount = table.Column<int>(type: "int", nullable: false, defaultValue: 30),
                    PriorityLevel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContributorExcellences",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    MainWebLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MainApiLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContributorExcellences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContributorExcellences_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ContributorExcellences_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionPlanInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RenewalDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 10, 20, 22, 33, 31, 633, DateTimeKind.Local).AddTicks(399)),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 11, 19, 22, 33, 31, 633, DateTimeKind.Local).AddTicks(887))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPlanInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionPlanInfos_SubscriptionPlans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "SubscriptionPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contributors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContributorConfirmInfoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ContributorExcellenceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubscriptionPlanInfoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contributors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contributors_ContributorConfirmInfos_ContributorConfirmInfoId",
                        column: x => x.ContributorConfirmInfoId,
                        principalTable: "ContributorConfirmInfos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Contributors_ContributorExcellences_ContributorExcellenceId",
                        column: x => x.ContributorExcellenceId,
                        principalTable: "ContributorExcellences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contributors_SubscriptionPlanInfos_SubscriptionPlanInfoId",
                        column: x => x.SubscriptionPlanInfoId,
                        principalTable: "SubscriptionPlanInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    IsEdited = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 10, 20, 22, 33, 31, 630, DateTimeKind.Local).AddTicks(3132)),
                    ChatRoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SenderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    ParticipantsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatRoomContributorModel", x => new { x.ChatRoomsId, x.ParticipantsId });
                    table.ForeignKey(
                        name: "FK_ChatRoomContributorModel_ChatRooms_ChatRoomsId",
                        column: x => x.ChatRoomsId,
                        principalTable: "ChatRooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatRoomContributorModel_Contributors_ParticipantsId",
                        column: x => x.ParticipantsId,
                        principalTable: "Contributors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_ChatRoomContributorModel_ParticipantsId",
                table: "ChatRoomContributorModel",
                column: "ParticipantsId");

            migrationBuilder.CreateIndex(
                name: "IX_ContributorExcellences_CurrencyId",
                table: "ContributorExcellences",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_ContributorExcellences_Name",
                table: "ContributorExcellences",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContributorExcellences_RegionId",
                table: "ContributorExcellences",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Contributors_ContributorConfirmInfoId",
                table: "Contributors",
                column: "ContributorConfirmInfoId",
                unique: true,
                filter: "[ContributorConfirmInfoId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Contributors_ContributorExcellenceId",
                table: "Contributors",
                column: "ContributorExcellenceId",
                unique: true,
                filter: "[ContributorExcellenceId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Contributors_SubscriptionPlanInfoId",
                table: "Contributors",
                column: "SubscriptionPlanInfoId",
                unique: true,
                filter: "[SubscriptionPlanInfoId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Contributors_UserId",
                table: "Contributors",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_Name",
                table: "Currencies",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPlanInfos_PlanId",
                table: "SubscriptionPlanInfos",
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
                name: "ChatRooms");

            migrationBuilder.DropTable(
                name: "Contributors");

            migrationBuilder.DropTable(
                name: "ContributorConfirmInfos");

            migrationBuilder.DropTable(
                name: "ContributorExcellences");

            migrationBuilder.DropTable(
                name: "SubscriptionPlanInfos");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "SubscriptionPlans");
        }
    }
}
