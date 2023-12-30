using BE_tasteal.API.Middleware;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_tasteal.Entity.DTO.Request
{
    public class OccasionReq
    {
        [Required]
        public string name { get; set; }
        [Required]
        public string description { get; set; }
        public string? image { get; set; }
        [ValidateTimeSpanString(ErrorMessage = "string format invalid")]
        public string start_at { get; set; }
        [ValidateTimeSpanString(ErrorMessage = "string format invalid")]
        public string end_at { get; set; }
        [DefaultValue(false)]
        public bool is_lunar_date { get; set; }
    }
    public class OccasionPutReq
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string description { get; set; }
        public string? image { get; set; }
        [ValidateTimeSpanString(ErrorMessage = "string format invalid")]
        public string start_at { get; set; }
        [ValidateTimeSpanString(ErrorMessage = "string format invalid")]
        public string end_at { get; set; }
        [DefaultValue(false)]
        public bool is_lunar_date { get; set; }
    }
}
