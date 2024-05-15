using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryMovie.Migrations
{
    /// <inheritdoc />
    public partial class NewChangesInMovieAndCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_MovieCategory_CategoryModelId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CategoryModelId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CategoryModelId",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryModelId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CategoryModelId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CategoryModelId",
                table: "Users",
                column: "CategoryModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_MovieCategory_CategoryModelId",
                table: "Users",
                column: "CategoryModelId",
                principalTable: "MovieCategory",
                principalColumn: "Id");
        }
    }
}
