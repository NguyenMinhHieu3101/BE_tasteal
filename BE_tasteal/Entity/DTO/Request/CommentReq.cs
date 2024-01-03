using System.ComponentModel.DataAnnotations;

namespace BE_tasteal.Entity.DTO.Request
{
    public class CommentReq
    {
        [Required]
        public string account_id { get; set; }
        [Required]
        [StringLength(255, MinimumLength = 1, ErrorMessage = "StringProperty must have at least one character.")]
        public string comment { get; set; }
        public string? image { get; set; }
    }

    public class CommentReqPut
    {
        [Required]
        [StringLength(255, MinimumLength = 1, ErrorMessage = "StringProperty must have at least one character.")]
        public string comment { get; set; }
        [Required]
        public string? image { get; set; }
    }
}
