using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryMovie.Migrations
{
    /// <inheritdoc />
    public partial class NewNameInMovieModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_MovieCategory_CreatorOfTheCategoryId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_CreatorOfTheCategoryId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "CreatorOfTheCategoryId",
                table: "Movies");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_CategoryId",
                table: "Movies",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_MovieCategory_CategoryId",
                table: "Movies",
                column: "CategoryId",
                principalTable: "MovieCategory",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_MovieCategory_CategoryId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_CategoryId",
                table: "Movies");

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
                name: "FK_Movies_MovieCategory_CreatorOfTheCategoryId",
                table: "Movies",
                column: "CreatorOfTheCategoryId",
                principalTable: "MovieCategory",
                principalColumn: "Id");
        }
    }
}
