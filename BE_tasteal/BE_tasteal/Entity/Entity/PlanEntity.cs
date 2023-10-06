using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_tasteal.Entity.Entity
{
    [Table("Plan")]
    public class PlanEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int plan_id { get; set; }
        public int account_id { get; set; }
        public int recipe_id { get; set; }
        public DateTime date { get; set; }
        public int serving_size { get; set; }

        [ForeignKey("account_id")]
        public AccountEntity AccountEntity { get; set; }
        [ForeignKey("recipe_id")]
        public RecipeEntity RecipeEntity { get; set; }
    }
}
