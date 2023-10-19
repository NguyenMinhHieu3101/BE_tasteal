using BE_tasteal.Entity.Entity;

namespace BE_tasteal.Entity.DTO.Response
{
    public class RecipeResponse
    {
        public int id { get; set; }
        public string? name { get; set; }
        public float rating { get; set; }
        public TimeSpan? totalTime { get; set; }
        public TimeSpan? active_time { get; set; }
        public int serving_size { get; set; }
        public string? introduction { get; set; }
        public string? author_note { get; set; }
        public bool is_private { get; set; }
        public string? image { get; set; }
        public int? author { get; set; }
        public Nutrition_InfoEntity nutrition { get; set; }
        public IngredientResponse ingredient { get; set; }
    }
}
