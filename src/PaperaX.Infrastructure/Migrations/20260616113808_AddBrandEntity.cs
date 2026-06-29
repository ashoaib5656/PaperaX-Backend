using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PaperaX.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBrandEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    LogoUrl = table.Column<string>(type: "text", nullable: true),
                    WebsiteUrl = table.Column<string>(type: "text", nullable: true),
                    ContactEmail = table.Column<string>(type: "text", nullable: true),
                    ContactPhone = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: -7,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 16, 11, 38, 6, 112, DateTimeKind.Utc).AddTicks(8360));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: -6,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 16, 11, 38, 6, 112, DateTimeKind.Utc).AddTicks(8355));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: -5,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 16, 11, 38, 6, 112, DateTimeKind.Utc).AddTicks(8351));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: -4,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 16, 11, 38, 6, 112, DateTimeKind.Utc).AddTicks(8346));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: -3,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 16, 11, 38, 6, 112, DateTimeKind.Utc).AddTicks(8341));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: -2,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 16, 11, 38, 6, 112, DateTimeKind.Utc).AddTicks(8335));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: -1,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 16, 11, 38, 6, 112, DateTimeKind.Utc).AddTicks(8325));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -8,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 16, 11, 38, 6, 112, DateTimeKind.Utc).AddTicks(7801));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -7,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 16, 11, 38, 6, 112, DateTimeKind.Utc).AddTicks(7611));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -6,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 16, 11, 38, 6, 112, DateTimeKind.Utc).AddTicks(7603));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -5,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 16, 11, 38, 6, 112, DateTimeKind.Utc).AddTicks(7596));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -4,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 16, 11, 38, 6, 112, DateTimeKind.Utc).AddTicks(7590));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -3,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 16, 11, 38, 6, 112, DateTimeKind.Utc).AddTicks(7583));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 16, 11, 38, 6, 112, DateTimeKind.Utc).AddTicks(7574));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -1,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 16, 11, 38, 6, 112, DateTimeKind.Utc).AddTicks(7557));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: -7,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 15, 9, 36, 15, 511, DateTimeKind.Utc).AddTicks(123));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: -6,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 15, 9, 36, 15, 511, DateTimeKind.Utc).AddTicks(119));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: -5,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 15, 9, 36, 15, 511, DateTimeKind.Utc).AddTicks(116));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: -4,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 15, 9, 36, 15, 511, DateTimeKind.Utc).AddTicks(112));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: -3,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 15, 9, 36, 15, 511, DateTimeKind.Utc).AddTicks(109));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: -2,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 15, 9, 36, 15, 511, DateTimeKind.Utc).AddTicks(105));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: -1,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 15, 9, 36, 15, 511, DateTimeKind.Utc).AddTicks(99));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -8,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 15, 9, 36, 15, 510, DateTimeKind.Utc).AddTicks(9737));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -7,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 15, 9, 36, 15, 510, DateTimeKind.Utc).AddTicks(9733));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -6,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 15, 9, 36, 15, 510, DateTimeKind.Utc).AddTicks(9728));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -5,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 15, 9, 36, 15, 510, DateTimeKind.Utc).AddTicks(9723));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -4,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 15, 9, 36, 15, 510, DateTimeKind.Utc).AddTicks(9718));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -3,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 15, 9, 36, 15, 510, DateTimeKind.Utc).AddTicks(9713));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 15, 9, 36, 15, 510, DateTimeKind.Utc).AddTicks(9707));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -1,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 15, 9, 36, 15, 510, DateTimeKind.Utc).AddTicks(9692));
        }
    }
}
