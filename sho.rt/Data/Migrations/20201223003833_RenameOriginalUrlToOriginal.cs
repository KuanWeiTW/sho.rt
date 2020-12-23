using Microsoft.EntityFrameworkCore.Migrations;

namespace sho.rt.Data.Migrations
{
    public partial class RenameOriginalUrlToOriginal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OriginalUrl",
                table: "Mapping",
                newName: "Original");

            migrationBuilder.RenameColumn(
                name: "OriginalUrl",
                table: "CustomMapping",
                newName: "Original");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Original",
                table: "Mapping",
                newName: "OriginalUrl");

            migrationBuilder.RenameColumn(
                name: "Original",
                table: "CustomMapping",
                newName: "OriginalUrl");
        }
    }
}
