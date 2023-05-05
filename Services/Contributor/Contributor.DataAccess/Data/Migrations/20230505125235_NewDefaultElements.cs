using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contributor.DataAccess.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewDefaultElements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "RenewalDate",
                table: "SubscriptionInfo",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 5, 15, 52, 35, 512, DateTimeKind.Local).AddTicks(7437),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 4, 12, 18, 11, 10, 251, DateTimeKind.Local).AddTicks(9664));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiryDate",
                table: "SubscriptionInfo",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 4, 15, 52, 35, 512, DateTimeKind.Local).AddTicks(7810),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 12, 18, 11, 10, 251, DateTimeKind.Local).AddTicks(9894));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "ChatMessages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 5, 15, 52, 35, 510, DateTimeKind.Local).AddTicks(4601),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 4, 12, 18, 11, 10, 250, DateTimeKind.Local).AddTicks(4552));

            migrationBuilder.UpdateData(
                table: "ContributorExcellences",
                keyColumn: "Id",
                keyValue: new Guid("3f46062f-56d8-4897-a37f-ff4e920b2d73"),
                columns: new[] { "Logo", "Name" },
                values: new object[] { "newegg.com.png", "NewEgg.com" });

            migrationBuilder.UpdateData(
                table: "SubscriptionInfo",
                keyColumn: "Id",
                keyValue: new Guid("cf7a198c-c551-456f-a519-e8679f3d0662"),
                columns: new[] { "ExpiryDate", "RenewalDate" },
                values: new object[] { new DateTime(2023, 6, 4, 15, 52, 35, 512, DateTimeKind.Local).AddTicks(8040), new DateTime(2023, 5, 5, 15, 52, 35, 512, DateTimeKind.Local).AddTicks(8039) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "RenewalDate",
                table: "SubscriptionInfo",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 12, 18, 11, 10, 251, DateTimeKind.Local).AddTicks(9664),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 5, 15, 52, 35, 512, DateTimeKind.Local).AddTicks(7437));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiryDate",
                table: "SubscriptionInfo",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 12, 18, 11, 10, 251, DateTimeKind.Local).AddTicks(9894),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 6, 4, 15, 52, 35, 512, DateTimeKind.Local).AddTicks(7810));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "ChatMessages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 12, 18, 11, 10, 250, DateTimeKind.Local).AddTicks(4552),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 5, 15, 52, 35, 510, DateTimeKind.Local).AddTicks(4601));

            migrationBuilder.UpdateData(
                table: "ContributorExcellences",
                keyColumn: "Id",
                keyValue: new Guid("3f46062f-56d8-4897-a37f-ff4e920b2d73"),
                columns: new[] { "Logo", "Name" },
                values: new object[] { "", "Test Name" });

            migrationBuilder.UpdateData(
                table: "SubscriptionInfo",
                keyColumn: "Id",
                keyValue: new Guid("cf7a198c-c551-456f-a519-e8679f3d0662"),
                columns: new[] { "ExpiryDate", "RenewalDate" },
                values: new object[] { new DateTime(2023, 5, 12, 18, 11, 10, 252, DateTimeKind.Local).AddTicks(144), new DateTime(2023, 4, 12, 18, 11, 10, 252, DateTimeKind.Local).AddTicks(143) });
        }
    }
}
