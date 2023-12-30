using System.ComponentModel.DataAnnotations;

namespace BE_tasteal.Entity.DTO.Request
{
    public class RecipeDirectionReq
    {
        [Required]
        public int step { get; set; }
        [Required]
        public string direction { get; set; }
        public string? image { get; set; }
    }
}
