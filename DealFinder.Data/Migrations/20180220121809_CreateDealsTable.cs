using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace DealFinder.Data.Migrations
{
    public partial class CreateDealsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Deals",
                columns: table => new
                {
                    Identifier = table.Column<Guid>(nullable: false),
                    DistanceInMeters = table.Column<double>(nullable: false),
                    Summary = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deals", x => x.Identifier);
                });

            migrationBuilder.CreateTable(
                name: "LocationRecord",
                columns: table => new
                {
                    Identifier = table.Column<Guid>(nullable: false),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationRecord", x => x.Identifier);
                    table.ForeignKey(
                        name: "FK_LocationRecord_Deals_Identifier",
                        column: x => x.Identifier,
                        principalTable: "Deals",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocationRecord");

            migrationBuilder.DropTable(
                name: "Deals");
        }
    }
}
