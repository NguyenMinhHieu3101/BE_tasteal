using System.ComponentModel.DataAnnotations;

namespace BE_tasteal.Entity.DTO.Request
{
    public class RecipeSearchReq
    {
        [Range(1, int.MaxValue)]
        [Required]
        public int? page { get; set; }
        [Range(1, int.MaxValue)]
        [Required]
        public int? pageSize { get; set; }
        public IEnumerable<int>? IngredientID { get; set; }
        public IEnumerable<int>? ExceptIngredientID { get; set; }
        public IEnumerable<int>? OccasionID { get; set; }
        public IEnumerable<string>? KeyWords { get; set; }
        public int? TotalTime { get; set; }
        public CaloriesReq? Calories { get; set; }
    }
}
