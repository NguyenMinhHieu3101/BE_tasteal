using System.ComponentModel.DataAnnotations;

namespace BE_tasteal.Entity.DTO.Request
{
    public class CreatePantryItemReq
    {
        [Required]
        public string account_id { get; set; }
        [Required]
        public int ingredient_id  { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "number >= 1")]
        public int number { get; set; }

    }
    public class UpdatePantryItemReq
    {
        [Required]
        public int id { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "number >= 1")]
        public int number { get; set; }

    }
    public class GetAllPantryItemReq
    {
        [Required]
        public string account_id { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "pageSize >= 1")]
        public int pageSize { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "page >= 1")]
        public int page { get; set; }

    }
}
