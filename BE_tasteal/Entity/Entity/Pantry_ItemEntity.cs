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
        public int? account_id { get; set; }
        public int? ingredient_id { get; set; }
        public int amount { get; set; }
        [ForeignKey("account_id")]
        public AccountEntity? account { get; set; }
        [ForeignKey("ingredient_id")]
        public IngredientEntity? IngredientEntity { get; set; }
    }
}
