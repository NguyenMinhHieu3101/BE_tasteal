namespace BE_tasteal.Entity.DTO.Request
{
    public class RecipeSearchDto
    {
        public IEnumerable<int>? IngredientID { get; set; }
        public IEnumerable<int>? ExceptIngredientID { get; set; }
        public IEnumerable<int>? OccasionID { get; set; }
        public IEnumerable<string>? KeyWords { get; set; }
        public int? TotalTime { get; set; }
        public int? ActiveTime { get; set; }
        public CaloriesDto? Calories { get; set; }
        public string? TextSearch { get; set; }
        public IEnumerable<string>? KeyWordsFormat { get; set; }
    }
}
