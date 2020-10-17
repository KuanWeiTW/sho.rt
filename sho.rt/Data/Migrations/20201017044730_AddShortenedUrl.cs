using Microsoft.EntityFrameworkCore.Migrations;

namespace sho.rt.Data.Migrations
{
    public partial class AddShortenedUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShortenedUrl",
                table: "Mapping",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Mapping_ShortenedUrl",
                table: "Mapping",
                column: "ShortenedUrl");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Mapping_ShortenedUrl",
                table: "Mapping");

            migrationBuilder.DropColumn(
                name: "ShortenedUrl",
                table: "Mapping");
        }
    }
}
