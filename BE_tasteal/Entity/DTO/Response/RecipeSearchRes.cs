using BE_tasteal.Entity.Entity;

namespace BE_tasteal.Entity.DTO.Response
{
    public class RecipeSearchRes
    {
        public int id { get; set; }
        public string? name { get; set; }
        public decimal? rating { get; set; }
        public int? totalTime { get; set; }
        public DateTime? active_time { get; set; }
        public int serving_size { get; set; }
        public string? introduction { get; set; }
        public string? author_note { get; set; }
        public bool is_private { get; set; }
        public string? image { get; set; }
        public string author { get; set; }
        public int? nutrition_info_id { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? updatedAt { get; set; }
        public AccountEntity? account { get; set; }
        public Nutrition_InfoEntity? nutrition_info { get; set; }
        public List<IngredientEntity>? ingredients { get; set; }
        public List<Recipe_DirectionEntity>? direction { get; set; }
        public decimal? calories { get; set; }
    }
}
