using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BE_tasteal.Migrations
{
    /// <inheritdoc />
    public partial class namepersonalcart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "personalCartItems",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name",
                table: "personalCartItems");
        }
    }
}
