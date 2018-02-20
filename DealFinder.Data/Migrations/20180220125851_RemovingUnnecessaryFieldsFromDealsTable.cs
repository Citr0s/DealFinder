using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DealFinder.Data.Migrations
{
    public partial class RemovingUnnecessaryFieldsFromDealsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DistanceInMeters",
                table: "Deals");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "DistanceInMeters",
                table: "Deals",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
