using System.ComponentModel.DataAnnotations;

namespace BE_tasteal.Entity.DTO.Request
{
    public class PersonalCartItemReq
    {
        [Required]
        public int ingredient_id { get; set; }
        [Required]
        public string account_id { get; set; }
        public string? name { get; set; }
        [Range(0, int.MaxValue)]
        public int amount { get; set; }
        [Required]
        public bool is_bought { get; set; }
    }
}
