using System.ComponentModel.DataAnnotations;

namespace BE_tasteal.Entity.DTO.Request
{
    public class RecipeByListIngreIdReq
    {
        [Required]
        public List<int> ListIngredientId {  get; set; }
        [Required]
        public PageReq page {  get; set; }
    }
}
