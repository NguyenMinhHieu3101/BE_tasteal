using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_tasteal.Entity.Entity
{
    [Table("Account")]
    public class AccountEntity
    {
        [Key]
        [MaxLength(255)]
        public string uid { get; set; }
        public string? name { get; set; }
        public string? avatar { get; set; }
        public string? introduction { get; set; }
    }
}
