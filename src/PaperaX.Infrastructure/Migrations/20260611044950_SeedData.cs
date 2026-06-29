using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PaperaX.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "OrdersCount",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalSpent",
                table: "Users",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Products",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DateString",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Destination",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EstDelivery",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ItemSummary",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ItemsJson",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Payment",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StringId",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TotalString",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CreatedAt", "CustomerName", "DateString", "DeliveryMethod", "Destination", "EstDelivery", "ItemSummary", "ItemsJson", "Payment", "Status", "StringId", "TotalAmount", "TotalString", "UserId" },
                values: new object[,]
                {
                    { -7, new DateTime(2026, 6, 11, 4, 49, 36, 952, DateTimeKind.Utc).AddTicks(7842), "Rohan J.", "22 Apr, 11:00 AM", "", "", "", "", "", "Card", "Delivered", "#PX-105", 0m, "?8,500", 0 },
                    { -6, new DateTime(2026, 6, 11, 4, 49, 36, 952, DateTimeKind.Utc).AddTicks(7838), "Global Export", "23 Apr, 04:20 PM", "", "", "", "", "", "Wire", "Pending", "#PX-104", 0m, "?4,20,000", 0 },
                    { -5, new DateTime(2026, 6, 11, 4, 49, 36, 952, DateTimeKind.Utc).AddTicks(7830), "Meera K.", "23 Apr, 06:45 PM", "", "", "", "", "", "COD", "Delivered", "#PX-103", 0m, "?1,200", 0 },
                    { -4, new DateTime(2026, 6, 11, 4, 49, 36, 952, DateTimeKind.Utc).AddTicks(7826), "B2B: Paper Solutions", "24 Apr, 09:15 AM", "", "", "", "", "", "NEFT", "Shipped", "#PX-102", 0m, "?85,000", 0 },
                    { -3, new DateTime(2026, 6, 11, 4, 49, 36, 952, DateTimeKind.Utc).AddTicks(7820), "Aarav S.", "24 Apr, 10:30 AM", "", "", "", "", "", "UPI", "Processing", "#PX-101", 0m, "?4,500", 0 },
                    { -2, new DateTime(2026, 6, 11, 4, 49, 36, 952, DateTimeKind.Utc).AddTicks(7816), "", "Apr 28, 2026", "", "Logistics Center, Whitefield, BLR", "May 02, 2026", "Premium Specialty Kraft Rolls", "[{\"name\":\"Premium Specialty Kraft Rolls\",\"qty\":\"4 Rolls\",\"price\":\"?32,500\"}]", "", "delivered", "#PX-ORDER-4811", 0m, "?32,500", 0 },
                    { -1, new DateTime(2026, 6, 11, 4, 49, 36, 952, DateTimeKind.Utc).AddTicks(7807), "", "May 14, 2026", "", "Main Corporate Hub, Hassan Industrial Area, KA", "May 19, 2026", "JK Excel Bond A4 +1 more", "[{\"name\":\"JK Excel Bond A4 Office Paper\",\"qty\":\"20 Boxes\",\"price\":\"?44,000\"},{\"name\":\"Double Coated Art Sheet (250GSM)\",\"qty\":\"2 Packs\",\"price\":\"?10,000\"}]", "", "shipped", "#PX-ORDER-4921", 0m, "?54,000", 0 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Description", "GSM", "ImageUrl", "Name", "Price", "Status", "StockQuantity" },
                values: new object[,]
                {
                    { -8, "Specialty", "", 55, "", "Thermal Receipt Rolls", 50m, "Low Stock", 100 },
                    { -7, "Notebooks", "", 70, "", "A5 Notebook Refills", 120m, "In Stock", 100 },
                    { -6, "Specialty", "", 250, "", "Cardstock Heavy", 800m, "In Stock", 100 },
                    { -5, "Packaging", "", 100, "", "Recycled Kraft Paper", 150m, "Low Stock", 100 },
                    { -4, "Specialty", "", 180, "", "Photo Glossy Paper", 600m, "In Stock", 100 },
                    { -3, "A3 Paper", "", 80, "", "A3 Executive Paper", 450m, "In Stock", 100 },
                    { -2, "A4 Paper", "", 80, "", "Color Print Paper (Assorted)", 300m, "Low Stock", 100 },
                    { -1, "A4 Paper", "", 75, "", "Premium A4 Copier Paper", 250m, "In Stock", 100 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Company", "CreatedAt", "Email", "FullName", "GoogleId", "IsEmailVerified", "OrdersCount", "PasswordHash", "Phone", "RefreshToken", "RefreshTokenExpiryTime", "Role", "Status", "TotalSpent", "Type" },
                values: new object[,]
                {
                    { -8, "N/A", new DateTime(2026, 6, 11, 4, 49, 36, 952, DateTimeKind.Utc).AddTicks(6968), "pooja.h@outlook.com", "Pooja Hegde", null, true, 3, "", "+91 99887 76655", null, null, "Customer", "Active", 8900m, "Retail" },
                    { -7, "Birla Publications", new DateTime(2026, 6, 11, 4, 49, 36, 952, DateTimeKind.Utc).AddTicks(6959), "aditya@birlapub.com", "Aditya Birla", null, true, 32, "", "+91 22 8877 6655", null, null, "Customer", "Active", 1890000m, "B2B" },
                    { -6, "Creative Prints", new DateTime(2026, 6, 11, 4, 49, 36, 952, DateTimeKind.Utc).AddTicks(6955), "ananya@creativeprints.in", "Ananya Sen", null, true, 18, "", "+91 33 2445 6677", null, null, "Customer", "Active", 340000m, "B2B" },
                    { -5, "N/A", new DateTime(2026, 6, 11, 4, 49, 36, 952, DateTimeKind.Utc).AddTicks(6949), "rohan.j@rediffmail.com", "Rohan Joshi", null, true, 9, "", "+91 76543 21098", null, null, "Customer", "Inactive", 76500m, "Retail" },
                    { -4, "Global Export Inc.", new DateTime(2026, 6, 11, 4, 49, 36, 952, DateTimeKind.Utc).AddTicks(6944), "j.doe@globalexport.com", "John Doe", null, true, 8, "", "+1 415 555 2671", null, null, "Customer", "Active", 1420000m, "B2B" },
                    { -3, "N/A", new DateTime(2026, 6, 11, 4, 49, 36, 952, DateTimeKind.Utc).AddTicks(6939), "meera.k@yahoo.com", "Meera Kapoor", null, true, 5, "", "+91 87654 32109", null, null, "Customer", "Active", 12400m, "Retail" },
                    { -2, "Paper Solutions Ltd", new DateTime(2026, 6, 11, 4, 49, 36, 952, DateTimeKind.Utc).AddTicks(6933), "v.mehta@papersolutions.com", "Vikram Mehta", null, true, 24, "", "+91 22 4567 8901", null, null, "Customer", "Active", 850000m, "B2B" },
                    { -1, "N/A", new DateTime(2026, 6, 11, 4, 49, 36, 952, DateTimeKind.Utc).AddTicks(6917), "aarav.sharma@gmail.com", "Aarav Sharma", null, true, 12, "", "+91 98765 43210", null, null, "Customer", "Active", 45000m, "Retail" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                table: "Users",
                keyColumn: "Id",
                keyValue: -8);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -7);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -6);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.DropColumn(
                name: "Company",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "OrdersCount",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TotalSpent",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DateString",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Destination",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "EstDelivery",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ItemSummary",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ItemsJson",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Payment",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "StringId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TotalString",
                table: "Orders");
        }
    }
}
