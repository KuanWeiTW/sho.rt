using Microsoft.EntityFrameworkCore.Migrations;

namespace sho.rt.Data.Migrations
{
    public partial class AddOwner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCustomized",
                table: "Mapping",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Mapping",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mapping_OwnerId",
                table: "Mapping",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mapping_AspNetUsers_OwnerId",
                table: "Mapping",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mapping_AspNetUsers_OwnerId",
                table: "Mapping");

            migrationBuilder.DropIndex(
                name: "IX_Mapping_OwnerId",
                table: "Mapping");

            migrationBuilder.DropColumn(
                name: "IsCustomized",
                table: "Mapping");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Mapping");
        }
    }
}
