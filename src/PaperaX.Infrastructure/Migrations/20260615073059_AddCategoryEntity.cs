using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PaperaX.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Slug = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Description", "IsActive", "Name", "Slug", "UpdatedAt" },
                values: new object[,]
                {
                    { -5, new DateTime(2026, 6, 15, 7, 30, 57, 741, DateTimeKind.Utc).AddTicks(4100), "Notebooks, diaries, and refills", true, "Notebooks", "notebooks", null },
                    { -4, new DateTime(2026, 6, 15, 7, 30, 57, 741, DateTimeKind.Utc).AddTicks(4099), "Kraft and packaging solutions", true, "Packaging", "packaging", null },
                    { -3, new DateTime(2026, 6, 15, 7, 30, 57, 741, DateTimeKind.Utc).AddTicks(4097), "Specialty printing and photo papers", true, "Specialty", "specialty", null },
                    { -2, new DateTime(2026, 6, 15, 7, 30, 57, 741, DateTimeKind.Utc).AddTicks(4095), "Large A3 size printing papers", true, "A3 Paper", "a3-paper", null },
                    { -1, new DateTime(2026, 6, 15, 7, 30, 57, 741, DateTimeKind.Utc).AddTicks(3984), "Standard A4 size copier papers", true, "A4 Paper", "a4-paper", null }
                });

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
                table: "Products",
                keyColumn: "Id",
                keyValue: -8,
                column: "CategoryId",
                value: -3);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -7,
                column: "CategoryId",
                value: -5);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -6,
                column: "CategoryId",
                value: -3);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -5,
                column: "CategoryId",
                value: -4);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -4,
                column: "CategoryId",
                value: -3);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -3,
                column: "CategoryId",
                value: -2);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -2,
                column: "CategoryId",
                value: -1);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -1,
                column: "CategoryId",
                value: -1);

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

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Products",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: -7,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 11, 4, 49, 36, 952, DateTimeKind.Utc).AddTicks(7842));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: -6,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 11, 4, 49, 36, 952, DateTimeKind.Utc).AddTicks(7838));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: -5,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 11, 4, 49, 36, 952, DateTimeKind.Utc).AddTicks(7830));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: -4,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 11, 4, 49, 36, 952, DateTimeKind.Utc).AddTicks(7826));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: -3,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 11, 4, 49, 36, 952, DateTimeKind.Utc).AddTicks(7820));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: -2,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 11, 4, 49, 36, 952, DateTimeKind.Utc).AddTicks(7816));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: -1,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 11, 4, 49, 36, 952, DateTimeKind.Utc).AddTicks(7807));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -8,
                column: "Category",
                value: "Specialty");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -7,
                column: "Category",
                value: "Notebooks");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -6,
                column: "Category",
                value: "Specialty");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -5,
                column: "Category",
                value: "Packaging");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -4,
                column: "Category",
                value: "Specialty");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -3,
                column: "Category",
                value: "A3 Paper");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -2,
                column: "Category",
                value: "A4 Paper");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -1,
                column: "Category",
                value: "A4 Paper");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -8,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 11, 4, 49, 36, 952, DateTimeKind.Utc).AddTicks(6968));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -7,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 11, 4, 49, 36, 952, DateTimeKind.Utc).AddTicks(6959));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -6,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 11, 4, 49, 36, 952, DateTimeKind.Utc).AddTicks(6955));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -5,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 11, 4, 49, 36, 952, DateTimeKind.Utc).AddTicks(6949));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -4,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 11, 4, 49, 36, 952, DateTimeKind.Utc).AddTicks(6944));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -3,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 11, 4, 49, 36, 952, DateTimeKind.Utc).AddTicks(6939));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 11, 4, 49, 36, 952, DateTimeKind.Utc).AddTicks(6933));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -1,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 11, 4, 49, 36, 952, DateTimeKind.Utc).AddTicks(6917));
        }
    }
}
