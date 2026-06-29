using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PaperaX.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCouponEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: -7);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: -6);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: -5);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: -4);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: -3);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.CreateTable(
                name: "Coupons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: false),
                    DiscountType = table.Column<string>(type: "text", nullable: false),
                    DiscountValue = table.Column<decimal>(type: "numeric", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ValidUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    TotalUsageLimit = table.Column<int>(type: "integer", nullable: true),
                    CurrentUsageCount = table.Column<int>(type: "integer", nullable: false),
                    LimitPerUser = table.Column<int>(type: "integer", nullable: true),
                    MinimumOrderValue = table.Column<decimal>(type: "numeric", nullable: true),
                    MaximumDiscountAmount = table.Column<decimal>(type: "numeric", nullable: true),
                    FirstTimeOnly = table.Column<bool>(type: "boolean", nullable: false),
                    ApplicableCategories = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupons", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Coupons_Code",
                table: "Coupons",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coupons");

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CreatedAt", "CustomerName", "DateString", "DeliveryMethod", "Destination", "EstDelivery", "ItemSummary", "ItemsJson", "Payment", "Status", "StringId", "TotalAmount", "TotalString", "UserId" },
                values: new object[,]
                {
                    { -7, new DateTime(2026, 6, 17, 8, 50, 0, 864, DateTimeKind.Utc).AddTicks(5894), "Rohan J.", "22 Apr, 11:00 AM", "", "", "", "", "", "Card", "Delivered", "#PX-105", 0m, "?8,500", 0 },
                    { -6, new DateTime(2026, 6, 17, 8, 50, 0, 864, DateTimeKind.Utc).AddTicks(5892), "Global Export", "23 Apr, 04:20 PM", "", "", "", "", "", "Wire", "Pending", "#PX-104", 0m, "?4,20,000", 0 },
                    { -5, new DateTime(2026, 6, 17, 8, 50, 0, 864, DateTimeKind.Utc).AddTicks(5889), "Meera K.", "23 Apr, 06:45 PM", "", "", "", "", "", "COD", "Delivered", "#PX-103", 0m, "?1,200", 0 },
                    { -4, new DateTime(2026, 6, 17, 8, 50, 0, 864, DateTimeKind.Utc).AddTicks(5887), "B2B: Paper Solutions", "24 Apr, 09:15 AM", "", "", "", "", "", "NEFT", "Shipped", "#PX-102", 0m, "?85,000", 0 },
                    { -3, new DateTime(2026, 6, 17, 8, 50, 0, 864, DateTimeKind.Utc).AddTicks(5884), "Aarav S.", "24 Apr, 10:30 AM", "", "", "", "", "", "UPI", "Processing", "#PX-101", 0m, "?4,500", 0 },
                    { -2, new DateTime(2026, 6, 17, 8, 50, 0, 864, DateTimeKind.Utc).AddTicks(5881), "", "Apr 28, 2026", "", "Logistics Center, Whitefield, BLR", "May 02, 2026", "Premium Specialty Kraft Rolls", "[{\"name\":\"Premium Specialty Kraft Rolls\",\"qty\":\"4 Rolls\",\"price\":\"?32,500\"}]", "", "delivered", "#PX-ORDER-4811", 0m, "?32,500", 0 },
                    { -1, new DateTime(2026, 6, 17, 8, 50, 0, 864, DateTimeKind.Utc).AddTicks(5874), "", "May 14, 2026", "", "Main Corporate Hub, Hassan Industrial Area, KA", "May 19, 2026", "JK Excel Bond A4 +1 more", "[{\"name\":\"JK Excel Bond A4 Office Paper\",\"qty\":\"20 Boxes\",\"price\":\"?44,000\"},{\"name\":\"Double Coated Art Sheet (250GSM)\",\"qty\":\"2 Packs\",\"price\":\"?10,000\"}]", "", "shipped", "#PX-ORDER-4921", 0m, "?54,000", 0 }
                });
        }
    }
}
