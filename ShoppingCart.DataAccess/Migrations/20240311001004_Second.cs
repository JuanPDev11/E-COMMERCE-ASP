using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingCart.DataAccess.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Followers",
                table: "Artists",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalSold",
                table: "Artists",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Volume",
                table: "Artists",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Followers",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "TotalSold",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "Volume",
                table: "Artists");
        }
    }
}
