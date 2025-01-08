using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_Task.Migrations
{
    /// <inheritdoc />
    public partial class editcoursetable3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instructor_Course_CourseId1",
                table: "Instructor");

            migrationBuilder.DropIndex(
                name: "IX_Instructor_CourseId1",
                table: "Instructor");

            migrationBuilder.DropColumn(
                name: "CourseId1",
                table: "Instructor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseId1",
                table: "Instructor",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Instructor_CourseId1",
                table: "Instructor",
                column: "CourseId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Instructor_Course_CourseId1",
                table: "Instructor",
                column: "CourseId1",
                principalTable: "Course",
                principalColumn: "Id");
        }
    }
}
