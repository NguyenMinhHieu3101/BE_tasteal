using System.ComponentModel.DataAnnotations;

namespace BE_tasteal.Entity.DTO.Request
{
    public class PageFilter
    {
        [Range(1, 999, ErrorMessage = "pageSize >= 1")]
        public int pageSize { get; set; }
        [Range(1, 999, ErrorMessage = "page >= 1")]
        public int page { get; set; }
        public bool isDescend { get; set; }
    }
}
