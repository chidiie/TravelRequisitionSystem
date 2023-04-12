using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelRequisitionSystem.Migrations
{
    public partial class InitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TravelRequests",
                columns: table => new
                {
                    TravelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TravelerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SourceLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SourceCountry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DestinationLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DestinationCountry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChargeCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartureDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TripType = table.Column<int>(type: "int", nullable: false),
                    TravelClass = table.Column<int>(type: "int", nullable: false),
                    RequisitionStatus = table.Column<int>(type: "int", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RequisitonNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelRequests", x => x.TravelId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TravelRequests");
        }
    }
}
