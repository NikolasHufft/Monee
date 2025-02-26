using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerLibrary.Data.Migrations
{
    /// <inheritdoc />
    public partial class Newupdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Employees_EmployeeId",
                table: "Doctors");

            //migrationBuilder.DropTable(
            //    name: "EmployeeDoctors");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_EmployeeId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "Average",
                table: "Vacations");

            migrationBuilder.DropColumn(
                name: "CiviId",
                table: "Vacations");

            migrationBuilder.DropColumn(
                name: "FileNumber",
                table: "Vacations");

            migrationBuilder.DropColumn(
                name: "CiviId",
                table: "Sanctions");

            migrationBuilder.DropColumn(
                name: "FileNumber",
                table: "Sanctions");

            migrationBuilder.DropColumn(
                name: "PunishmentDate",
                table: "Sanctions");

            migrationBuilder.DropColumn(
                name: "Average",
                table: "OverTimes");

            migrationBuilder.DropColumn(
                name: "CiviId",
                table: "OverTimes");

            migrationBuilder.DropColumn(
                name: "FileNumber",
                table: "OverTimes");

            migrationBuilder.AlterColumn<string>(
                name: "Other",
                table: "OverTimes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "Doctors",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            //migrationBuilder.CreateTable(
            //    name: "DoctorEmployee",
            //    columns: table => new
            //    {
            //        DoctorsId = table.Column<int>(type: "int", nullable: false),
            //        EmployeesId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_DoctorEmployee", x => new { x.DoctorsId, x.EmployeesId });
            //        table.ForeignKey(
            //            name: "FK_DoctorEmployee_Doctors_DoctorsId",
            //            column: x => x.DoctorsId,
            //            principalTable: "Doctors",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_DoctorEmployee_Employees_EmployeesId",
            //            column: x => x.EmployeesId,
            //            principalTable: "Employees",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_DoctorEmployee_EmployeesId",
            //    table: "DoctorEmployee",
            //    column: "EmployeesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorEmployee");

            migrationBuilder.AddColumn<double>(
                name: "Average",
                table: "Vacations",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "CiviId",
                table: "Vacations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileNumber",
                table: "Vacations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CiviId",
                table: "Sanctions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FileNumber",
                table: "Sanctions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "PunishmentDate",
                table: "Sanctions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Other",
                table: "OverTimes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Average",
                table: "OverTimes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "CiviId",
                table: "OverTimes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FileNumber",
                table: "OverTimes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "Doctors",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "EmployeeDoctors",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeDoctors", x => new { x.EmployeeId, x.DoctorId });
                    table.ForeignKey(
                        name: "FK_EmployeeDoctors_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeDoctors_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_EmployeeId",
                table: "Doctors",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDoctors_DoctorId",
                table: "EmployeeDoctors",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Employees_EmployeeId",
                table: "Doctors",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }
    }
}
