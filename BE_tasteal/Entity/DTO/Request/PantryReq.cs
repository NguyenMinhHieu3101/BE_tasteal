using System.ComponentModel.DataAnnotations;

namespace BE_tasteal.Entity.DTO.Request
{
    public class RecipesIngreAny
    {
        [Required]
        public List<int> ingredients { get; set; }
        [Required]
        public PageReq page { get; set; }
    }
    public class RecipesPantryAny
    {
        [Required]
        public int pantry_id { get; set; }
        [Required]
        public PageReq page { get; set; }
    }
}
