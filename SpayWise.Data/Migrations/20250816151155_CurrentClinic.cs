using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HydroApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class CurrentClinic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentClinicId",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CurrentClinicId",
                table: "AspNetUsers",
                column: "CurrentClinicId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Clinics_CurrentClinicId",
                table: "AspNetUsers",
                column: "CurrentClinicId",
                principalTable: "Clinics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Clinics_CurrentClinicId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CurrentClinicId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CurrentClinicId",
                table: "AspNetUsers");
        }
    }
}
