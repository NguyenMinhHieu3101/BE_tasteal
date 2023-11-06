using System.ComponentModel.DataAnnotations;

namespace BE_tasteal.Entity.DTO.Request
{
    public class Recipe_IngredientReq
    {
        public int? id { get; set; }
        public string? name { get; set; }
        [Required]
        public decimal amount { get; set; }
        public bool? isLiquid { get; set; }
    }
}
