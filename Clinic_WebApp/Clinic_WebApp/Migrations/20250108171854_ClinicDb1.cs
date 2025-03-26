using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinic_WebApp.Migrations
{
    /// <inheritdoc />
    public partial class ClinicDb1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    DoctorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dspeciality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dphone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.DoctorID);
                });

            migrationBuilder.CreateTable(
                name: "BookAppoitment",
                columns: table => new
                {
                    patientID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    pname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pemail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pphone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoctorID = table.Column<int>(type: "int", nullable: false),
                    pdate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pmsg = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookAppoitment", x => x.patientID);
                    table.ForeignKey(
                        name: "FK_BookAppoitment_Doctors_DoctorID",
                        column: x => x.DoctorID,
                        principalTable: "Doctors",
                        principalColumn: "DoctorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookAppoitment_DoctorID",
                table: "BookAppoitment",
                column: "DoctorID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookAppoitment");

            migrationBuilder.DropTable(
                name: "Doctors");
        }
    }
}
