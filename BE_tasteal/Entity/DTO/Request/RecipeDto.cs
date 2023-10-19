using BE_tasteal.API.Middleware;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BE_tasteal.Entity.DTO.Request
{
    public class RecipeDto
    {
        [Required]
        public string name { get; set; }
        [DefaultValue(0)]
        [Required]
        [Range(0.0, 5.0, ErrorMessage = "Giá trị phải nằm trong khoảng từ 1 đến 5")]
        public float rating { get; set; }
        public string? image { get; set; }
        [ValidateTimeSpanString(ErrorMessage = "string format invalid")]
        public string? totalTime { get; set; }
        [ValidateTimeSpanString(ErrorMessage = "string format invalid")]
        public string? active_time { get; set; }
        public int serving_size { get; set; }
        [MaxLength(500)]
        [DefaultValue("")]
        public string? introduction { get; set; }
        [MaxLength(255)]
        [DefaultValue("")]
        public string? author_note { get; set; }
        public bool? is_private { get; set; }
        public int? author { get; set; }
        public List<Recipe_IngredientDto> ingredients { get; set; }
        public List<RecipeDirectionDto> direction { get; set; }
    }
}
