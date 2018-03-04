using Microsoft.EntityFrameworkCore.Migrations;

namespace DealFinder.Data.Migrations
{
    public partial class AddingExtraFieldsToTheUsersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Users");
        }
    }
}
