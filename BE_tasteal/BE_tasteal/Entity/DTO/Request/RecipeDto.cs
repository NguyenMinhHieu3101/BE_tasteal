using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BE_tasteal.Entity.DTO.Request
{
    public class RecipeDto
    {
        [Required]
        public string name { get; set; }
        [DefaultValue(0)]
        public float rating { get; set; }
        public TimeSpan? totalTime { get; set; }
        public TimeSpan? active_time { get; set; }
        public int serving_size { get; set; }
        [MaxLength(500)]
        [DefaultValue("")]
        public string? introduction { get; set; }
        [MaxLength(255)]
        [DefaultValue("")]
        public string? author_note { get; set; }
        public bool is_private { get; set; }
        public string? image { get; set; }
        public int? author { get; set; }
        public int? nutrition_info_id { get; set; }
    }
}
