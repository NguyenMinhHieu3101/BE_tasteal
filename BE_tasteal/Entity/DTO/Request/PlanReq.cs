using BE_tasteal.API.Middleware;
using System.ComponentModel.DataAnnotations;

namespace BE_tasteal.Entity.DTO.Request
{
    public class PlanReq
    {
        [Required]
        public string account_id { get; set; }
        [Required]
        [ValidateTimeSpanString(ErrorMessage = "string format invalid")]
        public string date { get; set; }
        [Required]
        public List<int> recipeIds { get; set; }
    }

    public class PlanDeleteReq
    {
        [Required]
        public string account_id { get; set; }
        [Required]
        [ValidateTimeSpanString(ErrorMessage = "string format invalid")]
        public string date { get; set; }
        [Required]
        public int recipeId { get; set; }
        [Required]
        [Range(0, 20)]
        public int order { get; set; }
    }
}
