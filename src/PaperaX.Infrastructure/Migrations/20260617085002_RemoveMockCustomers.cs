using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PaperaX.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveMockCustomers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: -7,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 17, 8, 50, 0, 864, DateTimeKind.Utc).AddTicks(5894));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: -6,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 17, 8, 50, 0, 864, DateTimeKind.Utc).AddTicks(5892));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: -5,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 17, 8, 50, 0, 864, DateTimeKind.Utc).AddTicks(5889));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: -4,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 17, 8, 50, 0, 864, DateTimeKind.Utc).AddTicks(5887));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: -3,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 17, 8, 50, 0, 864, DateTimeKind.Utc).AddTicks(5884));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: -2,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 17, 8, 50, 0, 864, DateTimeKind.Utc).AddTicks(5881));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: -1,
                column: "CreatedAt",
                value: new DateTime(2026, 6, 17, 8, 50, 0, 864, DateTimeKind.Utc).AddTicks(5874));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Company", "CreatedAt", "Email", "FullName", "GoogleId", "IsEmailVerified", "OrdersCount", "PasswordHash", "Phone", "RefreshToken", "RefreshTokenExpiryTime", "Role", "Status", "TotalSpent", "Type" },
                values: new object[,]
                {
                    { -8, "N/A", new DateTime(2026, 6, 16, 11, 38, 6, 112, DateTimeKind.Utc).AddTicks(7801), "pooja.h@outlook.com", "Pooja Hegde", null, true, 3, "", "+91 99887 76655", null, null, "Customer", "Active", 8900m, "Retail" },
                    { -7, "Birla Publications", new DateTime(2026, 6, 16, 11, 38, 6, 112, DateTimeKind.Utc).AddTicks(7611), "aditya@birlapub.com", "Aditya Birla", null, true, 32, "", "+91 22 8877 6655", null, null, "Customer", "Active", 1890000m, "B2B" },
                    { -6, "Creative Prints", new DateTime(2026, 6, 16, 11, 38, 6, 112, DateTimeKind.Utc).AddTicks(7603), "ananya@creativeprints.in", "Ananya Sen", null, true, 18, "", "+91 33 2445 6677", null, null, "Customer", "Active", 340000m, "B2B" },
                    { -5, "N/A", new DateTime(2026, 6, 16, 11, 38, 6, 112, DateTimeKind.Utc).AddTicks(7596), "rohan.j@rediffmail.com", "Rohan Joshi", null, true, 9, "", "+91 76543 21098", null, null, "Customer", "Inactive", 76500m, "Retail" },
                    { -4, "Global Export Inc.", new DateTime(2026, 6, 16, 11, 38, 6, 112, DateTimeKind.Utc).AddTicks(7590), "j.doe@globalexport.com", "John Doe", null, true, 8, "", "+1 415 555 2671", null, null, "Customer", "Active", 1420000m, "B2B" },
                    { -3, "N/A", new DateTime(2026, 6, 16, 11, 38, 6, 112, DateTimeKind.Utc).AddTicks(7583), "meera.k@yahoo.com", "Meera Kapoor", null, true, 5, "", "+91 87654 32109", null, null, "Customer", "Active", 12400m, "Retail" },
                    { -2, "Paper Solutions Ltd", new DateTime(2026, 6, 16, 11, 38, 6, 112, DateTimeKind.Utc).AddTicks(7574), "v.mehta@papersolutions.com", "Vikram Mehta", null, true, 24, "", "+91 22 4567 8901", null, null, "Customer", "Active", 850000m, "B2B" },
                    { -1, "N/A", new DateTime(2026, 6, 16, 11, 38, 6, 112, DateTimeKind.Utc).AddTicks(7557), "aarav.sharma@gmail.com", "Aarav Sharma", null, true, 12, "", "+91 98765 43210", null, null, "Customer", "Active", 45000m, "Retail" }
                });
        }
    }
}
