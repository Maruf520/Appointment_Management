using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class appointmeentalted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appoinments_Patients_PatientId",
                table: "Appoinments");

            migrationBuilder.DropIndex(
                name: "IX_Appoinments_PatientId",
                table: "Appoinments");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Appoinments");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "StartTime",
                table: "Appoinments",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PatientId",
                table: "Appoinments",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "EndTime",
                table: "Appoinments",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PatientId1",
                table: "Appoinments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appoinments_PatientId1",
                table: "Appoinments",
                column: "PatientId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Appoinments_Patients_PatientId1",
                table: "Appoinments",
                column: "PatientId1",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appoinments_Patients_PatientId1",
                table: "Appoinments");

            migrationBuilder.DropIndex(
                name: "IX_Appoinments_PatientId1",
                table: "Appoinments");

            migrationBuilder.DropColumn(
                name: "PatientId1",
                table: "Appoinments");

            migrationBuilder.AlterColumn<string>(
                name: "StartTime",
                table: "Appoinments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(TimeSpan));

            migrationBuilder.AlterColumn<int>(
                name: "PatientId",
                table: "Appoinments",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EndTime",
                table: "Appoinments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(TimeSpan));

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Appoinments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Appoinments_PatientId",
                table: "Appoinments",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appoinments_Patients_PatientId",
                table: "Appoinments",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
