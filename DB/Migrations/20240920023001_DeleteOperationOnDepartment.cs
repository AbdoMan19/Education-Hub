using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_Task.Migrations
{
    /// <inheritdoc />
    public partial class DeleteOperationOnDepartment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_Department_DepartmentId",
                table: "Course");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructor_Department_Dept_Id",
                table: "Instructor");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainee_Department_DepartmentId",
                table: "Trainee");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_Department_DepartmentId",
                table: "Course",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Instructor_Department_Dept_Id",
                table: "Instructor",
                column: "Dept_Id",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Trainee_Department_DepartmentId",
                table: "Trainee",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_Department_DepartmentId",
                table: "Course");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructor_Department_Dept_Id",
                table: "Instructor");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainee_Department_DepartmentId",
                table: "Trainee");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_Department_DepartmentId",
                table: "Course",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Instructor_Department_Dept_Id",
                table: "Instructor",
                column: "Dept_Id",
                principalTable: "Department",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trainee_Department_DepartmentId",
                table: "Trainee",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id");
        }
    }
}
