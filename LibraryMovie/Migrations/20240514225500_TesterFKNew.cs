using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryMovie.Migrations
{
    /// <inheritdoc />
    public partial class TesterFKNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieCategory_Users_UserId",
                table: "MovieCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Users_UserId",
                table: "Movies");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Movies",
                newName: "UsersId");

            migrationBuilder.RenameIndex(
                name: "IX_Movies_UserId",
                table: "Movies",
                newName: "IX_Movies_UsersId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "MovieCategory",
                newName: "UsersId");

            migrationBuilder.RenameIndex(
                name: "IX_MovieCategory_UserId",
                table: "MovieCategory",
                newName: "IX_MovieCategory_UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieCategory_Users_UsersId",
                table: "MovieCategory",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Users_UsersId",
                table: "Movies",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieCategory_Users_UsersId",
                table: "MovieCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Users_UsersId",
                table: "Movies");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "Movies",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Movies_UsersId",
                table: "Movies",
                newName: "IX_Movies_UserId");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "MovieCategory",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_MovieCategory_UsersId",
                table: "MovieCategory",
                newName: "IX_MovieCategory_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieCategory_Users_UserId",
                table: "MovieCategory",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Users_UserId",
                table: "Movies",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
