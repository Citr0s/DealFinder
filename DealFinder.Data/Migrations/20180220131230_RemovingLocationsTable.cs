using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DealFinder.Data.Migrations
{
    public partial class RemovingLocationsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocationRecord");

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Deals",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Deals",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Deals");

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
    }
}
