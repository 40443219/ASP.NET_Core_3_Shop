using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop.Database.Migrations
{
    public partial class fixtypo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quentity",
                table: "Stocks");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Stocks",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Stocks");

            migrationBuilder.AddColumn<int>(
                name: "Quentity",
                table: "Stocks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
