using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_tasteal.Entity.Entity
{
    [Table("Plan")]
    public class PlanEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string account_id { get; set; }
        public DateTime date { get; set; }
        [Column(TypeName = "text")]
        public string note { get; set; }

        [ForeignKey("account_id")]
        public AccountEntity AccountEntity { get; set; }
    }
}
