using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryMovie.Migrations
{
    /// <inheritdoc />
    public partial class NewNameInUserModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieCategory_Users_UserId",
                table: "MovieCategory");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "MovieCategory",
                newName: "CreatorUserId");

            migrationBuilder.RenameIndex(
                name: "IX_MovieCategory_UserId",
                table: "MovieCategory",
                newName: "IX_MovieCategory_CreatorUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieCategory_Users_CreatorUserId",
                table: "MovieCategory",
                column: "CreatorUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieCategory_Users_CreatorUserId",
                table: "MovieCategory");

            migrationBuilder.RenameColumn(
                name: "CreatorUserId",
                table: "MovieCategory",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_MovieCategory_CreatorUserId",
                table: "MovieCategory",
                newName: "IX_MovieCategory_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieCategory_Users_UserId",
                table: "MovieCategory",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
