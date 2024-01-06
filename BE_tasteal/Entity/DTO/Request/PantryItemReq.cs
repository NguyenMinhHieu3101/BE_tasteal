using System.ComponentModel.DataAnnotations;

namespace BE_tasteal.Entity.DTO.Request
{
    public class PantryItemReq
    {
        [Required]
        public string account_id { get; set; }
        [Required]
        public int ingredient_id  { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "number >= 1")]
        public int number { get; set; }

    }
}
