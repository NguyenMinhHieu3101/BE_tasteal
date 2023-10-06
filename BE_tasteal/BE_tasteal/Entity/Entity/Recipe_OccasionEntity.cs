using System.ComponentModel.DataAnnotations.Schema;

namespace BE_tasteal.Entity.Entity
{
    public class Recipe_OccasionEntity
    {
        public int occasion_id { get; set; }
        public int recipe_id { get; set; }
        [ForeignKey("occasion_id")]
        public OccasionEntity OccasionEntity { get; set; }
        [ForeignKey("recipe_id")]
        public RecipeEntity RecipeEntity { get; set; }
    }
}
