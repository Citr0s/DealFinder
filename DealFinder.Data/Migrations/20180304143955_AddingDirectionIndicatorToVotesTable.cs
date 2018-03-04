using Microsoft.EntityFrameworkCore.Migrations;

namespace DealFinder.Data.Migrations
{
    public partial class AddingDirectionIndicatorToVotesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Positive",
                table: "Votes",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Positive",
                table: "Votes");
        }
    }
}
