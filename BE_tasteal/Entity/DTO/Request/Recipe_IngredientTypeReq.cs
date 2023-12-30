using System.ComponentModel.DataAnnotations;

namespace BE_tasteal.Entity.DTO.Request
{
    public class CreateIngredientTypeReq
    {
        [Required]
        public string name { get; set; }
    }
    public class UpdateIngredientTypeReq
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string name { get; set; }
    }
    public class DAGIngredientTypeReq
    {
        [Required]
        public int id { get; set; }
    }
}
