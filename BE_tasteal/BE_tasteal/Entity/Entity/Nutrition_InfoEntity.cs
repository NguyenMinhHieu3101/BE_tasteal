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
        public int? calories { get; set; }
        public int? fat { get; set; }
        public int? saturated_fat { get; set; }
        public int? trans_fat { get; set; }
        public int? cholesterol { get; set; }
        public int? carbohydrates { get; set; }
        public int? fiber { get; set; }
        public int? sugars { get; set; }
        public int? protein { get; set; }
        public int? sodium { get; set; }
        public int? vitaminD { get; set; }
        public int? calcium { get; set; }
        public int? iron { get; set; }
        public int? potassium { get; set; }
    }
}
