using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_tasteal.Entity.Entity
{
    [Table("CookBook")]
    public class CookBookEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [MaxLength(255)]
        public string name { get; set; }
        public string? owner { get; set; }
        [ForeignKey("owner")]
        public AccountEntity? account { get; set; }
    }
}
