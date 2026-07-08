using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaperaX.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBannerEntityForCMS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Banners",
                newName: "TargetAudience");

            migrationBuilder.AddColumn<string>(
                name: "BackgroundColor",
                table: "Banners",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BannerType",
                table: "Banners",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ButtonStyle",
                table: "Banners",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CtaLink",
                table: "Banners",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CtaText",
                table: "Banners",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Banners",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "Banners",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PromotionId",
                table: "Banners",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Subtitle",
                table: "Banners",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TextColor",
                table: "Banners",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Banners_PromotionId",
                table: "Banners",
                column: "PromotionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Banners_Promotions_PromotionId",
                table: "Banners",
                column: "PromotionId",
                principalTable: "Promotions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Banners_Promotions_PromotionId",
                table: "Banners");

            migrationBuilder.DropIndex(
                name: "IX_Banners_PromotionId",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "BackgroundColor",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "BannerType",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "ButtonStyle",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "CtaLink",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "CtaText",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "PromotionId",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "Subtitle",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "TextColor",
                table: "Banners");

            migrationBuilder.RenameColumn(
                name: "TargetAudience",
                table: "Banners",
                newName: "Type");
        }
    }
}
