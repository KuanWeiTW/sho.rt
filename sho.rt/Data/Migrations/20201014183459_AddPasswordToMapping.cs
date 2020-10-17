using Microsoft.EntityFrameworkCore.Migrations;

namespace sho.rt.Data.Migrations
{
    public partial class AddPasswordToMapping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Mapping",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Mapping");
        }
    }
}
