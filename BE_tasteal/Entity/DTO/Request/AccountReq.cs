using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_tasteal.Entity.DTO.Request
{
    public class AccountReq
    {
        [Required]
        public string uid { get; set; }
        public string? name { get; set; }
        public string? avatar { get; set; }
        public string? introduction { get; set; }
        public string? link { get; set; }
        public string? slogan { get; set; }
        public string? quote { get; set; }
        [DefaultValue(false)]
        public bool isDeleted { get; set; }
    }
}
