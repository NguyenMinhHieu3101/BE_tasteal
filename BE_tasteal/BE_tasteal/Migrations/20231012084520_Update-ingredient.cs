using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BE_tasteal.Migrations
{
    /// <inheritdoc />
    public partial class Updateingredient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredient_Ingredient_Type_type_id",
                table: "Ingredient");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingredient_Nutrition_Info_measurement_unit_id",
                table: "Ingredient");

            migrationBuilder.DropColumn(
                name: "measurement_unit_id",
                table: "Ingredient_Type");

            migrationBuilder.AlterColumn<decimal>(
                name: "vitaminD",
                table: "Nutrition_Info",
                type: "decimal(10,2)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "trans_fat",
                table: "Nutrition_Info",
                type: "decimal(10,2)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "sugars",
                table: "Nutrition_Info",
                type: "decimal(10,2)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "sodium",
                table: "Nutrition_Info",
                type: "decimal(10,2)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "saturated_fat",
                table: "Nutrition_Info",
                type: "decimal(10,2)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "protein",
                table: "Nutrition_Info",
                type: "decimal(10,2)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "potassium",
                table: "Nutrition_Info",
                type: "decimal(10,2)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "iron",
                table: "Nutrition_Info",
                type: "decimal(10,2)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "fiber",
                table: "Nutrition_Info",
                type: "decimal(10,2)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "fat",
                table: "Nutrition_Info",
                type: "decimal(10,2)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "cholesterol",
                table: "Nutrition_Info",
                type: "decimal(10,2)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "carbohydrates",
                table: "Nutrition_Info",
                type: "decimal(10,2)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "calories",
                table: "Nutrition_Info",
                type: "decimal(10,2)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "calcium",
                table: "Nutrition_Info",
                type: "decimal(10,2)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "Ingredient_Type",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "type_id",
                table: "Ingredient",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "nutrition_info_id",
                table: "Ingredient",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "Ingredient",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "measurement_unit_id",
                table: "Ingredient",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredient_Ingredient_Type_type_id",
                table: "Ingredient",
                column: "type_id",
                principalTable: "Ingredient_Type",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredient_Nutrition_Info_measurement_unit_id",
                table: "Ingredient",
                column: "measurement_unit_id",
                principalTable: "Nutrition_Info",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredient_Ingredient_Type_type_id",
                table: "Ingredient");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingredient_Nutrition_Info_measurement_unit_id",
                table: "Ingredient");

            migrationBuilder.AlterColumn<int>(
                name: "vitaminD",
                table: "Nutrition_Info",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "trans_fat",
                table: "Nutrition_Info",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "sugars",
                table: "Nutrition_Info",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "sodium",
                table: "Nutrition_Info",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "saturated_fat",
                table: "Nutrition_Info",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "protein",
                table: "Nutrition_Info",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "potassium",
                table: "Nutrition_Info",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "iron",
                table: "Nutrition_Info",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "fiber",
                table: "Nutrition_Info",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "fat",
                table: "Nutrition_Info",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "cholesterol",
                table: "Nutrition_Info",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "carbohydrates",
                table: "Nutrition_Info",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "calories",
                table: "Nutrition_Info",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "calcium",
                table: "Nutrition_Info",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "Ingredient_Type",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "measurement_unit_id",
                table: "Ingredient_Type",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "type_id",
                table: "Ingredient",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "nutrition_info_id",
                table: "Ingredient",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "Ingredient",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "measurement_unit_id",
                table: "Ingredient",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredient_Ingredient_Type_type_id",
                table: "Ingredient",
                column: "type_id",
                principalTable: "Ingredient_Type",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredient_Nutrition_Info_measurement_unit_id",
                table: "Ingredient",
                column: "measurement_unit_id",
                principalTable: "Nutrition_Info",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
