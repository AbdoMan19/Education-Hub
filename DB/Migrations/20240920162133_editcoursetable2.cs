using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_Task.Migrations
{
    /// <inheritdoc />
    public partial class editcoursetable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instructor_Course_Crs_Id",
                table: "Instructor");

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "Instructor",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Instructor_CourseId1",
                table: "Instructor",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Instructor_Course_CourseId1",
                table: "Instructor",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Instructor_Course_Crs_Id",
                table: "Instructor",
                column: "Crs_Id",
                principalTable: "Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instructor_Course_CourseId1",
                table: "Instructor");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructor_Course_Crs_Id",
                table: "Instructor");

            migrationBuilder.DropIndex(
                name: "IX_Instructor_CourseId1",
                table: "Instructor");

            migrationBuilder.DropColumn(
                name: "CourseId1",
                table: "Instructor");

            migrationBuilder.AddForeignKey(
                name: "FK_Instructor_Course_Crs_Id",
                table: "Instructor",
                column: "Crs_Id",
                principalTable: "Course",
                principalColumn: "Id");
        }
    }
}
