using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinic_WebApp.Migrations
{
    /// <inheritdoc />
    public partial class ClinicBookAppDb2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookAppoitment_Doctors_DoctorID",
                table: "BookAppoitment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookAppoitment",
                table: "BookAppoitment");

            migrationBuilder.RenameTable(
                name: "BookAppoitment",
                newName: "BookAppoitments");

            migrationBuilder.RenameIndex(
                name: "IX_BookAppoitment_DoctorID",
                table: "BookAppoitments",
                newName: "IX_BookAppoitments_DoctorID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookAppoitments",
                table: "BookAppoitments",
                column: "patientID");

            migrationBuilder.AddForeignKey(
                name: "FK_BookAppoitments_Doctors_DoctorID",
                table: "BookAppoitments",
                column: "DoctorID",
                principalTable: "Doctors",
                principalColumn: "DoctorID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookAppoitments_Doctors_DoctorID",
                table: "BookAppoitments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookAppoitments",
                table: "BookAppoitments");

            migrationBuilder.RenameTable(
                name: "BookAppoitments",
                newName: "BookAppoitment");

            migrationBuilder.RenameIndex(
                name: "IX_BookAppoitments_DoctorID",
                table: "BookAppoitment",
                newName: "IX_BookAppoitment_DoctorID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookAppoitment",
                table: "BookAppoitment",
                column: "patientID");

            migrationBuilder.AddForeignKey(
                name: "FK_BookAppoitment_Doctors_DoctorID",
                table: "BookAppoitment",
                column: "DoctorID",
                principalTable: "Doctors",
                principalColumn: "DoctorID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
