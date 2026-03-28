using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace it_career.data.Migrations
{
    /// <inheritdoc />
    public partial class cascadedelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookedFilms_FilmSchedule_FilmScheduleId",
                table: "BookedFilms");

            migrationBuilder.AddForeignKey(
                name: "FK_BookedFilms_FilmSchedule_FilmScheduleId",
                table: "BookedFilms",
                column: "FilmScheduleId",
                principalTable: "FilmSchedule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookedFilms_FilmSchedule_FilmScheduleId",
                table: "BookedFilms");

            migrationBuilder.AddForeignKey(
                name: "FK_BookedFilms_FilmSchedule_FilmScheduleId",
                table: "BookedFilms",
                column: "FilmScheduleId",
                principalTable: "FilmSchedule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
