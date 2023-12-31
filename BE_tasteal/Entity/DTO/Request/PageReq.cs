﻿using System.ComponentModel.DataAnnotations;

namespace BE_tasteal.Entity.DTO.Request
{
    public class PageReq
    {
        [Range(1, 5, ErrorMessage = "pageSize >= 1")]
        public int pageSize { get; set; }
        [Required]
        [Range(1, 50, ErrorMessage = "page >= 1")]
        public int page { get; set; }
    }
}
