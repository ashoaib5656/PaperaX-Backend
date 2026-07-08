using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PaperaX.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddVariantGroupToBanner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VariantGroup",
                table: "Banners",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Conversions",
                table: "BannerAnalytics",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "RevenueGenerated",
                table: "BannerAnalytics",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "BannerAssets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BannerId = table.Column<int>(type: "integer", nullable: false),
                    DesktopImageUrl = table.Column<string>(type: "text", nullable: true),
                    TabletImageUrl = table.Column<string>(type: "text", nullable: true),
                    MobileImageUrl = table.Column<string>(type: "text", nullable: true),
                    VideoUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BannerAssets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BannerAssets_Banners_BannerId",
                        column: x => x.BannerId,
                        principalTable: "Banners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BannerTargetingRules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BannerId = table.Column<int>(type: "integer", nullable: false),
                    MinCartValue = table.Column<decimal>(type: "numeric", nullable: true),
                    DeviceTarget = table.Column<string>(type: "text", nullable: true),
                    CountryTarget = table.Column<string>(type: "text", nullable: true),
                    MaxViewsPerUser = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BannerTargetingRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BannerTargetingRules_Banners_BannerId",
                        column: x => x.BannerId,
                        principalTable: "Banners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BannerVersions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BannerId = table.Column<int>(type: "integer", nullable: false),
                    VersionNumber = table.Column<int>(type: "integer", nullable: false),
                    SnapshotJson = table.Column<string>(type: "text", nullable: false),
                    ChangedBy = table.Column<string>(type: "text", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BannerVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BannerVersions_Banners_BannerId",
                        column: x => x.BannerId,
                        principalTable: "Banners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BannerAssets_BannerId",
                table: "BannerAssets",
                column: "BannerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BannerTargetingRules_BannerId",
                table: "BannerTargetingRules",
                column: "BannerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BannerVersions_BannerId",
                table: "BannerVersions",
                column: "BannerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BannerAssets");

            migrationBuilder.DropTable(
                name: "BannerTargetingRules");

            migrationBuilder.DropTable(
                name: "BannerVersions");

            migrationBuilder.DropColumn(
                name: "VariantGroup",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "Conversions",
                table: "BannerAnalytics");

            migrationBuilder.DropColumn(
                name: "RevenueGenerated",
                table: "BannerAnalytics");
        }
    }
}
