using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace DealFinder.Data.Migrations
{
    public partial class AddingReferenceToUserToDealsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserIdentifier",
                table: "Deals",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Deals_UserIdentifier",
                table: "Deals",
                column: "UserIdentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_Users_UserIdentifier",
                table: "Deals",
                column: "UserIdentifier",
                principalTable: "Users",
                principalColumn: "Identifier",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deals_Users_UserIdentifier",
                table: "Deals");

            migrationBuilder.DropIndex(
                name: "IX_Deals_UserIdentifier",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "UserIdentifier",
                table: "Deals");
        }
    }
}
