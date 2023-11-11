using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BE_tasteal.Migrations
{
    /// <inheritdoc />
    public partial class erdmeeting6112023 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "amount",
                table: "Recipe_Ingredient",
                newName: "amount_per_serving");

            migrationBuilder.AddColumn<string>(
                name: "link",
                table: "Account",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "quote",
                table: "Account",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "slogan",
                table: "Account",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "personalCartItems",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ingredient_id = table.Column<int>(type: "int", nullable: false),
                    account_id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    amount = table.Column<int>(type: "int", nullable: false),
                    is_bought = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personalCartItems", x => x.id);
                    table.ForeignKey(
                        name: "FK_personalCartItems_Account_account_id",
                        column: x => x.account_id,
                        principalTable: "Account",
                        principalColumn: "uid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_personalCartItems_Ingredient_ingredient_id",
                        column: x => x.ingredient_id,
                        principalTable: "Ingredient",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_personalCartItems_account_id",
                table: "personalCartItems",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_personalCartItems_ingredient_id",
                table: "personalCartItems",
                column: "ingredient_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "personalCartItems");

            migrationBuilder.DropColumn(
                name: "link",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "quote",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "slogan",
                table: "Account");

            migrationBuilder.RenameColumn(
                name: "amount_per_serving",
                table: "Recipe_Ingredient",
                newName: "amount");
        }
    }
}
