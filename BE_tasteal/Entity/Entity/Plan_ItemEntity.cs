using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BE_tasteal.Entity.Entity
{
    [Table("plan_item")]
    public class Plan_ItemEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int plan_id { get; set; }
        public int recipe_id { get; set; }
        public int serving_size { get; set; }
        public int order {  get; set; }
        [ForeignKey("plan_id")]
        public PlanEntity plan { get; set; }
        [ForeignKey("recipe_id")]
        public RecipeEntity recipe { get; set; }
    }
}
