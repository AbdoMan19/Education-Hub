using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_Task.Migrations
{
    /// <inheritdoc />
    public partial class ninthmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instructor_Course_CourseId",
                table: "Instructor");

            migrationBuilder.DropIndex(
                name: "IX_Instructor_CourseId",
                table: "Instructor");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Instructor");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Trainee",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Department",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Course",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Instructor_Crs_Id",
                table: "Instructor",
                column: "Crs_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Instructor_Course_Crs_Id",
                table: "Instructor",
                column: "Crs_Id",
                principalTable: "Course",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instructor_Course_Crs_Id",
                table: "Instructor");

            migrationBuilder.DropIndex(
                name: "IX_Instructor_Crs_Id",
                table: "Instructor");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Trainee",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "Instructor",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Department",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Course",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.CreateIndex(
                name: "IX_Instructor_CourseId",
                table: "Instructor",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Instructor_Course_CourseId",
                table: "Instructor",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "Id");
        }
    }
}
