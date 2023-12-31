using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_tasteal.Entity.Entity
{
    [Table("Pantry")]
    public class PantryEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string account_id { get; set; }
        [ForeignKey("account_id")]
        public AccountEntity? account { get; set; }
    }
}
