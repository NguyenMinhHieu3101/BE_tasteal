using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BE_tasteal.Migrations
{
    /// <inheritdoc />
    public partial class pantry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pantry_Item_Account_account_id",
                table: "Pantry_Item");

            migrationBuilder.DropIndex(
                name: "IX_Pantry_Item_account_id",
                table: "Pantry_Item");

            migrationBuilder.DropColumn(
                name: "account_id",
                table: "Pantry_Item");

            migrationBuilder.AlterColumn<int>(
                name: "totalTime",
                table: "Recipe",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "rating",
                table: "Recipe",
                type: "float",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "float");

            migrationBuilder.AddColumn<int>(
                name: "pantry_id",
                table: "Pantry_Item",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Pantry",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    account_id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pantry", x => x.id);
                    table.ForeignKey(
                        name: "FK_Pantry_Account_account_id",
                        column: x => x.account_id,
                        principalTable: "Account",
                        principalColumn: "uid",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Pantry_Item_pantry_id",
                table: "Pantry_Item",
                column: "pantry_id");

            migrationBuilder.CreateIndex(
                name: "IX_Pantry_account_id",
                table: "Pantry",
                column: "account_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pantry_Item_Pantry_pantry_id",
                table: "Pantry_Item",
                column: "pantry_id",
                principalTable: "Pantry",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pantry_Item_Pantry_pantry_id",
                table: "Pantry_Item");

            migrationBuilder.DropTable(
                name: "Pantry");

            migrationBuilder.DropIndex(
                name: "IX_Pantry_Item_pantry_id",
                table: "Pantry_Item");

            migrationBuilder.DropColumn(
                name: "pantry_id",
                table: "Pantry_Item");

            migrationBuilder.AlterColumn<int>(
                name: "totalTime",
                table: "Recipe",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "rating",
                table: "Recipe",
                type: "float",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "account_id",
                table: "Pantry_Item",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Pantry_Item_account_id",
                table: "Pantry_Item",
                column: "account_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pantry_Item_Account_account_id",
                table: "Pantry_Item",
                column: "account_id",
                principalTable: "Account",
                principalColumn: "uid");
        }
    }
}
