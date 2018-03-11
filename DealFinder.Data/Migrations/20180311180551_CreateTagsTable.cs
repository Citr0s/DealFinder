using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DealFinder.Data.Migrations
{
    public partial class CreateTagsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Identifier = table.Column<Guid>(nullable: false),
                    DealRecordIdentifier = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Identifier);
                    table.ForeignKey(
                        name: "FK_Tags_Deals_DealRecordIdentifier",
                        column: x => x.DealRecordIdentifier,
                        principalTable: "Deals",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tags_DealRecordIdentifier",
                table: "Tags",
                column: "DealRecordIdentifier");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tags");
        }
    }
}
