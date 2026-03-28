using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace it_career.data.Migrations
{
    /// <inheritdoc />
    public partial class BookedFilmRework : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookedFilms_AspNetUsers_AppUserId1",
                table: "BookedFilms");

            migrationBuilder.DropForeignKey(
                name: "FK_BookedFilms_FilmSchedule_FilmScheduleId1",
                table: "BookedFilms");

            migrationBuilder.DropIndex(
                name: "IX_BookedFilms_AppUserId1",
                table: "BookedFilms");

            migrationBuilder.DropIndex(
                name: "IX_BookedFilms_FilmScheduleId1",
                table: "BookedFilms");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "BookedFilms");

            migrationBuilder.DropColumn(
                name: "FilmScheduleId1",
                table: "BookedFilms");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "BookedFilms",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "FilmScheduleId1",
                table: "BookedFilms",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_BookedFilms_AppUserId1",
                table: "BookedFilms",
                column: "AppUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_BookedFilms_FilmScheduleId1",
                table: "BookedFilms",
                column: "FilmScheduleId1");

            migrationBuilder.AddForeignKey(
                name: "FK_BookedFilms_AspNetUsers_AppUserId1",
                table: "BookedFilms",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookedFilms_FilmSchedule_FilmScheduleId1",
                table: "BookedFilms",
                column: "FilmScheduleId1",
                principalTable: "FilmSchedule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
