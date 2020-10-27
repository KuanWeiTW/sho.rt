using Microsoft.EntityFrameworkCore.Migrations;

namespace sho.rt.Data.Migrations
{
    public partial class ChangeSchemaToIdOnly : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Mapping_ShortenedUrl",
                table: "Mapping");

            migrationBuilder.DropColumn(
                name: "IsCustomized",
                table: "Mapping");

            migrationBuilder.DropColumn(
                name: "ShortenedUrl",
                table: "Mapping");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCustomized",
                table: "Mapping",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ShortenedUrl",
                table: "Mapping",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mapping_ShortenedUrl",
                table: "Mapping",
                column: "ShortenedUrl",
                unique: true,
                filter: "[ShortenedUrl] IS NOT NULL");
        }
    }
}
