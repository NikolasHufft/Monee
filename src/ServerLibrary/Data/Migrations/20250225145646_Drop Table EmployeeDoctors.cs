using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerLibrary.Data.Migrations
{
    /// <inheritdoc />
    public partial class DropTableEmployeeDoctors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Doctors",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_EmployeeId",
                table: "Doctors",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Employees_EmployeeId",
                table: "Doctors",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Employees_EmployeeId",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_EmployeeId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Doctors");
        }
    }
}
