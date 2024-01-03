using System.ComponentModel.DataAnnotations;

namespace BE_tasteal.Entity.DTO.Request
{
    public class RecipeToCartReq
    {
        [Required]
        public string account_id { get; set; }
        [Required]
        public IEnumerable<int> recipe_ids { get; set; }
    }

}
