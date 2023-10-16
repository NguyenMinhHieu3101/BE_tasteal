using System.ComponentModel.DataAnnotations.Schema;

namespace BE_tasteal.Entity.Entity
{
    [Table("Recipe_Direction")]
    public class Recipe_DirectionEntity
    {
        public int recipe_id { get; set; }
        public int step { get; set; }
        [Column(TypeName = "text")]
        public string? direction { get; set; }
        [Column(TypeName = "text")]
        public string? image { get; set; }
        [ForeignKey("recipe_id")]
        public RecipeEntity? RecipeEntity { get; set; }
    }
}
