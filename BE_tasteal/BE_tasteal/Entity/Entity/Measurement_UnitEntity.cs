using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_tasteal.Entity.Entity
{
    [Table("Measurement_Unit")]
    public class Measurement_UnitEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int unit_id { get; set; }
        [Required]
        [MaxLength(50)]
        public required string name { get; set; }
    }
}
