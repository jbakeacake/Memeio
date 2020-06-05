using Microsoft.EntityFrameworkCore.Migrations;

namespace Memeio.API.Migrations
{
    public partial class AddedAuthorPhotoUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorPhotoUrl",
                table: "Photos_Tbl",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorPhotoUrl",
                table: "Photos_Tbl");
        }
    }
}
