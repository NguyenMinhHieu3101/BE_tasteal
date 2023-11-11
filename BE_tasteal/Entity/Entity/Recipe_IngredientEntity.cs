using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_tasteal.Entity.Entity
{
    [Table("Recipe_Ingredient")]
    public class Recipe_IngredientEntity
    {
        public int recipe_id { get; set; }
        public int ingredient_id { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? amount_per_serving { get; set; }
        [MaxLength(255)]
        public string? note { get; set; }
        [ForeignKey("recipe_id")]
        public RecipeEntity? recipe { get; set; }
        [ForeignKey("ingredient_id")]
        public IngredientEntity? ingredient { get; set; }
    }
}
