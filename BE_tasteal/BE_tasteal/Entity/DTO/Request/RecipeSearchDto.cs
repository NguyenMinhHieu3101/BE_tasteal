namespace BE_tasteal.Entity.DTO.Request
{
    public class RecipeSearchDto
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
