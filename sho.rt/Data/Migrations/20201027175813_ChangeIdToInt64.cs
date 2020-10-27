using Microsoft.EntityFrameworkCore.Migrations;

namespace sho.rt.Data.Migrations
{
    public partial class ChangeIdToInt64 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Mapping",
                table: "Mapping");
            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Mapping",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");
            migrationBuilder.AddPrimaryKey(
                name: "PK_Mapping",
                table: "Mapping",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Mapping",
                table: "Mapping");
            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Mapping",
                type: "int",
                nullable: false,
                oldClrType: typeof(long))
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");
            migrationBuilder.AddPrimaryKey(
                name: "PK_Mapping",
                table: "Mapping",
                column: "Id");
        }
    }
}
