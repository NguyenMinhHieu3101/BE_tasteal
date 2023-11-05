using System.ComponentModel;
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
        [Column(TypeName = "nvarchar")]
        public string name { get; set; }
        [Column(TypeName = "text")]
        public string? image { get; set; }
        public int? nutrition_info_id { get; set; }
        public int? type_id { get; set; }
        [DefaultValue(false)]
        public bool? isLiquid { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        [DefaultValue(1)]
        public decimal? ratio { get; set; }
        [NotMapped]
        public decimal? amount { get; set; }
        [NotMapped]
        public string? note { get; set; }

        #region foreign key

        [ForeignKey("type_id")]
        public Ingredient_TypeEntity? ingredient_type { get; set; }
        [ForeignKey("nutrition_info_id")]
        public Nutrition_InfoEntity? nutrition_info { get; set; }
        #endregion
    }
}
