using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_tasteal.Entity.Entity
{
    [Table("Nutrition_Info")]
    public class Nutrition_InfoEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? calories { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? fat { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? saturated_fat { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? trans_fat { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? cholesterol { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? carbohydrates { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? fiber { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? sugars { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? protein { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? sodium { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? vitaminD { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? calcium { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? iron { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? potassium { get; set; }
    }
}
