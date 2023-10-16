using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BE_tasteal.Migrations
{
    /// <inheritdoc />
    public partial class updateerdmeeting1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    username = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Ingredient_Type",
                columns: table => new
                {
                    ingredient_type_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    measurement_unit_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredient_Type", x => x.ingredient_type_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LoaiSanPham",
                columns: table => new
                {
                    MaLoaiSanPham = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TenLoaiSanPham = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MoTa = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TrangThai = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiSanPham", x => x.MaLoaiSanPham);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Nutrition_Info",
                columns: table => new
                {
                    nutrition_info_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    calories = table.Column<int>(type: "int", nullable: false),
                    fat = table.Column<int>(type: "int", nullable: false),
                    saturated_fat = table.Column<int>(type: "int", nullable: false),
                    trans_fat = table.Column<int>(type: "int", nullable: false),
                    cholesterol = table.Column<int>(type: "int", nullable: false),
                    carbohydrates = table.Column<int>(type: "int", nullable: false),
                    fiber = table.Column<int>(type: "int", nullable: false),
                    sugars = table.Column<int>(type: "int", nullable: false),
                    protein = table.Column<int>(type: "int", nullable: false),
                    sodium = table.Column<int>(type: "int", nullable: false),
                    vitaminD = table.Column<int>(type: "int", nullable: false),
                    calcium = table.Column<int>(type: "int", nullable: false),
                    iron = table.Column<int>(type: "int", nullable: false),
                    potassium = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nutrition_Info", x => x.nutrition_info_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Occasion",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "text", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    image = table.Column<string>(type: "text", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    start_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Occasion", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CookBook",
                columns: table => new
                {
                    cook_book_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    owner = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CookBook", x => x.cook_book_id);
                    table.ForeignKey(
                        name: "FK_CookBook_Account_owner",
                        column: x => x.owner,
                        principalTable: "Account",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SanPham",
                columns: table => new
                {
                    MaSanPham = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MaLoaiSanPham = table.Column<int>(type: "int", nullable: false),
                    TenSanPham = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPham", x => x.MaSanPham);
                    table.ForeignKey(
                        name: "FK_SanPham_LoaiSanPham_MaLoaiSanPham",
                        column: x => x.MaLoaiSanPham,
                        principalTable: "LoaiSanPham",
                        principalColumn: "MaLoaiSanPham",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Ingredient",
                columns: table => new
                {
                    ingredient_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    image = table.Column<string>(type: "text", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    measurement_unit_id = table.Column<int>(type: "int", nullable: false),
                    type_id = table.Column<int>(type: "int", nullable: false),
                    nutrition_info_id = table.Column<int>(type: "int", nullable: false),
                    isLiquid = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ratio = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredient", x => x.ingredient_id);
                    table.ForeignKey(
                        name: "FK_Ingredient_Ingredient_Type_type_id",
                        column: x => x.type_id,
                        principalTable: "Ingredient_Type",
                        principalColumn: "ingredient_type_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ingredient_Nutrition_Info_measurement_unit_id",
                        column: x => x.measurement_unit_id,
                        principalTable: "Nutrition_Info",
                        principalColumn: "nutrition_info_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Recipe",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    rating = table.Column<float>(type: "float", nullable: false),
                    totalTime = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    active_time = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    serving_size = table.Column<int>(type: "int", nullable: false),
                    introduction = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    author_note = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_private = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    image = table.Column<string>(type: "text", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    author = table.Column<int>(type: "int", nullable: true),
                    nutrition_info_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe", x => x.id);
                    table.ForeignKey(
                        name: "FK_Recipe_Account_author",
                        column: x => x.author,
                        principalTable: "Account",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Recipe_Nutrition_Info_nutrition_info_id",
                        column: x => x.nutrition_info_id,
                        principalTable: "Nutrition_Info",
                        principalColumn: "nutrition_info_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Cart_Item",
                columns: table => new
                {
                    cart_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    account_id = table.Column<int>(type: "int", nullable: true),
                    ingredient_id = table.Column<int>(type: "int", nullable: true),
                    amount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart_Item", x => x.cart_id);
                    table.ForeignKey(
                        name: "FK_Cart_Item_Account_account_id",
                        column: x => x.account_id,
                        principalTable: "Account",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Cart_Item_Ingredient_ingredient_id",
                        column: x => x.ingredient_id,
                        principalTable: "Ingredient",
                        principalColumn: "ingredient_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Pantry_Item",
                columns: table => new
                {
                    pantry_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    account_id = table.Column<int>(type: "int", nullable: true),
                    ingredient_id = table.Column<int>(type: "int", nullable: true),
                    amount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pantry_Item", x => x.pantry_id);
                    table.ForeignKey(
                        name: "FK_Pantry_Item_Account_account_id",
                        column: x => x.account_id,
                        principalTable: "Account",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Pantry_Item_Ingredient_ingredient_id",
                        column: x => x.ingredient_id,
                        principalTable: "Ingredient",
                        principalColumn: "ingredient_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    comment_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    recipe_id = table.Column<int>(type: "int", nullable: false),
                    account_id = table.Column<int>(type: "int", nullable: false),
                    comment = table.Column<string>(type: "text", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.comment_id);
                    table.ForeignKey(
                        name: "FK_Comment_Account_account_id",
                        column: x => x.account_id,
                        principalTable: "Account",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comment_Recipe_recipe_id",
                        column: x => x.recipe_id,
                        principalTable: "Recipe",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CookBook_Recipe",
                columns: table => new
                {
                    cook_book_id = table.Column<int>(type: "int", nullable: false),
                    recipe_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CookBook_Recipe", x => new { x.cook_book_id, x.recipe_id });
                    table.ForeignKey(
                        name: "FK_CookBook_Recipe_CookBook_cook_book_id",
                        column: x => x.cook_book_id,
                        principalTable: "CookBook",
                        principalColumn: "cook_book_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CookBook_Recipe_Recipe_recipe_id",
                        column: x => x.recipe_id,
                        principalTable: "Recipe",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Plan",
                columns: table => new
                {
                    plan_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    account_id = table.Column<int>(type: "int", nullable: false),
                    recipe_id = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    serving_size = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plan", x => x.plan_id);
                    table.ForeignKey(
                        name: "FK_Plan_Account_account_id",
                        column: x => x.account_id,
                        principalTable: "Account",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Plan_Recipe_recipe_id",
                        column: x => x.recipe_id,
                        principalTable: "Recipe",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    recipe_id = table.Column<int>(type: "int", nullable: false),
                    account_id = table.Column<int>(type: "int", nullable: false),
                    rating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rating", x => new { x.recipe_id, x.account_id });
                    table.ForeignKey(
                        name: "FK_Rating_Account_account_id",
                        column: x => x.account_id,
                        principalTable: "Account",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rating_Recipe_recipe_id",
                        column: x => x.recipe_id,
                        principalTable: "Recipe",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Recipe_Direction",
                columns: table => new
                {
                    recipe_id = table.Column<int>(type: "int", nullable: false),
                    step = table.Column<int>(type: "int", nullable: false),
                    direction = table.Column<string>(type: "text", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    image = table.Column<string>(type: "text", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe_Direction", x => new { x.recipe_id, x.step });
                    table.ForeignKey(
                        name: "FK_Recipe_Direction_Recipe_recipe_id",
                        column: x => x.recipe_id,
                        principalTable: "Recipe",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Recipe_Image",
                columns: table => new
                {
                    recipe_image_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    recipe_id = table.Column<int>(type: "int", nullable: false),
                    image = table.Column<string>(type: "text", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe_Image", x => x.recipe_image_id);
                    table.ForeignKey(
                        name: "FK_Recipe_Image_Recipe_recipe_id",
                        column: x => x.recipe_id,
                        principalTable: "Recipe",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Recipe_Ingredient",
                columns: table => new
                {
                    recipe_id = table.Column<int>(type: "int", nullable: false),
                    ingredient_id = table.Column<int>(type: "int", nullable: false),
                    amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    note = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_required = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe_Ingredient", x => new { x.recipe_id, x.ingredient_id });
                    table.ForeignKey(
                        name: "FK_Recipe_Ingredient_Ingredient_ingredient_id",
                        column: x => x.ingredient_id,
                        principalTable: "Ingredient",
                        principalColumn: "ingredient_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recipe_Ingredient_Recipe_recipe_id",
                        column: x => x.recipe_id,
                        principalTable: "Recipe",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "recipe_OccasionEntities",
                columns: table => new
                {
                    occasion_id = table.Column<int>(type: "int", nullable: false),
                    recipe_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recipe_OccasionEntities", x => new { x.recipe_id, x.occasion_id });
                    table.ForeignKey(
                        name: "FK_recipe_OccasionEntities_Occasion_occasion_id",
                        column: x => x.occasion_id,
                        principalTable: "Occasion",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_recipe_OccasionEntities_Recipe_recipe_id",
                        column: x => x.recipe_id,
                        principalTable: "Recipe",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_Item_account_id",
                table: "Cart_Item",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_Item_ingredient_id",
                table: "Cart_Item",
                column: "ingredient_id");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_account_id",
                table: "Comment",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_recipe_id",
                table: "Comment",
                column: "recipe_id");

            migrationBuilder.CreateIndex(
                name: "IX_CookBook_owner",
                table: "CookBook",
                column: "owner");

            migrationBuilder.CreateIndex(
                name: "IX_CookBook_Recipe_recipe_id",
                table: "CookBook_Recipe",
                column: "recipe_id");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredient_measurement_unit_id",
                table: "Ingredient",
                column: "measurement_unit_id");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredient_type_id",
                table: "Ingredient",
                column: "type_id");

            migrationBuilder.CreateIndex(
                name: "IX_Pantry_Item_account_id",
                table: "Pantry_Item",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_Pantry_Item_ingredient_id",
                table: "Pantry_Item",
                column: "ingredient_id");

            migrationBuilder.CreateIndex(
                name: "IX_Plan_account_id",
                table: "Plan",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_Plan_recipe_id",
                table: "Plan",
                column: "recipe_id");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_account_id",
                table: "Rating",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_author",
                table: "Recipe",
                column: "author");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_nutrition_info_id",
                table: "Recipe",
                column: "nutrition_info_id");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_Image_recipe_id",
                table: "Recipe_Image",
                column: "recipe_id");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_Ingredient_ingredient_id",
                table: "Recipe_Ingredient",
                column: "ingredient_id");

            migrationBuilder.CreateIndex(
                name: "IX_recipe_OccasionEntities_occasion_id",
                table: "recipe_OccasionEntities",
                column: "occasion_id");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_MaLoaiSanPham",
                table: "SanPham",
                column: "MaLoaiSanPham");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cart_Item");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "CookBook_Recipe");

            migrationBuilder.DropTable(
                name: "Pantry_Item");

            migrationBuilder.DropTable(
                name: "Plan");

            migrationBuilder.DropTable(
                name: "Rating");

            migrationBuilder.DropTable(
                name: "Recipe_Direction");

            migrationBuilder.DropTable(
                name: "Recipe_Image");

            migrationBuilder.DropTable(
                name: "Recipe_Ingredient");

            migrationBuilder.DropTable(
                name: "recipe_OccasionEntities");

            migrationBuilder.DropTable(
                name: "SanPham");

            migrationBuilder.DropTable(
                name: "CookBook");

            migrationBuilder.DropTable(
                name: "Ingredient");

            migrationBuilder.DropTable(
                name: "Occasion");

            migrationBuilder.DropTable(
                name: "Recipe");

            migrationBuilder.DropTable(
                name: "LoaiSanPham");

            migrationBuilder.DropTable(
                name: "Ingredient_Type");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Nutrition_Info");
        }
    }
}
