using System.ComponentModel.DataAnnotations;

namespace BE_tasteal.Entity.DTO.Request
{
    public class Recipe_IngredientDto
    {
        [Required]
        public string name { get; set; }
        [Required]
        public decimal amount { get; set; }
        [Required]
        public bool isLiquid { get; set; }
    }
}
