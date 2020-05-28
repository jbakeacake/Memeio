using Microsoft.EntityFrameworkCore.Migrations;

namespace Memeio.API.Migrations
{
    public partial class AddPhotoAuthor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Photos_Tbl",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "Photos_Tbl",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "Photos_Tbl");

            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "Photos_Tbl");
        }
    }
}
