using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PaperaX.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPromotionsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Promotions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CampaignName = table.Column<string>(type: "text", nullable: false),
                    PromotionType = table.Column<string>(type: "text", nullable: false),
                    DiscountValue = table.Column<decimal>(type: "numeric", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    AppliesTo = table.Column<string>(type: "text", nullable: false),
                    ApplicableCategories = table.Column<string>(type: "text", nullable: true),
                    ApplicableProducts = table.Column<string>(type: "text", nullable: true),
                    MinimumOrderValue = table.Column<decimal>(type: "numeric", nullable: true),
                    MaximumDiscountAmount = table.Column<decimal>(type: "numeric", nullable: true),
                    Priority = table.Column<int>(type: "integer", nullable: true),
                    BannerImage = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotions", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Promotions");
        }
    }
}
