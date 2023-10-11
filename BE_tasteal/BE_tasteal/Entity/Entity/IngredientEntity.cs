using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_tasteal.Entity.Entity
{
    [Table("Ingredient")]
    public class IngredientEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        [MaxLength(255)]
        public required string name { get; set; }
        [Column(TypeName = "text")]
        public string? image { get; set; }
        public int measurement_unit_id { get; set; }
        public int type_id { get; set; }
        public int nutrition_info_id { get; set; }
        public bool isLiquid { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal ratio { get; set; }

        #region foreign key

        [ForeignKey("type_id")]
        public Ingredient_TypeEntity? ingredient_type { get; set; }
        [ForeignKey("measurement_unit_id")]
        public Nutrition_InfoEntity? nutrition_info { get; set; }
        #endregion
    }
}
