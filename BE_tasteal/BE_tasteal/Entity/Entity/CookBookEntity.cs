using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_tasteal.Entity.Entity
{
    [Table("CookBook")]
    public class CookBookEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int cook_book_id { get; set; }
        [Required]
        [MaxLength(255)]
        public required string name { get; set; }
        public int? owner { get; set; }
        [ForeignKey("owner")]
        public AccountEntity? account { get; set; }
    }
}
