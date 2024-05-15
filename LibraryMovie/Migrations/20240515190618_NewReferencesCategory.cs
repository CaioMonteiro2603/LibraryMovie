using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryMovie.Migrations
{
    /// <inheritdoc />
    public partial class NewReferencesCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieCategory_Users_UsersId",
                table: "MovieCategory");

            migrationBuilder.DropIndex(
                name: "IX_MovieCategory_UsersId",
                table: "MovieCategory");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "MovieCategory");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "MovieCategory",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MovieCategory_UserId",
                table: "MovieCategory",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieCategory_Users_UserId",
                table: "MovieCategory",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieCategory_Users_UserId",
                table: "MovieCategory");

            migrationBuilder.DropIndex(
                name: "IX_MovieCategory_UserId",
                table: "MovieCategory");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "MovieCategory");

            migrationBuilder.AddColumn<int>(
                name: "UsersId",
                table: "MovieCategory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MovieCategory_UsersId",
                table: "MovieCategory",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieCategory_Users_UsersId",
                table: "MovieCategory",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
