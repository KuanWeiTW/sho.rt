using Microsoft.EntityFrameworkCore.Migrations;

namespace sho.rt.Data.Migrations
{
    public partial class ModifyIndexScheme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Mapping_ShortenedUrl",
                table: "Mapping");

            migrationBuilder.AlterColumn<string>(
                name: "ShortenedUrl",
                table: "Mapping",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Mapping_ShortenedUrl",
                table: "Mapping",
                column: "ShortenedUrl",
                unique: true,
                filter: "[ShortenedUrl] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Mapping_ShortenedUrl",
                table: "Mapping");

            migrationBuilder.AlterColumn<string>(
                name: "ShortenedUrl",
                table: "Mapping",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Mapping_ShortenedUrl",
                table: "Mapping",
                column: "ShortenedUrl");
        }
    }
}
