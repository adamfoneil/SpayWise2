using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HydroApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class MoreTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppointmentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClinicId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    BuiltInType = table.Column<int>(type: "integer", nullable: false),
                    BackColor = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    TextColor = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppointmentTypes_Clinics_ClinicId",
                        column: x => x.ClinicId,
                        principalTable: "Clinics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DailyCountLimits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LocationId = table.Column<int>(type: "integer", nullable: false),
                    DayOfWeek = table.Column<int>(type: "integer", nullable: false),
                    SexId = table.Column<int>(type: "integer", nullable: false),
                    SpeciesId = table.Column<int>(type: "integer", nullable: false),
                    Maximum = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyCountLimits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyCountLimits_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DailyCountLimits_Sexes_SexId",
                        column: x => x.SexId,
                        principalTable: "Sexes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DailyCountLimits_Species_SpeciesId",
                        column: x => x.SpeciesId,
                        principalTable: "Species",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DailyPointLimits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LocationId = table.Column<int>(type: "integer", nullable: false),
                    DayOfWeek = table.Column<int>(type: "integer", nullable: false),
                    Maximum = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyPointLimits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyPointLimits_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OverrideCounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LocationId = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Maximum = table.Column<int>(type: "integer", nullable: false),
                    Comments = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    SexId = table.Column<int>(type: "integer", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OverrideCounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OverrideCounts_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OverrideCounts_Sexes_SexId",
                        column: x => x.SexId,
                        principalTable: "Sexes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OverridePoints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LocationId = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Maximum = table.Column<int>(type: "integer", nullable: false),
                    Comments = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OverridePoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OverridePoints_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClinicId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Data = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentMethods_Clinics_ClinicId",
                        column: x => x.ClinicId,
                        principalTable: "Clinics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Species_ClinicId_Abbreviation",
                table: "Species",
                columns: new[] { "ClinicId", "Abbreviation" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentTypes_ClinicId_Name",
                table: "AppointmentTypes",
                columns: new[] { "ClinicId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DailyCountLimits_LocationId_DayOfWeek_SexId_SpeciesId",
                table: "DailyCountLimits",
                columns: new[] { "LocationId", "DayOfWeek", "SexId", "SpeciesId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DailyCountLimits_SexId",
                table: "DailyCountLimits",
                column: "SexId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyCountLimits_SpeciesId",
                table: "DailyCountLimits",
                column: "SpeciesId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyPointLimits_LocationId_DayOfWeek",
                table: "DailyPointLimits",
                columns: new[] { "LocationId", "DayOfWeek" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OverrideCounts_LocationId_Date",
                table: "OverrideCounts",
                columns: new[] { "LocationId", "Date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OverrideCounts_SexId",
                table: "OverrideCounts",
                column: "SexId");

            migrationBuilder.CreateIndex(
                name: "IX_OverridePoints_LocationId_Date",
                table: "OverridePoints",
                columns: new[] { "LocationId", "Date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethods_ClinicId_Name",
                table: "PaymentMethods",
                columns: new[] { "ClinicId", "Name" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentTypes");

            migrationBuilder.DropTable(
                name: "DailyCountLimits");

            migrationBuilder.DropTable(
                name: "DailyPointLimits");

            migrationBuilder.DropTable(
                name: "OverrideCounts");

            migrationBuilder.DropTable(
                name: "OverridePoints");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropIndex(
                name: "IX_Species_ClinicId_Abbreviation",
                table: "Species");
        }
    }
}
