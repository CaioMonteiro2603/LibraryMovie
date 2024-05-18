using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryMovie.Migrations
{
    /// <inheritdoc />
    public partial class NewNameInUserModelAndMovieModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieCategory_Users_CreatorUserId",
                table: "MovieCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_Movies_MovieCategory_CategoryId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_CategoryId",
                table: "Movies");

            migrationBuilder.RenameColumn(
                name: "CreatorUserId",
                table: "MovieCategory",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_MovieCategory_CreatorUserId",
                table: "MovieCategory",
                newName: "IX_MovieCategory_UserId");

            migrationBuilder.AddColumn<int>(
                name: "CreatorOfTheCategoryId",
                table: "Movies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movies_CreatorOfTheCategoryId",
                table: "Movies",
                column: "CreatorOfTheCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieCategory_Users_UserId",
                table: "MovieCategory",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_MovieCategory_CreatorOfTheCategoryId",
                table: "Movies",
                column: "CreatorOfTheCategoryId",
                principalTable: "MovieCategory",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieCategory_Users_UserId",
                table: "MovieCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_Movies_MovieCategory_CreatorOfTheCategoryId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_CreatorOfTheCategoryId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "CreatorOfTheCategoryId",
                table: "Movies");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "MovieCategory",
                newName: "CreatorUserId");

            migrationBuilder.RenameIndex(
                name: "IX_MovieCategory_UserId",
                table: "MovieCategory",
                newName: "IX_MovieCategory_CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_CategoryId",
                table: "Movies",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieCategory_Users_CreatorUserId",
                table: "MovieCategory",
                column: "CreatorUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_MovieCategory_CategoryId",
                table: "Movies",
                column: "CategoryId",
                principalTable: "MovieCategory",
                principalColumn: "Id");
        }
    }
}
