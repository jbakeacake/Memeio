using Microsoft.EntityFrameworkCore.Migrations;

namespace Memeio.API.Migrations
{
    public partial class AddedAuthorId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "Profile_Comments_Tbl",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "Post_Comments_Tbl",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Profile_Comments_Tbl");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Post_Comments_Tbl");
        }
    }
}
