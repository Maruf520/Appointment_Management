using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class dsfhsdjvmfgggdfgyghbjsafg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorTimeslots");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "TimeSlot");

            migrationBuilder.DropColumn(
                name: "IsBooked",
                table: "Appoinments");

            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "TimeSlot",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Appoinments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlot_DoctorId",
                table: "TimeSlot",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSlot_Doctors_DoctorId",
                table: "TimeSlot",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeSlot_Doctors_DoctorId",
                table: "TimeSlot");

            migrationBuilder.DropIndex(
                name: "IX_TimeSlot_DoctorId",
                table: "TimeSlot");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "TimeSlot");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Appoinments");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "TimeSlot",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsBooked",
                table: "Appoinments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "DoctorTimeslots",
                columns: table => new
                {
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    TimeSoltId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorTimeslots", x => new { x.DoctorId, x.TimeSoltId, x.Date });
                    table.ForeignKey(
                        name: "FK_DoctorTimeslots_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorTimeslots_TimeSlot_TimeSoltId",
                        column: x => x.TimeSoltId,
                        principalTable: "TimeSlot",
                        principalColumn: "TimeSlotId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorTimeslots_TimeSoltId",
                table: "DoctorTimeslots",
                column: "TimeSoltId");
        }
    }
}
