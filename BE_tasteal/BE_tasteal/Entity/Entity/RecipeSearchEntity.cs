using System.ComponentModel.DataAnnotations.Schema;

namespace BE_tasteal.Entity.Entity
{
    public class RecipeSearchEntity
    {
        public IEnumerable<int>? IngredientID { get; set; }
        public IEnumerable<int>? ExceptIngredientID { get; set; }
        public int? TotalTime { get; set; }
        public int? ActiveTime { get; set; }
        public int? OccasionID { get; set; }
        public int? Calories { get; set; }
        public string? TextSearch { get; set; }
    }
}
