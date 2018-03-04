using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DealFinder.Data.Migrations
{
    public partial class CreateVotesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Votes",
                columns: table => new
                {
                    Identifier = table.Column<Guid>(nullable: false),
                    DealIdentifier = table.Column<Guid>(nullable: true),
                    UserIdentifier = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votes", x => x.Identifier);
                    table.ForeignKey(
                        name: "FK_Votes_Deals_DealIdentifier",
                        column: x => x.DealIdentifier,
                        principalTable: "Deals",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Votes_Users_UserIdentifier",
                        column: x => x.UserIdentifier,
                        principalTable: "Users",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Votes_DealIdentifier",
                table: "Votes",
                column: "DealIdentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_UserIdentifier",
                table: "Votes",
                column: "UserIdentifier");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Votes");
        }
    }
}
