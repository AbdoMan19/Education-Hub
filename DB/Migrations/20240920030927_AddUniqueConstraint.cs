using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_Task.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Department_Name",
                table: "Department",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Course_Name",
                table: "Course",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Department_Name",
                table: "Department");

            migrationBuilder.DropIndex(
                name: "IX_Course_Name",
                table: "Course");
        }
    }
}
