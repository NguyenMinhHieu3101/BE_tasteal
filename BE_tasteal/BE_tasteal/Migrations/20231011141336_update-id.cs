using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BE_tasteal.Migrations
{
    /// <inheritdoc />
    public partial class updateid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "plan_id",
                table: "Plan",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "pantry_id",
                table: "Pantry_Item",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "nutrition_info_id",
                table: "Nutrition_Info",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ingredient_type_id",
                table: "Ingredient_Type",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ingredient_id",
                table: "Ingredient",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "cook_book_id",
                table: "CookBook",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "cart_id",
                table: "Cart_Item",
                newName: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "Plan",
                newName: "plan_id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Pantry_Item",
                newName: "pantry_id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Nutrition_Info",
                newName: "nutrition_info_id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Ingredient_Type",
                newName: "ingredient_type_id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Ingredient",
                newName: "ingredient_id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "CookBook",
                newName: "cook_book_id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Cart_Item",
                newName: "cart_id");
        }
    }
}
