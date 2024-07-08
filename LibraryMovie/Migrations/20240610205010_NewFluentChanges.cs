﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryMovie.Migrations
{
    /// <inheritdoc />
    public partial class NewFluentChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieCategory_Users_UserId",
                table: "MovieCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_Movies_MovieCategory_CategoryId",
                table: "Movies");

            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Users_UserId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_UserId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_MovieCategory_UserId",
                table: "MovieCategory");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "Movies",
                newName: "Title");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(12)",
                oldMaxLength: 12);

            migrationBuilder.AddColumn<int>(
                name: "UsersId",
                table: "Movies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsersId",
                table: "MovieCategory",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movies_UsersId",
                table: "Movies",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieCategory_UsersId",
                table: "MovieCategory",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieCategory_Users_UsersId",
                table: "MovieCategory",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_MovieCategory_CategoryId",
                table: "Movies",
                column: "CategoryId",
                principalTable: "MovieCategory",
                principalColumn: "MovieCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Users_UsersId",
                table: "Movies",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieCategory_Users_UsersId",
                table: "MovieCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_Movies_MovieCategory_CategoryId",
                table: "Movies");

            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Users_UsersId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_UsersId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_MovieCategory_UsersId",
                table: "MovieCategory");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "MovieCategory");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Movies",
                newName: "title");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_UserId",
                table: "Movies",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieCategory_UserId",
                table: "MovieCategory",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieCategory_Users_UserId",
                table: "MovieCategory",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_MovieCategory_CategoryId",
                table: "Movies",
                column: "CategoryId",
                principalTable: "MovieCategory",
                principalColumn: "MovieCategoryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Users_UserId",
                table: "Movies",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}