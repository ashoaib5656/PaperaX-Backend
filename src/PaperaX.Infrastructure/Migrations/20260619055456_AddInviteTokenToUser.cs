using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaperaX.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddInviteTokenToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InviteToken",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InviteTokenExpiry",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InviteToken",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "InviteTokenExpiry",
                table: "Users");
        }
    }
}
