using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_tasteal.Entity.Entity
{
    [Table("Recipe_Image")]
    public class Recipe_ImageEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int recipe_image_id { get; set; }
        public int recipe_id { get; set; }
        [Column(TypeName = "text")]
        public string? image { get; set; }
        [ForeignKey("recipe_id")]
        public RecipeEntity? RecipeEntity { get; set; }
    }
}
