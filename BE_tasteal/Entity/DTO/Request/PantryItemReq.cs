using System.ComponentModel.DataAnnotations;

namespace BE_tasteal.Entity.DTO.Request
{
    public class PantryItemReq
    {
        [Required]
        public string account_id { get; set; }
        [Required]
        public int ingredient_id  { get; set; }
        [Required]
        public int number { get; set; }

    }
}
