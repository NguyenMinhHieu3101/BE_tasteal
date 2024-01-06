using System.ComponentModel.DataAnnotations;

namespace BE_tasteal.Entity.DTO.Request
{
    public class RatingReq
    {

        [Required]
        public string account_id { get; set; }
        [Required]
        [Range(0, 5)]
        public decimal rating { get; set; }
    }

    public class RatingPutReq
    {
        [Required]
        [Range(0, 5)]
        public decimal rating { get; set; }
    }

}
