using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BE_tasteal.Migrations
{
    /// <inheritdoc />
    public partial class Addservingsize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isBought",
                table: "Cart");

            migrationBuilder.AddColumn<int>(
                name: "serving_size",
                table: "Cart",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "serving_size",
                table: "Cart");

            migrationBuilder.AddColumn<bool>(
                name: "isBought",
                table: "Cart",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
