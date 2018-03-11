using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DealFinder.Data.Migrations
{
    public partial class AddingDealTagLinkTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Deals_DealRecordIdentifier",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_DealRecordIdentifier",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "DealRecordIdentifier",
                table: "Tags");

            migrationBuilder.CreateTable(
                name: "DealTags",
                columns: table => new
                {
                    DealIdentifier = table.Column<Guid>(nullable: false),
                    TagIdentifier = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DealTags", x => new { x.DealIdentifier, x.TagIdentifier });
                    table.ForeignKey(
                        name: "FK_DealTags_Deals_DealIdentifier",
                        column: x => x.DealIdentifier,
                        principalTable: "Deals",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DealTags_Tags_TagIdentifier",
                        column: x => x.TagIdentifier,
                        principalTable: "Tags",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DealTags_TagIdentifier",
                table: "DealTags",
                column: "TagIdentifier");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DealTags");

            migrationBuilder.AddColumn<Guid>(
                name: "DealRecordIdentifier",
                table: "Tags",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_DealRecordIdentifier",
                table: "Tags",
                column: "DealRecordIdentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Deals_DealRecordIdentifier",
                table: "Tags",
                column: "DealRecordIdentifier",
                principalTable: "Deals",
                principalColumn: "Identifier",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
