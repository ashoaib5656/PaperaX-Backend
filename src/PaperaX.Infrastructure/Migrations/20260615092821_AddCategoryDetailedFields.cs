using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaperaX.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryDetailedFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Categories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "Categories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Categories",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: -5,
                columns: new[] { "Code", "CreatedAt", "DisplayOrder", "ImageUrl" },
                values: new object[] { "", new DateTime(2026, 6, 15, 9, 28, 19, 73, DateTimeKind.Utc).AddTicks(9873), 0, null });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: -4,
                columns: new[] { "Code", "CreatedAt", "DisplayOrder", "ImageUrl" },
                values: new object[] { "", new DateTime(2026, 6, 15, 9, 28, 19, 73, DateTimeKind.Utc).AddTicks(9870), 0, null });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: -3,
                columns: new[] { "Code", "CreatedAt", "DisplayOrder", "ImageUrl" },
                values: new object[] { "", new DateTime(2026, 6, 15, 9, 28, 19, 73, DateTimeKind.Utc).AddTicks(9868), 0, null });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: -2,
                columns: new[] { "Code", "CreatedAt", "DisplayOrder", "ImageUrl" },
                values: new object[] { "", new DateTime(2026, 6, 15, 9, 28, 19, 73, DateTimeKind.Utc).AddTicks(9865), 0, null });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "Code", "CreatedAt", "DisplayOrder", "ImageUrl" },
                values: new object[] { "", new DateTime(2026, 6, 15, 9, 28, 19, 73, DateTimeKind.Utc).AddTicks(9858), 0, null });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: -7,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 15, 9, 28, 19, 74, DateTimeKind.Utc).AddTicks(14));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: -6,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 15, 9, 28, 19, 74, DateTimeKind.Utc).AddTicks(11));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: -5,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 15, 9, 28, 19, 74, DateTimeKind.Utc).AddTicks(8));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: -4,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 15, 9, 28, 19, 74, DateTimeKind.Utc).AddTicks(5));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: -3,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 15, 9, 28, 19, 74, DateTimeKind.Utc).AddTicks(2));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: -2,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 15, 9, 28, 19, 73, DateTimeKind.Utc).AddTicks(9999));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: -1,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 15, 9, 28, 19, 73, DateTimeKind.Utc).AddTicks(9995));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -8,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 15, 9, 28, 19, 73, DateTimeKind.Utc).AddTicks(9446));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -7,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 15, 9, 28, 19, 73, DateTimeKind.Utc).AddTicks(9442));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -6,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 15, 9, 28, 19, 73, DateTimeKind.Utc).AddTicks(9438));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -5,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 15, 9, 28, 19, 73, DateTimeKind.Utc).AddTicks(9434));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -4,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 15, 9, 28, 19, 73, DateTimeKind.Utc).AddTicks(9430));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -3,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 15, 9, 28, 19, 73, DateTimeKind.Utc).AddTicks(9425));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 15, 9, 28, 19, 73, DateTimeKind.Utc).AddTicks(9421));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -1,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 15, 9, 28, 19, 73, DateTimeKind.Utc).AddTicks(9411));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Categories");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: -5,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 15, 7, 30, 57, 741, DateTimeKind.Utc).AddTicks(4100));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: -4,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 15, 7, 30, 57, 741, DateTimeKind.Utc).AddTicks(4099));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: -3,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 15, 7, 30, 57, 741, DateTimeKind.Utc).AddTicks(4097));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: -2,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 15, 7, 30, 57, 741, DateTimeKind.Utc).AddTicks(4095));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: -1,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 15, 7, 30, 57, 741, DateTimeKind.Utc).AddTicks(3984));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: -7,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 15, 7, 30, 57, 741, DateTimeKind.Utc).AddTicks(4223));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: -6,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 15, 7, 30, 57, 741, DateTimeKind.Utc).AddTicks(4221));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: -5,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 15, 7, 30, 57, 741, DateTimeKind.Utc).AddTicks(4219));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: -4,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 15, 7, 30, 57, 741, DateTimeKind.Utc).AddTicks(4217));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: -3,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 15, 7, 30, 57, 741, DateTimeKind.Utc).AddTicks(4214));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: -2,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 15, 7, 30, 57, 741, DateTimeKind.Utc).AddTicks(4210));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: -1,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 15, 7, 30, 57, 741, DateTimeKind.Utc).AddTicks(4206));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -8,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 15, 7, 30, 57, 741, DateTimeKind.Utc).AddTicks(3775));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -7,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 15, 7, 30, 57, 741, DateTimeKind.Utc).AddTicks(3771));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -6,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 15, 7, 30, 57, 741, DateTimeKind.Utc).AddTicks(3768));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -5,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 15, 7, 30, 57, 741, DateTimeKind.Utc).AddTicks(3761));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -4,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 15, 7, 30, 57, 741, DateTimeKind.Utc).AddTicks(3758));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -3,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 15, 7, 30, 57, 741, DateTimeKind.Utc).AddTicks(3754));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 15, 7, 30, 57, 741, DateTimeKind.Utc).AddTicks(3749));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -1,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 15, 7, 30, 57, 741, DateTimeKind.Utc).AddTicks(3740));
        }
    }
}
