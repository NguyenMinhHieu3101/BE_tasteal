using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_tasteal.Entity.Entity
{
    [Table("Pantry_Item")]
    public class Pantry_ItemEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int? pantry_id { get; set; }
        public int? ingredient_id { get; set; }
        public int amount { get; set; }
        [ForeignKey("pantry_id")]
        public PantryEntity? Pantry { get; set; }
        [ForeignKey("ingredient_id")]
        public IngredientEntity? Ingredient { get; set; }
    }
}
