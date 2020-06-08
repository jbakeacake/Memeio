using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Memeio.API.Migrations
{
    public partial class InitialAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users_Tbl",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    LastActive = table.Column<DateTime>(nullable: false),
                    PhotoUrl = table.Column<string>(nullable: true),
                    Introduction = table.Column<string>(nullable: true),
                    Followers = table.Column<int>(nullable: false),
                    Follows = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users_Tbl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Photos_Tbl",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Url = table.Column<string>(nullable: true),
                    Author = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    DatePosted = table.Column<DateTime>(nullable: false),
                    PublicId = table.Column<string>(nullable: true),
                    Likes = table.Column<int>(nullable: false),
                    Dislikes = table.Column<int>(nullable: false),
                    Favorites = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos_Tbl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_Tbl_Users_Tbl_UserId",
                        column: x => x.UserId,
                        principalTable: "Users_Tbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Profile_Comments_Tbl",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Author = table.Column<string>(nullable: true),
                    AuthorId = table.Column<int>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profile_Comments_Tbl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profile_Comments_Tbl_Users_Tbl_UserId",
                        column: x => x.UserId,
                        principalTable: "Users_Tbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Post_Comments_Tbl",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Author = table.Column<string>(nullable: true),
                    AuthorId = table.Column<int>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    PostId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post_Comments_Tbl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Post_Comments_Tbl_Photos_Tbl_PostId",
                        column: x => x.PostId,
                        principalTable: "Photos_Tbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Photos_Tbl_UserId",
                table: "Photos_Tbl",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Post_Comments_Tbl_PostId",
                table: "Post_Comments_Tbl",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Profile_Comments_Tbl_UserId",
                table: "Profile_Comments_Tbl",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Post_Comments_Tbl");

            migrationBuilder.DropTable(
                name: "Profile_Comments_Tbl");

            migrationBuilder.DropTable(
                name: "Photos_Tbl");

            migrationBuilder.DropTable(
                name: "Users_Tbl");
        }
    }
}
