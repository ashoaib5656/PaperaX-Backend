using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PaperaX.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDummyCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: -5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: -4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: -3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: -1);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Code", "CreatedAt", "Description", "DisplayOrder", "ImageUrl", "IsActive", "Name", "Slug", "UpdatedAt" },
                values: new object[,]
                {
                    { -5, "", new DateTime(2026, 6, 15, 9, 28, 19, 73, DateTimeKind.Utc).AddTicks(9873), "Notebooks, diaries, and refills", 0, null, true, "Notebooks", "notebooks", null },
                    { -4, "", new DateTime(2026, 6, 15, 9, 28, 19, 73, DateTimeKind.Utc).AddTicks(9870), "Kraft and packaging solutions", 0, null, true, "Packaging", "packaging", null },
                    { -3, "", new DateTime(2026, 6, 15, 9, 28, 19, 73, DateTimeKind.Utc).AddTicks(9868), "Specialty printing and photo papers", 0, null, true, "Specialty", "specialty", null },
                    { -2, "", new DateTime(2026, 6, 15, 9, 28, 19, 73, DateTimeKind.Utc).AddTicks(9865), "Large A3 size printing papers", 0, null, true, "A3 Paper", "a3-paper", null },
                    { -1, "", new DateTime(2026, 6, 15, 9, 28, 19, 73, DateTimeKind.Utc).AddTicks(9858), "Standard A4 size copier papers", 0, null, true, "A4 Paper", "a4-paper", null }
                });

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

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "GSM", "ImageUrl", "Name", "Price", "Status", "StockQuantity" },
                values: new object[,]
                {
                    { -8, -3, "", 55, "", "Thermal Receipt Rolls", 50m, "Low Stock", 100 },
                    { -7, -5, "", 70, "", "A5 Notebook Refills", 120m, "In Stock", 100 },
                    { -6, -3, "", 250, "", "Cardstock Heavy", 800m, "In Stock", 100 },
                    { -5, -4, "", 100, "", "Recycled Kraft Paper", 150m, "Low Stock", 100 },
                    { -4, -3, "", 180, "", "Photo Glossy Paper", 600m, "In Stock", 100 },
                    { -3, -2, "", 80, "", "A3 Executive Paper", 450m, "In Stock", 100 },
                    { -2, -1, "", 80, "", "Color Print Paper (Assorted)", 300m, "Low Stock", 100 },
                    { -1, -1, "", 75, "", "Premium A4 Copier Paper", 250m, "In Stock", 100 }
                });
        }
    }
}
